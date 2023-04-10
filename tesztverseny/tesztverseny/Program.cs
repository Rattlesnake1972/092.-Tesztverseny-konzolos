using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.AccessControl;

namespace tesztverseny
{
    internal class Program
    {
        struct versenyrekord
        {
            public string kod;
            public string valasz;
            public int pont;
        }

        static versenyrekord[] verseny = new versenyrekord[500];
        static int db = 0;
        static string jovalasz = "";
        static string azonosito = "";
        static int hanyadik = 0;

        static void Main(string[] args)
        {
            feladat1();
            feladat2();
            feladat3();
            feladat4();
            feladat5();
            feladat6();
            feladat7();
            Console.WriteLine("A program gombnyomásra vár!");
            Console.ReadKey();
        }

        static void feladat1()
        {
            Console.WriteLine("1. feladat: Az adatok beolvasása");
            Console.WriteLine();
            FileStream fajlbe = new FileStream("..\\..\\valaszok.txt", FileMode.Open);
            StreamReader beolvas = new StreamReader(fajlbe);
            jovalasz = beolvas.ReadLine();
            while (!beolvas.EndOfStream) 
            {
                string[] tordel = beolvas.ReadLine().Split(' ');
                verseny[db].kod = tordel[0];
                verseny[db].valasz = tordel[1];
                db++;
            }
            beolvas.Close();
            fajlbe.Close();
        }

        static void feladat2()
        {
            Console.WriteLine("2. feladat: A vetélkedőn {0} versenyző indult.", db);
            Console.WriteLine();
        }

        static void feladat3()
        {
            Console.Write("3. feladat: A versenyző azonosítója= ");
            azonosito = Console.ReadLine();
            int i = 0;
            while (azonosito != verseny[i].kod && i<db)
            {
                i++;
            }
            if (i<db)
            {
                Console.WriteLine("{0}     (a versenyző válasza)", verseny[i].valasz);
                i = hanyadik;
            }
            else
            {
                Console.WriteLine("Nincs ilyen kóddal versenyző.");
            }
            Console.WriteLine();
        }

        static void feladat4()
        {
            Console.WriteLine("4. feladat: ");
            Console.WriteLine("{0}     (a helyes megoldás)", jovalasz);
            for (int i = 0; i < jovalasz.Length; i++)
            {
                if (jovalasz[i] == verseny[hanyadik].valasz[i])
                {
                    Console.Write("+");
                }
                else
                {
                    Console.Write(' ');
                }
            }
            Console.WriteLine("     (a versenyző helyes válaszai)");
            Console.WriteLine();
        }

        static void feladat5()
        {
            Console.Write("5. feladat: A feladat sorszáma = ");
            int sorszam = Convert.ToInt32(Console.ReadLine())-1;
            int helyes = 0;
            for (int i = 0; i < db; i++)
            {
                if (jovalasz[sorszam] == verseny[i].valasz[sorszam]) 
                {
                    helyes++;
                }
            }
            double szazalek = Convert.ToDouble(helyes)/Convert.ToDouble(db) * 100;
            Console.WriteLine("A feladatra {0} fő, a versenyzők {1}%-a adott helyes választ.", helyes, szazalek);
            Console.WriteLine();
        }

        static void feladat6()
        {
            Console.WriteLine("6. feladat: A versenyzők pontszámának meghatározása");
            for (int i = 0;i < db; i++)
            {
                for (int j = 0;j < jovalasz.Length; j++)
                {
                    if (jovalasz[j] == verseny[i].valasz[j])
                    {
                        if (j >= 0 && j <= 4)
                        {
                            verseny[i].pont += 3;
                        }
                        if (j >= 5 && j <= 9)
                        {
                            verseny[i].pont += 4;
                        }
                        if (j >= 10 && j <= 12)
                        {
                            verseny[i].pont += 5;
                        }
                        if (j == 13)
                        {
                            verseny[i].pont += 6;
                        }
                    }

                }
            }
            FileStream fajlki = new FileStream("..\\..\\pontok.txt", FileMode.Create);
            StreamWriter kiir = new StreamWriter(fajlki);
            for (int i = 0; i < db; i++) 
            {
                kiir.WriteLine("{0} {1}", verseny[i].kod, verseny[i].pont);
            }

            kiir.Close();
            fajlki.Close();
            Console.WriteLine();
        }

        static void feladat7()
        {
            Console.WriteLine("7. feladat: A verseny legjobbjai:");
            Console.WriteLine();

            // Rendezzük pontszám szerint csökkenő sorrendben a versenyzők pontszámait. Legjobbak legelöl.

            for (int i = 0; i < db; i++)
            {
                for (int j = 0; j < db-i; j++)
                {
                    if (verseny[j].pont <= verseny[j+1].pont)
                    {
                        versenyrekord csere = verseny[j];
                        verseny[j] = verseny[j+1];
                        verseny[j+1] = csere;
                    }
                }
            }

            // Ellenőrző kiíratás

            /*for (int i = 0;i < db; i++)
            {
                Console.WriteLine("{0} {1}", verseny[i].kod, verseny[i].pont);
                Console.ReadKey();

            }*/

            int helyezes = 1;
            Console.WriteLine("{0}. díj ({1} pont): {2}", helyezes, verseny[0].pont, verseny[0].kod);
            int k = 0;
            do
            {
                k++;
                if (verseny[k].pont == verseny[k-1].pont)
                {
                    Console.WriteLine("{0}. díj ({1} pont): {2}", helyezes, verseny[k].pont, verseny[k].kod);
                }
                if (verseny[k].pont < verseny[k - 1].pont)
                {
                    helyezes++;
                    Console.WriteLine("{0}. díj ({1} pont): {2}", helyezes, verseny[k].pont, verseny[k].kod);
                }
            } 
            while (helyezes < 3 || k == db);
        }
    }
}
