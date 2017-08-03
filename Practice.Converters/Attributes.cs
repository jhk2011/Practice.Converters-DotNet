using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Practice.Converters {

    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class DataMemberAttribute : Attribute {
        public string Name { get; set; }
        public bool Required { get; set; }
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class DataContractAttribute : Attribute {
        public DataContractAttribute(int id) {
            Id = id;
        }

        public int Id { get; private set; }
    }
}
