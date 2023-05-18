using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Request.Hospital
{
    public class GetItemTypesRequest : RequestPageBase
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>  
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
