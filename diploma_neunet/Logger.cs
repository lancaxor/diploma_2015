using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace diploma_neunet
{
    class Logger
    {
        Thread logger;
        Queue<String> set;
        Boolean running;
        StringBuilder output;   //instead of string...
        String path;
        String fileName;
        StreamWriter writer;

        public Boolean isRunning { get { return this.running; } }
        public Logger(String logDirectory)
        {
            this.path = (logDirectory == "" ? Directory.GetCurrentDirectory() + "/Logs/" : logDirectory);
            this.Init();

        }
        public Logger()
        {
            this.path = Directory.GetCurrentDirectory() + "/Logs/";
            this.Init();
        }
        private void Init()
        {
            this.logger = new Thread(new ThreadStart(this.LoggerProcess));
            this.set = new Queue<string>();
            this.running = false;
            this.output = new StringBuilder();

            if (!Directory.Exists(this.path))
                Directory.CreateDirectory(this.path);
        }
        public void Start()
        {
            this.Init();
            this.running = true;
            this.logger.Start();
            fileName = DateTime.Now.ToString().Replace(':', '.').Replace(' ', '_') + ".log";       //mm.dd.yy hh.mm.ss.ms
            this.writer = File.CreateText(path + fileName);
        }
        public void Stop()
        {
            this.running = false;
            if (writer.BaseStream != null)
                writer.Close();
        }
        private void LoggerProcess()
        {
            while (this.running)
            {
                if (this.set.Count == 0 && this.output.Length == 0)
                    continue;
                else if (this.output.Length != 0 && this.set.Count == 0)
                {
                    this.LogToFS(this.output.ToString());
                    this.output.Clear();
                }
                else                            //optimization -- decrease disc IO, but increase RAM memory
                    this.output.AppendLine(this.set.Dequeue());
            }
        }
        public void Log(String logString)
        {
            this.set.Enqueue(logString);
        }
        private void LogToFS(String logString)
        {
            try
            {
                writer.Write(logString);
            }
            catch (IOException)
            {
                System.Windows.Forms.MessageBox.Show(String.Format("Cannot write to file {0}. The logger will be disabled for current learning session.", this.path + this.fileName));
                this.output.Clear();
                this.set.Clear();
                this.running = false;
            }
        }
    }
}
