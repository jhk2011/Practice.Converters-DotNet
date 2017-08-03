using System;
using System.Collections.Generic;
using System.Linq;

namespace Practice.Converters {
    public class TypeMetadata {

        private static Dictionary<Type, TypeMetadata> _types = new Dictionary<Type, TypeMetadata>();

        public static TypeMetadata GetMetadata(Type type) {
            lock (_types) {
                if (_types.ContainsKey(type)) {
                    return _types[type];
                }
                var properties = type.GetProperties().Select(x => new PropertyMetadata(x));
                TypeMetadata metadata = new TypeMetadata {
                    Type = type,
                    Properties = new PropertyMetadataCollection(properties)
                };
                _types.Add(type, metadata);
                return metadata;
            }
        }

        public Type Type { get; set; }
        public PropertyMetadataCollection Properties { get; set; }
    }
}
