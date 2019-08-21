using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class UploadhdHeadImgRequest : WechatRequestBase
    {
        /// <summary>
        /// 将图片转换为16进制进行提交
        /// </summary>
        public string image { get; set; }

        /// <summary>
        /// 图片总长
        /// </summary>
        public string totalLen { get; set; }


        /// <summary>
        /// 当前上传的数据是原图的某位置
        /// </summary>
        public string startPos { get; set; }

   
    }
}
