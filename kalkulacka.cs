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

            string inputJedna = Console.ReadLine();
            //int a; 
            //float a; //abych mohla zadavat i vetsi cisla
            double a = 0;  //abych mohla pouzivat desetinna cisla
            bool validInputJedna = false;
            while (!validInputJedna)
            {
                try
                {
                    //a = Int32.Parse(input); // jestli jde na int
                    a = double.Parse(inputJedna);
                    validInputJedna = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("spatny formát čísla. Zadej znovu. "); // chyba
                    inputJedna = Console.ReadLine();
                }
            }


            Console.WriteLine("Zadej druhe číslo:");

            string inputDva = Console.ReadLine();
            //int b;
            //float b;
            double b = 0;
            bool validInputDva = false;
            while (!validInputDva)
            {
                try
                {
                    //b = Int32.Parse(input); // jestli jde na int
                    b = double.Parse(inputDva);
                    validInputDva = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("spatny formát čísla. Zadej znovu. "); // chyba
                    inputDva = Console.ReadLine();
                }
            }


            //int result=0;
            //float result;
            double result = 0;
            bool validOperation = false;
            while (!validOperation)
            {

                Console.WriteLine("zadej soucet, odecet, nasobeni,deleni(prvni cislo delime druhym), mocnina(mocneni prvniho cisla na druhe)"); //co chceme
                string zn = Console.ReadLine();
                if (zn == "soucet")
                {
                    result = a + b;
                    Console.WriteLine("vysledek je " + result);
                    validOperation = true;
                }
                else if (zn == "odecet")
                {
                    result = a - b;
                    Console.WriteLine("vysledek je " + result);
                    validOperation = true;
                }
                else if (zn == "nasobeni")
                {
                    result = a * b;
                    Console.WriteLine("vysledek je " + result);
                    validOperation = true;
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
                        validOperation = true;
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
                    validOperation = true;

                }
                else
                {
                    Console.WriteLine("spatne zadano");

                }
            }

            Console.WriteLine("Zadej treti číslo:");

            string inputTri = Console.ReadLine();
            //int c; 
            //float c; //abych mohla zadavat i vetsi cisla
            double c = 0;  //abych mohla pouzivat desetinna cisla
            bool validInputTri = false;
            while (!validInputTri)
            {
                try
                {
                    //c = Int32.Parse(input); // jestli jde na int
                    c = double.Parse(inputTri);
                    validInputTri = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("spatny formát čísla. Zadej znovu. "); // chyba
                    inputTri = Console.ReadLine();
                }
            }

            double vysledek = 0;
            bool validOperationDva = false;
            while (!validOperationDva)
            {

                Console.WriteLine("zadej dalsi operaci, soucet, odecet, nasobeni, deleni(vysledek novym cislem) "); // dalsi cislo
                string zna = Console.ReadLine();
                if (zna == "soucet")
                {
                    vysledek = result + c;
                    Console.WriteLine("vysledek je " + vysledek);
                    validOperationDva = true;
                }
                else if (zna == "odecet")
                {
                    vysledek = result - c;
                    Console.WriteLine("vysledek je " + vysledek);
                    validOperationDva = true;
                }
                else if (zna == "nasobeni")
                {
                    vysledek = result * c;
                    Console.WriteLine("vysledek je " + vysledek);
                    validOperationDva = true;
                }
                else if (zna == "deleni")
                {
                    if (c == 0)
                    {
                        Console.WriteLine("nelze delit nulou");
                    }
                    else
                    {
                        vysledek = result / c;
                        Console.WriteLine("vysledek je " + vysledek);
                        validOperationDva = true;
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
