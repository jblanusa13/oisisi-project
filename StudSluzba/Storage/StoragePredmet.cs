using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Serialization;
using StudSluzba.Entiteti;

namespace StudSluzba.Storage
{
    internal class StoragePredmet
    {
        private const string StoragePath = "../../../Podaci/Predmeti.csv";
        private readonly Serializer<Predmet> _serijalizacijaPredmeta;

        public StoragePredmet()
        {
            _serijalizacijaPredmeta = new Serializer<Predmet>();
        }

        public List<Predmet> CitanjePredmeta()
        {
            return _serijalizacijaPredmeta.fromCSV(StoragePath);
        }

        public void CuvanjePredmeta(List<Predmet> predmeti)
        {
            _serijalizacijaPredmeta.toCSV(StoragePath, predmeti);
        }
    }
}
