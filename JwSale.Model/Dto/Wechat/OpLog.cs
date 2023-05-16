using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class OpLogRequest : WechatRequestBase
    {
        /// <summary>
        /// 固定"64"
        /// </summary>
        public string cmdid { get; set; }

        /// <summary>
        /// 嵌套JSON
        /// </summary> 
        /// <remarks>
        /// 设置昵称包:
        /// {
        /// 	"cmdid": "64",
        /// 	"cmdbuf": "{\"bitFlag\":\"1\",\"str\":\"新昵称\"}",
        /// 	"token": "46b9ec1ad97d8b12813cf3cedafef159"
        /// }     
        /// 
        /// 设置更多资料包:
        /// {
        ///  "cmdid":"1",
        ///  "cmdbuf":"{\"bitFlag\":\"2178\",\"nickName\":\"昵称\",\"sex\":\"2\",\"province\":\"Zhejiang\",\"city\":\"Hangzhou\",\"signature\":\"个性签名666\",\"country\":\"CN\"}",
        ///   "token":"C6814E420DF5CEDF2ADE133CCF44DE6D"
        ///  }
        ///  
        /// </remarks>
        public string cmdbuf { get; set; }


    }

    public class WechatNickName
    {
        public string bitFlag { get; set; } = "64";

        public string str { get; set; }

    }

    public class WechatProfile
    {
        public string bitFlag { get; set; } = "2178";

        public string nickName { get; set; }

        public string province { get; set; }

        public string city { get; set; }

        public string signature { get; set; }

        public int sex { get; set; }
        public string country { get; set; } = "CN";
    }


}
