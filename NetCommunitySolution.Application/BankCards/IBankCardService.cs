using Abp.Application.Services;
using Abp.Application.Services.Dto;
using NetCommunitySolution.Domain.BankCards;
using System.Collections.Generic;

namespace NetCommunitySolution.BankCards
{
    /// <summary>
    /// 银行卡服务接口
    /// </summary>
    public interface IBankCardService : IApplicationService
    {
        /// <summary>
        /// 新增银行卡
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        int CreateCard(BankCard card);

        /// <summary>
        /// 更新银行卡
        /// </summary>
        /// <param name="card"></param>
        void UpdateCard(BankCard card);


        /// <summary>
        /// 更新银行卡
        /// </summary>
        /// <param name="cardId"></param>
        BankCard GetCardById(int cardId);

        /// <summary>
        /// 删除银行卡
        /// </summary>
        /// <param name="cardId"></param>
        void DeleteCardById(int cardId);

        /// <summary>
        /// 获取我的银行卡
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="cardMode"></param>
        /// <returns></returns>
        IList<BankCard> GetMyCards(int customerId, BankCardMode? cardMode = null);

        /// <summary>
        /// 获取所有的银行卡
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="cardMode"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IPagedResult<BankCard> GetAllCards(int customerId= 0, BankCardMode? cardMode = null,
            int? bank = null, int pageIndex = 0, int pageSize = int.MaxValue);

    }
}
