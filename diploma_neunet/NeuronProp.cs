using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace diploma_neunet
{
    abstract class NeuronProp
    {

        static List<int> FixedHidden = new List<int>();
        static List<int> FixedOutput = new List<int>();

        public static double RBF(double input, double sigma)
        {
            return Math.Exp(((-1.0) * input * input) / (sigma * sigma));
        }

        public static double SigmaFunction(double input, double alpha)
        {
            //return (input / Math.Sqrt(1 + input * input));
            return (-0.5 + 1.0 / (1.0 + Math.Exp(alpha*input)));
        }
    }
}
