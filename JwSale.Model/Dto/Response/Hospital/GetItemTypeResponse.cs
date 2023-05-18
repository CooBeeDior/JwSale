using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Response.Hospital
{
    public class GetItemTypeResponse : IResponse
    {
        public string Id { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 收费项目数量
        /// </summary>
        public int ItemCount { get; set; }

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
