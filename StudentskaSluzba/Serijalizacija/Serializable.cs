using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Serialization
{
    public interface Serializable
    {

        string[] ToCSV();

        void FromCSV(string[] values);

    }
}
