using System;
using System.Xml.Serialization;

namespace StaticTranslation.XML
{
    [Serializable]
    public class Translation
    {
        [XmlAttribute]
        public string Key { get; set; }

        [XmlAttribute]
        public string Value { get; set; }

        public Translation()
        {

        }
        public Translation(string Key, string Value) : this()
        {
            this.Key = Key;
            this.Value = Value;
        }
    }
}
