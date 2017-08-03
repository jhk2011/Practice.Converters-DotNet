using System;
using System.IO;

namespace Practice.Converters {
    public class Int16Converter:Converter<Int16> {
        protected override short ReadObject(Type type, BinaryReader reader, Convert convert) {
            return reader.ReadInt16();
        }

        protected override void WriteObject(BinaryWriter writer, Type type, short value, Convert convert) {
            writer.Write(value);
        }
    }

}
