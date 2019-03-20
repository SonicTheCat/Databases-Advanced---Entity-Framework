using System;
using System.Collections.Generic;
using System.Text;

namespace CustomAutoMapper
{
    public class Person
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public List<string> Courses { get; set; } = new List<string>();

    }
}