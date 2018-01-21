using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace NetCommunitySolution.Domain.Products
{
    /// <summary>
    /// 商品类别
    /// </summary>
    public class Category : Entity
    {

        [Required, MaxLength(50)]
        public string Name { get; set; }               

        public int ParentId { get; set; }
        public bool Published { get; set; }
        public int DisplayOrder { get; set; }
        
        
    }
}
