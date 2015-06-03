using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using diploma_neunet;

namespace NeuroNet_Hard
{
    public partial class TestFormPaint : Form
    {
        private bool drawing = false;
        Bitmap bmp;         // for recognizing
        Point oldP;
        Pen pen;
        const int bsize = 28;
        NeuNet net;

        public TestFormPaint(NeuNet network)
        {
            InitializeComponent();
            //this.pictureBox1.Width = this.pictureBox1.Height = 50;
            this.pictureBox1.BackColor = Color.White;
            //bmp = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            bmp = new Bitmap(bsize, bsize);
            this.pictureBox1.Image = (Image)new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            this.net = network;
            oldP = new Point();
            pen = new Pen(Brushes.Black);
            pen.Width = 3;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            oldP = new Point(e.X, e.Y);
            using (Graphics g = Graphics.FromImage(this.pictureBox1.Image))
            {
                g.DrawLine(pen, new Point(e.X, e.Y), new Point(e.X+1, e.Y+1));
                this.pictureBox1.Refresh();
            }
            drawing = true;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (drawing)
            {
                using (Graphics g = Graphics.FromImage(this.pictureBox1.Image))
                {
                    g.DrawLine(pen, new Point(e.X, e.Y), oldP);
                    oldP.X = e.X;
                    oldP.Y = e.Y;
                    this.pictureBox1.Refresh();
                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            drawing = false;
        }

        private void btnClear_Click(object sender, EventArgs e)     //clear
        {
            //this.pictureBox1.Image = (Image)(bmp = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height));
            this.pictureBox1.Image = (Image)new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            bmp = new Bitmap(bsize, bsize);
        }

        private int[] PictureToInt()            //this.picture.image => int[]
        {
            CropImage();
            int[] netIn = new int[this.bmp.Height * this.bmp.Width];
            Color c = new Color();
            for (int i = 0; i < netIn.Length; i++)
            {
                c = bmp.GetPixel((int)(i % this.bmp.Width), (int)(i / this.bmp.Width));
                netIn[i] = c.A;     //image is black-white => c.R = c.G = c.B
            }
            return netIn;
        }

        private void button2_Click(object sender, EventArgs e)          //recognize
        {
            int result = this.net.Test(this.PictureToInt());
            MessageBox.Show(String.Format("Recognized: {0}", result));
        }

        private void CropImage()
        {
            int left =0, right=0, top =0, bottom = 0;       //widths of lines will be cropped
            int curSum = 0;

            for (int y = 0; y < this.pictureBox1.Height; y++)       //crop top
            {
                curSum = 0;
                for (int x = 0; x < this.pictureBox1.Width; x++)
                    curSum += (this.pictureBox1.Image as Bitmap).GetPixel(x, y).A == 0 ? 0 : 1;       //top
                if (curSum != 0)
                {
                    top = y;
                    break;
                }
            }

            for (int y = this.pictureBox1.Height-1; y >0 ; y--)       //crop bottom
            {
                curSum = 0;
                for (int x = 0; x < this.pictureBox1.Width; x++)
                    curSum += (this.pictureBox1.Image as Bitmap).GetPixel(x, y).A == 0 ? 0 : 1;       //top
                if (curSum != 0)
                {
                    bottom = y;
                    break;
                }
            }

            for (int x = 0; x < this.pictureBox1.Width; x++)        //crop left
            {
                curSum = 0;
                for (int y = 0; y < this.pictureBox1.Height; y++)
                    curSum += (this.pictureBox1.Image as Bitmap).GetPixel(x, y).A == 0 ? 0 : 1;       //top
                if (curSum != 0)
                {
                    left = x;
                    break;
                }
            }

            for (int x = this.pictureBox1.Width - 1; x > 0; x--)        //crop right
            {
                curSum = 0;
                for (int y = 0; y < this.pictureBox1.Height; y++)
                    curSum += (this.pictureBox1.Image as Bitmap).GetPixel(x, y).A == 0 ? 0 : 1;       //top
                if (curSum != 0)
                {
                    right = x;
                    break;
                }
            }

            if ((bottom <= top) || (right <= left))
            {
                //MessageBox.Show("Nothing will be cropped!");
                return;
            }
            //MessageBox.Show("l: " + left.ToString() + " r: " + right.ToString() + " t: " + top.ToString() + " b: " + bottom.ToString());
            //bmp = new Bitmap(this.pictureBox1.Image);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                Rectangle newRect = new Rectangle(left, top, right - left + 1 , bottom - top + 1);
                Rectangle destRect = new Rectangle(0,0, this.bmp.Width, this.bmp.Height);
                //g.Clear(Color.White);
                g.DrawImage(this.pictureBox1.Image, destRect, newRect, GraphicsUnit.Pixel);
            }
            //this.pictureBox1.Image = (Image)bmp;
            this.pictureBox1.Refresh();
        }
    }
}
