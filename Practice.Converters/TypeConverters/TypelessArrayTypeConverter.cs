using System;

namespace Practice.Converters {
    public class TypelessArrayTypeConverter : ITypeConverter {
        public bool CanConvert(Type type, object obj) {
            return type.IsArray && obj is Array;
        }
        public object Convert(Type type, object obj) {
            Array array = obj as Array;
            Array array2 = Array.CreateInstance(type.GetElementType(), array.Length);
            for (int index = 0; index < array.Length; index++) {
                object value = array.GetValue(index);
                array2.SetValue(value, index);
            }
            return array2;
        }
    }
}
