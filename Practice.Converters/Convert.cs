using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

/*
1.typemap 将类型映射到名称 名称映射到类型
2.converter 将类型转换到字节数组 字节数组转换到类型
3.typeconverter 将类型转换到另一个类型
（其他语言可能不支持泛型类型，
将其他语言的非泛型类型转换至泛型类型 ）

.Net 的枚举是整数值常量 Java枚举是带顺序值的对象常量 互相传输存在困难 需要为其中一个赋予附加信息

Java 附加常量值，或者.Net附加顺序值


Java 没有运行时泛型 .Net中的List<T>在Java中解析为ArrayList


Java 中的 ArrayList 在赋值给.Net对象属性时，如果属性定义类型List<T> 会进行转换 

    例如 Java ArrayList<int>(运行时没有类型信息) => C# List<int>


Java 中int这类基础类型有包装类Integer，他们是两个类型，在设置值是可能需要转换 

    例如 C# int[] => Java Integer[]

Java 中没有可空类型 .Net 中的int? 无法表示 包装类可以

*/

namespace Practice.Converters {
    public class Convert {
        public ITypeResolver Resolver { get; private set; }
        public ConverterCollection Converters { get; private set; }

        public TypeConverter TypeConverter { get; set; }

        private static ConverterCollection _converters = new ConverterCollection {
            new BooleanConverter(),

            new ByteConverter(),
            new Int32Converter(),
            new Int64Converter(),

            new SingleConverter(),
            new DoubleConverter(),

            new StringConverter(),
            new DateTimeConverter(),
            new GuidConverter(),
            new DateTimeConverter(),

            new ByteArrayConverter(),
            new EnumConverter(),

            new ArrayConverter(),
            new ListConverter(),
            new IListConverter(),

            new ObjectConverter()
        };

        public Convert(ITypeResolver resolver, ConverterCollection converters) {
            Resolver = resolver ?? new DefaultTypeResolver();
            Converters = converters ?? _converters;
            TypeConverter = new TypeConverter();
        }

        public Convert()
            : this(null, null) {

        }

        public void Write(BinaryWriter writer, Type type, object obj) {
            Converter converter = Converters.GetConverter(type);
            if (converter == null) throw new InvalidOperationException(string.Format("不能转换类型 {0}", type.Name));
            Resolver.Write(writer, type);
            converter.Write(writer, type, obj, this);
        }

        public object Read(BinaryReader reader) {
            Type type = Resolver.Read(reader);
            if (type == null) throw new InvalidOperationException("没有找到类型");
            Converter converter = Converters.GetConverter(type);
            if (converter == null) throw new InvalidOperationException(string.Format("不能转换类型 {0}", type.Name));
            return converter.Read(reader, type, this);
        }

        public void Write(Stream stream, object obj) {
            if (stream == null) throw new ArgumentNullException(nameof(stream));
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (!stream.CanWrite) throw new ArgumentException("stream can not write");

            Type type = obj.GetType();

            using (BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8, true)) {
                Write(writer, type, obj);
                writer.Flush();
            }
        }

        public byte[] Write(object obj) {
            if (obj == null) throw new ArgumentNullException("obj");
            Type type = obj.GetType();

            using (MemoryStream ms = new MemoryStream()) {
                Write(ms, obj);
                return ms.ToArray();
            }
        }

        public object Read(Stream stream) {
            if (stream == null) throw new ArgumentNullException(nameof(stream));
            if (!stream.CanRead) throw new ArgumentException("stream can not read");
            using (BinaryReader reader = new BinaryReader(stream, Encoding.UTF8, true)) {
                return Read(reader);
            }
        }

        public object Read(byte[] buffer) {

            if (buffer == null) throw new ArgumentNullException(nameof(buffer));

            using (MemoryStream ms = new MemoryStream(buffer)) {
                return Read(ms);
            }
        }
    }
}
