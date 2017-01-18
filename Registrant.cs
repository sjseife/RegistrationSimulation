/**
 * ---------------------------------------------------------------------------
 * File name: Registrant.cs<br/>
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
using System.Text;
using System.Threading.Tasks;

namespace _2210_201_SeiferthStan_Project4
{

    class Registrant : IComparable
    {
        public double TimeInLine { get; set; }
        public double ArrTime { get; set; }

        /**
         * default constructor for registrant class<br>
         *
         * <hr>
         * Date created: Nov 12, 2013<br>
         * Date last modified: Nov 16, 2013<br>
         * <hr>
         * @author Stan Seiferth
         */
        public Registrant()
        {

        }//end Registrant


        /**
         * Provides a means to sort registrants by arrival time<br>
         *
         * <hr>
         * Date created: Nov 12, 2013<br>
         * Date last modified: Nov 16, 2013<br>
         * <hr>
         * @author Stan Seiferth
         */
        public int CompareTo(object obj)
        {
            if (obj == null) return -1;
            Registrant reg = obj as Registrant;
            if (reg == null) return -1;
            double inReg = reg.ArrTime;
            
            double thisReg = this.ArrTime;
           
            if (thisReg > inReg) return 1;
            if (thisReg < inReg) return -1;
            return 0;
        }//provides a means to sort registrants

    }
}
