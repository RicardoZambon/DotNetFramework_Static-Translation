using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Serialization;

namespace StaticTranslation.XML
{
    [Serializable, XmlRoot("TranslationFile")]
    public class TranslationFile
    {
        [XmlAttribute]
        public string Language { get; set; }

        [XmlIgnore]
        public CultureInfo LanguageInfo {
            get
            {
                return CultureInfo.GetCultureInfo(Language);
            }
        }

        [XmlArray("Translations"), XmlArrayItem("Translation", typeof(Translation))]
        public Translation[] Translations { get; set; }

        public TranslationFile()
        {
            Translations = new List<Translation>().ToArray();
        }
        public TranslationFile(string Language) : this()
        {
            this.Language = Language;
        }
        public TranslationFile(CultureInfo LanguageInfo) : this()
        {
            this.Language = LanguageInfo.Name;
        }
    }
}
