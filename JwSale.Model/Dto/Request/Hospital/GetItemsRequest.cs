using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Request.Hospital
{
    public class GetItemsRequest : RequestPageBase
    {   
        /// <summary>
           /// 名称
           /// </summary> 
        public string Name { get; set; }
        /// <summary>
        /// 项目类别Id
        /// </summary> 
        public string ItemTypeId { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
