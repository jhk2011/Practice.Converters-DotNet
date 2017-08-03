using System;
using System.Collections;
using System.IO;

namespace Practice.Converters {
    public class IListConverter : Converter<IList> {

        public override bool CanConvert(Type type) {
            return typeof(IList).IsAssignableFrom(type);
        }
        protected override void WriteObject(BinaryWriter writer, Type type, IList value, Convert convert) {
            int count = value.Count;
            writer.Write(count);
            if (count > 0) {
                foreach (var item in value) {
                    convert.Write(writer, item == null ? typeof(object) : item.GetType(), item);
                }
            }
        }
        protected override IList ReadObject(Type type, BinaryReader reader, Convert convert) {
            if (type == null) throw new InvalidOperationException("type");

            int count = reader.ReadInt32();
            IList value = new ArrayList();
            if (count > 0) {
                for (int i = 0; i < count; i++) {
                    object item = convert.Read(reader);
                    value.Add(item);
                }
            }
            return value;
        }
    }
}
