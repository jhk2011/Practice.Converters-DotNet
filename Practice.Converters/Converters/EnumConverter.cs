using System;
using System.IO;

namespace Practice.Converters {

    public class EnumConverter : Converter {

        public override bool CanConvert(Type type) {
            return type.IsEnum;
        }

        public override void Write(BinaryWriter writer, Type type, object value, Convert convert) {
            writer.Write((int)value);
        }

        public override object Read(BinaryReader reader, Type type, Convert convert) {
            int value = reader.ReadInt32();
            return value;
        }
    }
}
