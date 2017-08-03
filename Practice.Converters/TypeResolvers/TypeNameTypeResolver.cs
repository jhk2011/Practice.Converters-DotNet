using System;
using System.IO;

/*
1.typemap 将类型映射到名称 名称映射到类型
2.converter 将类型转换到字节数组 字节数组转换到类型
3.typeconverter 将类型转换到另一个类型
（其他语言可能不支持泛型类型，
将其他语言的非泛型类型转换至泛型类型 ）
*/

namespace Practice.Converters {

    public class TypeNameTypeResolver : ITypeResolver {

        TypeMap typeMap = new TypeMap();

        public void Add(TypeNameMap map) {
            typeMap.Add(map);
        }

        public Type Read(BinaryReader reader) {

            string name = reader.ReadStringSection();

            Type type = typeMap.GetType(name);

            if (type == null) throw new InvalidOperationException("类型名称 " + name + " 没有对应的类型");

            return type;
        }

        public void Write(BinaryWriter writer, Type type) {

            string name = typeMap.GetName(type);

            if (name == null) throw new InvalidOperationException("类型 " + type.Name + " 没有对应的类型名称");

            writer.WriteSection(name);

        }

    }
}
