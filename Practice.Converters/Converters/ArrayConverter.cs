using System;
using System.IO;

namespace Practice.Converters {
    public class ArrayConverter : Converter<Array> {
        public override bool CanConvert(Type type) {
            return type.IsArray && type.GetArrayRank() == 1;
        }

        protected override void WriteObject(BinaryWriter writer, Type type, Array value, Convert convert) {

            int count = value.Length;

            writer.Write(count);

            Type elementType = type.GetElementType();

            if (count > 0) {
                foreach (var item in value) {
                    convert.Write(writer, item == null ? elementType : item.GetType(), item);
                }
            }
        }

        protected override Array ReadObject(Type type, BinaryReader reader, Convert convert) {

            int count = reader.ReadInt32();

            Type elementType = type.GetElementType();

            Array array = Array.CreateInstance(elementType, count);

            for (int i = 0; i < count; i++) {
                object value = convert.Read(reader);
                array.SetValue(value, i);
            }
            return array;
        }
    }
}
