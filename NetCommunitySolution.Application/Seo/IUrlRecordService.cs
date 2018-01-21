using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using NetCommunitySolution.Domain.Seo;
using System.Collections.Generic;

namespace NetCommunitySolution.Seo
{

    public interface IUrlRecordService: IApplicationService
    {
        /// <summary>
        /// 删除URL记录
        /// </summary>
        /// <param name="urlRecord">Url记录</param>
        void DeleteUrlRecord(int urlRecordId);

        /// <summary>
        /// 删除URL记录
        /// </summary>
        /// <param name="urlRecordIds">URL records</param>
        void DeleteUrlRecords(IList<int> urlRecordIds);

        /// <summary>
        /// 根据Id获取Url
        /// </summary>
        /// <param name="urlRecordId">id</param>
        /// <returns>URL record</returns>
        UrlRecord GetUrlRecordById(int urlRecordId);

        /// <summary>
        /// 根据Id获取Url
        /// </summary>
        /// <param name="urlRecordIds">ids</param>
        /// <returns>URL record</returns>
        IList<UrlRecord> GetUrlRecordsByIds(int[] urlRecordIds);

        /// <summary>
        /// 新增Url
        /// </summary>
        /// <param name="urlRecord">URL record</param>
        void InsertUrlRecord(UrlRecord urlRecord);

        /// <summary>
        /// 更新Url
        /// </summary>
        /// <param name="urlRecord">URL record</param>
        void UpdateUrlRecord(UrlRecord urlRecord);

        /// <summary>
        /// 查找Url记录
        /// </summary>
        /// <param name="slug">Slug</param>
        /// <returns>Found URL record</returns>
        UrlRecord GetBySlug(string slug);

        /// <summary>
        /// 查找Url（缓存版本）.
        /// </summary>
        /// <param name="slug">Slug</param>
        /// <returns>Found URL record</returns>
        UrlRecordService.UrlRecordForCaching GetBySlugCached(string slug);

        /// <summary>
        /// Gets all URL records
        /// </summary>
        /// <param name="slug">Slug</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>URL records</returns>
        IPagedResult<UrlRecord> GetAllUrlRecords(string slug = "", int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// 查找slug
        /// </summary>
        /// <param name="entityId">主键</param>
        /// <param name="entityName">实体对象</param>
        /// <returns>Found slug</returns>
        string GetActiveSlug(int entityId, string entityName);

        /// <summary>
        /// 保存记录
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="entity">Entity</param>
        /// <param name="slug">Slug</param>
        void SaveSlug<T>(T entity, string slug) where T : Entity, ISlugSupported;
    }
}
