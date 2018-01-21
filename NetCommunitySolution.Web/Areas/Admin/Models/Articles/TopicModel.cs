using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using NetCommunitySolution.Domain.Articles;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Areas.Admin.Models.Articles
{

    [AutoMap(typeof(Topic))]
    public partial class TopicModel : EntityDto
    {

        [DisplayName("系统名称")]
        [AllowHtml]
        public string SystemName { get; set; }
        

        [DisplayName("首页显示")]
        public bool IncludeInTopMenu { get; set; }

        [DisplayName("权重")]
        public int DisplayOrder { get; set; }

        [DisplayName("网站关闭可以访问")]
        public bool AccessibleWhenStoreClosed { get; set; }

        [DisplayName("URL")]
        [AllowHtml]
        public string Url { get; set; }

        [DisplayName("标题")]
        [AllowHtml]
        public string Title { get; set; }

        [DisplayName("内容")]
        [AllowHtml]
        [UIHint("Editor")]
        public string Body { get; set; }

        [DisplayName("是否发布")]
        public bool Published { get; set; }

        [DisplayName("MetaKeywords")]
        [AllowHtml]
        public string MetaKeywords { get; set; }

        [DisplayName("MetaDescription")]
        [AllowHtml]
        public string MetaDescription { get; set; }

        [DisplayName("MetaTitle")]
        [AllowHtml]
        public string MetaTitle { get; set; }

        [DisplayName("别名")]
        [AllowHtml]
        public string SeName { get; set; }        
                

    }
    
}