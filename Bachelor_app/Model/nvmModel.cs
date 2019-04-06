using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bachelor_app.Model
{
    public class nvmModel
    {
        public int imageCount { get; set; }
        public int pointCount { get; set; }

        public List<nvmPointModel> listPointModel { get; set; } = new List<nvmPointModel>();
        public List<nvmImageModel> listImageModel { get; set; } = new List<nvmImageModel>();
    }
}
