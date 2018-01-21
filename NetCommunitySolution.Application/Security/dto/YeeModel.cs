using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCommunitySolution.Security.dto
{
    public  class YeeModel: IParamater
    {

        /// <summary>
        /// appId
        /// </summary>
        public string APPID { get; set; }

        /// <summary>
        /// 随机字符串
        /// </summary>
        public string nonce_str { get; set; }

        /// <summary>
        /// 请求时间戳
        /// </summary>
        public string timestamp { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public string version { get; set; }        
    }
}
