﻿using JwSale.Util.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JwSale.Model.Dto.Request
{
    /// <summary>
    /// 添加用户
    /// </summary>
    public class AddUserRequest : RequestBase
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        ///// <summary>
        ///// 密码
        ///// </summary>
        //[Required]
        //public string Password { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>    
        public string RealName { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [Phone]
        public string Phone { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        //[EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// 性别 1：男 2：女
        /// </summary>
        public SexType? Sex { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? BirthDay { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>

        public string IdCard { get; set; }

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
        /// 备注
        /// </summary>
        public string Remark { get; set; }


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
        /// 头像
        /// </summary>
        public string HeadImageUrl { get; set; }

        /// <summary>
        /// 类型 1:端口授权  2:月租授权
        /// </summary>
        public int Type { get; set; }


        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime? ExpiredTime { get; set; }

    }
}
