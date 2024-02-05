using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_Server
{
    public class Command
    {
        public string? CommandName { get; set; }
        public List<string>? Flags { get; set; }
        public List<string>? Arguments { get; set; }

        public Command() 
        {
            Flags = new List<string>();
            Arguments = new List<string>();
        }
    }
}
