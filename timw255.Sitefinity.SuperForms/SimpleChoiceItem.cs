using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace timw255.Sitefinity.SuperForms
{
    [DataContract]
    internal class SimpleChoiceItem
    {
        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public string Value { get; set; }
    }
}