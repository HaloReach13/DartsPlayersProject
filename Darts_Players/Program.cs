using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;

namespace Darts_Players
{
    public class Program
    {
        static List<DartsPlayers> players = new List<DartsPlayers>();
        static void ReadDartsPlayers(string fileName)
        {
            foreach (var item in File.ReadLines(fileName))
            {
                var parts = item.Split(';');

                var birthdateParts = parts[3].Split('.');
                var birthdate = new Birthdate(int.Parse(birthdateParts[0]), int.Parse(birthdateParts[1]), int.Parse(birthdateParts[2]));

                var player = new DartsPlayers(int.Parse(parts[0]), parts[1], parts[2], birthdate, int.Parse(parts[4]), int.Parse(parts[5]));

                players.Add(player);
            }
        }

        static void WriteDartsPlayers() 
        {
            foreach (var player in players)
            {
                Console.WriteLine($"{player.Id};{player.Name};{player.Nationality};{player.Birthdate};{player.WorldTitles};{player.DartsWeight}");
            }
        }

        static int CountNationality(List<DartsPlayers> players, string nationality)
        {
            int count = 0;
            foreach (var item in players) 
            {
                if (item.Nationality == nationality)
                {
                    count++;
                }
            }

            return count;
        }

        static int CountDartsWeight(List<DartsPlayers> players, int weight)
        {
            int count = 0;
            foreach (var item in players) 
            {
                if (item.DartsWeight != weight)
                {
                    count++;
                }
            }

            return count;
        }

        static int OldestDartsPlayer(List<DartsPlayers> players)
        {
            int oldest = 0;
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].Birthdate.Year < players[oldest].Birthdate.Year)
                {
                    oldest = i;
                }
            }

            return oldest;
        }

        static int NationalityAndWorldTitleCount(List<DartsPlayers> players, string nationality, int wordTitles)
        {
            int count = 0;
            foreach (var item in players)
            {
                if (item.Nationality == nationality && item.WorldTitles == wordTitles)
                {
                    count++;
                }
            }

            return count;
        }

        static Dictionary<int, int> NationalityAndDartsWeight(List<DartsPlayers> players, string nationality)
        {
            Dictionary<int, int> dartsWeightCount = new Dictionary<int, int>();

            foreach (var item in players)
            {
                if (item.Nationality.Contains(nationality))
                {
                    if (dartsWeightCount.ContainsKey(item.DartsWeight))
                    {
                        dartsWeightCount[item.DartsWeight]++;
                    }
                    else
                    {
                        dartsWeightCount[item.DartsWeight] = 1;
                    }
                }
            }

            return dartsWeightCount;
        }

        static Dictionary<int, HashSet<string>> GroupByWorldTitlesAndNationality(List<DartsPlayers> players)
        {
            Dictionary<int, HashSet<string>> result = new Dictionary<int, HashSet<string>>();

            foreach (var player in players)
            {
                if (!result.ContainsKey(player.WorldTitles))
                {
                    result[player.WorldTitles] = new HashSet<string>();
                }
                result[player.WorldTitles].Add(player.Nationality);
            }

            return result;
        }

        static void Main(string[] args)
        {
            string filename = "darts_players.txt";
            ReadDartsPlayers(filename);
            Console.WriteLine("--------------------------------------------------------------------");
            WriteDartsPlayers();
            Console.WriteLine("--------------------------------------------------------------------");

            Console.WriteLine($"Number of lines in the file: {players.Count}\n");

            Console.Write("Choose a nationality: ");
            string nationality = Console.ReadLine();
            int nationalityCount = CountNationality(players, nationality);
            if (nationalityCount == 0)
            {
                Console.WriteLine("There are no players of this nationality in this txt.\n");
            }
            else
            {
                Console.WriteLine($"Number of players with this nationality: {nationalityCount}\n");
            }

            Console.Write("Enter a darts weight: ");
            int weight = int.Parse(Console.ReadLine());
            int dartsWeightCount = CountDartsWeight(players, weight);
            if (dartsWeightCount == 0)
            {
                Console.WriteLine("There are no players with this darts weight in this txt.\n");
            }
            else
            {
                Console.WriteLine($"The dart weight you entered is not displayed this many times: {dartsWeightCount}\n");
            }

            int oldest = OldestDartsPlayer(players);
            Console.WriteLine("The oldest player is:\n");
            Console.WriteLine($"Name: {players[oldest].Name} -> ID: {players[oldest].Id}");
            Console.WriteLine($"Nationality: {players[oldest].Nationality}");
            Console.WriteLine($"Birthdate: {players[oldest].Birthdate.Year}.{players[oldest].Birthdate.Month}.{players[oldest].Birthdate.Day}");
            Console.WriteLine($"World titles: {players[oldest].WorldTitles}");
            Console.WriteLine($"Darts weight: {players[oldest].DartsWeight}\n");

            Console.Write("Choose a nationality: ");
            nationality = Console.ReadLine();
            Console.Write("Choose a world title: ");
            int worldTitles = int.Parse(Console.ReadLine());
            int nationalityAndWorldTitlesCount = NationalityAndWorldTitleCount(players, nationality, worldTitles);
            if (nationalityAndWorldTitlesCount == 0)
            {
                Console.WriteLine("There are no players with this nationality and world title in this txt.\n");
            }
            else
            {
                Console.WriteLine($"Players with this nationality and world title: {nationalityAndWorldTitlesCount}\n");
            }

            Console.Write("Choose a nationality for darts weight analysis: ");
            nationality = Console.ReadLine();
            var weightCount = NationalityAndDartsWeight(players, nationality);
            foreach (var kvp in weightCount)
            {
                Console.WriteLine($"Darts weight {kvp.Key}: {kvp.Value} players");
            }

            var groupedData = GroupByWorldTitlesAndNationality(players);
            foreach (var kvp in groupedData)
            {
                Console.WriteLine($"World titles: {kvp.Key}, Nationalities: {string.Join(", ", kvp.Value)}");
            }

            Console.ReadKey();
        }
    }
}
