using StaticTranslation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticTranslationTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Iniciando criação do arquivo de config");

            //Translation.CreateTranslation("pt-BR");
            //Console.WriteLine("pt-BR");
            //Translation.CreateTranslation("en-US");
            //Console.WriteLine("en-US");
            //Translation.CreateTranslation("es-ES");
            //Console.WriteLine("es-ES");
            //Translation.CreateTranslation("it-IT");
            //Console.WriteLine("it-IT");
            //Translation.CreateTranslation("fr-FR");
            //Console.WriteLine("fr-FR");
            //Translation.CreateTranslation("de-DE");
            //Console.WriteLine("de-DE");
            //Translation.CreateTranslation("ru-RU");
            //Console.WriteLine("ru-RU");
            //Translation.CreateTranslation("zh-CN");
            //Console.WriteLine("zh-CN");
            //Translation.CreateTranslation("ar-DZ");
            //Console.WriteLine("ar-DZ");
            //Translation.CreateTranslation("ja-JP");
            //Console.WriteLine("`ja-JP");
            //Translation.CreateTranslation("tr-TR");
            //Console.WriteLine("tr-TR");
            //Translation.CreateTranslation("bo-CN");
            //Console.WriteLine("bo-CN");

            //Console.WriteLine("Salvando arquivo...");
            //Translation.SaveConfigToFile();
            //Console.WriteLine("Arquivo salvo");


            //Translation.LoadTranslationsFromFile();

            //string[] languages = new string[] { "pt-BR", "en-US", "es-ES", "it-IT", "fr-FR", "de-DE", "ru-RU", "zh-CN", "ar-DZ", "ja-JP", "tr-TR", "bo-CN" };
            //foreach(string language in languages)
            //{
            //    Console.WriteLine(language);
            //    Translation.ActiveLanguage = CultureInfo.GetCultureInfo(language);
            //    for (int i = 0; i <= 15000; i++)
            //    {
            //        Translation.CreateTranslatedValue("_" + i.ToString(), i.ToString());
            //        if (i % 3000 == 0)
            //            Console.WriteLine(i.ToString());
            //    }

            //}
            //Translation.SaveTranslationsToFile();

            Console.WriteLine("Iniciando leitura");
            Translation.LoadTranslationsFromFile();
            Console.WriteLine(Translation.GetTranslatedValue("_100"));
            Console.WriteLine("Concluido");
            Console.Read();

        }
    }
}
