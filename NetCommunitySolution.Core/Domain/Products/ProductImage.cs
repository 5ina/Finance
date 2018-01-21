using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace NetCommunitySolution.Domain.Products
{
    public class ProductImage : Entity
    {
        public int ProductId { get; set; }

        /// <summary>
        /// 默认图片
        /// </summary>
        public bool DefaultImage { get; set; }

        [MaxLength(500)]
        public string Url { get; set; }
    }
}
