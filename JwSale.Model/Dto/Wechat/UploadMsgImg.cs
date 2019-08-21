using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class UploadMsgImgRequest : WechatRequestBase
    {
        /// <summary>
        /// 接收账号
        /// </summary>
        public string recv_uin { get; set; }

        /// <summary>
        /// 图片数据 将图片转换为16进制进行提交
        /// </summary>
        public string image { get; set; }

        /// <summary>
        /// 图片总长 
        /// </summary>
        public string totalLen { get; set; }

        /// <summary>
        /// 原图的某位置 当前上传的数据是原图的某位置
        /// </summary>
        public string startPos { get; set; }

        /// <summary>
        /// 消息ID 图片没有上传完毕时ID不变
        /// </summary>
        public string clientmsgid { get; set; }
    }
}
