using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lab1_Server
{
    public class BaseParser : IParser
    {
        public Command Parse(string message)
        {
            var regex = new Regex("^\\w+");
            var command = new Command();
            command.CommandName = regex.Match(message).Value;
            regex = new Regex("-\\w");
            foreach(Match flag in regex.Matches(message))
            {
                command.Flags.Add(flag.Value);
            }

            regex = new Regex(" \\w+");
            foreach (Match arg in regex.Matches(message))
            {
                command.Arguments.Add(arg.Value.Substring(1));
            }

            return command;
        }
    }
}
