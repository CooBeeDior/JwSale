using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Response.Hospital
{
    public class GetItemResponse : IResponse
    {
 
        public string Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary> 
        public string Name { get; set; }
        /// <summary>
        /// 项目类别Id
        /// </summary> 
        public string ItemTypeId { get; set; }
       
  

        /// <summary>
        /// 编号
        /// </summary>
        public string ItemTypeCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>  
        public string ItemTypeName { get; set; }

       

        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 图片
        /// </summary>      
        public string ImageUrl { get; set; }

        /// <summary>
        /// 1:上架 2：下架
        /// </summary>
        public int Status { get; set; } = 1;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// UpdateTime
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// AddTime
        /// </summary>
        public DateTime AddTime { get; set; }
        /// <summary>
        /// AddUserId
        /// </summary>
        public string AddUserId { get; set; }
        /// <summary>
        /// UpdateUserId
        /// </summary>
        public string UpdateUserId { get; set; }
        /// <summary>
        /// AddUserRealName
        /// </summary>
        public string AddUserRealName { get; set; }
        /// <summary>
        /// UpdateUserRealName
        /// </summary>
        public string UpdateUserRealName { get; set; }
    }
}
