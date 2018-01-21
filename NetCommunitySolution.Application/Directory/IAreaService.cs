using Abp.Application.Services;
using NetCommunitySolution.Domain.Directory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCommunitySolution.Directory
{
    public interface IAreaService: IApplicationService
    {
        IList<Area> GetAllArea();

        List<string> GetProvinces();

        List<string> GetCities(string provine);
        List<Area> GetCounty(string city);

        Area GetAreaByCode(string code);
    }
}
