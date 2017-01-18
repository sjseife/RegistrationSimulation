/**
 * ---------------------------------------------------------------------------
 * File name: Driver.cs<br/>
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
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2210_201_SeiferthStan_Project4
{
    
    static class Driver
    {
        /**
         * Main entry point for application<br>
         *
         * <hr>
         * Date created: Nov 12, 2013<br>
         * Date last modified: Nov 12, 2013<br>
         * <hr>
         * @author Stan Seiferth
         */
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Simulation());
        }
    }
}
