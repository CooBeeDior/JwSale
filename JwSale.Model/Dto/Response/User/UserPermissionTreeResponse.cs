using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Response
{
    public class UserPermissionTreeResponse : Function, IResponse
    {
        public IList<UserPermissionTreeResponse> Tree { get; set; }
    }

    public class Function
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }


        /// <summary>
        /// 是否有权限
        /// </summary>
        public bool IsPermission { get; set; }

    }
}
