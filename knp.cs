using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    internal class Program
    {
        static int LoadAndCheckInput()
        { 
        
        }
        static void Main(string[] args)
        {
            Random rng = new Random();

            int userScore = 0;
            int pcScore= 0;

            int ScoreToWin = 3;
            //Console.WriteLine("zadej jednu z akcí: kamen, nuzky, papir");
            while (userScore<ScoreToWin && pcScore<ScoreToWin)
            {
                //bool gotCorrectInput = false;
                int userNumImput = -1;
                while (userNumImput == -1)
                {
                    Console.WriteLine("zadej jednu z akcí: kamen, nuzky, papir");
                    string userInput = Console.ReadLine();

                    

                    /*if (userInput == "kamen")
                    {
                        userNumImput = 0;
                    }
                    else if (userInput == "nuzky")
                    {
                        userNumImput= 1;
                    }
                    else if (userInput == "papir")
                    {
                        userNumImput = 2;
                    }
                    else
                    {
                        Console.WriteLine(" Neznamy vstup");
                    } */

                    switch (userInput) //neni na porovnavaní 
                    {
                        case "kamen":
                            userNumImput = 0;
                            break;
                        case "nuzky":
                            userNumImput = 1;
                            break;
                        case "papir":
                            userNumImput = 2;
                            break;
                        default:
                            Console.WriteLine("neznamy vstup");
                            break;
                    }
                }

                    int pcInput = rng.Next(3); // nebo rng.Next()%3 coz je deleni se zbytkem
                if (pcInput == 0)
                {
                    Console.WriteLine("pocitac dal kamen");
                }
                else if (pcInput == 1)
                {
                    Console.WriteLine("pocitac dal nuzky");
                }
                else
                {
                    Console.WriteLine("pocitac dal papir");
                }

                if (userNumImput== pcInput) // remiza
                {
                    Console.WriteLine("Oba hraci dali to stejné, doslo k remize");
                }
                else if (
                    (userNumImput==1 && pcInput == 0) // nuzky proti kameni
                    || (userNumImput== 2 && pcInput==1)  // papir proti nuzkam
                    || (userNumImput== 0 && pcInput==2)) // kamen proti papiru
                    // prohra
                {
                    Console.WriteLine("Prohrál jsi, pocitac te porazil");
                    pcScore++;
                }
                else // vyhra
                {
                    Console.WriteLine("Vyhrál jsi, porazil jsi pocitac");
                    userScore++;
                }
                //Console.WriteLine("Aktualni skore: \nHrac: {0} \nPocitac {1} \n", userScore, pcScore);
                Console.WriteLine(" Aktualni skore: \nHrac: " + userScore+ "\nPocitac: " + pcScore);

                if (userScore == 3 )
                {
                    Console.WriteLine("Vyhral jsi celou hru");

                }
                else if (pcScore == 3 ) 
                {
                    Console.WriteLine("pocitac vyhral celou hru");
                }

            }

            Console.ReadKey();
        }
    }
}
