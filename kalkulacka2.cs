using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Zadej první číslo:");

            string inputOne = Console.ReadLine();
            //int a; 
            //float a; //abych mohla zadavat i vetsi cisla
            double a = 0;  //abych mohla pouzivat desetinna cisla
            bool ValidInputOne = false;
            while (!ValidInputOne)
            {
                try
                {
                    //a = Int32.Parse(input); // jestli jde na int
                    a = double.Parse(inputOne);
                    ValidInputOne = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("spatny formát čísla. Zadej znovu. "); // chyba
                    inputOne = Console.ReadLine();
                }
            }


            Console.WriteLine("Zadej druhe číslo:");

            string inputTwo = Console.ReadLine();
            //int b;
            //float b;
            double b = 0;
            bool ValidInputTwo = false;
            while (!ValidInputTwo)
            {
                try
                {
                    //b = Int32.Parse(input); // jestli jde na int
                    b = double.Parse(inputTwo);
                    ValidInputTwo = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("spatny formát čísla. Zadej znovu. "); // chyba
                    inputTwo = Console.ReadLine();
                }
            }


            //int result=0;
            //float result;
            double result = 0;
            bool ValidOperation = false;
            while (!ValidOperation)
            {

                Console.WriteLine("zadej soucet, odecet, nasobeni,deleni(prvni cislo delime druhym), mocnina(mocneni prvniho cisla na druhe)"); //co chceme
                string zn = Console.ReadLine();
                if (zn == "soucet")
                {
                    result = a + b;
                    Console.WriteLine("vysledek je " + result);
                    ValidOperation = true;
                }
                else if (zn == "odecet")
                {
                    result = a - b;
                    Console.WriteLine("vysledek je " + result);
                    ValidOperation = true;
                }
                else if (zn == "nasobeni")
                {
                    result = a * b;
                    Console.WriteLine("vysledek je " + result);
                    ValidOperation = true;
                }
                else if (zn == "deleni")
                {
                    if (b == 0)
                    {
                        Console.WriteLine("nelze delit nulou");
                    }
                    else
                    {
                        result = a / b;
                        Console.WriteLine("vysledek je " + result);
                        ValidOperation = true;
                    }
                }
                else if (zn == "mocnina")
                {
                    result = 1;//musim definovat hodnotu abych mohla nasobit
                    for (int i = 0; i < b; i++)
                    {
                        result *= a;
                    }
                    Console.WriteLine("vysledek je " + result);
                    ValidOperation = true;

                }
                else
                {
                    Console.WriteLine("spatne zadano");

                }
            }

            Console.WriteLine("Zadej treti číslo:");

            string inputThree = Console.ReadLine();
            //int c; 
            //float c; //abych mohla zadavat i vetsi cisla
            double c = 0;  //abych mohla pouzivat desetinna cisla
            bool ValidInputThree = false;
            while (!ValidInputThree)
            {
                try
                {
                    //c = Int32.Parse(input); // jestli jde na int
                    c = double.Parse(inputThree);
                    ValidInputThree = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("spatny formát čísla. Zadej znovu. "); // chyba
                    inputThree = Console.ReadLine();
                }
            }

            double resultTwo = 0;
            bool ValidOperationTwo = false;
            while (!ValidOperationTwo)
            {

                Console.WriteLine("zadej dalsi operaci, soucet, odecet, nasobeni, deleni(vysledek novym cislem) "); // dalsi cislo
                string zna = Console.ReadLine();
                if (zna == "soucet")
                {
                    resultTwo = result + c;
                    Console.WriteLine("vysledek je " + resultTwo);
                    ValidOperationTwo = true;
                }
                else if (zna == "odecet")
                {
                    resultTwo = result - c;
                    Console.WriteLine("vysledek je " + resultTwo);
                    ValidOperationTwo = true;
                }
                else if (zna == "nasobeni")
                {
                    resultTwo = result * c;
                    Console.WriteLine("vysledek je " + resultTwo);
                    ValidOperationTwo = true;
                }
                else if (zna == "deleni")
                {
                    if (c == 0)
                    {
                        Console.WriteLine("nelze delit nulou");
                    }
                    else
                    {
                        resultTwo = result / c;
                        Console.WriteLine("vysledek je " + resultTwo);
                        ValidOperationTwo = true;
                    }
                }
                else
                {
                    Console.WriteLine("spatne zadano");

                }
            }
            Console.ReadKey();


        }

    }
}
