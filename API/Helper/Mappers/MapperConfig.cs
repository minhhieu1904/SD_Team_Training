using AgileObjects.AgileMapper;
using AgileObjects.AgileMapper.Configuration;
using API.DTOs.Report;
using API.Models;

namespace API.Helper.Mappers
{
    public class MapperConfig : MapperConfiguration
    {
        protected override void Configure()
        {
            // Mapper.GetPlanFor<MsQrSort>().ToANew<SortSumReportDTO>(cfg => cfg
            //         .Map((p, pvm) => p.Title + " " + p.Name)
            //         .To(pvm => p.Name));
        }
    }
}