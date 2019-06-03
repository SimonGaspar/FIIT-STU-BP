using System.Linq;
using System.Reflection;
using System.Resources;

namespace Bachelor_app.Resources
{
    public static class Localizer
    {
        private static ResourceManager mainResourse = Resources_en_EN.ResourceManager;

        public static void InitLocalizedResource(string languagePrefix, string resourseBase, string delimeter = "_")
        {
            string fullResourseName = resourseBase;
            Assembly assembly = Assembly.GetExecutingAssembly();

            System.Collections.Generic.List<string> ResList = assembly.GetManifestResourceNames().ToList();

            if (ResList.
                Where(x => x.Equals(fullResourseName + delimeter + languagePrefix + ".resources"))
                .Count() == 1)
            {
                fullResourseName += delimeter + languagePrefix;
            }

            mainResourse = new ResourceManager(fullResourseName, assembly);
        }

        public static string Localize(this string str)
        {
            return GetString(str);
        }

        public static string GetString(string name)
        {
            try
            {
                if (mainResourse == null)
                {
                    return name;
                }

                string result = mainResourse.GetString(name);
                return result ?? name;
            }
            catch
            {
                return name;
            }
        }
    }
}
