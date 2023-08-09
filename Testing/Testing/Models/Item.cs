using SQLite;
using System;

namespace Testing.Models
{
    public class Item
    {
        [PrimaryKey]
        public string Id { get; set; }

        public string Name { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }

        public DateTime TimeStamp { get; set; }
        public int Score { get; set; }

        public int Durations { get; set; }
        public bool Addition { get; set; }
        public bool Deduction { get; set; }
        public bool Multiplication { get; set; }
        public bool Division { get; set; }

        public int Addition1stLowerLimit { get; set; }
        public int Addition1stUpperLimit { get; set; }

        public int Addition2ndLowerLimit { get; set; }
        public int Addition2ndUpperLimit { get; set; }

        public int Multiplication1stLowerLimit { get; set; }
        public int Multiplication1stUpperLimit { get; set; }

        public int Multiplication2ndLowerLimit { get; set; }
        public int Multiplication2ndUpperLimit { get; set; }


    }
}