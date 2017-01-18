/**
 * ---------------------------------------------------------------------------
 * File name: Events.cs<br/>
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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _2210_201_SeiferthStan_Project4
{
    class Events
    {

        private List<Registrant> registrants;
        public double EventTimeSpan { get; set; }
        public double OpenTime { get; set; }
        public double CloseTime { get; set; }
        private int placeHolder = 0;
        private int grab = 0;
        private Queue[] line;
        public double currentRegTime { get; private set; }
        private int people;
        private double[] whenDone;

        /**
         * default constructor for Events class<br>
         *
         * <hr>
         * Date created: Nov 12, 2013<br>
         * Date last modified: Nov 16, 2013<br>
         * <hr>
         * @author Stan Seiferth
         */
        public Events()
        {

        }//end Events()

        /**
         * determines how long it takes to register for each registrant<br>
         *
         * <hr>
         * Date created: Nov 12, 2013<br>
         * Date last modified: Nov 16, 2013<br>
         * <hr>
         * @author Stan Seiferth
         */
        public void FindTimeInLine(int people, double expected, double shortest)
        {
            double arrival;
            double observed;
            double xSqr;
            double result = 0;
            Registrant temp;
            Random time = new Random();
            this.people = people;
            //StreamWriter sw = new StreamWriter("excel.csv");
            registrants = new List<Registrant>();

            for (int i = 0; i < people; i++)
            {
                for (int j = 0; j < 256; j++)
                {
                    observed = time.Next(15, 200);  //cast statistical mojo
                    observed = observed / 10.0;     //make random numbers follow chi square distribution
                    xSqr = observed - 4.25;
                    result += (xSqr * xSqr) / 4.25;
                }
                result = (result / 255) - (12.34 + (4.25 - expected)); //adjust average to match user input
                if (result < shortest)  //if less than shortest time make shortest time
                    result = shortest;
                
                arrival = ArrEvent(time.Next(1000));   //find arrival time for individual
                arrival = Math.Round((arrival * EventTimeSpan) + OpenTime, 2);
                
                temp = new Registrant();    //make a temporary registrant to hold values
                temp.TimeInLine = result;   //assign time needed to register 
                temp.ArrTime = arrival;     //assign time arrived to register
                registrants.Add(temp);      // add that temporary to list
                
                //sw.Write(result + ",");Math.Round(, 2)
                result = 0;
                
            }
            registrants.Sort();
           //SinkSort(registrants);
          
        }

        /**
         * takes current time of timer and determines if anyone else has arrived<br>
         *
         * <hr>
         * Date created: Nov 12, 2013<br>
         * Date last modified: Nov 16, 2013<br>
         * <hr>
         * @author Stan Seiferth
         */
        public int FeedTimer(double currentTime)
        {
            int result = 0;
            bool found = true;

            while (found)
            {
                if (placeHolder < people)
                {
                    if (registrants[placeHolder].ArrTime <= currentTime)
                    {
                        placeHolder++;
                        result++;
                        found = true;
                    }
                    else
                        found = false;
                    
                }
                else
                    return -1;
            }

            return result;
        }//FeedTimer

        //public double ManageEvents(int grab)
        //{
        //    double regTime = 0.0;
        //    regTime = registrants[grab].ArrTime;

        //    return regTime;
        //}//end ManageEvents()

        /**
         * finds the time the each registrant will arrive<br>
         *
         * <hr>
         * Date created: Nov 12, 2013<br>
         * Date last modified: Nov 16, 2013<br>
         * <hr>
         * @author Stan Seiferth
         */
        public double ArrEvent (int random)
        {
            double result;
            result = (random / 1000.0);
            return result;

        }//end ArrEvent(int):double

        /**
         * loads registrants into queue upon their arrival<br>
         *
         * <hr>
         * Date created: Nov 12, 2013<br>
         * Date last modified: Nov 16, 2013<br>
         * <hr>
         * @author Stan Seiferth
         */
        public int LoadQueues(double currentTime)
        {
            int shortest = 2000;
            int num = -1;
            int windows = line.Length;
           
            
                for (int j = 0; j < windows; j++)
                {

                    if (line[j].Count < shortest)
                    {
                        shortest = line[j].Count;
                        num = j;
                    }
                }
          currentRegTime = registrants[grab].TimeInLine;
          line[num].Enqueue(registrants[grab]);
          if (line[num].Count == 1)
              whenDone[num] = currentTime + (currentRegTime / 100);
          grab++;

                return num;
            
        }//end loadQueues():int

        /**
         * determines if registrant has finished registering<br>
         *
         * <hr>
         * Date created: Nov 12, 2013<br>
         * Date last modified: Nov 16, 2013<br>
         * <hr>
         * @author Stan Seiferth
         */
        public int CheckDone(int i, double currentTime)
        {
            int result = -1;
            Registrant temp = new Registrant();
            if ((whenDone[i] <= currentTime) && (whenDone[i] > 0))
            {
                line[i].Dequeue();
                whenDone[i] = 0;
                if (line[i].Count > 0)
                {
                    temp = line[i].Peek() as Registrant;
                    whenDone[i] = (temp.ArrTime / 100) + currentTime;
                }
                result = i;
                return result;
            }
            return result;
        }//end CheckDone(double):int

        /**
         * initialize a queue for each register window to hold people in line<br>
         *
         * <hr>
         * Date created: Nov 12, 2013<br>
         * Date last modified: Nov 16, 2013<br>
         * <hr>
         * @author Stan Seiferth
         */
        public void InitializeQueues(int num)
        {
           
            line = new Queue[num];
            whenDone = new double[num];
            for (int i = 0; i < num; i++)
            {
                line[i] = new Queue();
                
            }
            
        }//end initializeQueues(int)

        /**
         * returns the number of people in a particular line<br>
         *
         * <hr>
         * Date created: Nov 12, 2013<br>
         * Date last modified: Nov 16, 2013<br>
         * <hr>
         * @author Stan Seiferth
         */
        public int GetNumInLine(int y)
        {
            int result = line[y].Count;
            return result;
        }
    }
}
