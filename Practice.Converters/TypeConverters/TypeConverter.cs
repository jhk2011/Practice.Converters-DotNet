using System;
using System.Collections.Generic;



namespace Practice.Converters {
    public class TypeConverter {

        List<ITypeConverter> converters = new List<ITypeConverter>();
        public object Convert(Type type, object obj) {
            foreach (var converter in converters) {
                if (converter.CanConvert(type, obj)) return converter.Convert(type, obj);
            }
            return obj;
        }
    }
}
