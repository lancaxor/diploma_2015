using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace diploma_neunet
{
    class NetConfig
    {
        public NetConfig()
        {
            this.maxEpoch = 100;
            this.minError = 0.2;
            this.minErrorChange = 0.001;
            this.NumInput = 2500;
            this.NumHidden = 25000;
            this.NumOutput = 10;
        }

        public NetConfig(int MaxEpoch, double MinimumError, double MinimumErrorChange)
        {
            this.maxEpoch = MaxEpoch;
            this.minError = MinimumError;
            this.minErrorChange = MinimumErrorChange;
        }

        public int maxEpoch { get; set; }
        public double minError { get; set; }
        public double minErrorChange { get; set; }
        public int NumInput { get; set; }
        public int NumHidden { get; set; }
        public int NumOutput { get; set; }

        public double ActivationFunction(double input, params double[] parameter)
        {
            //var result = NeuronProp.RBF(input, parameter[0]);
            var result = NeuronProp.SigmaFunction(input, parameter[0]);
            //var result = NeuronProp.LogisticFunction(input, parameter[0]);
            return result;
        }
    }
}
