using System.IO;

namespace Practice.Converters.Tests {
    public class FileStream2 : FileStream {

        FileStream fs;
        StreamWriter writer;
        public FileStream2(string path, FileMode m, FileAccess a) : base(path, m, a) {
            fs = new FileStream(path + ".txt", m, a);
            writer = new StreamWriter(fs);
        }

        public override void Write(byte[] array, int offset, int count) {
            base.Write(array, offset, count);
            foreach (var b in array) {
                writer.WriteLine("{0}({1})", b, (char)b);
                writer.Flush();
            }
        }

        public override void Close() {
            base.Close();
            fs.Flush();
            fs.Close();
        }
    }
}
