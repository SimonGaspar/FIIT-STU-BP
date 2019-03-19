using System;
using Bachelor_app.Resources;

namespace Bachelor_app.Extension
{
    public static class EnumExtension
    {
        public static string Display(this Enum type)
        {
            return Localizer.GetString(type.ToString());
        }
    }
}
