using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Practice.Converters {

    public class ObjectConverter : Converter<object> {
        public override bool CanConvert(Type type) {

            if (type.IsInterface) return false;
            if (type.IsAbstract) return false;
            if (type.IsPrimitive) return false;

            ConstructorInfo[] constructors = type.GetConstructors();

            foreach (var constructor in constructors) {
                if (constructor.GetParameters().Length == 0) {
                    return true;
                }
            }

            return false;
        }

        protected override void WriteObject(BinaryWriter writer, Type type, object obj, Convert convert) {

            var metadata = TypeMetadata.GetMetadata(type);

            writer.Write(metadata.Properties.Count);

            foreach (var property in metadata.Properties) {
                string name = property.Property.Name;
                writer.WriteSection(name);
                object value = property.GetValue(obj);
                convert.Write(writer, property.Property.PropertyType, value);
            }
        }

        protected override object ReadObject(Type type, BinaryReader reader, Convert convert) {

            object obj = Activator.CreateInstance(type);

            int count = reader.ReadInt32();

            if (count > 0) {
                var metadata = TypeMetadata.GetMetadata(type);

                var properties = metadata.Properties;

                for (int i = 0; i < count; i++) {

                    string name = reader.ReadStringSection();


                    object value = convert.Read(reader);

                    var property = properties.Where(x => x.Property.Name == name).FirstOrDefault();

                    if (property != null) {

                        if (!property.Property.PropertyType.IsInstanceOfType(value)) {
                            value = convert.TypeConverter.Convert(property.Property.PropertyType, value);
                        }

                        property.SetValue(obj, value);
                    }
                }
            }
            return obj;
        }
    }
}
