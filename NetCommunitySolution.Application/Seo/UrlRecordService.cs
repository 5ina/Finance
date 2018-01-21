using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Runtime.Caching;
using NetCommunitySolution.Domain.Seo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCommunitySolution.Seo
{
    public class UrlRecordService: NetCommunitySolutionAppServiceBase,IUrlRecordService
    {

        #region Constants

        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : entity ID
        /// {1} : entity name
        /// </remarks>
        private const string URLRECORD_ACTIVE_BY_ID_NAME_KEY = "net.urlrecord.active.id-name-{0}-{1}";
        /// <summary>
        /// Key for caching
        /// </summary>
        private const string URLRECORD_ALL_KEY = "net.urlrecord.all";
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : slug
        /// </remarks>
        private const string URLRECORD_BY_SLUG_KEY = "net.urlrecord.active.slug-{0}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string URLRECORD_PATTERN_KEY = "net.urlrecord.";

        #endregion

        #region Fields

        private readonly IRepository<UrlRecord> _urlRecordRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="urlRecordRepository">URL record repository</param>
        public UrlRecordService(ICacheManager cacheManager,
            IRepository<UrlRecord> urlRecordRepository)
        {
            this._cacheManager = cacheManager;
            this._urlRecordRepository = urlRecordRepository;
        }

        #endregion

        #region Utilities

        protected UrlRecordForCaching Map(UrlRecord record)
        {
            if (record == null)
                throw new ArgumentNullException("record");

            var urlRecordForCaching = new UrlRecordForCaching
            {
                Id = record.Id,
                EntityId = record.EntityId,
                EntityName = record.EntityName,
                Slug = record.Slug,
                IsActive = record.IsActive,
            };
            return urlRecordForCaching;
        }

        /// <summary>
        /// Gets all cached URL records
        /// </summary>
        /// <returns>cached URL records</returns>
        protected virtual IList<UrlRecordForCaching> GetAllUrlRecordsCached()
        {
            //cache
            string key = string.Format(URLRECORD_ALL_KEY);
            return _cacheManager.GetCache(key).Get(key, () =>
            {
                var query = from ur in _urlRecordRepository.GetAll()
                            select ur;
                var urlRecords = query.ToList();
                var list = new List<UrlRecordForCaching>();
                foreach (var ur in urlRecords)
                {
                    var urlRecordForCaching = Map(ur);
                    list.Add(urlRecordForCaching);
                }
                return list;
            });
        }

        #endregion

        #region Nested classes

        [Serializable]
        public class UrlRecordForCaching
        {
            public int Id { get; set; }
            public int EntityId { get; set; }
            public string EntityName { get; set; }
            public string Slug { get; set; }
            public bool IsActive { get; set; }
        }

        #endregion

        #region Methods
        
        public void DeleteUrlRecord(int urlRecordId)
        {
            if (urlRecordId <= 0)
                throw new ArgumentNullException("urlRecord");

            _urlRecordRepository.Delete(urlRecordId);

            //cache
            _cacheManager.RemoveByPattern(URLRECORD_PATTERN_KEY);
        }
        
        public void DeleteUrlRecords(IList<int> urlRecordIds)
        {
            if (urlRecordIds == null)
                throw new ArgumentNullException("urlRecords");

            _urlRecordRepository.Delete(u => urlRecordIds.Contains(u.Id));

            //cache
            _cacheManager.RemoveByPattern(URLRECORD_PATTERN_KEY);
        }
        
        public IList<UrlRecord> GetUrlRecordsByIds(int[] urlRecordIds)
        {
            var query = _urlRecordRepository.GetAll();

            return query.Where(p => urlRecordIds.Contains(p.Id)).ToList();
        }
        
        public UrlRecord GetUrlRecordById(int urlRecordId)
        {
            if (urlRecordId == 0)
                return null;

            return _urlRecordRepository.Get(urlRecordId);
        }
        
        public void InsertUrlRecord(UrlRecord urlRecord)
        {
            if (urlRecord == null)
                throw new ArgumentNullException("urlRecord");

            _urlRecordRepository.Insert(urlRecord);

            //cache
            _cacheManager.RemoveByPattern(URLRECORD_PATTERN_KEY);
        }
        
        public void UpdateUrlRecord(UrlRecord urlRecord)
        {
            if (urlRecord == null)
                throw new ArgumentNullException("urlRecord");

            _urlRecordRepository.Update(urlRecord);

            //cache
            _cacheManager.RemoveByPattern(URLRECORD_PATTERN_KEY);
        }
        
        public UrlRecord GetBySlug(string slug)
        {
            if (String.IsNullOrEmpty(slug))
                return null;

            var query = from ur in _urlRecordRepository.GetAll()
                        where ur.Slug == slug
                        orderby ur.IsActive descending, ur.Id
                        select ur;
            var urlRecord = query.FirstOrDefault();
            return urlRecord;
        }
        
        public UrlRecordForCaching GetBySlugCached(string slug)
        {
            if (String.IsNullOrEmpty(slug))
                return null;
            
            //gradual loading
            string key = string.Format(URLRECORD_BY_SLUG_KEY, slug);
            return _cacheManager.GetCache(key).Get(key, () =>
            {
                var urlRecord = GetBySlug(slug);
                if (urlRecord == null)
                    return null;

                var urlRecordForCaching = Map(urlRecord);
                return urlRecordForCaching;
            });
        }
        
        public IPagedResult<UrlRecord> GetAllUrlRecords(string slug = "",
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _urlRecordRepository.GetAll();
            if (!String.IsNullOrWhiteSpace(slug))
                query = query.Where(ur => ur.Slug.Contains(slug));
            query = query.OrderBy(ur => ur.Slug);

            var urlRecords = new PagedResult<UrlRecord>(query, pageIndex, pageSize);
            return urlRecords;
        }
        
        public string GetActiveSlug(int entityId, string entityName)
        {
            //gradual loading
            string key = string.Format(URLRECORD_ACTIVE_BY_ID_NAME_KEY, entityId, entityName);
            return _cacheManager.GetCache(key).Get(key, () =>
            {
                var source = _urlRecordRepository.GetAll();
                var query = from ur in source
                            where ur.EntityId == entityId &&
                            ur.EntityName == entityName &&
                            ur.IsActive
                            orderby ur.Id descending
                            select ur.Slug;
                var slug = query.FirstOrDefault();
                    if (slug == null)
                    slug = "";
                return slug;
            });
        }
        
        public void SaveSlug<T>(T entity, string slug) where T : Entity, ISlugSupported
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            int entityId = entity.Id;
            string entityName = typeof(T).Name;

            var query = from ur in _urlRecordRepository.GetAll()
                        where ur.EntityId == entityId &&
                        ur.EntityName == entityName 
                        orderby ur.Id descending
                        select ur;
            var allUrlRecords = query.ToList();
            var activeUrlRecord = allUrlRecords.FirstOrDefault(x => x.IsActive);

            if (activeUrlRecord == null && !string.IsNullOrWhiteSpace(slug))
            {
                //find in non-active records with the specified slug
                var nonActiveRecordWithSpecifiedSlug = allUrlRecords
                    .FirstOrDefault(x => x.Slug.Equals(slug, StringComparison.InvariantCultureIgnoreCase) && !x.IsActive);
                if (nonActiveRecordWithSpecifiedSlug != null)
                {
                    //mark non-active record as active
                    nonActiveRecordWithSpecifiedSlug.IsActive = true;
                    UpdateUrlRecord(nonActiveRecordWithSpecifiedSlug);
                }
                else
                {
                    //new record
                    var urlRecord = new UrlRecord
                    {
                        EntityId = entityId,
                        EntityName = entityName,
                        Slug = slug,
                        IsActive = true,
                    };
                    InsertUrlRecord(urlRecord);
                }
            }

            if (activeUrlRecord != null && string.IsNullOrWhiteSpace(slug))
            {
                activeUrlRecord.IsActive = false;
                UpdateUrlRecord(activeUrlRecord);
            }

            if (activeUrlRecord != null && !string.IsNullOrWhiteSpace(slug))
            {
                if (!activeUrlRecord.Slug.Equals(slug, StringComparison.InvariantCultureIgnoreCase))
                {
                    var nonActiveRecordWithSpecifiedSlug = allUrlRecords
                        .FirstOrDefault(x => x.Slug.Equals(slug, StringComparison.InvariantCultureIgnoreCase) && !x.IsActive);
                    if (nonActiveRecordWithSpecifiedSlug != null)
                    {
                        nonActiveRecordWithSpecifiedSlug.IsActive = true;
                        UpdateUrlRecord(nonActiveRecordWithSpecifiedSlug);
                        
                        activeUrlRecord.IsActive = false;
                        UpdateUrlRecord(activeUrlRecord);
                    }
                    else
                    {
                        var urlRecord = new UrlRecord
                        {
                            EntityId = entityId,
                            EntityName = entityName,
                            Slug = slug,
                            IsActive = true,
                        };
                        InsertUrlRecord(urlRecord);

                        //disable the previous active URL record
                        activeUrlRecord.IsActive = false;
                        UpdateUrlRecord(activeUrlRecord);
                    }

                }
            }
        }

        #endregion
    }
}
