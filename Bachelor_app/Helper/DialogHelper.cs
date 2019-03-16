using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bakalárska_práca
{
    public static class DialogHelper
    {
        public static void AddFilterToDialog<T>(OpenFileDialog ofd, string filterName) where T: Enum
        {
            var fileExtension = Enum.GetValues(typeof(T)).Cast<T>().Select(x => $"*.{x.ToString()}");
            if (string.IsNullOrEmpty(ofd.Filter))
                ofd.Filter = $"{filterName}|{string.Join(";", fileExtension)}";
            else
                ofd.Filter = $"{ofd.Filter}|{filterName}|{string.Join(";", fileExtension)}";
        }
    }
}
