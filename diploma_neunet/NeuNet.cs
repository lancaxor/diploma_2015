using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace diploma_neunet
{
    public class NeuNet
    {
        int N0, N1, N2;     // num of neurons in input, hidden and output layers
        int era;
        const double alpha = 0.1;       //parameter of sigmoida's HAKJlOH 0_o
        const double eta = 1;           //learning speed coeficient

        int[] out0;         // layer 0 (input)
        double[] out1;      // layer 1 (hidden)
        double[] output;    // layer 2 (output)
        double[] correct;   // correct output
        double[] error;     // learn error
        double[,] weights01;// weights between input and hidden layers
        double[,] weights12;// weights between hidden and output layers
        double[] delta;     //local grad between hidden and input layers

        List<int> fxHidden, fxOut;

        Random r;

        #region Properties
        public int NumInput { get { return this.N0; } set { this.N0 = value; } }
        public int NumHidden { get { return this.N1; } set { this.N1 = value; } }
        public int NumOutput { get { return this.N2; } set { this.N2 = value; } }
        public int Epoch { get { return this.era; } }
        #endregion

        public NeuNet()
        {
            r = new Random();
            era = 0;
        }
        #region Learning

        public void LearnInt()
        {
            int currInput;
            AllocMem();
            InitWeights();

            while (true)
            {
                currInput = r.Next(9);      //for 0..9
                GenerateIntInput(currInput);
                GenerateIntOutput(currInput);

                ForwardPass();
                BackwardPass();
                CountError();
                this.era++;
            }
        }

        private void ForwardPass()
        {
            for (int i = 0; i < N1; i++)        //input => hidden
            {
                out1[i] = 0;
                for (int j = 0; j < N0; j++)
                    out1[i] += out0[j] * weights01[j, i];
                out1[i] = NeuronProp.SigmaFunction(out1[i], alpha);
            }

            for (int i = 0; i < N2; i++)        //hidden => output
            {
                output[i] = 0;
                for (int j = 0; j < N1; j++)
                    output[i] += out1[j] * weights12[j, i];
                output[i] = NeuronProp.SigmaFunction(output[i], alpha);
            }

            for (int i = 0; i < N2; i++)
                error[i] = output[i] - correct[i];
        }

        private void BackwardPass()
        {
            double sig, delta;
            for (int i = 0; i < N2; i++)        //output => hidden layer
            {
                if (fxOut.Contains(i))      //out neuron with index i is fixed //slow down!
                    continue;
                sig = output[i] * (1 - output[i]) * (correct[i] - output[i]);
                this.delta[i] = sig;
                for (int j = 0; j < N1; j++)
                {
                    delta = eta * sig * out1[j];
                    weights12[j, i] += delta;
                }
            }

            sig = 0;
            for (int i = 0; i < N1; i++)        //hidden=>input layer
            {
                if (fxHidden.Contains(i))   //hidden neuron with index i is fixed
                    continue;
                for (int k = 0; k < N2; k++)
                    sig += (this.delta[i] * weights12[i, k]);
                sig *= out1[i] * (1 - out1[i]);
                for (int j = 0; j < N0; j++) {
                    weights01[i, j] += eta * sig * out0[j];
                }
            }

        }

        private double CountError()         /////////
        {
            double res = 0;
            for (int i = 0; i < N2; i++)
                res += this.error[i] * this.error[i] / N2;
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

        public int Test(int[] inputData)
        {
            //◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘
            int res = 0;
            if (this.N0 != inputData.Length)
            {
                System.Windows.Forms.MessageBox.Show("Wrong input test data!");
                return -1;
            }

            for (int i = 0; i < this.N0; i++)
                this.out0[i] = inputData[i];

            return 0;
        }

        private void GenerateIntInput(int num)
        {
            this.out0 = LearnDataGenerator.Generate((char)num, (int)Math.Sqrt(this.N0));
        }

        private void GenerateIntOutput(int num)
        {
            for (int i = 0; i < N2; i++)
                this.correct[i] = (i == num) ? 1 : 0;
        }

        private void InitWeights()
        {
            for (int i = 0; i < N0; i++)
                for (int j = 0; j < N1; j++)
                    this.weights01[i, j] = (r.NextDouble() - 0.5) / 10;         //-0.05...0.05

            for(int i=0;i<N1;i++)
                for(int j=0;j<N2;j++)
                    this.weights12[i, j] = (r.NextDouble() - 0.5) / 10;         //-0.05...0.05
        }
    }
}
