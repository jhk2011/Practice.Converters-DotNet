using System;
using System.Collections.Generic;
using System.Linq;

namespace Practice.Converters {

    public class DateTimeConverter : Converter<DateTime> {

        static DateTime date = new DateTime(1970, 1, 1);

        protected override byte[] GetBytes(Type type, DateTime value, Convert convert) {

            long millseconds = (value.Ticks - date.Ticks) / 10000;

            Console.WriteLine("writer date {0}", millseconds);

            return BitConverter.GetBytes(millseconds);
        }

        protected override DateTime GetValue(Type type, byte[] bytes, Convert convert) {

            long millseconds = BitConverter.ToInt64(bytes, 0);

            Console.WriteLine("read date {0}", millseconds);


            if (millseconds < DateTime.MinValue.Ticks / 10000) {
                return DateTime.MinValue;
            }

            if (millseconds > DateTime.MaxValue.Ticks  / 10000) {
                return DateTime.MaxValue;
            }

            return date.AddMilliseconds(millseconds);
        }
    }

}
