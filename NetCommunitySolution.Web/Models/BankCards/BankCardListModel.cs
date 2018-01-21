using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetCommunitySolution.Web.Models.BankCards
{
    public class BankCardListModel
    {

        public BankCardListModel()
        {
            this.Cards = new List<BankCardModel>();
        }

        public int CustomerId { get; set; }

        public IList<BankCardModel> Cards { get; set; }
    }
}