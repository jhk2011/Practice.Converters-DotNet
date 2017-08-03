using System;
using System.IO;

namespace Practice.Converters {
    public class BooleanConverter : Converter<Boolean>
    {
        protected override bool ReadObject(Type type, BinaryReader reader, Convert convert) {
            return reader.ReadBoolean();
        }

        protected override void WriteObject(BinaryWriter writer, Type type, bool value, Convert convert) {
            writer.Write(value);
        }

    }

}
