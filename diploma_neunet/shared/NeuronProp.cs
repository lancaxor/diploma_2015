using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace diploma_neunet
{
    static class NeuronProp
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
            return (1.0 / (1.0 + Math.Exp(-1*alpha*input)));
        }

        public static double LogisticFunction(double input, double alpha)
        {
            double K = 1.0;
            double P0 = 0.5;
            var exp = Math.Exp(input * alpha);
            double res = K * P0 * exp / (K + P0 * (exp - 1));
            return (res);// > 0.0 ? -1.0 : 1.0);
        }

        public static double TahnFunction(double input)
        {
            double a = 1.7159;
            double b = 2/3;
            double res = a * Math.Tanh(input * b);
            return res;
        }

        public static string ArrToProps(double[] array)
        {
            string res = String.Empty;
            for (int i = 0; i < array.Length; i++)
                res = String.Concat(res, String.Format("{0} ", ((int)array[i]).ToString()));
            return res;
        }
    }
}
