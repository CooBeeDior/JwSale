using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class MmSnsUploadRequest:WechatRequestBase
    {
        /// <summary>
        /// 图片数据 将图片转换为16进制进行提交
        /// </summary>
        public string image { get; set; }

        /// <summary>
        /// 原图的某位置
        /// </summary>
        public string startPos { get; set; }

        /// <summary>
        /// 图片总长
        /// </summary>
        public string totalLen { get; set; }

        /// <summary>
        /// 消息ID 每次发消息自增1，发送成功会返回该值 【必须是正整数】
        /// </summary>
        public string clientmsgid { get; set; }
    }

}
