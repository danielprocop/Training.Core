using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Training.Core.ProvinceData
{
    public interface IAverageProvinceDataExportService
    {
        void Export(IList<AverageProvinceData> averageProvinceDataList, Stream outputStream);
    }

}
