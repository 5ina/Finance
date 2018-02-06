using Abp.Application.Services;
using NetCommunitySolution.Domain.Orders;
using NetCommunitySolution.Security.YeeDto;

namespace NetCommunitySolution.Security
{
    public interface IYeeSevice: IApplicationService
    {
        /// <summary>
        /// 创建易宝商户
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        int MchCreate(string mobile, int customerId);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="mchId"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        MchStatusResultModel MchQuery(int mchId, int customerId);

        /// <summary>
        ///  注册支付系统
        /// </summary>
        /// <param name="model"></param>
        /// <returns> true 注册成功 / false 注册失败</returns>
        bool Paymerchantreg(PaymerchantregModel model);

        /// <summary>
        /// 管理员将分润转账给商户并提现
        /// </summary>
        /// <param name="sysmch_id">快捷系统商户ID（管理员）</param>
        /// <param name="to_sysmch_id">快捷系统商户ID（商户）</param>
        /// <param name="type">类型,2总代理商将分润划分给商户并提现</param>
        /// <param name="transfer_amount">金额</param>
        /// <param name="out_trade_no">外部订单号</param>
        /// <param name="remark">备注</param>
        /// <param name="pay_type">支付方式，1 通道一支付，2通道二支付</param>
        /// <returns></returns>
        TransferStatus Transfer(int sysmch_id, int to_sysmch_id, int type, decimal transfer_amount, string out_trade_no, string remark, int pay_type);

        /// <summary>
        /// 管理员提现分润
        /// </summary>
        /// <param name="sysmch_id">快捷系统商户ID（管理员）</param>
        /// <param name="type">类型,2总代理商将分润划分给商户并提现</param>
        /// <param name="transfer_amount">金额</param>
        /// <param name="out_trade_no">外部订单号</param>
        /// <param name="pay_type">支付方式，1 通道一支付，2通道二支付</param>
        /// <returns></returns>
        TransferStatus ProfitTransfer(int sysmch_id, int type, decimal transfer_amount, string out_trade_no, int pay_type);

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="files"></param>
        /// <param name="mimeType"></param>
        /// <param name="suffix"></param>
        /// <returns></returns>
        string Uploadimage(byte[] files, string mimeType, string suffix);

        /// <summary>
        /// 支付
        /// </summary>
        /// <param name="sysmch_id"></param>
        /// <param name="total"></param>
        /// <param name="orderNo"></param>
        /// <param name="pay_type"></param>
        PaymentResultModel Payment(int sysmch_id, decimal total, string orderNo, int pay_type = 1);

        /// <summary>
        /// 费率查询
        /// </summary>
        /// <param name="sysmch_id">商户Id</param>
        /// <param name="product_type">1.交易手续费（单位千分之一） 3.提现手续费（单位元）</param>
        /// <param name="pay_type">支付方式</param>
        /// <returns></returns>
        RateResultModel QueryRate(int sysmch_id, int product_type, int pay_type = 1);

        /// <summary>
        /// 商户费率设置
        /// </summary>
        /// <param name="sysmch_id"></param>
        /// <param name="product_type">1.交易手续费（单位千分之一） 3.提现手续费（单位元）</param>
        /// <param name="rate"></param>
        /// <param name="pay_type">支付方式</param>
        /// <returns></returns>
        RateResultModel SetRate(int sysmch_id, int product_type, decimal rate, int pay_type = 1);

        /// <summary>
        /// 查询默认交易费率
        /// </summary>
        /// <returns></returns>
        DefaultQueryResultModel QueryDefaultRate();

        /// <summary>
        /// 设置默认交易费率
        /// </summary>
        /// <param name="def_rate">通道一默认交易费率（万分3 的值为3）</param>
        /// <param name="def_transferrate">通道一默认提现费率</param>
        /// <param name="tf_def_rate">通道二默认交易费率（万分3 的值为3）</param>
        /// <param name="tf_def_transferrate">通道二默认提现费率</param>
        /// <returns></returns>
        DefaultQueryResultModel SetDefaultRate(decimal def_rate, int def_transferrate, decimal tf_def_rate, int tf_def_transferrate);
    }
}
