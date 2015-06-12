using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace diploma_neunet
{
    public class NeuNet
    {
        int N0, N1, N2;     // num of neurons in input, hidden and output layers
        int era;
        const double alpha = 0.6667;       //parameter of sigmoida's HAKJlOH 0_o
        const double maxEta = 0.7;               //learning speed coeficient
        const int NumOfInputs = 10;     //number of input symbols
        const double thresh = 0.0;        //threshold
        double momentum = 0.0;    //momentum constant (ischerpivausche, da? XD)

        double[] out0;         // layer 0 (input)
        double[] out1;      // layer 1 (hidden)
        double[] output;    // layer 2 (output)
        double[] correct;   // correct output
        double[] error;     // learn error
        double[,] weights01;// weights between input and hidden layers
        double[,] weights12;// weights between hidden and output layers
        double[,] old_weights01;
        double[,] old_weights12;
        double[] delta;     //local grad between hidden and input layers

        List<int> inputIndecies;

        double avgErr;      //average error (for all iterations)
        double lastAvgError;  // last average error
        double currErr;     //current error in all neurons
        double eta;

        List<int> fxHidden;

        double[][] preInput;
        double[][] preOutput;

        Random r = null;
        LearnDataGenerator learner = null;
        NetConfig config = null;
        MainForm parent;

        #region Properties
        public int Epoch { get { return this.era; } }
        #endregion

        public NeuNet(NetConfig Config)
        {
            r = new Random();
            learner = new LearnDataGenerator();
            this.config = Config;
                //(100, 0.2, 0.0001);

            era = 0;
            this.N0 = config.NumInput;
            this.N1 = config.NumHidden;
            this.N2 = config.NumOutput;

            fxHidden = new List<int>();             //hidden layer fixed
            inputIndecies = new List<int>(N2);
        }

        #region Learning
        public NetData LearnInt(MainForm parentForm)
        {
            int currInput, currIndex;
            this.parent = parentForm;
            this.era = 0;
            this.avgErr= int.MaxValue;
            this.currErr = 0;
            double currAbsErr = 0;
            this.eta = maxEta;

            this.AllocMem();
            this.InitWeights();
            this.PreGenerateInputOutput();          //speed up, memory down

            DateTime start = DateTime.Now;
            NetData data = NetData.Empty;
            
            do
            {
                if (!this.parent.GetState())
                    break;
                currErr = 0;

                for (int i = 0; i < NumOfInputs; i++)
                {
                    inputIndecies.Add(i);
                }
                
                while(inputIndecies.Count>0)
                {
                    currIndex = r.Next(inputIndecies.Count - 1);
                    currInput = inputIndecies[currIndex];
                    inputIndecies.RemoveAt(currIndex);

                    GenerateIntInput(currInput);
                    GenerateIntOutput(currInput);

                    ForwardPass();
                    BackwardPass();

                    currErr += CountCurrentError();
                    Application.DoEvents();
                }
                this.lastAvgError = this.avgErr;
                this.avgErr = CountAverageError();
                this.era++;
                currAbsErr = Math.Abs(this.avgErr);
                this.eta /= 1.01;
            } while (currAbsErr > config.minError &&
                Math.Abs(lastAvgError - avgErr) > config.minErrorChange &&
                this.era < config.maxEpoch);

            data.time = DateTime.Now.Subtract(start);
            data.avgErr = this.avgErr;
            data.epoch = this.Epoch;
            data.errChange = this.lastAvgError - this.avgErr;

            return data;
        }
        public bool Fix(int hiddenNeuronIndex)
        {
            if (hiddenNeuronIndex > (this.N1 - 1))
                return false;
            this.fxHidden.Add(hiddenNeuronIndex);
            return true;
        }

        public bool Fix(List<int> fixedNeuronsIndecies)
        {
            foreach (var i in fixedNeuronsIndecies)
            {
                if (i > (this.N1 - 1))
                    return false;
                this.fxHidden.Add(i);
            }
            return true;
        }

        public bool Unfix(int hiddenNeuronIndex)
        {
            if (hiddenNeuronIndex > (this.N1 - 1) || !this.fxHidden.Contains(hiddenNeuronIndex))
                return false;
            this.fxHidden.Remove(hiddenNeuronIndex);
            return true;
        }
        public bool Unfix()
        {
            this.fxHidden.Clear();
            return true;
        }

        public bool IsFixed(int index)
        {
            return this.fxHidden.Contains(index);
        }
        private void ForwardPass(bool learning = true)
        {
            double currNeuWeight;
            for (int i = 0; i < N1; i++)        //input => hidden
            {
                currNeuWeight = thresh;
                for (int j = 0; j < N0; j++)
                    currNeuWeight += out0[j] * weights01[j, i];
                out1[i] = config.ActivationFunction(currNeuWeight, alpha);
            }

            for (int i = 0; i < N2; i++)        //hidden => output
            {
                currNeuWeight = thresh;
                for (int j = 0; j < N1; j++)
                    currNeuWeight += out1[j] * weights12[j, i];
                output[i] = (config.ActivationFunction(currNeuWeight, alpha));// >= 0 ? 1 : 0);
            }

            if (learning)
                for (int i = 0; i < N2; i++)
                {
                    error[i] = (correct[i] - output[i]);
                }
        }

        private void BackwardPass()
        {
            double sig;

            for (int i = 0; i < N2; i++)        //output => hidden layer
            {
                sig = config.ActivationFunctionDerivative(output[i], alpha) * (correct[i] - output[i]);
                this.delta[i] = sig;
                for (int j = 0; j < N1; j++)
                {
                    double old = weights12[j, i];
                    weights12[j, i] += eta * sig * out1[j] + momentum * old_weights12[j, i];
                    old_weights12[j, i] = old;
                }
            }

            for (int i = 0; i < N1; i++)        //hidden=>input layer
            {
                if (fxHidden.Contains(i))   //hidden neuron with index j is fixed
                    continue;
                sig = 0;
                for (int k = 0; k < N2; k++)
                    sig += (this.delta[k] * weights12[i, k]);
                sig *= config.ActivationFunctionDerivative(out1[i], alpha);         //out1[i] * (1 - out1[i]) * alpha;
                for (int j = 0; j < N0; j++) {
                    double old = weights01[j, i];
                    weights01[j, i] += eta * sig * out0[j] + momentum * old_weights01[j, i];
                    old_weights01[j, i] = old;
                }
            }

        }

        private double CountAverageError()
        {
            double res = currErr / this.error.Length;
            return res;
        }

        private double CountCurrentError()
        {
            double res = 0;
            for (int i = 0; i < N2; i++)
            {
                var e = this.error[i];
                res += e*e;
            }
            
            res *= 0.5;
            return res;
        }

        #endregion Learning

        private void AllocMem()
        {
            this.out0 = new double[N0];                //input data
            this.out1 = new double[N1];             //hidden layer output
            this.output = new double[N2];           //output layer output
            this.correct = new double[N2];          //correct output data
            this.error = new double[N2];            //~Abs(correct-incorrect)
            this.weights01 = new double[N0, N1];    //input-hidden weights
            this.weights12 = new double[N1, N2];    //hidden-output weights
            this.old_weights01 = new double[N0, N1];
            this.old_weights12 = new double[N1, N2];
            this.delta = new double[N2];    // Math.Max(N2, N1)];
            this.preInput = new double[NumOfInputs][];
            this.preOutput = new double[NumOfInputs][];
        }
        public string Test(double[] inputData)
        {
            if (this.N0 != inputData.Length)
            {
                System.Windows.Forms.MessageBox.Show("Wrong input test data!");
                return "";
            }

            if (this.out1 == null)
                return "Network has not been learned.";

            for (int i = 0; i < this.N0; i++)
                this.out0[i] = inputData[i];
            this.ForwardPass(false);

            int resInt = (int)(output[0] * 10.0);
            if (Math.Abs((double)resInt - output[0]*10.0) > 0.7)     //if output = 0.9 => resInt = 1.0
                resInt++;

            string res = Array.IndexOf(this.output, this.output.Max<double>()).ToString();
            return res;
        }

        private void PreGenerateInputOutput()
        {
            for (int i = 0; i < NumOfInputs; i++)
            {
                this.preInput[i] = learner.Generate(Char.Parse(i.ToString()), (int)Math.Sqrt(this.N0));

                this.preOutput[i] = new double[N2];

                for (int j = 0; j < N2; j++)
                    this.preOutput[i][j] = ((j == i) ? 0.9 : 0.1);  //i / 10.0;
            }
        }
        private void GenerateIntInput(int num)
        {
            this.out0 = this.preInput[num];
        }

        private void GenerateIntOutput(int num)
        {
            this.correct = this.preOutput[num];
        }

        private void InitWeights()      //disp = links_num^(-1/2)
        {

            var d1 = 1.0 / Math.Sqrt(N1);
            var d2 = 1.0 / Math.Sqrt(N2);

            for (int i = 0; i < N0; i++)
                for (int j = 0; j < N1; j++)
                    if (!this.fxHidden.Contains(j))
                        this.weights01[i, j] = d1 * (r.NextDouble() * 2 - 1);

            for (int i = 0; i < N1; i++)
                for (int j = 0; j < N2; j++)
                    this.weights12[i, j] = d2 * (r.NextDouble() * 2 - 1);
        }
    }
}
