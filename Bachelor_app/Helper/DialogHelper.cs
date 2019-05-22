using System;
using System.Linq;
using System.Windows.Forms;

namespace Bachelor_app
{
    /// <summary>
    /// Helper for Dialog
    /// </summary>
    public static class DialogHelper
    {
        /// <typeparam name="T">Enum, which contain file extensions</typeparam>
        /// <param name="ofd"></param>
        /// <param name="filterName"></param>
        public static void AddFilterToDialog<T>(OpenFileDialog ofd, string filterName) where T : Enum
        {
            var fileExtension = Enum.GetValues(typeof(T)).Cast<T>().Select(x => $"*.{x.ToString()}");

            if (string.IsNullOrEmpty(ofd.Filter))
                ofd.Filter = $"{filterName}|{string.Join(";", fileExtension)}";
            else
                ofd.Filter = $"{ofd.Filter}|{filterName}|{string.Join(";", fileExtension)}";
        }
    }
}
