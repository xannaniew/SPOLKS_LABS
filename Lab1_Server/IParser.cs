using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_Server
{
    public interface IParser
    {
        public Command Parse(string message); 
    }
}
