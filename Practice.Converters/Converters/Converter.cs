using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Practice.Converters
{

    public abstract class Converter
    {
        public abstract bool CanConvert(Type type);

        public abstract void Write(BinaryWriter writer, Type type, object value, Convert convert);

        public abstract object Read(BinaryReader reader, Type type, Convert convert);
    }

    public abstract class Converter<T> : Converter
    {
        public override bool CanConvert(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return CanConvert(type.GetGenericArguments()[0]);
            }
            return typeof(T) == (type);
        }

        public override void Write(BinaryWriter writer, Type type, object value, Convert convert)
        {
            if (value == null)
            {
                writer.Write(-1);
            }
            else
            {
                byte[] bytes = GetBytes(type, (T)value, convert);
                if (bytes == null)
                {
                    writer.Write(0);
                }
                else
                {
                    writer.Write(bytes.Length);
                    writer.Write(bytes);
                }
            }
        }

        public override object Read(BinaryReader reader, Type type, Convert convert)
        {
            int length = reader.ReadInt32();
            if (length == -1) return null;
            byte[] bytes = reader.ReadBytes(length);
            return GetValue(type, bytes, convert);
        }

        protected virtual byte[] GetBytes(Type type, T value, Convert convert) {
            MemoryStream ms = new MemoryStream();
            using (BinaryWriter writer = new BinaryWriter(ms)) {
                Console.WriteLine("写入:{0}", type.Name);
                WriteObject(writer, type, value, convert);
                return ms.ToArray();
            }
        }

        protected virtual void WriteObject(BinaryWriter writer, Type type, T value, Convert convert) {
            throw new NotSupportedException();
        }

        protected virtual T GetValue(Type type, byte[] bytes, Convert convert) {
            MemoryStream ms = new MemoryStream(bytes);
            using (BinaryReader reader = new BinaryReader(ms)) {
                Console.WriteLine("读取:{0}", type.Name);
                return ReadObject(type, reader, convert);
            }
        }

        protected virtual T ReadObject(Type type, BinaryReader reader, Convert convert) {
            throw new NotSupportedException();
        }
    }

    public class ConverterCollection : Collection<Converter>
    {
        public ConverterCollection()
        {

        }

        public ConverterCollection(IList<Converter> collection)
            : base(collection)
        {

        }

        public Converter GetConverter(Type type)
        {
            foreach (var converter in this)
            {
                if (converter.CanConvert(type)) return converter;
            }
            return null;
        }

    }

}
