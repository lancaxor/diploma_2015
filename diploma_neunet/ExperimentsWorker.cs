using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace diploma_neunet
{
    class ExperimentsWorker
    {
        private List<Experiment> exps;
        public int Count { get { return this.exps.Count; } }

        public ExperimentsWorker()
        {
            exps = new List<Experiment>();
        }

        public Experiment this[int index]
        {
            get {
                if (this.exps.Count > index)
                    return exps[index];
                else return null;
            }
            set
            {
                if (exps.Count > index)
                    exps[index] = value;
            }
        }

        public void Add(Experiment experiment)
        {
            this.exps.Add(new Experiment { fixedNeurons = new List<int>(experiment.fixedNeurons), data = experiment.data, name = experiment.name, repeats=experiment.repeats });
        }
        public void RemoveAt(int index)
        {
            this.exps.RemoveAt(index);
        }

        public void Clear()
        {
            this.exps.Clear();
        }
    }
}
