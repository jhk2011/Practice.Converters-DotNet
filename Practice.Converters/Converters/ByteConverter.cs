using System;

namespace Practice.Converters {
    public class ByteConverter : Converter<Byte>
    {
        protected override byte[] GetBytes(Type type, byte value, Convert convert)
        {
            return new byte[] { value };
        }

        protected override byte GetValue(Type type, byte[] bytes, Convert convert)
        {
            return bytes[0];
        }
    }

}
