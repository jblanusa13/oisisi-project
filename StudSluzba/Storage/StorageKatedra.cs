using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Serialization;
using StudSluzba.Entiteti;

namespace StudSluzba.Storage
{
    internal class StorageKatedra
    {
        private const string StoragePath = "../../../Podaci/Katedre.csv";
        private readonly Serializer<Katedra> _serijalizacijaKatedri;
        public StorageKatedra()
        {
            _serijalizacijaKatedri = new Serializer<Katedra>();
        }
        public List<Katedra> CitanjeKatedri()
        {
            return _serijalizacijaKatedri.fromCSV(StoragePath);
        }
        public void CuvanjeKatedri(List<Katedra> katedre)
        {
            _serijalizacijaKatedri.toCSV(StoragePath, katedre);
        }
    }
}
