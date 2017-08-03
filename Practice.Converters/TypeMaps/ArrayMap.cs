using System;



namespace Practice.Converters {
    public class ArrayMap : ITypeNameMap {

        public ArrayMap(TypeMap typeMap) {
            this.TypeMap = typeMap;
        }

        public TypeMap TypeMap { get; set; }

        public bool CanGetName(Type type) {
            return type.IsArray;
        }

        public bool CanGetType(string name) {
            return name.EndsWith("[]");
        }

        public string GetName(Type type) {

            Type elementType = type.GetElementType();

            string elementName = TypeMap.GetName(elementType);

            if (elementName == null) throw new InvalidOperationException("类型" + elementName + "没有对应名称");

            return elementName + "[]";
        }

        public Type GetType(string name) {

            string elementName = name.Substring(0, name.Length - 2);

            Type elementType = TypeMap.GetType(elementName);

            if (elementType == null) throw new InvalidOperationException("名称" + elementName + "没有对应类型");

            return elementType.MakeArrayType();
        }

    }
}
