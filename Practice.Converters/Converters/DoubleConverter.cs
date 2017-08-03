using System;

namespace Practice.Converters {
    public class DoubleConverter : Converter<Double>
    {
        protected override byte[] GetBytes(Type type, Double value, Convert convert)
        {
            return BitConverter.GetBytes(value);
        }

        protected override Double GetValue(Type type, byte[] bytes, Convert convert)
        {
            return BitConverter.ToDouble(bytes, 0);
        }
    }

}
