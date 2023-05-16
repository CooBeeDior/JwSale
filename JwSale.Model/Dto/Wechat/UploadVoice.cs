using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class UploadVoiceRequest : WechatRequestBase
    {
        /// <summary>
        /// 接收账号
        /// </summary>
        public string recv_uin { get; set; }

        /// <summary>
        /// 音频文件 将语音转换为16进制进行提交
        /// </summary>
        public string voice { get; set; }

        /// <summary>
        /// 音频格式 	UNKNOWN -1，AMR 0，SPEEX 1，MP3 2，WAVE 3，SILK 4
        /// </summary>
        public string voiceFormat { get; set; }

        /// <summary>
        /// 语音时间 (毫秒)最长为65秒
        /// </summary>
        public string voiceLen { get; set; }

        /// <summary>
        /// 消息ID 音频没有上传完毕时ID不变
        /// </summary>
        public string clientmsgid { get; set; }
    }
}
