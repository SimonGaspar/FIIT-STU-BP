using System;
using System.Linq;
using Bachelor_app.Resources;

namespace Bachelor_app.Extension
{
    public static class EnumExtension
    {
        public static string Display(this Enum type)
        {
            return Localizer.GetString(type.ToString());
        }

        public static T ReturnEnumValue<T>(string EnumStringValue) where T : Enum
        {
            return Enum.GetValues(typeof(T)).Cast<T>().First(x => x.Display() == EnumStringValue);
        }
    }
}
