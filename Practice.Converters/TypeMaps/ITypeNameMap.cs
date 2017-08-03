using System;



namespace Practice.Converters {
    public interface ITypeNameMap {
        bool CanGetName(Type type);
        bool CanGetType(string name);
        string GetName(Type type);
        Type GetType(string name);
    }
}
