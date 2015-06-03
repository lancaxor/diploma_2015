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
        const double alpha = 5;       //parameter of sigmoida's HAKJlOH 0_o
        const double eta = 0.5;           //learning speed coeficient
        const int NumOfInputs = 10;     //number of input symbols
        const double thresh = 0.3;

        int[] out0;         // layer 0 (input)
        double[] out1;      // layer 1 (hidden)
        double[] output;    // layer 2 (output)
        double[] correct;   // correct output
        double[] error;     // learn error
        double[,] weights01;// weights between input and hidden layers
        double[,] weights12;// weights between hidden and output layers
        double[] delta;     //local grad between hidden and input layers
        double avgErr;      //average error
        double lastAvgError;  // last average error

        List<int> fxHidden, fxOut;

        int[][] preInput;
        double[][] preOutput;

        Random r = null;
        LearnDataGenerator learner = null;
        NetConfig config = null;

        #region Properties
        public int Epoch { get { return this.era; } }
        #endregion

        public NeuNet()
        {
            r = new Random();
            learner = new LearnDataGenerator();
            config = new NetConfig { maxEpoch = 500, minError = 0.01, minErrorChange = 0.0001, NumInput = 784, NumHidden = 15000, NumOutput = 10 };
                //(100, 0.2, 0.0001);
            preInput = new int[NumOfInputs][];
            preOutput = new double[NumOfInputs][];

            era = 0;
            this.N0 = config.NumInput;
            this.N1 = config.NumHidden;
            this.N2 = config.NumOutput;
        }

        #region Learning
        string junk = string.Empty;
        public TimeSpan LearnInt()
        {
            int currInput;
            AllocMem();
            InitWeights();
            this.era = 0;
            this.avgErr= int.MaxValue;
            this.PreGenerateInputOutput();          //speed up, memory down

            DateTime start = DateTime.Now;
            do
            {

                for (currInput = 0; currInput < NumOfInputs; currInput++)
                {
                    GenerateIntInput(currInput);
                    GenerateIntOutput(currInput);

                    ForwardPass();
                    BackwardPass();
                }
                this.lastAvgError = this.avgErr;
                this.avgErr = CountAverageError();
                 this.era++;
            } while (Math.Abs(this.avgErr) > config.minError ||
                this.error.Max<double>() > this.config.minError ||
                Math.Abs(lastAvgError - avgErr) > config.minErrorChange ||
                this.era < config.maxEpoch);

             /*
            for (currInput = 0; currInput < 10; currInput++)
            {
                do
                {
                    GenerateIntInput(currInput);
                    GenerateIntOutput(currInput);

                    ForwardPass();
                    BackwardPass();

                    this.avgErr = CountAverageError();
                    //junk = NeuronProp.ArrToProps(this.output);

                    this.era++;

                } while (Math.Abs(this.avgErr) > config.minError &&
                    this.error.Max<double>() > this.config.minError &&
                    this.era < config.maxEpoch);
            }
            */
            MessageBox.Show("Testing time!!!");
            this.TestOutputInput();
            return DateTime.Now - start;
        }

        private void ForwardPass(bool learning = true)
        {
            double currNeuWeight;
            for (int i = 0; i < N1; i++)        //input => hidden
            {
                currNeuWeight = thresh;
                for (int j = 0; j < N0; j++)
                    currNeuWeight += out0[j] * weights01[j, i] * 10;
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
                    var err = (correct[i] - output[i]);
                    error[i] = (err * err * 0.5);
                }
        }

        private void BackwardPass() //◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘
        {
            double sig, deltaSum;

            for (int i = 0; i < N2; i++)        //output => hidden layer
            {
                if (fxOut.Contains(i))      //out neuron with index j is fixed //slow down!
                    continue;
                sig = output[i] * (1 - output[i]) * (correct[i] - output[i]);
                this.delta[i] = sig;
                for (int j = 0; j < N1; j++)
                {
                    deltaSum = eta * sig * out1[j];
                    weights12[j, i] += deltaSum;
                }
            }

            for (int i = 0; i < N1; i++)        //hidden=>input layer
            {
                if (fxHidden.Contains(i))   //hidden neuron with index j is fixed
                    continue;
                sig = 0;
                for (int k = 0; k < N2; k++)
                    sig += (this.delta[k] * weights12[i, k]);
                sig *= out1[i] * (1 - out1[i]);
                for (int j = 0; j < N0; j++) {
                    weights01[j, i] += eta * sig * out0[j];
                }
            }

        }

        private double CountAverageError()
        {
            double res = 0;
            for (int i = 0; i < N2; i++)
                res += this.error[i];
            res /= this.error.Length;
            return res;
        }

        #endregion Learning

        private void AllocMem()
        {
            this.out0 = new int[N0];                //input data
            this.out1 = new double[N1];             //hidden layer output
            this.output = new double[N2];           //output layer output
            this.correct = new double[N2];          //correct output data
            this.error = new double[N2];            //~Abs(correct-incorrect)
            this.weights01 = new double[N0, N1];    //input-hidden weights
            this.weights12 = new double[N1, N2];    //hidden-output weights
            this.delta = new double[N2];    // Math.Max(N2, N1)];
            fxHidden = new List<int>();             //hidden layer fixed
            fxOut = new List<int>();                //output layer fixed;
        }

        private void TestOutputInput()
        {
            for (int i = 0; i < NumOfInputs; i++)
            {
                double[] d_21 = new double[N1];
                for (int i21 = 0; i21 < N1; i21++)      //output => hidden
                {
                    d_21[i21] = 0;
                    for (int j = 0; j < N2; j++)
                    {
                        d_21[i21] += (preOutput[i][j] / weights12[i21, j]);
                    }
                }

                double[] d_10 = new double[N0];         //this must me in picture!
                for (int i10 = 0; i10 < N0; i10++)      //hidden => input
                {
                    d_10[i10] = 0;
                    for (int j = 0; j < N1; j++)
                    {
                        d_10[i10] += (d_21[j] / weights01[i10, j]);
                    }
                }
                TestForm(d_10);
            }
        }

        private void TestForm(double[] dots)
        {
            int size = (int)Math.Sqrt(this.N0);
            Bitmap bmp = new Bitmap(size, size);
            double avg = (dots.Max() + dots.Min()) / 2;
            for (int i = 0; i < dots.Count(); i++)
            {
                int x = (int) i / size;
                int y = ((int)(i / size) == 0 ? i : (int)(i % size));

                //double min = Math.Abs(dots.Min()) + dots[i];
                //int color = (int)(min * 255 / (dots.Max()+Math.Abs(dots.Min())));
                int color = (dots[i] < avg) ? 255 : 0;
                bmp.SetPixel(x, y, Color.FromArgb(color, color, color));
            }

            Form tf = new Form();
            PictureBox pc = new PictureBox();
            pc.BackColor = Color.Blue;
            pc.Image = (Image)bmp;
            pc.Dock = DockStyle.Fill;
            tf.Controls.Add(pc);
            tf.StartPosition = FormStartPosition.CenterParent;
            tf.ShowDialog();
        }
        public int Test(int[] inputData)
        {
            int res = 0;
            if (this.N0 != inputData.Length)
            {
                System.Windows.Forms.MessageBox.Show("Wrong input test data!");
                return -1;
            }

            for (int i = 0; i < this.N0; i++)
                this.out0[i] = inputData[i];
            this.ForwardPass(false);
            res = Array.IndexOf(this.output, this.output.Max<double>());
            return res;
        }

        private void PreGenerateInputOutput()
        {
            for (int i = 0; i < NumOfInputs; i++)
            {
                this.preInput[i] = learner.Generate(Char.Parse(i.ToString()), (int)Math.Sqrt(this.N0));

                this.preOutput[i] = new double[N2];

                for (int j = 0; j < N2; j++)
                    this.preOutput[i][j] = ((j == i) ? 1 : 0);
            }
        }
        private void GenerateIntInput(int num)
        {
            //this.out0 = learner.Generate(Char.Parse(num.ToString()), (int)Math.Sqrt(this.N0));
            this.out0 = this.preInput[num];
        }

        private void GenerateIntOutput(int num)
        {
            //for (int i = 0; i < N2; i++)
            //    this.correct[i] = ((i == num) ? 1 : 0);
            this.correct = this.preOutput[num];
        }

        private void InitWeights()
        {
            for (int i = 0; i < N0; i++)
                for (int j = 0; j < N1; j++)
                    this.weights01[i, j] = (r.NextDouble() - 0.5) / 10;         //-0.05...0.05

            for (int i = 0; i < N1; i++)
                for (int j = 0; j < N2; j++)
                    this.weights12[i, j] = (r.NextDouble() - 0.5) / 10;         //-0.05...0.05
        }
    }
}
