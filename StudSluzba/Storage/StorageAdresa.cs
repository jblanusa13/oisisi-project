using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Serialization;
using StudSluzba.Entiteti;


namespace StudSluzba.Storage
{
    class StorageAdresa
    {
        private const string StoragePath = "../../../Podaci/Adrese.csv";
        private readonly Serializer<Adresa> _serijalizacijaAdresa;
        public StorageAdresa()
        {
            _serijalizacijaAdresa = new Serializer<Adresa>();
        }
        public List<Adresa> CitanjeAdresa()
        {
            return _serijalizacijaAdresa.fromCSV(StoragePath);
        }
        public void CuvanjeAdresa(List<Adresa> adrese)
        {
            _serijalizacijaAdresa.toCSV(StoragePath, adrese);
        }
    }
}
