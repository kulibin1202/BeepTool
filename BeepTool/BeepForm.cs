using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeepTool
{
    public partial class BeepForm : Form
    {
        public BeepForm()
        {
            InitializeComponent();

            button1.Click += Button1_Click;

            toolTip1.SetToolTip(label1, "The frequency of the beep, ranging from 37 to 32767 hertz.");
            toolTip1.SetToolTip(label2, "The duration of the beep measured in milliseconds.");
            toolTip1.SetToolTip(label4, "milliseconds.");      

            txtFrequency.KeyPress += TextBox_KeyPress;
            txtDuration.KeyPress += TextBox_KeyPress;
            txtCount.KeyPress += TextBox_KeyPress;
            txtDelay.KeyPress += TextBox_KeyPress;
        }       

        private void Button1_Click(object sender, EventArgs e)
        {
            int frequency = int.Parse(txtFrequency.Text);
            int duration = int.Parse(txtDuration.Text);
            int count = int.Parse(txtCount.Text);
            int delay = int.Parse(txtDelay.Text);

            if (frequency < 37 || frequency > 32767)
            {
                MessageBox.Show("The frequency of the beep, ranging from 37 to 32767 hertz", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            panel1.Enabled = false;

            Task.Run(async () =>
            {
                do
                {
                    Console.Beep(frequency, duration);
                    count--;

                    if (count > 0)
                        await Task.Delay(delay);

                } while (count > 0);

                Invoke((MethodInvoker)delegate
                {
                    panel1.Enabled = true;
                });
            });
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar < 48 | (int)e.KeyChar > 57)
            {
                e.Handled = true;
            }
        }
    }
}
