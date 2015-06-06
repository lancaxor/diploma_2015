using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace diploma_neunet
{
    class LearnDataGenerator        //Generate vector for neunet learning: char to bitmap then to array
    {
        private static Random r;
        private const int MaxNoise = 0;     // %
        private static int currNoise = 0;

        public LearnDataGenerator()
        {
            r = new Random();
            //Generate(r.Next(0, 9).ToString()[0], 50);      //0..9
            //Generate((char)('A' + r.Next(0, 25)), 50);   //A..Z
        }

        public double[] Generate(char ch, int size)
        {
            //r = new Random();
            currNoise = r.Next(MaxNoise);

            double[] data = new double[size * size];
            Bitmap res = new Bitmap(size, size);
            using (Graphics g = Graphics.FromImage((Image)res))
            {
                g.Clear(Color.White);
                g.DrawString(ch.ToString(), new Font("Arial", size - 2, GraphicsUnit.Pixel), Brushes.Black, 0, 0);
            }

            for (int i = 0; i < (int)(size * size * currNoise / 100); i++)      //Noise generator:  size --> 100%, => (size * currNoise / 100) -> currNoise%
            {
                int x = r.Next(size), y = r.Next(size);
                res.SetPixel(x, y, res.GetPixel(x, y).B > 127 ? Color.Black : Color.White);
            }
            //MessageBox.Show(ch.ToString());
            //TestForm(res);
            for (int y = 0, i = 0; y < size; y++)
                for (int x = 0; x < size; x++, i++)
                    data[i] = res.GetPixel(x, y).B > 127 ? -0.95: 0.95;
            return data;
        }

        private void TestForm(Bitmap bmp)
        {
            Form tf = new Form();
            PictureBox pc = new PictureBox();
            pc.BackColor = Color.Black;
            pc.Image = (Image)bmp;
            pc.Dock = DockStyle.Fill;
            tf.Controls.Add(pc);
            tf.StartPosition = FormStartPosition.CenterParent;
            tf.ShowDialog();
        }
    }
}
