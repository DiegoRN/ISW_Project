using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestDep.Entities;

namespace Proves
{
    class Program
    {
        static void Main(string[] args)
        {
            User usuari = new User("Camí de Vera", "ES1092810291829","12345678A","Nom de prova",46021, DateTime.Now.AddYears(-20), false);
            Console.WriteLine("Nom d'usuari: " + usuari.Name);
            Console.WriteLine("Domicili de l'usuari: " + usuari.Address);
            Console.WriteLine("IBAN de l'usuari: " + usuari.IBAN);
            Console.WriteLine("DNI de l'usuari: " + usuari.Id);
            Console.WriteLine("Codi postal de l'usuari: " + usuari.ZipCode);
            Console.WriteLine("Data de naixement: " + usuari.BirthDate);
            Console.WriteLine("Jubilat/da?: " + (usuari.Retired ? "Sí" : "No"));

            //La línia següent és per a evitar que el programe es tanque sense poder llegir el que trau per pantalla.
            //Quan polseu qualsevol tecla es tancarà.
            Console.ReadLine();

        }
    }
}
