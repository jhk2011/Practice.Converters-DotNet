using System;
using System.Collections.Generic;
using System.IO;

namespace Practice.Converters {

    /// <summary>
    /// 使用程序集限定名，适用于.Net内部使用
    /// </summary>
    public class DefaultTypeResolver : ITypeResolver {

        Dictionary<string, Type> types = new Dictionary<string, Type>();
        Dictionary<Type, string> names = new Dictionary<Type, string>();


        public void Write(BinaryWriter writer, Type type) {

            string name;

            if (!names.TryGetValue(type, out name)) {
                name = type.AssemblyQualifiedName;
            }

            writer.WriteSection(name);
        }

        public Type Read(BinaryReader reader) {
            string fullName = reader.ReadStringSection();

            Type type;

            if (!types.TryGetValue(fullName, out type)) {
                type = Type.GetType(fullName, false, false);
                types.Add(fullName, type);
            }

            return type;
        }

    }
}
