using System;
using System.Linq;


namespace Practice.Converters {
    public class GenericTypeMap : ITypeNameMap {

        public GenericTypeMap(TypeMap typeMap, Type gerericType, string gerericName) {
            this.TypeMap = typeMap;
            this.GerericType = gerericType;
            this.GerericName = gerericName;
        }

        public string GerericName { get; set; }
        public Type GerericType { get; set; }

        public TypeMap TypeMap { get; set; }

        protected string GetGerericName(Type type) {
            return GerericName;
        }

        protected Type GetGenericType(string genericName) {
            return GerericType;
        }

        protected bool CanGetGerericName(Type genericType, Type[] genericArgumentTypes) {
            return GerericType == genericType;
        }
        protected bool CanGetGerericType(string genericName, string[] genericArgumentNames) {
            return GerericName == genericName;
        }

        protected virtual string GetGenericName(Type type, Type[] genericArgumentTypes) {
            string gerericName = GetGerericName(type);

            string[] names = new string[genericArgumentTypes.Length];


            for (int i = 0; i < genericArgumentTypes.Length; i++) {
                string name = TypeMap.GetName(genericArgumentTypes[i]);
                if (name == null)
                    throw new InvalidOperationException(genericArgumentTypes[i].Name + "不能获取名称");

                names[i] = name;
            }

            //string[] names = genericArgumentTypes.Select(x => TypeMap.GetName(x)).ToArray();

            return string.Format("{0}<{1}>", gerericName, string.Join(",", names));
        }

        protected virtual Type GetGenericType(string genericName, string[] genericArgumentsNames) {
            Type gerericType = GetGenericType(genericName);
            Type[] type = genericArgumentsNames.Select(x => TypeMap.GetType(x)).ToArray();
            return gerericType.MakeGenericType(type);
        }

        public bool CanGetName(Type type) {

            if (!type.IsGenericType) return false;

            return CanGetGerericName(type.GetGenericTypeDefinition(), type.GetGenericArguments());
        }

        public bool CanGetType(string name) {

            if (!name.EndsWith(">")) return false;

            int index = name.IndexOf("<");

            if (index <= 0) return false;

            string genericParameter = name.Substring(index + 1, name.Length - index - 2);

            string genericName = name.Substring(0, index);

            return CanGetGerericType(genericName, genericParameter.Split(','));
        }

        public virtual string GetName(Type type) {
            return GetGenericName(type.GetGenericTypeDefinition(), type.GetGenericArguments());
        }

        public virtual Type GetType(string name) {

            if (!name.EndsWith(">")) throw new InvalidOperationException("name is not generic");

            int index = name.IndexOf("<");

            if (index <= 0) throw new ArgumentException("name is not generic");

            string genericParameter = name.Substring(index + 1, name.Length - index - 2);

            string genericName = name.Substring(0, index);

            return GetGenericType(genericName, genericParameter.Split(','));
        }
    }
}
