using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LoggerKit;

namespace ProcessManager
{
    public partial class Form1 : Form
    {
        Process[] running;
        LoggerKit.Logger myLog = new LoggerKit.Logger();
        public Form1()
        {
            myLog.generateLogSheet("processLog");
            myLog.setVerbose(true);
            InitializeComponent();
        }
        ~Form1()
        {
            myLog.endLogger();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            button1.Text = "Refresh";   

             running = Process.GetProcesses();

             panel1.Controls.Clear();

            

            // How many buttons do you want ?
            int NumOfButtons = running.Length;
            // X Location of each created button in the panel
            int loc = 20;

        

            for (int i = 0; i < NumOfButtons; i++)
            {
                Button btn = new Button();
                Label lbl = new Label();

                {

                    myLog.inputLogLine(running[i].ToString());

                    btn.Name = "Btn-" + i.ToString();
                    btn.Size = new Size(80, 20);
                    btn.Tag = i;
                    lbl.Size = new Size(100, 20);


                    int pos1 = running[i].ToString().IndexOf("(");
                    int pos2 = running[i].ToString().LastIndexOf(")")-1;

                    string newString = running[i].ToString().Substring(pos1+1, pos2 - pos1);
                    btn.Name = newString;

                    lbl.Text = newString;
                    btn.Text = "KILL >:)";
                    lbl.Location = new Point(90, loc);
                    btn.Location = new Point(5, loc);
                }
                // Add Click event Handler for each created button
                btn.Click += Buttons_Click;
                
                loc += 20;
                // Add the created btn to panl
               
                panel1.Controls.Add(btn);
                panel1.Controls.Add(lbl);
            }

           
        }
        private void Buttons_Click(System.Object sender, System.EventArgs e)
        {
            // Use "Sender" to know which button was clicked ?
            Button btn = sender as Button;

            // btn name set to program name

            for (int i = 0; i < running.Length; i++)
            {
                int pos1 = running[i].ToString().IndexOf("(");
                int pos2 = running[i].ToString().LastIndexOf(")") - 1;

                string newString = running[i].ToString().Substring(pos1 + 1, pos2 - pos1);

                if (newString == btn.Name)
                {
                    running[i].Kill();

                    // kill process
                    btn.Text = "Dead";
                    btn.Enabled = false;

                    

                    

                }

            }



        }

    }
}
