using System;
using System.Collections.Generic;
using System.Text;

namespace _03.MinionNames
{
    public class Minion
    {
        public Minion(string name, int age)
        {
            this.Name = name;
            this.Age = age; 
        }

        public string Name { get; set; }

        public int Age { get; set; }

        public override string ToString()
        {
            return this.Name + " " + this.Age; 
        }
    }
}
