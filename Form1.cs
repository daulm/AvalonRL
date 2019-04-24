using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AvalonRL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int count = 0;
            int spyWins = 0;
            int goodWins = 0;
            if (Int32.TryParse(textBox1.Text, out count))
            {
                for (int i = 0; i < count; i++)
                {
                    BotGame7 newGame = new BotGame7(0, 1);
                    if (newGame.Play() == 0)
                    {
                        spyWins++;
                    }
                    else
                    {
                        goodWins++;
                    }
                }

            }
            MessageBox.Show(spyWins.ToString() + " | " + goodWins.ToString());
        }
    }
}
