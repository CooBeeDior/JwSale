using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    /// <summary>
    /// 同步收藏
    /// </summary>
    public class SyncCollectRequest : WechatRequestBase
    {

    }

    public class SyncCollectResponse
    {
        public int ret { get; set; }

        public cmdList cmdList { get; set; }

        public keyBuf keyBuf { get; set; }

        public int continueFlag { get; set; }
    }

    public class cmdList
    {
        public int count { get; set; }

        public IList<cmdList_list> list { get; set; }
    }

    public class cmdList_list
    {
        public int cmdid { get; set; }

        public cmdBuf cmdBuf { get; set; }
    }

    public class cmdBuf
    {
        public int len { get; set; }

        public byte[] data { get; set; }
    }

    public class keyBuf
    {
        public int iLen { get; set; }

        public byte[] buffer { get; set; }

    }

}
