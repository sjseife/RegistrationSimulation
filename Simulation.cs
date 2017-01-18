/**
 * ---------------------------------------------------------------------------
 * File name: Simulation.cs<br/>
 * Project name: Project 4<br/>
 * ---------------------------------------------------------------------------
 * Creator's name and email: Stan Seiferth, zsjs19@goldmail.etsu.edu<br/>
 * Course:  CSCI 2210-201<br/>
 * Creation Date: Nov 12, 2013<br/>
 * Date of Last Modification: Nov 16, 2013
 * ---------------------------------------------------------------------------
 */

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

namespace _2210_201_SeiferthStan_Project4
{
    public partial class Simulation : Form
    {
        private ChangeSettings cs = new ChangeSettings(); //initialize shared variables
        private int runs;
        private double dAVGTime;
        private double dShortTime;
        private List<TextBox> Lane = new List<TextBox>();
        private List<Label>[] waiting;
        private Events events = new Events();
        private double open;
        private double close;
        int whole;
        double part;
        private Timer timer = new Timer();
        private int windows;
        private Stack<double> stack = new Stack<double>();
        private string[] labelText;
        private string AMPM;
        private bool PM = false;
        private int expand = 6;
        private int registered = 0;
        private int arrived = 0;
        private int longest = 0;
        private int moveDown = 201;



        /**
         * Initiate simulation form<br>
         *
         * <hr>
         * Date created: Nov 12, 2013<br>
         * Date last modified: Nov 16, 2013<br>
         * <hr>
         * @author Stan Seiferth
         */
        public Simulation()
        {
            InitializeComponent();

        }


        /**
         * Opens change settings form when users click on menu item<br>
         *
         * <hr>
         * Date created: Nov 12, 2013<br>
         * Date last modified: Nov 16, 2013<br>
         * <hr>
         * @author Stan Seiferth
         */
        private void changeValuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cs.Show();
            label3.Visible = false;
            label1.Text = "Hint: now press start.";
           
        }


        /**
         * starts the simulation, intializes variables with values, and starts timer<br>
         *
         * <hr>
         * Date created: Nov 12, 2013<br>
         * Date last modified: Nov 16, 2013<br>
         * <hr>
         * @author Stan Seiferth
         */
        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int reg;
            int min;
            int sec;
            int hrs;
            double time;
            

            if (cs.Windows != null && int.TryParse(cs.Windows, out runs))
            {
                label1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                TimeLabel.Text = cs.OpenTime + ":" + cs.OpenMin;
                AMPM = cs.OpenAMPM;
                if (AMPM.Equals(PM))
                    PM = true;

                labReg.Visible = true;
                labArr.Visible = true;
                labLong.Visible = true;
                labLong.Location = new Point(7, moveDown);

                for (int j = Lane.Count - 1; j > 0; j--)          //clear all previous text boxes
                {
                    Lane[j].Visible = false;           //remove any left over lanes if running simulation
                    this.Controls.Remove(Lane[j]);     //more than once 
                    Lane[j].Dispose();
                    Lane.Remove(Lane[j]);
                }
                Lane = new List<TextBox>();
                getReady(runs, Lane);               //prepare the lanes for lines to form
                int.TryParse(cs.Reg, out reg);
                int.TryParse(cs.Windows, out windows);
                events.InitializeQueues(windows);
                TimeLabel.Location = new Point((ActiveForm.Width / 2) - 30, 4);

                labelText = new string[windows];
                waiting = new List<Label>[windows]; //make and array of list<labels> for easy removal
                for (int k = 0; k < windows; k++)
                {
                    waiting[k] = new List<Label>();
                    labelText[k] = string.Empty;
                }

                    int.TryParse(cs.OpenMin, out min); //convert opening time to a double
                int.TryParse(cs.OpenTime, out hrs);
                open = min / 60.0;
                open += hrs;
                if (cs.OpenAMPM.Equals("PM"))
                    open += 12;

                int.TryParse(cs.CloseMin, out min); //convert closing time to a double
                int.TryParse(cs.CloseTime, out hrs);
                close = min / 60.0;
                close += hrs;
                if (cs.CloseAMPM.Equals("PM"))
                    close += 12;

                events.OpenTime = open;             //set open time
                events.CloseTime = close;           //set close time
                events.EventTimeSpan = close - open;//set time length of event
               
                int.TryParse(cs.AVGMin, out min);   //convert average registration time to a double
                int.TryParse(cs.AVGSec, out sec);
                dAVGTime = sec / 60.0;
                dAVGTime += min;
               
                int.TryParse(cs.ShortMin, out min); //convert shortest registration time to double
                int.TryParse(cs.ShortSec, out sec);
                dShortTime = sec / 60.0;
                dShortTime += min;
                events.FindTimeInLine(reg, dAVGTime, dShortTime); //find registrants  time to register
                
                time = open;
                whole = (int)open;
                part = time - whole;
                part = (part * 60);
                timer.Tick += new EventHandler(timer_Tick);
                timer.Interval = (1000);             // Timer will tick every 1 second
                timer.Enabled = true;                       // Enable the timer
                timer.Start();                              // Start the timer
                
            }
            else
            {

            }
        }


        /**
         * Prepares form's size and textbox controls<br>
         *
         * <hr>
         * Date created: Nov 12, 2013<br>
         * Date last modified: Nov 16, 2013<br>
         * <hr>
         * @author Stan Seiferth
         */
        private static void getReady(int runs, List<TextBox> Lane)
        {
           int x = 47;
           int y = 59;
           int expand = 160;
           int formX = 113;
           ActiveForm.Size = new System.Drawing.Size(113, 281);
           
           TextBox temp;

           for (int i = 0; i < runs; i++)                   //create lanes 
           {
               temp = new TextBox();
               temp.Size = new Size(70, 31);
               temp.Location = new Point(x, y);
               temp.Font = new Font("Microsoft Sans Serif", 12);
               temp.Text = "Lane " + (i+1);
               temp.ReadOnly = true;
               temp.TabStop = false;
               Lane.Add(temp);
               x += 88;
               ActiveForm.Size = new System.Drawing.Size(formX, 261);
               formX += expand;
               if (expand > 101)
                    expand -= 70;
               ActiveForm.Controls.Add(Lane[i]);
           }
            
            
        }


        /**
         * receives tick event from timer, check for events every second 
         * to keep simulation form up to date. <br>
         * <hr>
         * Date created: Nov 12, 2013<br>
         * Date last modified: Nov 16, 2013<br>
         * <hr>
         * @author Stan Seiferth
         */
        void timer_Tick(object sender, EventArgs e)
        {
            string strOut = string.Empty;
            double time = 0;
            int numOfRegs;
            int line;
            int result;
       
            part++;
            if (part == 60)
            {
                whole++;
                part = 0;
            }
            if (whole > 11)
                AMPM = "PM";
            if (whole > 12)
            {
                whole -= 12;
                PM = true;
            }
            if (part < 10)
                strOut = whole + ":0" + part + " " + AMPM;
            else
                strOut = whole + ":" + part + " " + AMPM;
            TimeLabel.Text = strOut;
            time = part / 60;
            time += whole;

            numOfRegs = events.FeedTimer(time);
            if (numOfRegs == -1)
            {
                timer.Stop();
                return;
            }
            for (int i = 0; i < numOfRegs; i++)
            {
                line = events.LoadQueues(time);
                stack.Push(events.currentRegTime);
                getInLine(line);
                arrived++;
                
            }

            for (int i = 0; i < windows; i++)
            {
                result = events.CheckDone(i, time);

                if (result > -1)
                {
                    removeLabel(result);
                    registered++;
                }
            }

            labArr.Text = "Arrivals: " + arrived;
            labReg.Text = "Registered: " + registered;
            labLong.Text = "Longest line so far: " + longest;

        }


        /**
         * removes registrant from line once registration is complete<br>
         *
         * <hr>
         * Date created: Nov 12, 2013<br>
         * Date last modified: Nov 16, 2013<br>
         * <hr>
         * @author Stan Seiferth
         */
        private void removeLabel(int line)
        {
            int last = waiting[line].Count - 1;
            for (int i = 0; i < last; i++) //move labels up
            {
                    waiting[line][i].Text = waiting[line][i+1].Text; //move labels up in line
                
            }

            waiting[line][last].Visible = false;            //remove last position in line
            this.Controls.Remove(waiting[line][last]);      //
            waiting[line][last].Dispose();                  //
            waiting[line].Remove(waiting[line][last]);      //
        }//end removeLabel(int):void


        /**
         * Puts registrant in line upon arrival event<br>
         *
         * <hr>
         * Date created: Nov 12, 2013<br>
         * Date last modified: Nov 16, 2013<br>
         * <hr>
         * @author Stan Seiferth
         */
        private void getInLine(int line)
        {
            int time = 0;
            
            string strTime = string.Empty;
            double regTime = stack.Pop();
            int inLine = events.GetNumInLine(line);
            int x = 65 + (line * 88);
            int y = 65 + (20 * inLine);
            Label temp;
            temp = new Label();
            temp.Location = new Point(x, y);
            temp.Font = new Font("Microsoft Sans Serif", 10);
            temp.Size = new Size(45, 20);
            time = (int)regTime;
            regTime -= time;
            regTime = Math.Round((regTime * 60), 0);
            if (regTime >= 60)
            {
                regTime -= 60;
                time++;
            }
            if (regTime < 10)
                strTime += time + ":0" + regTime;
            else
                strTime += time + ":" + regTime;
            temp.Text = strTime;
            temp.TabStop = false;
            waiting[line].Add(temp);
            try
            {

                if (inLine > expand)
                {
                    this.Height += 20;
                    moveDown += 20;
                    labLong.Location = new Point(7, moveDown);
                    expand++;
                    longest++;
                }
                else if (inLine > longest)
                {
                    longest++;
                }
                this.Controls.Add(waiting[line][waiting[line].Count - 1]); //add label control to form
            }
            catch
            {
               // MessageBox.Show("Please keep window active when program is running.");
            }

        }//end getInLine()


        /**
         * closes form upon exit click <br>
         *
         * <hr>
         * Date created: Nov 12, 2013<br>
         * Date last modified: Nov 16, 2013<br>
         * <hr>
         * @author Stan Seiferth
         */
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

       

        
    }
}
