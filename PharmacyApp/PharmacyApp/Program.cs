using System;
using System.CodeDom;
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
            Console.WriteLine("Witaj!");
            Console.WriteLine();
            Console.WriteLine("Dostępne komendy: \"leki\" (operacje na bazie leków) | \"klient\" (sprzedaż leków) | \"exit\" (wyjście z aplikacji");
            Console.WriteLine();
            Console.WriteLine("Wprowadź operację do wykonania:");
            string command = Console.ReadLine();
            if (command == "leki")
            {

                medicine.Show();
                Console.WriteLine("Komenda \"usuń\" jeżeli chcesz usunąć lek, komanda \"edytuj\" jeżeli chcesz edytować lek");
                if (command == "usuń")
                {
                    Console.Write("Podaj numer leku do usunięcia: ");
                    medicine.Delete(int.Parse(command));
                }
                else if (command == "edytuj")
                {
                    Console.Write("Podaj numer leku do edycji: ");
                    medicine.Reload(int.Parse(command));
                }
                
                Console.WriteLine("Jeżeli chcesz sprzedać lek, wpisz \"dodaj\"");


            }

        }
    }
}



