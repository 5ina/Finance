using Abp.Runtime.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCommunitySolution
{
    public static class CacheExtensions
    {
        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="pattern"></param>
        public static void RemoveByPattern(this ICacheManager cache, string pattern)
        {
            var cacheList = cache.GetAllCaches();
            var patternList = cacheList.Where(c => c.Name.Contains(pattern)).ToList();

            patternList.ForEach(item => {
                cache.GetCache(item.Name).Remove(item.Name);
            });
        }

        /// <summary>
        /// 清空缓存
        /// </summary>
        /// <param name="cache"></param>
        public static void ClearCache(this ICacheManager cache)
        {
            var cacheList = cache.GetAllCaches();
            cacheList.ToList().ForEach(item =>
            {
                cache.GetCache(item.Name).Remove(item.Name);
            });
        }
    }
}
