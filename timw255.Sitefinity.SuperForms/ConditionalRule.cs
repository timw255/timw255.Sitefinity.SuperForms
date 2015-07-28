using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace timw255.Sitefinity.SuperForms
{
    [DataContract]
    internal class ConditionalRule
    {
        [DataMember(Name = "target")]
        public string Target { get; set; }

        [DataMember(Name = "action")]
        public string Action { get; set; }

        [DataMember(Name = "bool")]
        public string Bool { get; set; }

        [DataMember(Name = "fields")]
        public string[] Fields { get; set; }

        [DataMember(Name = "checks")]
        public List<CriteriaItem> Checks { get; set; }
    }
}