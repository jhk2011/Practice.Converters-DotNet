using System;

namespace Practice.Converters {
    public class ByteArrayConverter : Converter<Byte[]>
    {
        protected override byte[] GetBytes(Type type, byte[] value, Convert convert)
        {
            return value;
        }

        protected override byte[] GetValue(Type type, byte[] bytes, Convert convert)
        {
            return bytes;
        }
    }

}
