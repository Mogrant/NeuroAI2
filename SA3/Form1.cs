using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SA3
{

    public partial class Form1 : Form
    {

        bool debug = true;

        struct LearnEx
        {
            ArrayList Arr;
            ArrayList YCorrect;

            public LearnEx(ArrayList arr, ArrayList ycorrect)
            {
                this.Arr = arr;
                this.YCorrect = ycorrect;
            }

            public ArrayList LearnArray
            {
                get
                {
                    return this.Arr;
                }
                set
                {
                    this.Arr = value;
                }
            }

            public ArrayList LearnYCorrect
            {
                get
                {
                    return this.YCorrect;
                }
                set
                {
                    this.YCorrect = value;
                }
            }
        }

        List<LearnEx> learns;


        Color defaultColor = Color.White;
        Random rnd = new Random();
        Graphics g;
        Brush br;
        Pen pn;



        int Xcount = 10;
        int Zcount = 8;
        int Ycount = 2;
        List<Neuron> Xneurons = new List<Neuron>();
        List<Neuron> Zneurons = new List<Neuron>(); //добавить возможность изменения количества внутренних слоев
        List<Neuron> Yneurons = new List<Neuron>();

        bool addedNeurons = false;
        //List<OutputNeuron> Yneurons = new List<OutputNeuron>();
        public void WriteLineConsole(string str)
        {
            richTextBox1.AppendText(str + Environment.NewLine);
            richTextBox1.ScrollToCaret();
        }
        public void WriteConsole(string str)
        {
            richTextBox1.AppendText(str);
            richTextBox1.ScrollToCaret();
        }
        public void WriteLineConsole(Color clr, string str)
        {
            richTextBox1.SelectionColor = clr;
            richTextBox1.AppendText(str + Environment.NewLine);
            richTextBox1.ScrollToCaret();
            richTextBox1.SelectionColor = defaultColor;
        }
        public void WriteConsole(Color clr, string str)
        {
            richTextBox1.SelectionColor = clr;
            richTextBox1.AppendText(str);
            richTextBox1.ScrollToCaret();
            richTextBox1.SelectionColor = defaultColor;

        }
        public void DebugWriteLineConsole(string str)
        {
            if (!debug) return;
            richTextBox1.AppendText(str + Environment.NewLine);
            richTextBox1.ScrollToCaret();
        }
        public void DebugWriteConsole(string str)
        {
            if (!debug) return;
            richTextBox1.AppendText(str);
            richTextBox1.ScrollToCaret();
        }
        public void DebugWriteLineConsole(Color clr, string str)
        {
            if (!debug) return;
            richTextBox1.SelectionColor = clr;
            richTextBox1.AppendText(str + Environment.NewLine);
            richTextBox1.ScrollToCaret();
            richTextBox1.SelectionColor = defaultColor;
        }
        public void DebugWriteConsole(Color clr, string str)
        {
            if (!debug) return;
            richTextBox1.SelectionColor = clr;
            richTextBox1.AppendText(str);
            richTextBox1.ScrollToCaret();
            richTextBox1.SelectionColor = defaultColor;

        }


        public Form1()
        {
            InitializeComponent();
        }

        private void drawCircle(Color clr, float x, float y , int radius)
        {
            g.FillEllipse((new SolidBrush(clr)), x - radius / 2, y - radius / 2, radius, radius);
            g.DrawEllipse(pn, x-radius/2, y-radius/2, radius, radius) ;
            
        }

        private void Render()
        {
            g.Clear(Color.White);
            float yXDivide = (float)(pictureBox1.Height / Xneurons.Count);
            float xDivide = (float)(pictureBox1.Width / 3);
            for (int i = 0; i < Xneurons.Count; i++)
            {
                if (Xneurons[i].Result > 0)
                {
                    drawCircle(Color.Red, xDivide - xDivide / 2, yXDivide * i + yXDivide / 2, 20);
                }
                else
                {
                    drawCircle(Color.LightBlue, xDivide - xDivide / 2, yXDivide * i + yXDivide / 2, 20);
                }
                
                //g.DrawString(Convert.ToString(Xneurons[i].Result), new Font("Arial", 10, FontStyle.Regular), new SolidBrush(Color.Black), xDivide - xDivide / 2, yXDivide * i + yXDivide / 2);

            }

            float yZDivide = (float)(pictureBox1.Height / Zneurons.Count);
            for (int i = 0; i < Zneurons.Count; i++)
            {
                for (int j = 0; j < Xneurons.Count; j++)
                {
                    if (Zneurons[i].Axons[j].ConnectedNeuron.Index == j)
                    {
                        g.DrawLine(new Pen(Color.Black,  (float)Zneurons[i].Axons[j].Omega*5), xDivide- xDivide/2, yXDivide * j + yXDivide/2, xDivide * 2 - xDivide/2, yZDivide * i + yZDivide/2);
                    }
                }
                drawCircle(Color.FromArgb(255, (int)((double)(Zneurons[i].ActivationFunc()*255)),255-(int)((double)(Zneurons[i].ActivationFunc() * 255)), 0), xDivide*2 - xDivide / 2, yZDivide * i + yZDivide/2, 20);
                g.DrawString(Convert.ToString(Zneurons[i].ActivationFunc()), new Font("Arial", 10, FontStyle.Regular), new SolidBrush(Color.Black), xDivide * 2 - xDivide / 2, yZDivide * i + yZDivide / 2);

            }

            float yYDivide = (float)(pictureBox1.Height / Yneurons.Count);
            for (int i = 0; i < Yneurons.Count; i++)
            {
                for (int j = 0; j < Zneurons.Count; j++)
                {
                    if (Yneurons[i].Axons[j].ConnectedNeuron.Index == j)
                    {
                        g.DrawLine(new Pen(Color.Black, (float)Yneurons[i].Axons[j].Omega * 5), xDivide*2 - xDivide / 2, yZDivide * j + yZDivide / 2, xDivide * 3 - xDivide / 2, yYDivide * i + yYDivide / 2);
                    }
                }
                drawCircle(Color.LightSalmon, xDivide*3 - xDivide / 2, yYDivide * i + yYDivide / 2, 20);
                g.DrawString(Convert.ToString(Yneurons[i].ActivationFunc()), new Font("Arial", 10, FontStyle.Regular), new SolidBrush(Color.Black), xDivide * 3 - xDivide / 2, yYDivide * i + yYDivide / 2);
            }

        }

        private void AddNeurons()
        {
            if (!addedNeurons)
            {
                addedNeurons = true;
                for (int i = 0; i < Xcount; i++)
                {
                    Xneurons.Add(new Neuron(i));
                    Xneurons[Xneurons.Count - 1].Result = rnd.Next(0, 2);
                    DebugWriteLineConsole(Color.LightBlue, "Added X neuron [" + i + "]");
                }

                for (int i = 0; i < Zcount; i++)
                {
                    Zneurons.Add(new Neuron(i));
                    DebugWriteLineConsole(Color.LightCoral, "Added Z neuron [" + i + "]");
                    for (int j = 0; j < Xneurons.Count; j++)
                    {
                        Zneurons[i].AddAxon(Xneurons[j], rnd.NextDouble());
                        DebugWriteLineConsole(Color.LightCoral, " + Added axon : Z neuron [" + i + "] connected with " + Xneurons[j].Index + " w = " + Zneurons[i].Axons[Zneurons[i].Axons.Count - 1].Omega);
                    }
                }

                for (int i = 0; i < Ycount; i++)
                {
                    Yneurons.Add(new Neuron(i));
                    DebugWriteLineConsole(Color.LightSalmon, "Added Y neuron [" + i + "]");
                    for (int j = 0; j < Zneurons.Count; j++)
                    {
                        Yneurons[i].AddAxon(Zneurons[j], rnd.NextDouble());
                        DebugWriteLineConsole(Color.LightSalmon, " + Added axon : Y neuron [" + i + "] connected with " + Zneurons[j].Index + " w = " + Yneurons[i].Axons[Yneurons[i].Axons.Count - 1].Omega);
                    }
                }
                WriteLineConsole("Created neurons X = " + Xneurons.Count + " Z = " + Zneurons.Count + " Y = " + Yneurons.Count);
            }
        }

        private LearnEx AddLearnEx(string file, int resultY)
        {
            if (!System.IO.File.Exists("./" + file))
            {
                WriteLineConsole(Color.Red, "File : \"" + file + "\" is not exists!");
            }
            System.Drawing.Image image;
            System.Drawing.Bitmap picture;
            int[,] picMatrix;
            try
            {
                image = Image.FromFile("./" + file);
                picture = new Bitmap(image);
                WriteLineConsole("bitmap created");
                picMatrix = new int[picture.Width, picture.Height];
                Color clr;
                for (int i = 0; i < picture.Width; i++)
                {
                    for (int j = 0; j < picture.Height; j++)
                    {
                        clr = picture.GetPixel(i, j);
                        if (clr.R > 0 && clr.G > 0 && clr.B > 0)
                        {
                            picMatrix[i, j] = 0;
                        }
                        else
                        {
                            picMatrix[i, j] = 1;
                        }
                    }
                }
            }
            catch
            {
                picMatrix = new int[0, 0];
                WriteLineConsole("Error load image");
            }

            ArrayList arr = new ArrayList();
            for (int i = 0; i < picMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < picMatrix.GetLength(1); j++)
                {
                    WriteConsole(" " + picMatrix[i, j]);
                    arr.Add(picMatrix[i, j]);

                }
                WriteLineConsole("");
            }
            ArrayList Ycorrect = new ArrayList();
            Ycorrect.Add(resultY);
            return new LearnEx(arr, Ycorrect);
        }

        //EVENTS
        private void Form1_Load(object sender, EventArgs e)
        {
            g = pictureBox1.CreateGraphics();
            pn = new Pen(Color.Black, 1);
            br = new SolidBrush(Color.Beige);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddNeurons();
            Render();
            learns = new List<LearnEx>();
            learns.Add(AddLearnEx("l1.png", 0));
            learns.Add(AddLearnEx("l2.png", 0));
            learns.Add(AddLearnEx("l3.png", 0));
        }
    }



}
