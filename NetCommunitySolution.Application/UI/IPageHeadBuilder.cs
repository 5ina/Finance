

namespace NetCommunitySolution.UI
{

    /// <summary>
    /// Page head builder
    /// </summary>
    public partial interface IPageHeadBuilder
    {
        /// <summary>
        /// 生成标题
        /// </summary>
        /// <returns></returns>
        string GenerateTitle();

        /// <summary>
        /// 生成说明
        /// </summary>
        /// <returns></returns>
        string GenerateMetaDescription();

        /// <summary>
        /// 生成关键字
        /// </summary>
        /// <returns></returns>
        string GenerateMetaKeywords();
    }
}
