using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StaticTranslation.XML
{
    [Serializable]
    public class Language
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlIgnore]
        public CultureInfo NameInfo
        {
            get
            {
                return CultureInfo.GetCultureInfo(Name);
            }
        }
    }
}
