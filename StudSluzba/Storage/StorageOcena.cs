using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Serialization;
using StudSluzba.Entiteti;


namespace StudSluzba.Storage
{
    class StorageOcena
    {
        private const string StoragePath = "../../../Podaci/Ocene.csv";
        private readonly Serializer<Ocena> _serijalizacijaOcena;
        public StorageOcena()
        {
            _serijalizacijaOcena = new Serializer<Ocena>();;
        }
        public List<Ocena> CitanjeOcena()
        {
            return _serijalizacijaOcena.fromCSV(StoragePath);
        }
        public void CuvanjeOcena(List<Ocena> ocene)
        {
            _serijalizacijaOcena.toCSV(StoragePath, ocene);
        }
    }
}
