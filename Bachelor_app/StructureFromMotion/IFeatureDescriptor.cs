using Bakalárska_práca.Model;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakalárska_práca.StructureFromMotion
{
    public interface IFeatureDescriptor
    {
        Mat ComputeDescriptor(KeyPointModel keyPoints);

        void UpdateModel<T>(T model);
        void ShowSettingForm();
    }
}
