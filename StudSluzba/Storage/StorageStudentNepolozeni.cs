using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Serialization;
using StudSluzba.Entiteti;

namespace StudSluzba.Storage
{
    internal class StorageStudentNepolozeni
    {
       
        private readonly Serializer<StudentNepolozeniPredmet> _serijalizacijaNepolozenih;
        private const string StoragePath = "../../../Podaci/NepolozeniPredmeti.csv";
        public StorageStudentNepolozeni()
        {
            _serijalizacijaNepolozenih = new Serializer<StudentNepolozeniPredmet>();
        }
        public List<StudentNepolozeniPredmet> CitanjeNepolozenih()
        {
            return _serijalizacijaNepolozenih.fromCSV(StoragePath);
        }
        public void CuvanjeNepolozenih(List<StudentNepolozeniPredmet> nepolozeni)
        {
            _serijalizacijaNepolozenih.toCSV(StoragePath, nepolozeni);
        }
    }
}
