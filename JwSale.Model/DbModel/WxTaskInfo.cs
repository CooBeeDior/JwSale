﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JwSale.Model
{
    /// <summary>
    /// 拉群微信Id
    /// </summary>
    [Table("WxTaskInfo")]
    public class WxTaskInfo : Entity
    {
        /// <summary>
        /// 任务Id
        /// </summary>
        public Guid ChatRoomTaskInfoId { get; set; }


        /// <summary>
        /// 微信Id
        /// </summary>
        public string WxId { get; set; }


        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string HeadImgUrl { get; set; }


        /// <summary>
        /// 状态 0：未开启  1：开启
        /// </summary>
        public int Status { get; set; }
    }
}
