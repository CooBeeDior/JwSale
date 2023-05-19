﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JwSale.Model.Dto.Request.Hospital
{
    public class UpdateDoctorRequest : RequestBase
    {
        [Required]
        public string Id { get; set; }
        /// <summary>
        /// 科室Id
        /// </summary>
        [Required]
        public string EpartmeneId { get; set; }

        /// <summary>
        /// 备注
        /// </summary> 
        public string Remark { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        //[EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        [Required]
        public string RealName { get; set; }

        /// <summary>
        /// 性别 1：男 2：女
        /// </summary>
        public int Sex { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? BirthDay { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public string IdCard { get; set; }

        /// <summary>
        /// qq号
        /// </summary>
        public string Qq { get; set; }
        /// <summary>
        /// 微信号
        /// </summary>
        public string WxNo { get; set; }
        /// <summary>
        /// 座机
        /// </summary>
        public string TelPhone { get; set; }
        /// <summary>
        /// 职位名称
        /// </summary>
        public string PositionName { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 区
        /// </summary>
        public string Area { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        //[Url]
        public string HeadImageUrl { get; set; }
    }
}
