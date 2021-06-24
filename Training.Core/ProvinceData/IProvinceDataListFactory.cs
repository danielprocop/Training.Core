using System.Collections.Generic;

namespace Training.Core.ProvinceData
{
    public interface IProvinceDataListFactory
    {
        IList<ProvinceData> CreateProvinceDataList(IList<ConsistentReading> consistentReadings);
    }

}
