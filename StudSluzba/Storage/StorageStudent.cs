using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Serialization;
using StudSluzba.Entiteti;

namespace StudSluzba.Storage
{
    internal class StorageStudent 
    {
        private const string StoragePath = "../../../Podaci/Studenti.csv";
        private readonly Serializer<Student> _serijalizacijaStudenata;

        public StorageStudent()
        {
            _serijalizacijaStudenata = new Serializer<Student>();       
        }
        public List<Student> CitanjeStudenta()
        {
            return _serijalizacijaStudenata.fromCSV(StoragePath);
        }

        public void CuvanjeStudenata(List<Student> studenti)
        {
            _serijalizacijaStudenata.toCSV(StoragePath, studenti);
        }
    }
}
