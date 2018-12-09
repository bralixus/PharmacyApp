using System;
using System.CodeDom;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Medicine medicine = new Medicine();
            string command = "";
            do
            {
                Console.WriteLine("Witaj!");
                Console.WriteLine();
                Console.WriteLine("Dostępne komendy: \"leki\" (operacje na bazie leków) | \"klient\" (sprzedaż leków) | \"exit\" (wyjście z aplikacji");
                Console.WriteLine();
                Console.WriteLine("Wprowadź operację do wykonania:");
                command = Console.ReadLine().ToLower();
                if (command == "leki")
                {

                    medicine.Show();
                    Console.WriteLine();
                    Console.WriteLine("Komenda \"usuń\" jeżeli chcesz usunąć lek, komanda \"edytuj\" jeżeli chcesz edytować lek");
                    command = Console.ReadLine().ToLower();
                    if (command == "usuń")
                    {
                        Console.Write("Podaj numer leku do usunięcia: ");
                        command = Console.ReadLine();

                        medicine.Delete(int.Parse(command));
                    }
                    if (command == "edytuj")
                    {
                        Console.Write("Podaj numer leku do edycji: ");
                        command = Console.ReadLine();
                        medicine.Reload(int.Parse(command));
                    }

                    Console.WriteLine("Jeżeli chcesz sprzedać lek, wpisz \"dodaj\"");

                }
            } while (command != "exit");
        }
    }
}



