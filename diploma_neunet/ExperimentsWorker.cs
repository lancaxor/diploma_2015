using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace diploma_neunet
{
    class ExperimentsWorker
    {
        private List<Experiment> exps;

        public sealed ExperimentsWorker()
        {
            exps = new List<Experiment>();
        }

        public Experiment this[int i]
        {
            get { return exps[i]; }
            set
            {
                if (exps.Count < i)
                    exps[i] = value;
            }
        }

        public void Create(string str)
        {
            this.exps.Add(new Experiment { name = str });
        }
    }
}
