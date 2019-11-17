using System;
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

        Color defaultColor = Color.White;
        int Xcount = 10;
        List<InputNeuron> Xneurons = new List<InputNeuron>();
        //List<HideNeuron> Zneurons = new List<HideNeuron>();
        //List<OutputNeuron> Yneurons = new List<OutputNeuron>();
        //

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < Xcount; i++)
            {
                Xneurons.Add(new InputNeuron());
            }
        }

        private void WriteLineConsole(string str)
        {
            richTextBox1.AppendText(str + Environment.NewLine);
            richTextBox1.ScrollToCaret();
        }

        private void WriteConsole(string str)
        {
            richTextBox1.AppendText(str);
            richTextBox1.ScrollToCaret();
        }

        private void WriteLineConsole(Color clr, string str)
        {
            richTextBox1.SelectionColor = clr;
            richTextBox1.AppendText(str + Environment.NewLine);
            richTextBox1.ScrollToCaret();
            richTextBox1.SelectionColor = defaultColor;
        }

        private void WriteConsole(Color clr, string str)
        {
            richTextBox1.SelectionColor = clr;
            richTextBox1.AppendText(str);
            richTextBox1.ScrollToCaret();
            richTextBox1.SelectionColor = defaultColor;

        }

    }
}
