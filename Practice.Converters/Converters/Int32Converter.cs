using System;
using System.IO;

namespace Practice.Converters {

    public class Int32Converter : Converter<Int32>
    {
        protected override int ReadObject(Type type, BinaryReader reader, Convert convert) {
            return reader.ReadInt32();
        }

        protected override void WriteObject(BinaryWriter writer, Type type, int value, Convert convert) {
            writer.Write(value);
        }

        //protected override byte[] GetBytes(Type type, int value, Convert convert)
        //{
        //    return BitConverter.GetBytes(value);
        //}

        //protected override int GetValue(Type type, byte[] bytes, Convert convert)
        //{
        //    return BitConverter.ToInt32(bytes, 0);
        //}
    }

}
