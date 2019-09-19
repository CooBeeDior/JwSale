using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class GetContactRequest : WechatRequestBase
    {
        /// <summary>
        /// 暂无用处填空即可
        /// </summary>
        public string v1 { get; set; }

        /// <summary>
        /// 单填获取联系人详情，配合chatroom则获取群友详情
        /// </summary>
        public string wxid { get; set; }

        /// <summary>
        /// 查询通讯录某成员时空,查询群友时必填
        /// </summary>
        public string chatroom { get; set; }
    }

    public class GetContactResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public BaseResponse baseResponse { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int contactCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ContactListItem> contactList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<int> ret { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<VerifyUserValidTicketListItem> verifyUserValidTicketList { get; set; }
    }
}
