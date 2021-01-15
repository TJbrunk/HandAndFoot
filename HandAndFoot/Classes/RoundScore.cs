using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandAndFoot.Classes
{
    public class RoundScore
    {
        public bool WentOut { get; set; } // 100pt bonus
        public int CleanBooks { get; set; }
        public int DirtyBooks { get; set; }
        public int CardsTotalFor { get; set; }
        public int CardDrawBonus { get; set; }

        // Negative Points:
        public int RedThrees { get; set; }
        public int BlackThrees { get; set; }
        public int CardsTotalAgainst { get; set; }


        public int CalculateTotal()
        {
            var total = this.WentOut ? PointValues.GoOutBonus : 0;
            total += this.CleanBooks * PointValues.CleanBooks;
            total += this.DirtyBooks * PointValues.DirtyBooks;
            //Console.WriteLine($"Card draw bonus of: {this.CardDrawBonus}");
            total += this.CardDrawBonus * PointValues.CardDrawBonus;
            total += CardsTotalFor;

            total += this.RedThrees * PointValues.RedThrees;
            total += this.BlackThrees * PointValues.BlackThrees;
            total -= CardsTotalAgainst;

            return total;
        }
    }
}
