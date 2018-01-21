using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace NetCommunitySolution.Web.Models.BankCards
{
    public class CustomerCardsModel : EntityDto
    {

        public CustomerCardsModel()
        {
            this.CardItems = new List<BankCardModel>();
        }

        public IList<BankCardModel> CardItems { get; set; }
    }
}