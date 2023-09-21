using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudSluzba.Observer
{
    public interface IObserver
    {
        void UpdateStudent();
        void UpdateProfesor();
        void UpdatePredmet();
        void UpdateKatedra();
    }
}
