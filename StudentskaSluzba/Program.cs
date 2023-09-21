// See https://aka.ms/new-console-template for more information

using StudentskaSluzba.Serijalizacija;
using StudentskaSluzba.Ispis;
using System;

namespace StudentskaSluzba
{
    class Program
    {
        static void Main()
        {
            // ovo ne znam dal se ovako radi, napisala sam samo da proverim ispis
            IspisNaEkran ispis = new IspisNaEkran();
            ispis.pokretanjeIzbora();
        }
    }
}