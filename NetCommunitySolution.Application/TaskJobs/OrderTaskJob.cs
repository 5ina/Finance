using Abp.BackgroundJobs;
using Abp.Dependency;
using NetCommunitySolution.Orders;

namespace NetCommunitySolution.TaskJobs
{
    public class OrderTaskJob : BackgroundJob<int>, ITransientDependency
    {        
        /// <summary>
        /// 订单支付完成后的通知
        /// </summary>
        /// <param name="args"></param>
        public override void Execute(int args)
        {
            var orderService = Abp.Dependency.IocManager.Instance.Resolve<IOrderService>();                        
        }
    }
}
