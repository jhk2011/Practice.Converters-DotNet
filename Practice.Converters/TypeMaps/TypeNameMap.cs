using System;



namespace Practice.Converters {
    public class TypeNameMap : ITypeNameMap {

        public TypeNameMap(Type type, string name) {
            this.Type = type;
            this.Name = name;
        }

        public TypeNameMap(Type type) : this(type, type.Name) {

        }

        public string Name { get; set; }

        public Type Type { get; set; }

        public bool CanGetName(Type type) {
            return type == Type;
        }

        public bool CanGetType(string name) {
            return name == Name;
        }

        public string GetName(Type type) {

            return Name;
        }

        public Type GetType(string name) {
            return Type;
        }
    }
}
