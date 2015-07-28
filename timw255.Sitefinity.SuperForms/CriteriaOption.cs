using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace timw255.Sitefinity.SuperForms
{
    [DataContract]
    internal class CriteriaOption
    {
        [DataMember(Name = "fieldName")]
        public string FieldName { get; set; }

        [DataMember(Name = "fieldId")]
        public string FieldId { get; set; }

        [DataMember(Name = "fieldType", EmitDefaultValue = false)]
        public string FieldType { get; set; }

        [DataMember(Name = "conditions", EmitDefaultValue = false)]
        public List<SimpleChoiceItem> Conditions { get; set; }

        [DataMember(Name = "options", EmitDefaultValue = false)]
        public List<SimpleChoiceItem> Options { get; set; }
    }
}