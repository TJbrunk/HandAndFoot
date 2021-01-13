using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandAndFoot.Classes
{
    public static class PointValues
    {
        public static int GoOutBonus = 100;
        public static int CleanBooks = 500;
        public static int DirtyBooks = 300;
        public static int CardDrawBonus = 100;


        /// <summary>
        /// NEGATIVE point value for Black Threes
        /// </summary>
        public static int BlackThrees = -100;
        /// <summary>
        /// NEGATIVE point value for Red Threes
        /// </summary>
        public static int RedThrees = -300;
    }
}
