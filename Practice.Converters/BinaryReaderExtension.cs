using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Practice.Converters
{
    public static class BinaryReaderExtension
    {

        public static DateTime ReadDateTime(this BinaryReader reader)
        {
            return DateTime.FromBinary(reader.ReadInt64());
        }

        public static Guid ReadGuid(this BinaryReader reader)
        {
            return new Guid(reader.ReadBytes(16));
        }

        public static byte[] ReadBytesSection(this BinaryReader reader)
        {
            int length = reader.ReadInt32();
            if (length < 0) throw new InvalidOperationException("长度不能为负数");
            if (length == 0) return null;
            return reader.ReadBytes(length);
        }

        public static string ReadStringSection(this BinaryReader reader, Encoding encoding)
        {
            byte[] buffer = ReadBytesSection(reader);
            if (buffer != null)
            {
                return encoding.GetString(buffer);
            }
            return null;
        }

        public static string ReadStringSection(this BinaryReader reader)
        {
            return ReadStringSection(reader, Encoding.UTF8);
        }

        public static Int32 ReadInt32Section(this BinaryReader reader)
        {
            reader.ReadInt32();
            return reader.ReadInt32();
        }


        public static Boolean ReadBooleanSection(this BinaryReader reader)
        {
            int length = reader.ReadInt32();
            if (length != 1)
            {
                throw new InvalidOperationException("");
            }
            return reader.ReadBoolean();
        }

        public static DateTime ReadDateTimeSection(this BinaryReader reader)
        {
            reader.ReadInt32();
            return DateTime.FromBinary(reader.ReadInt64());
        }



        public static Guid ReadGuidSection(this BinaryReader reader)
        {
            return new Guid(reader.ReadBytesSection());
        }
    }
}
