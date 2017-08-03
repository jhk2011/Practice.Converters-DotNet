using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Practice.Converters.Tests {

    class Program {

        public static Type DetectType(List<object> list) {

            Type type = null;

            foreach (var item in list) {

                if (item == null) continue;

                Type cur = item.GetType();

                type = GetSameParent(type, cur);
            }

            return type;
        }

        private static Type GetSameParent(Type type, Type cur) {

            if (type == cur) return type;

            if (type == null) return cur;

            if (cur == null) return type;

            if (type.IsAssignableFrom(cur)) {
                return type;
            }

            if (cur.IsAssignableFrom(type)) {
                return cur;
            }

            if (type.BaseType == typeof(object) || cur.BaseType == typeof(object)) {

                var interfacec = type.GetInterfaces().Intersect(cur.GetInterfaces()).ToList();

                if (interfacec.Count == 1) {
                    return interfacec[0];
                }
            }

            return typeof(object);
        }

        static void Main(string[] args) {

            Person person = new Person {
                Guid = new Guid("ABCDEF12-1234-4578-9988-A1B2C3D4E5F6"),
                Id = 1,
                Name = "test",
                Grade = 3.14,
                Courses = new object[] { "语文", "英语", 123 },
                List = new List<string> { "item1", "item2" },
                Matrix = new int[][] {
                    new int[]{1,2,3 },new int[]{4,5,6}
                },
                Date=new DateTime(2088,8,8)

            };

            TypeNameTypeResolver typeNameResolver = new TypeNameTypeResolver();

            typeNameResolver.Add(new TypeNameMap(typeof(Person), "person"));
            typeNameResolver.Add(new TypeNameMap(typeof(MyClass), "myclass"));
            typeNameResolver.Add(new TypeNameMap(typeof(MyClass2), "myclass2"));
            typeNameResolver.Add(new TypeNameMap(typeof(ArrayClass)));

            //var resolver2 = new DefaultTypeResolver();

            var convert = new Convert(typeNameResolver, null);

            object obj = null;

            obj = new ArrayClass() {
                Array = new int[] { 1, 2, 3 }
            };

            //obj = new List<string>() { "1" };

            //obj = new List<List<String>> {
            //    new List<String>{"1"}
            //};

            obj = person;

            //obj = new List<string> { "1", "2", "800" };

            using (FileStream fs = new FileStream(@"F:/person.dat", FileMode.Create, FileAccess.ReadWrite)) {
                convert.Write(fs, obj);
            }

            Console.WriteLine();

            using (FileStream fs = new FileStream(@"F:/person.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite)) {
                object obj2 = convert.Read(fs);
                Console.WriteLine(obj2.GetType().Name);
            }

        }

        private static void TestTimeZone() {
            TimeZone zone = TimeZone.CurrentTimeZone;

            Console.WriteLine(zone.StandardName);

            var utcNow = DateTimeOffset.UtcNow;
            var now = DateTimeOffset.Now;

            var zones = TimeZoneInfo.GetSystemTimeZones();


            foreach (var z in zones) {

                var rules = z.GetAdjustmentRules();
                Console.WriteLine(z.DisplayName);
                foreach (var r in rules) {
                    //Console.WriteLine("{0}-{1}",r.DateStart,r.DateEnd);
                    //Console.WriteLine("{0}-{1}", r.DaylightTransitionStart, r.DaylightTransitionEnd);
                }
            }
        }
    }
}
