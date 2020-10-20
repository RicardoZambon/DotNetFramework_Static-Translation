using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace StaticTranslation
{
    public static class Translation
    {
        private static CultureInfo defaultLanguage;
        public static CultureInfo DefaultLanguage { get { return defaultLanguage; } }

        private static CultureInfo baseLanguage { get; set; }
        public static CultureInfo BaseLanguage { get { return baseLanguage; } }

        public static CultureInfo ActiveLanguage { get; set; }
        internal static XML.Translation[] ActiveTranslations
        {
            get
            {
                return LoadedTranslations[ActiveLanguage].Translations;
            }
            set
            {
                LoadedTranslations[ActiveLanguage].Translations = value;
            }
        }

        private static Dictionary<CultureInfo, XML.TranslationFile> loadedTranslations;
        internal static Dictionary<CultureInfo, XML.TranslationFile> LoadedTranslations
        {
            get
            {
                if (loadedTranslations == null)
                    loadedTranslations = new Dictionary<CultureInfo, XML.TranslationFile>();
                return loadedTranslations;
            }
            set
            {
                loadedTranslations = value;
            }
        }


        internal static string GetAppPath()
        {
            return Directory.GetCurrentDirectory();
        }
        internal static string GetConfigFullPath(string Path = "")
        {
            if (Path == string.Empty)
                Path = GetAppPath();
            return Path + @"\Translation.xmlconfig";
        }
        internal static string GetFileFullPath(string LanguageName, string Path = "")
        {
            if (Path == string.Empty)
                Path = GetAppPath();
            return Path + @"\Translation." + LanguageName + ".xmltranslation";
        }


        public static XML.LanguageConfig LoadConfigFromFile(string Path = "")
        {
            var serializer = new XmlSerializer(typeof(XML.LanguageConfig));

            Path = GetConfigFullPath(Path);
            if (!File.Exists(Path))
            {
                var file = File.Create(Path);
                file.Close();
            }

            XML.LanguageConfig config;
            using (var reader = new StreamReader(Path))
                try
                {
                    config = (XML.LanguageConfig)serializer.Deserialize(reader);
                }
                catch
                {
                    config = new XML.LanguageConfig();
                }

            return config;
        }
        public static void LoadTranslationsFromFile(string Path = "")
        {
            if (LoadedTranslations != null)
                LoadedTranslations.Clear();

            var config = LoadConfigFromFile(Path);
            if (config != null && config.Languages.Count() > 0)
            {
                defaultLanguage = config.DefaultLanguageInfo;
                baseLanguage = config.BaseLanguageInfo;

                if (ActiveLanguage == null)
                    ActiveLanguage = DefaultLanguage;

                foreach (var language in config.Languages)
                    LoadedTranslations.Add(language.NameInfo, LoadLanguagesFromFile(language.NameInfo, Path));
            }
        }
        public static XML.TranslationFile LoadLanguagesFromFile(CultureInfo Language, string Path = "")
        {
            if (Path == string.Empty)
                Path = Directory.GetCurrentDirectory();

            var serializer = new XmlSerializer(typeof(XML.TranslationFile));

            Path = GetFileFullPath(Language.Name, Path);
            if (!File.Exists(Path))
            {
                var file = File.Create(Path);
                file.Close();
            }

            XML.TranslationFile translation;
            using (var reader = new StreamReader(Path))
                try
                {
                    translation = (XML.TranslationFile)serializer.Deserialize(reader);
                }
                catch
                {
                    translation = new XML.TranslationFile(Language);
                }

            return translation;
        }

        public static void SaveConfigToFile(string Path = "")
        {
            if (LoadedTranslations == null || LoadedTranslations.Count == 0)
                return;

            if (BaseLanguage == null)
                baseLanguage = LoadedTranslations.First().Key;

            if (DefaultLanguage == null)
                defaultLanguage = LoadedTranslations.First().Key;

            var config = new XML.LanguageConfig() { BaseLanguage = BaseLanguage.Name, DefaultLanguage = DefaultLanguage.Name };
            var list = new List<XML.Language>();
            foreach(var language in LoadedTranslations.Values)
            {
                list.Add(new XML.Language() { Name = language.Language });
            }
            config.Languages = list.ToArray();

            var serializer = new XmlSerializer(typeof(XML.LanguageConfig));
            using (var writer = new StreamWriter(GetConfigFullPath(Path)))
                serializer.Serialize(writer, config);
        }
        public static void SaveTranslationsToFile(string Path = "")
        {
            if (LoadedTranslations == null || LoadedTranslations.Count == 0)
                return;

            SaveConfigToFile(Path);

            var serializer = new XmlSerializer(typeof(XML.TranslationFile));
            foreach (var language in LoadedTranslations.Values)
            {
                using (var writer = new StreamWriter(GetFileFullPath(language.Language, Path)))
                    serializer.Serialize(writer, language);
            }
        }

        public static string GetTranslatedValue(string Key)
        {
            if (LoadedTranslations.ContainsKey(ActiveLanguage) && LoadedTranslations[ActiveLanguage].Translations.Count(x => x.Key == Key) > 0)
                return LoadedTranslations[ActiveLanguage].Translations.First(x => x.Key == Key).Value;
            else
            {
#if !DEBUG
                if (LoadedTranslations.ContainsKey(BaseLanguage) && LoadedTranslations[BaseLanguage].Translations.Count(x => x.Key == Key) > 0)
                    return LoadedTranslations[BaseLanguage].Translations.First(x => x.Key == Key).Value;
#endif
                return string.Format("#{0}#", Key);
            }

        }


        public static void CreateTranslation(string Language, bool ActivateLanguage = true)
        {
            if (Language == string.Empty)
                return;

            CultureInfo languageInfo = CultureInfo.GetCultureInfo(Language);
            if (languageInfo == null)
                return;

            if (LoadedTranslations == null || LoadedTranslations.Count == 0)
                LoadTranslationsFromFile();

            if (!LoadedTranslations.ContainsKey(languageInfo))
                LoadedTranslations.Add(languageInfo, new XML.TranslationFile(languageInfo));

            if (ActivateLanguage)
                ActiveLanguage = languageInfo;
        }
        public static void CreateTranslatedValue(string Key, string Value)
        {
            if (Key == string.Empty)
                return;

            if (ActiveTranslations.Where(x => x.Key == Key).Count() == 0)
            {
                var list = ActiveTranslations.ToList();
                list.Add(new XML.Translation(Key, Value));
                ActiveTranslations = list.ToArray();
            }
            else
                UpdatedTranslatedValue(Key, Value);

        }
        public static void UpdatedTranslatedValue(string Key, string Value)
        {
            if (Key == string.Empty)
                return;

            if (ActiveTranslations.Where(x => x.Key == Key).Count() == 0)
                CreateTranslatedValue(Key, Value);
            else
            {

            }
        }
        public static void RemoveTranslatedValue(string Key, string Value)
        {

        }
    }
}
