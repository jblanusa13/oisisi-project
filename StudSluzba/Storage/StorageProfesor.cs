using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Serialization;
using StudSluzba.Entiteti;

namespace StudSluzba.Storage
{
    internal class StorageProfesor
    {
        private const string StoragePath = "../../../Podaci/Profesori.csv";
        private readonly Serializer<Profesor> _serijalizacijaProfesora;
        public StorageProfesor()
        {
            _serijalizacijaProfesora = new Serializer<Profesor>();
        }
        public List<Profesor> CitanjeProfesora()
        {
            return _serijalizacijaProfesora.fromCSV(StoragePath);
        }
        public void CuvanjeProfesora(List<Profesor> profesori)
        {
            _serijalizacijaProfesora.toCSV(StoragePath, profesori);
        }
    }
}
