using System.Linq;
using System.Reflection;

namespace Practice.Converters {
    public class PropertyMetadata {
        public PropertyInfo Property { get; private set; }

        public DataMemberAttribute DataMember { get; set; }

        public PropertyMetadata(PropertyInfo property) {
            Property = property;
            DataMember = property.GetCustomAttributes(typeof(DataMemberAttribute), false)
                .OfType<DataMemberAttribute>().FirstOrDefault();
        }

        public object GetValue(object obj) {
            return Property.GetValue(obj, null);
        }

        public void SetValue(object obj, object value) {
            Property.SetValue(obj, value, null);
        }
    }
}
