using System;
using System.Collections.Generic;
using System.IO;

namespace Practice.Converters {

    public class BObject {

        Dictionary<string, BObject> properties = new Dictionary<string, BObject>();

        public BObject this[string name]
        {
            get
            {
                return properties[name];
            }
            set
            {
                properties[name] = value;
            }
        }
    }

    public class BArray {

        List<BObject> items = new List<BObject>();

        public int Length
        {
            get
            {
                return items.Count;
            }
        }

        public BObject this[int index]
        {
            get { return items[index]; }
            set { items[index] = value; }
        }
    }

    public class ListConverter<T> : Converter<List<T>> {
        protected override void WriteObject(BinaryWriter writer, Type type, List<T> value, Convert convert) {
            int count = value.Count;
            writer.Write(count);
            if (count > 0) {
                foreach (var item in value) {
                    convert.Write(writer, item == null ? typeof(T) : item.GetType(), item);
                }
            }
        }
        protected override List<T> ReadObject(Type type, BinaryReader reader, Convert convert) {
            if (type == null) throw new InvalidOperationException("type");

            int count = reader.ReadInt32();
            List<T> value = Activator.CreateInstance(type) as List<T>;
            if (count > 0) {
                for (int i = 0; i < count; i++) {
                    T item = (T)convert.Read(reader);
                    value.Add(item);
                }
            }
            return value;
        }
    }
}
