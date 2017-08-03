using System;
using System.Collections;
using System.Collections.Generic;

namespace Practice.Converters.Tests {

    [Serializable]
    class Person {

        public Guid Guid { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public double Grade { get; set; }

        public Object[] Courses { get; set; }

        public List<string> List { get; set; }

        public int[][] Matrix { get; set; }

        public ArrayList List2 { get; set; }

        public DateTime Date { get; set; }

    }

    public class MyClass {
        public int Value { get; set; }
    }

    public class MyClass2 : MyClass {
        public string Value2 { get; set; }
    }

    public class ArrayClass {
        public int[] Array { get; set; }
    }
}
