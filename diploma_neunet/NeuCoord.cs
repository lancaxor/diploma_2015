using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace diploma_neunet
{
    public class NeuCoord
    {
        private int layer;
        private int index;

        public int Layer { get { return this.layer; } set { this.layer = value; } }
        public int Index { get { return this.index; } set { this.index = value; } }
    }
}
