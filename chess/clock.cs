using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chess
{
    class clock
    {
        private int hour;
        private int min;
        private int sec;
        public clock(int h, int m, int s)
        {
            hour = h;
            min = m;
            sec = s;
        }
        public clock()
        {
            hour = 0;
            min = 0;
            sec = 0;
        }
        public int Hour
        {
            get
            {
                return hour;
            }
            set
            {
                hour = value;
            }
        }
        public int Min
        {
            get
            {
                return min;
            }
            set
            {
                min = value;
            }
        }
        public int Sec
        {
            get
            {
                return sec;

            }
            set
            {
                sec = value;
            }
        }
    }
}
