using System;
using System.Collections.Generic;
using System.IO;

//Java 的泛型在运行时是不存在的，实际上只要List一个类型，容纳Object类型元素
//.Net 运行时是存在有实际的泛型类型
//如果只保留非泛型，则在.Net端的泛型无法使用，如果保留泛型，额外的信息需要附加到字段上（注解）

namespace Practice.Converters {

    public class ListConverter : Converter {
        public override bool CanConvert(Type type) {

            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>);
        }

        public override void Write(BinaryWriter writer, Type type, object value, Convert convert) {
            Converter converter = GetConverter(type);
            converter.Write(writer, type, value, convert);
        }

        public override object Read(BinaryReader reader, Type type, Convert convert) {
            Converter converter = GetConverter(type);
            return converter.Read(reader, type, convert);
        }

        private Converter GetConverter(Type type) {
            Type type0 = typeof(ListConverter<>).MakeGenericType(type.GetGenericArguments()[0]);
            return Activator.CreateInstance(type0) as Converter;
        }
    }
}
