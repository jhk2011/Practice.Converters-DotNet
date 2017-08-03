using System;

namespace Practice.Converters {
    public class GuidConverter : Converter<Guid>
    {

        protected override byte[] GetBytes(Type type, Guid value, Convert convert)
        {
            Byte[] bytes = value.ToByteArray();

            foreach (var b in bytes) {
                Console.WriteLine(b);
            }

            return bytes;
        }

        protected override Guid GetValue(Type type, byte[] bytes, Convert convert)
        {
            return new Guid(bytes);
        }
    }

}
