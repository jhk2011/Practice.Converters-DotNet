using System;
using System.IO;

namespace Practice.Converters {
    public class SingleConverter:Converter<Single> {
        protected override float ReadObject(Type type, BinaryReader reader, Convert convert) {
            return reader.ReadSingle();
        }

        protected override void WriteObject(BinaryWriter writer, Type type, float value, Convert convert) {
            writer.Write(value);
        }
    }

}
