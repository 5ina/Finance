using NetCommunitySolution.Domain.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Areas.Admin.Models.Orders
{
    public class CashListModel
    {
        public CashListModel()
        {
            AvailableOrderStatuses = new List<SelectListItem>();
        }

        [DisplayName("开始时间")]
        [UIHint("DateNullable")]
        public DateTime? StartDate { get; set; }

        [DisplayName("结束时间")]
        [UIHint("DateNullable")]
        public DateTime? EndDate { get; set; }

        [DisplayName("订单状态")]
        public OrderStatus? OrderStatusId { get; set; }

        public IList<SelectListItem> AvailableOrderStatuses { get; set; }
    }
}