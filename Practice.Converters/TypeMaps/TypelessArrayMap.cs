using System;


namespace Practice.Converters {


    public class TypelessArrayMap : ITypeNameMap {
        public bool CanGetName(Type type) {
            return type.IsArray;
        }

        public bool CanGetType(string name) {
            return name == "array";
        }

        public string GetName(Type type) {
            return "array";
        }

        public Type GetType(string name) {
            return typeof(object[]);
        }
    }
}
