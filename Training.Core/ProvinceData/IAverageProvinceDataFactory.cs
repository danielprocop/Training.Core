using System.Text;

namespace Training.Core.ProvinceData
{
    public interface IAverageProvinceDataFactory
{
    Result<AverageProvinceData> CreateProvinceAverageData(ProvinceData provinceData);
}

}
