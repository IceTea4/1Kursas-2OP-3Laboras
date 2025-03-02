using System.ComponentModel;
using System.Text;

namespace U1_24KompiuterinisZaidimas
{
    /*U3_24. Kompiuterinis žaidimas.
     * Sugrupavote herojus pagal dvi rases, ir surašėte jų duomenis į 
     * skirtingus failus. Duomenų formatas dabar toks: pirmoje 
     * eilutėje – rasės pavadinimas. Antroje – pradinis miestas. Toliau 
     * pateikta informacija tokiu pačiu formatu kaip L1 užduotyje, tik 
     * nebėra rasės stulpelio.
        • Raskite kiekvienos rasės stipriausią herojų: herojus stiprumą rodo
    gyvybės ir gynybos taškų suma sumažinta žalos taškais. Ekrane
    atspausdinkite visus herojaus duomenis ir jo rasę.
        • Sudarykite visų herojų klasių sąrašą, klasių pavadinimus
    atspausdinkite ekrane.
        • Raskite, kokių klasių herojų „trūksta“ kiekvienai rasei. Į
    failą „Trūkstami.csv“ įrašykite kiekvienos rasės pavadinimą, ir
    trūkstamų klasių sąrašą. Jei rasė turi bent po vieną kiekvienos klasės
    atstovą, parašykite žodį „VISI“.
        • Sudarykite herojų, kurie turi gyvybės taškų daugiau nei gynybos
    taškų, sąrašą. Surikiuokite herojus pagal gyvybės taškus, gynybos taškus
    ir vardus. Rezultatus įrašykite į failą „Herojai.csv“.
    */

    class Program
    {
        static void Main(string[] args)
        {
            //Heroes and their data are read from the “Troliai.csv” and
            //"Elfai.csv" files and then are placed into the register
            HeroRegister registerT =
                InputOutput.ReadHeroes(@"../../../Troliai.csv");
            HeroRegister registerE =
                InputOutput.ReadHeroes(@"../../../Elfai.csv");

            //Deletes file so there would be no text from the past
            File.Delete("Duomenys.txt");
            File.Delete("Herojai.csv");

            HeroRegister[] registers
                = new HeroRegister[] { registerT, registerE };

            //All heroes and their data in the table are printed to
            //txt file
            InputOutput.PrintHeroesToTxt("Duomenys.txt", registers);

            //All heroes and their data are printed to the console
            //in a table
            Console.WriteLine("Registro informacija:");
            InputOutput.PrintHeroes(registerT);
            InputOutput.PrintHeroes(registerE);

            //First task rezult: Searches in which race there is the
            //strongest hero and prints it, but if there is more than
            //one prints others too
            List<HeroRegister> strongest = CollectStrongest(registers);
            InputOutput.PrintStrongest(strongest);

            //Second task result (all different heroes classes) is
            //printed to the console
            List<int> classes = CollectClasses(registers);
            InputOutput.PrintNumbers(classes);

            //Third task result (all missing each race classes) are printed
            //to the "Trukstami.csv" file
            List<int[]> missingClasses = MissingClasses(registers);
            InputOutput.PrintMissingNumbers("Trukstami.csv",
                missingClasses, registers);

            //Forth task: selected and sorted heroes are printed to
            //"Herojai.csv" file
            List<HeroRegister> healthiest = CollectHealthiest(registers);
            InputOutput.PrintHealthiest("Herojai.csv", healthiest);
        }

        /// <summary>
        /// Method collects all different classes
        /// </summary>
        /// <param name="registers"></param>
        /// <returns></returns>
        public static List<int> CollectClasses(HeroRegister[] registers)
        {
            List<int> results = new List<int>();

            foreach (HeroRegister register in registers)
            {
                List<int> registerClasses = register.AllHeroes.FindClasses();

                foreach (int clas in registerClasses)
                {
                    if (!results.Contains(clas))
                    {
                        results.Add(clas);
                    }
                }
            }

            results.Sort();

            return results;
        }

        /// <summary>
        /// Method which finds all missing classes and puts them in one list
        /// </summary>
        /// <param name="registers"></param>
        /// <returns></returns>
        public static List<int[]> MissingClasses(HeroRegister[] registers)
        {
            List<int[]> result = new List<int[]>();
            List<int> allClasses = CollectClasses(registers);

            foreach (HeroRegister register in registers)
            {
                result.Add(allClasses.Except(
                    register.AllHeroes.FindClasses()).ToArray()
                    );
            }

            return result;
        }

        /// <summary>
        /// Method collects all the strongest heroes into one list
        /// </summary>
        /// <param name="registers"></param>
        /// <returns></returns>
        public static List<HeroRegister> CollectStrongest(
            HeroRegister[] registers)
        {
            HeroContainer[] strongest = new HeroContainer[registers.Length];
            double strength = 0;

            for (int i = 0; i < registers.Length; i++)
            {
                strongest[i] = registers[i].AllHeroes.FindAllStrongest();

                if (strongest[i].Get(0).GetStrength() > strength)
                {
                    strength = strongest[i].Get(0).GetStrength();
                }
            }

            return CollectStrongest(registers, strongest, strength);
        }

        /// <summary>
        /// Mthod collects strongest heroes into the List
        /// </summary>
        /// <param name="registers"></param>
        /// <param name="strongest"></param>
        /// <param name="strength"></param>
        /// <returns></returns>
        public static List<HeroRegister> CollectStrongest(
            HeroRegister[] registers,
            HeroContainer[] strongest,
            double strength)
        {
            List<HeroRegister> result = new List<HeroRegister>();

            for (int i = 0; i < strongest.Length; i++)
            {
                if (strongest[i].Get(0).GetStrength() == strength)
                {
                    result.Add(new HeroRegister(
                        registers[i].race,
                        registers[i].city,
                        strongest[i]
                        )
                    );
                }
            }

            return result;
        }

        /// <summary>
        /// Method collects all healthiest heroes into one list
        /// </summary>
        /// <param name="registers"></param>
        /// <returns></returns>
        public static List<HeroRegister> CollectHealthiest(
            HeroRegister[] registers)
        {
            List<HeroRegister> result = new List<HeroRegister>();
            double health = 0;

            for (int i = 0; i < registers.Length; i++)
            {
                HeroContainer healthiest = registers[i].AllHeroes.MoreHealth();

                if (healthiest.Count == 0)
                {
                    continue;
                }

                if (healthiest.Get(0).GetHealth() > health)
                {
                    result = new List<HeroRegister>();
                    health = healthiest.Get(0).GetHealth();
                }
                else if (healthiest.Get(0).GetHealth() < health)
                {
                    continue;
                }

                healthiest.Sort();
                result.Add(new HeroRegister(
                    registers[i].race,
                    registers[i].city,
                    healthiest)
                    );
            }

            return result;
        }
    }

}
