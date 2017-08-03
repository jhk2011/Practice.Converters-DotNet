using System;
using System.Collections;
using System.Collections.Generic;


namespace Practice.Converters {
    public class TypelessListConverter : ITypeConverter {
        public bool CanConvert(Type type, object obj) {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>) && obj is IList;
        }

        public object Convert(Type type, object obj) {

            IList list = obj as IList;

            IList ret = (IList)typeof(List<>).MakeGenericType(type.GetGenericArguments()[0]);

            foreach (var item in list) {
                ret.Add(item);
            }
            return ret;
        }
    }
}
