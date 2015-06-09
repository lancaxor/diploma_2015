using System;
namespace diploma_neunet
{
    public struct NetData
    {
        public TimeSpan time;
        public Int32 epoch;
        public Double avgErr;
        public Double errChange;
        public Double seconds;

        public static NetData Empty = new NetData { time = TimeSpan.Zero, epoch = 0, avgErr = 0.0, errChange = 0.0, seconds = 0.0 };
        public NetData(NetData data)
        {
            this.time = data.time;
            this.epoch = data.epoch;
            this.avgErr = data.avgErr;
            this.errChange = data.errChange;
            this.seconds = data.seconds;
        }
    }
}