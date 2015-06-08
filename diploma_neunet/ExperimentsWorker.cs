using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace diploma_neunet
{
    class ExperimentsWorker
    {
        private List<Experiment> exps;

        public ExperimentsWorker()
        {
            exps = new List<Experiment>();
        }

        public Experiment this[int i]
        {
            get {
                if (this.exps.Count < i)
                    return exps[i];
                else return null;
            }
            set
            {
                if (exps.Count < i)
                    exps[i] = value;
            }
        }

        public void Add(Experiment experiment)
        {
            this.exps.Add(new Experiment { fixedNeurons = experiment.fixedNeurons, data = experiment.data, name = experiment.name });
        }

        public void Clear()
        {
            this.exps.Clear();
        }
    }
}
