using System;


namespace Practice.Converters {
    public interface ITypeConverter {
        bool CanConvert(Type type, object obj);
        object Convert(Type type, object obj);
    }
}
