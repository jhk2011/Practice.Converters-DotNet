using System;
using System.Collections;


namespace Practice.Converters {

    public class TypelessListMap : ITypeNameMap {
        public bool CanGetName(Type type) {
            return type == typeof(IList) || type == typeof(ArrayList);
        }

        public bool CanGetType(string name) {
            return name == "list";
        }

        public string GetName(Type type) {
            return "list";
        }

        public Type GetType(string name) {
            return typeof(ArrayList);
        }
    }
}
