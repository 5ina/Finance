using Abp.Application.Services.Dto;

namespace NetCommunitySolution.Web.Models.Articles
{

    public partial class TopicModel : EntityDto
    {
        public string SystemName { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public string MetaKeywords { get; set; }

        public string MetaDescription { get; set; }

        public string MetaTitle { get; set; }

        public string SeName { get; set; }

        public int TopicTemplateId { get; set; }
    }
}