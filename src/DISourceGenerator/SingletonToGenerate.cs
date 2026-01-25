using System;
using System.Collections.Generic;
using System.Text;

namespace DISourceGenerator
{
    public record struct SingletonToGenerate
    {
        public string Name;
        public List<string> Values;

        public SingletonToGenerate(string name, List<string> values)
        {
            Name = name;
            Values = values;
        }
    }
}
