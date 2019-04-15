using System;
using System.Linq;
using System.Windows.Forms;

namespace Bakalárska_práca
{
    /// <summary>
    /// Helper for Dialog
    /// </summary>
    public static class DialogHelper
    {
        /// <summary>
        /// Add filtering for dialog
        /// </summary>
        /// <typeparam name="T">Enum, which contain file extensions</typeparam>
        /// <param name="ofd">Dialog</param>
        /// <param name="filterName">Name of filter</param>
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
