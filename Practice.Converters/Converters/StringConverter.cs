using System;
using System.Text;

namespace Practice.Converters {
    public class StringConverter : Converter<String>
    {
        protected override byte[] GetBytes(Type type, string value, Convert convert)
        {
            return Encoding.UTF8.GetBytes(value);
        }

        protected override string GetValue(Type type, byte[] bytes, Convert convert)
        {
            return Encoding.UTF8.GetString(bytes);
        }
    }

}
