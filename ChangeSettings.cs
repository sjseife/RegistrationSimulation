/**
 * ---------------------------------------------------------------------------
 * File name: ChangeSettings.cs<br/>
 * Project name: Project 4<br/>
 * ---------------------------------------------------------------------------
 * Creator's name and email: Stan Seiferth, zsjs19@goldmail.etsu.edu<br/>
 * Course:  CSCI 2210-201<br/>
 * Creation Date: Nov 12, 2013<br/>
 * Date of Last Modification: Nov 16, 2013
 * ---------------------------------------------------------------------------
 */
using System;
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
    public partial class ChangeSettings : Form
    {
        private string defReg = "1000";
        private string defWind = "2";
        private string defOpen = "7";
        private string defOpenMin = "00";
        private string defOpenAMPM = "AM";
        private string defClose = "5";
        private string defCloseMin = "00";
        private string defCloseAMPM = "PM";
        private string defAVGMin = "4";
        private string defAVGSec = "15";
        private string defShortMin = "1";
        private string defShortSec = "30";
        private bool modified = false;

        public string Reg {get; private set;}
        public string Windows {  get; private set; }
        public string OpenTime {  get; private set; }
        public string OpenMin { get; private set; }
        public string OpenAMPM {  get; private set; }
        public string CloseTime {  get; private set; }
        public string CloseMin { get; private set; }
        public string CloseAMPM {  get; private set; }
        public string AVGMin {  get; private set; }
        public string AVGSec {  get; private set; }
        public string ShortMin {  get; private set; }
        public string ShortSec {  get; private set; }

        /**
         * initialize form<br>
         *
         * <hr>
         * Date created: Nov 12, 2013<br>
         * Date last modified: Nov 16, 2013<br>
         * <hr>
         * @author Stan Seiferth
         */
        public ChangeSettings()
        {
            InitializeComponent();
            
        }

        /**
         * sets values to user input after pressing OK button<br>
         *
         * <hr>
         * Date created: Nov 12, 2013<br>
         * Date last modified: Nov 16, 2013<br>
         * <hr>
         * @author Stan Seiferth
         */
        private void OKButton_Click(object sender, EventArgs e)
        {
            Reg = RegistrantsBox.Text;
            Windows = WindowsBox.Text;
            OpenTime = OpenBox.Text;
            OpenMin = Open2Box.Text;
            OpenAMPM = (string)OpenCombo.SelectedItem;
            CloseTime = CloseBox.Text;
            CloseMin = Close2Box.Text;
            CloseAMPM = (string)CloseCombo.SelectedItem;
            AVGMin = AVGMinBox.Text;
            AVGSec = AVGSecBox.Text;
            ShortMin = ShortMinBox.Text;
            ShortSec = ShortSecBox.Text;
            modified = true;


            Hide();
        }

        /**
         * resets values to their default values when reset button is pressed<br>
         *
         * <hr>
         * Date created: Nov 12, 2013<br>
         * Date last modified: Nov 16, 2013<br>
         * <hr>
         * @author Stan Seiferth
         */
        private void ResetButton_Click(object sender, EventArgs e)
        {
            RegistrantsBox.Text = defReg;
            WindowsBox.Text = defWind;
            OpenBox.Text = defOpen;
            Open2Box.Text = defOpenMin;
            OpenCombo.SelectedItem = defOpenAMPM;
            CloseBox.Text = defClose;
            Close2Box.Text = defCloseMin;
            CloseCombo.SelectedItem = defCloseAMPM;
            AVGMinBox.Text = defAVGMin;
            AVGSecBox.Text = defAVGSec;
            ShortMinBox.Text = defShortMin;
            ShortSecBox.Text = defShortSec;

        }


        /**
         * cancels user's input and restores previous values before changes were made
         * and hides form <br>
         * <hr>
         * Date created: Nov 12, 2013<br>
         * Date last modified: Nov 16, 2013<br>
         * <hr>
         * @author Stan Seiferth
         */
        private void CancelBox_Click(object sender, EventArgs e)
        {
            if (modified)
            {
                RegistrantsBox.Text = Reg;
                WindowsBox.Text = Windows;
                OpenBox.Text = OpenTime;
                Open2Box.Text = OpenMin;
                OpenCombo.SelectedItem = OpenAMPM;
                CloseBox.Text = CloseTime;
                Close2Box.Text = CloseMin;
                CloseCombo.SelectedItem = CloseAMPM;
                AVGMinBox.Text = AVGMin;
                AVGSecBox.Text = AVGSec;
                ShortMinBox.Text = ShortMin;
                ShortSecBox.Text = ShortSec;
            }
            else
            {
                RegistrantsBox.Text = defReg;
                WindowsBox.Text = defWind;
                OpenBox.Text = defOpen;
                Open2Box.Text = defOpenMin;
                OpenCombo.SelectedItem = defOpenAMPM;
                CloseBox.Text = defClose;
                Close2Box.Text = defCloseMin;
                CloseCombo.SelectedItem = defCloseAMPM;
                AVGMinBox.Text = defAVGMin;
                AVGSecBox.Text = defAVGSec;
                ShortMinBox.Text = defShortMin;
                ShortSecBox.Text = defShortSec;
            }
            Hide();
        }
    }
}
