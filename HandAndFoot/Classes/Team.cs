using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace HandAndFoot.Classes
{
    [TypeConverter(typeof(StringToTeamTypeConverter))]
    public class Team
    {
        public string Name { get; set; } = string.Empty;
        public string StorageId { get; set; }
        public List<RoundScore> RoundScores { get; set; } = new List<RoundScore>();// List of 'scores' for each round
        public int CardDrawBonusCount { get; set; }
        public int TotalScore
        {
            get
            {
                var t = 0;
                foreach(var s in this.RoundScores)
                {
                    t += s.CalculateTotal();
                }
                return t;
            }
        }

        public Team() 
        {
        }

        public int AddRoundBookScore(int round, RoundScore score)
        {
            if(round == this.RoundScores.Count && this.RoundScores.Count < 4)
            {
                this.RoundScores.Add(new RoundScore());
            }
            Console.WriteLine($"Adding Books score for round {round}");
            Console.WriteLine($"Total ROunds played: {this.RoundScores.Count}");
            this.RoundScores[round].BlackThrees = score.BlackThrees;
            this.RoundScores[round].RedThrees = score.RedThrees;
            this.RoundScores[round].CleanBooks = score.CleanBooks;
            this.RoundScores[round].DirtyBooks = score.DirtyBooks;
            this.RoundScores[round].WentOut = score.WentOut;
            return this.RoundScores[round].CalculateTotal();
        }

        public int AddRoundCardScore(int round, RoundScore score)
        {
            if(round == this.RoundScores.Count && this.RoundScores.Count < 4)
            {
                Console.WriteLine("Adding new empty round to score list");
                this.RoundScores.Add(new RoundScore());
            }

            Console.WriteLine($"Adding card score for round {round}");
            this.RoundScores[round].CardsTotalAgainst = score.CardsTotalAgainst;
            this.RoundScores[round].CardsTotalFor = score.CardsTotalFor;
            var s = this.RoundScores[round].CalculateTotal();
            return s;
        }

        public int AddCardDrawBonus(int round)
        {
            if(round == this.RoundScores.Count && this.RoundScores.Count < 4)
            {
                this.RoundScores.Add(new RoundScore());
            }

            this.RoundScores[round].CardDrawBonus += 1;
            return this.RoundScores[round].CalculateTotal();
        }

        public override string ToString() =>
            JsonSerializer.Serialize(this);
    }


    public class StringToTeamTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                string[] s = ((string)value).Split(',');
                return new Team();
            }
            return base.ConvertFrom(context, culture, value);
        }
    }
}
