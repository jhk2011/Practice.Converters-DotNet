using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;

namespace Practice.Converters {
    public class PropertyMetadataCollection : Collection<PropertyMetadata> {
        public PropertyMetadataCollection() {

        }

        public PropertyMetadataCollection(IEnumerable<PropertyMetadata> list)
            : base(list.ToList()) {

        }
    }
}
