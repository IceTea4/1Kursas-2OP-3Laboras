using System.Text; //Library used for text encoding

namespace U1_24KompiuterinisZaidimas
{
    /// <summary>
    /// A class that performs scans and prints
    /// </summary>
    public class InputOutput
    {
        /// <summary>
        /// A method that reads heroes and their data from the files
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static HeroRegister ReadHeroes(string fileName)
        {
            //Reads all lines from a file in UTF-8 encoding
            string[] Lines = File.ReadAllLines(fileName, Encoding.UTF8);

            string race = Lines[0];
            string city = Lines[1];

            HeroRegister heroes = new HeroRegister(race, city);

            //Parses each line
            for (int i = 2; i < Lines.Length; i++)
            {
                string[] values = Lines[i].Split(";");
                string name = values[0];
                int number = int.Parse(values[1]);
                int health = int.Parse(values[2]);
                int mana = int.Parse(values[3]);
                int damage = int.Parse(values[4]);
                int defend = int.Parse(values[5]);
                int strength = int.Parse(values[6]);
                int speed = int.Parse(values[7]);
                int intellect = int.Parse(values[8]);
                string power = values[9];

                //Creates new hero object
                Hero hero = new Hero(name, number, health, mana,
                    damage, defend, strength, speed, intellect, power);

                //Adds the created Hero object to the list of heroes
                heroes.AllHeroes.Add(hero);
            }

            return heroes;
        }

        /// <summary>
        /// A method that prints all heroes and their data to the console
        /// </summary>
        /// <param name="register"></param>
        public static void PrintHeroes(HeroRegister register)
        {
            //A table is created to store the data
            Console.WriteLine($"Rasė: {register.race}; " +
                $"Miestas: {register.city}");
            Console.WriteLine(new string('-', 128));
            Console.WriteLine("| {0,-12} | {1,-5} | {2,-14} | " +
                "{3,-4} | {4,-12} | {5,-14} | {6,-4} | {7,-8} | " +
                "{8,-10} | {9,-14} |", "Vardas", "Klasė",
                "Gyvybės taškai", "Mana", "Žalos taškai",
                "Gynybos taškai", "Jėga", "Vikrumas", "Intelektas",
                "Ypatinga galia");
            Console.WriteLine(new string('-', 128));

            for (int i = 0; i < register.AllHeroes.Count; i++)
            {
                Hero hero = register.AllHeroes.Get(i);

                Console.WriteLine(hero.ToString());
            }

            Console.WriteLine(new string('-', 128));

            Console.WriteLine();
        }

        /// <summary>
        /// A method that prints all heroes and their data to a txt file
        /// The data is stored in a table
        /// </summary>
        /// <param name="fileNameTxt"></param>
        /// <param name="registers"></param>
        public static void PrintHeroesToTxt(string fileNameTxt,
            HeroRegister[] registers)
        {
            File.AppendAllText(
                fileNameTxt,
                "Registro informacija:\r\n",
                Encoding.UTF8);

            foreach (HeroRegister register in registers)
            {
                List<string> lines = new List<string>();

                lines.Add(String.Format("Rasė: {0}; Miestas: {1}",
                    register.race, register.city));
                lines.Add(new string('-', 128));
                lines.Add(String.Format("| {0,-12} | {1,-5} | {2,-14} | " +
                    "{3,-4} | {4,-12} | {5,-14} | {6,-4} | {7,-8} | " +
                    "{8,-10} | {9,-14} |", "Vardas", "Klasė",
                    "Gyvybės taškai", "Mana", "Žalos taškai", "Gynybos taškai",
                    "Jėga", "Vikrumas", "Intelektas", "Ypatinga galia"));
                lines.Add(new string('-', 128));

                for (int i = 0; i < register.AllHeroes.Count; i++)
                {
                    Hero hero = register.AllHeroes.Get(i);

                    lines.Add(hero.ToString());
                }

                lines.Add(new string('-', 128));

                lines.Add("");

                //Prints on each line of the file
                File.AppendAllLines(fileNameTxt, lines, Encoding.UTF8);
            }
        }

        /// <summary>
        /// A method that passes one heroes register to the printing method
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="registers"></param>
        public static void PrintHealthiest(string fileName,
            List<HeroRegister> registers)
        {
            foreach (HeroRegister register in registers)
            {
                if (register.AllHeroes.Count > 0)
                {
                    PrintAllHeroesToCSV(fileName, register);
                }
                else
                {
                    List<string> lines = new List<string>();
                    lines.Add("Nėra tokių herojų");
                    File.WriteAllLines("Herojai.csv", lines, Encoding.UTF8);
                }
                
            }
        }

        /// <summary>
        /// A method that passes one heroes register to the printing method
        /// </summary>
        /// <param name="registers"></param>
        public static void PrintStrongest(List<HeroRegister> registers)
        {
            Console.WriteLine("Stipriausi herojai:");
            foreach (HeroRegister register in registers)
            {
                PrintHeroes(register);
            }
        }

        /// <summary>
        /// A method that prints all heroes and their data to a csv file
        /// The data is stored in a table
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="heroes"></param>
        public static void PrintAllHeroesToCSV(string fileName,
            HeroRegister heroes)
        {
            List<string> lines = new List<string>();

            lines.Add(String.Format($"Rasė: {heroes.race}; " +
                $"Miestas: {heroes.city}"));
            lines.Add(String.Format($"{"Vardas"}; {"Klasė"}; " +
                $"{"Gyvybės taškai"}; {"Mana"}; {"Žalos taškai"}; " +
                $"{"Gynybos taškai"}; {"Jėga"}; {"Vikrumas"}; " +
                $"{"Intelektas"}; {"Ypatinga galia"}"));

            for (int i = 0; i < heroes.AllHeroes.Count; i++)
            {
                Hero hero = heroes.AllHeroes.MoreHealth().Get(i);

                lines.Add(String.Format($"{hero.name}; {hero.number}; " +
                    $"{hero.health}; {hero.mana}; {hero.damage}; " +
                    $"{hero.defend}; {hero.strength}; {hero.speed}; " +
                    $"{hero.intellect}; {hero.power}"));
            }

            lines.Add("");

            //Prints on each line of the file
            File.AppendAllLines(fileName, lines, Encoding.UTF8);
        }

        /// <summary>
        /// The method prints hero classes (without duplicates) to console
        /// </summary>
        /// <param name="numbers"></param>
        public static void PrintNumbers(List<int> numbers)
        {
            Console.WriteLine("Herojų klasės:");

            for (int i = 0; i < numbers.Count; i++)
            {
                Console.WriteLine(String.Format("{0}", numbers[i]));
            }
        }

        /// <summary>
        /// The method prints heroes missing classes to csv file
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="numbers"></param>
        /// <param name="registers"></param>
        public static void PrintMissingNumbers(
            string fileName,
            List<int[]> numbers,
            HeroRegister[] registers)
        {
            List<string> lines = new List<string>();

            lines.Add("Trūkstamos klasės:");

            for (int i = 0; i < registers.Count(); i++)
            {
                lines.Add(registers[i].race + ":");

                if (numbers[i].Length == 0)
                {
                    lines.Add("VISI");
                }
                else
                {
                    foreach (int clas in numbers[i])
                    {
                        lines.Add(String.Format("{0}", clas));
                    }
                }
            }

            ///Prints to all lines of the file
            File.WriteAllLines(fileName, lines, Encoding.UTF8);
        }
    }
}

