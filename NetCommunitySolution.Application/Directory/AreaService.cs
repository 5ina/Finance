using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Runtime.Caching;
using NetCommunitySolution.Domain.Directory;

namespace NetCommunitySolution.Directory
{
    public class AreaService : NetCommunitySolutionAppServiceBase, IAreaService
    {
        #region Constants

        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// order id
        /// </remarks>
        private const string CACHE_AREA = "net.areas";
        private const string CACHE_AREA_BY_PARENTID = "net.areas.parentid.{0}";

        #endregion
        #region Fields

        private readonly IRepository<Area> _areaRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        public AreaService(IRepository<Area> areaRepository, ICacheManager cacheManager)
        {
            this._areaRepository = areaRepository;
            this._cacheManager = cacheManager;
        }        

        #endregion

        #region Method
        public IList<Area> GetAllArea()
        {
            return _cacheManager.GetCache(CACHE_AREA).Get(CACHE_AREA, () => {
                return _areaRepository.GetAllList();
            });
        }

        public Area GetAreaByCode(string code)
        {
            return  _areaRepository.FirstOrDefault(a => a.areaCode == code);
        }

        public List<string> GetCities(string provine)
        {
            var query = _areaRepository.GetAll();
            query = query.Where(a => a.Province == provine);
            var items = (from p in query orderby p.Id select p.City).Distinct().ToList();
            return items;
        }

        public List<Area> GetCounty(string city)
        {
            var query = _areaRepository.GetAll();
            query = query.Where(a => a.City == city).OrderBy(a => a.Id);
            return query.ToList();
        }

        public List<string> GetProvinces()
        {
            var query = _areaRepository.GetAll();
            var items = (from p in query select p.Province).Distinct().ToList();
            return items;
        }
        #endregion
    }
}
