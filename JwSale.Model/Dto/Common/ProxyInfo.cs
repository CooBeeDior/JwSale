using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Common
{
    public class ProxyInfo
    {
        /// <summary>
        /// 代理Ip
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 代理端口号
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        ///  代理用户名（可以为空）
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        ///  代理密码（可以为空）
        /// </summary>
        public string PassWord { get; set; }
 
    }
}
