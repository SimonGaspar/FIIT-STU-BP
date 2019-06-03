using System;
using System.Linq;
using Bachelor_app.Resources;

namespace Bachelor_app.Extension
{
    /// <summary>
    /// Extension for all enum.
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// Localization of enum string value.
        /// </summary>
        /// <param name="type"></param>
        /// <returns>String of enum value from localization.</returns>
        public static string Display(this Enum type)
        {
            return Localizer.GetString(type.ToString());
        }

        /// <summary>
        /// Localization of enum string value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumStringValue">String of enum value.</param>
        /// <returns>Enum value</returns>
        public static T ReturnEnumValue<T>(string enumStringValue)
            where T : Enum
        {
            return Enum.GetValues(typeof(T)).Cast<T>().First(x => x.Display() == enumStringValue);
        }
    }
}
