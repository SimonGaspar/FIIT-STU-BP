using System.Linq;
using System.Reflection;
using System.Resources;

namespace Bachelor_app.Resources
{
    public static class Localizer
    {
        private static ResourceManager MainResourse = Resources_en_EN.ResourceManager;

        public static void InitLocalizedResource(string LanguagePrefix, string ResourseBase, string Delimeter = "_")
        {
            string FullResourseName = ResourseBase;
            Assembly assembly = Assembly.GetExecutingAssembly();

            System.Collections.Generic.List<string> ResList = assembly.GetManifestResourceNames().ToList();

            if (ResList.
                Where(x => x.Equals(FullResourseName + Delimeter + LanguagePrefix + ".resources"))
                .Count() == 1)
            {
                FullResourseName += Delimeter + LanguagePrefix;
            }

            MainResourse = new ResourceManager(FullResourseName, assembly);
        }

        public static string Localize(this string str)
        {
            return GetString(str);
        }

        public static string GetString(string name)
        {
            try
            {
                if (MainResourse == null)
                {
                    return name;
                }

                string result = MainResourse.GetString(name);
                return result ?? name;
            }
            catch
            {
                return name;
            }
        }
    }
}
