using System;
using System.IO;

namespace Practice.Converters {
    public class Int64Converter : Converter<Int64>
    {
        protected override byte[] GetBytes(Type type, Int64 value, Convert convert)
        {
            return BitConverter.GetBytes(value);
        }

        protected override long ReadObject(Type type, BinaryReader reader, Convert convert) {
            return base.ReadObject(type, reader, convert);
        }

        protected override Int64 GetValue(Type type, byte[] bytes, Convert convert)
        {
            return BitConverter.ToInt64(bytes, 0);
        }
    }

}
