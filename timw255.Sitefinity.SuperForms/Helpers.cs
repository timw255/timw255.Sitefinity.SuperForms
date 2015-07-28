using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.UI;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields;

namespace timw255.Sitefinity.SuperForms
{
    internal static class Helpers
    {
        public static bool IsNot<T>(this object obj)
        {
            return !(obj is T);
        }

        public static string GetFieldName(FieldControl fieldControl)
        {
            IFormFieldControl formFieldControl = (IFormFieldControl)fieldControl;

            if (string.IsNullOrEmpty(formFieldControl.MetaField.FieldName))
            {
                return string.Concat(fieldControl.GetType().Name, "_", ((Control)fieldControl).ID.ToString());
            }
            else
            {
                return formFieldControl.MetaField.FieldName;
            }
        }

        public static string SerializeJSON<T>(T t)
        {
            string jsonString = String.Empty;

            using (MemoryStream stream = new MemoryStream())
            {
                DataContractJsonSerializer ds = new DataContractJsonSerializer(typeof(T));
                ds.WriteObject(stream, t);
                jsonString = Encoding.UTF8.GetString(stream.ToArray());
            }

            return jsonString;
        }

        public static T DeserializeJSON<T>(string json)
        {
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(json)))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                return (T)serializer.ReadObject(ms);
            }
        }

        public static string GenerateTargetId()
        {
            return String.Format(CultureInfo.InvariantCulture, "{0:d9}", (DateTime.Now.Ticks / 10) % 1000000000);
        }
    }
}