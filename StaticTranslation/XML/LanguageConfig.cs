using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StaticTranslation.XML
{
    [Serializable, XmlRoot("LanguageConfig")]
    public class LanguageConfig
    {
        [XmlAttribute]
        public string DefaultLanguage { get; set; }

        [XmlIgnore]
        public CultureInfo DefaultLanguageInfo
        {
            get
            {
                return CultureInfo.GetCultureInfo(DefaultLanguage);
            }
        }

        [XmlAttribute]
        public string BaseLanguage { get; set; }

        [XmlIgnore]
        public CultureInfo BaseLanguageInfo
        {
            get
            {
                return CultureInfo.GetCultureInfo(BaseLanguage);
            }
        }

        [XmlArray("Languages"), XmlArrayItem("Language", typeof(Language))]
        public Language[] Languages { get; set; }


        public LanguageConfig()
        {
            Languages = new List<Language>().ToArray();
        }
    }
}
