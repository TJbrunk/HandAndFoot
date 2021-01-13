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
        public List<RoundScore> RoundScores { get; set; } = new List<RoundScore>(); // List of 'scores' for each round
        public uint CardDrawBonusCount { get; set; }
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

        public Team() {}

        public int AddRoundScore(RoundScore score)
        {
            this.RoundScores.Add(score);
            var s = score.CalculateTotal();
            return s;
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
            if(value is string)
            {
                string[] s = ((string)value).Split(',');
                return new Team();
            }
            return base.ConvertFrom(context, culture, value);
        }
    }
}
