using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darts_Players
{
    public class DartsPlayers
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Nationality { get; set; }
        public Birthdate Birthdate { get; set; }
        public int WorldTitles { get; set; }
        public int DartsWeight { get; set; }


        public DartsPlayers(int id, string name, string nationality, Birthdate playerBirthdate, int worldTitles, int dartsWeight)
        {
            Id = id;
            Name = name;
            Nationality = nationality;
            Birthdate = playerBirthdate;
            WorldTitles = worldTitles;
            DartsWeight = dartsWeight;
        }

        public override string ToString()
        {
            return $"{Id};{Name};{Nationality};{Birthdate};{WorldTitles};{DartsWeight}";
        }
    }
}
