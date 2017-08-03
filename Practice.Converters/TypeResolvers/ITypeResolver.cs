using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Practice.Converters {


    /// <summary>
    /// 定义如何写入和解析类型
    /// </summary>
    public interface ITypeResolver {
        void Write(BinaryWriter writer, Type type);
        Type Read(BinaryReader reader);
    }
}
