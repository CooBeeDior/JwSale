using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Response.Hospital
{
    public class GetEpartmeneResponse : IResponse
    {
        public string Id { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 位置
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 状态 默认赋值：1
        /// </summary>
        public int Status { get; set; } = 1;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 医院Id
        /// </summary>
        public string HospitalId { get; set; }

        /// <summary>
        /// 医院编码
        /// </summary>
        public string HospitalCode { get; set; }
        /// <summary>
        /// 医院名称
        /// </summary>
        public string HospitalName { get; set; }
        /// <summary>
        /// 医院全称
        /// </summary>
        public string HospitalFullName { get; set; }

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
