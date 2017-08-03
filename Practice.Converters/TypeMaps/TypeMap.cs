using System;
using System.Collections.Generic;


namespace Practice.Converters {
    public class TypeMap {

        List<ITypeNameMap> maps = new List<ITypeNameMap>();

        public TypeMap() {
            maps.Add(new TypeNameMap(typeof(int), "int"));
            maps.Add(new TypeNameMap(typeof(int), "Integer"));
            maps.Add(new TypeNameMap(typeof(string), "string"));
            maps.Add(new TypeNameMap(typeof(double), "double"));
            maps.Add(new TypeNameMap(typeof(double), "Double"));
            maps.Add(new TypeNameMap(typeof(object), "object"));
            maps.Add(new TypeNameMap(typeof(Guid), "guid"));
            maps.Add(new TypeNameMap(typeof(DateTime), "datetime"));
            maps.Add(new ArrayMap(this));
            maps.Add(new TypelessArrayMap());
            maps.Add(new GenericTypeMap(this, typeof(List<>), "list"));
            maps.Add(new TypelessListMap());
        }

        public void Add(ITypeNameMap map) {
            maps.Add(map);
        }

        public Type GetType(string name) {
            foreach (var map in maps) {
                if (map.CanGetType(name)) {
                    Console.WriteLine("读取类型:{0},{1}", name, map.GetType().Name);
                    return map.GetType(name);
                }
            }
            return null;
        }
        public string GetName(Type type) {
            foreach (var map in maps) {
                if (map.CanGetName(type)) {
                    string name = map.GetName(type);
                    Console.WriteLine("写入类型:{0}({1})", name, map.GetType().Name);
                    return name;
                }
            }
            return null;
        }
    }
}
