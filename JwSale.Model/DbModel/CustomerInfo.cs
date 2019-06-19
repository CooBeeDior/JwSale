using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwSale.Model
{
    /// <summary>
    /// 创建人：LTL
    /// 日  期：2019.06.11 10:12
    /// 描  述：CustomerInfo实体
    /// </summary>
    [Table("CustomerInfo")]
    public class CustomerInfo : Entity 
    { 
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 真实姓名拼音
        /// </summary>
        public string RealNamePin { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// qq号
        /// </summary>
        public string Qq { get; set; }
        /// <summary>
        /// 微信号
        /// </summary>
        public string WxNo { get; set; }
        /// <summary>
        /// 类型1：一般客户 2：大客户 3：代理商
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 字典
        /// </summary>
        public Guid TypeId { get; set; }
        /// <summary>
        /// 属性1：个人，2：个体户，3：企业，4：机构
        /// </summary>
        public string PropertyName { get; set; }
        /// <summary>
        /// 字典
        /// </summary>
        public Guid PropertyId { get; set; }
        /// <summary>
        /// 来源1：推广 2：客户转介3：大客户拜访 4：展会行会 5：个人人脉 
        /// </summary>
        public string OriginName { get; set; }
        /// <summary>
        /// 字典
        /// </summary>
        public Guid COriginId { get; set; }
        /// <summary>
        /// 推广Id
        /// </summary>
        public Guid GeneralizeInfoId { get; set; }
        /// <summary>
        /// 是否成交
        /// </summary>
        public bool IsDeal { get; set; }
        /// <summary>
        /// 复购次数
        /// </summary>
        public int ReBuy { get; set; }
        /// <summary>
        /// 转介绍人Id
        /// </summary>
        public Guid ParentId { get; set; }
        /// <summary>
        /// 接触时间
        /// </summary>
        public DateTime TouchTime { get; set; }
        /// <summary>
        /// 行业
        /// </summary>
        public string Industry { get; set; }
        /// <summary>
        /// 首次成交时间
        /// </summary>
        public DateTime FirstDealTime { get; set; }
        /// <summary>
        /// 最后跟进时间
        /// </summary>
        public DateTime LastFollowTime { get; set; }
        /// <summary>
        /// 代理商申请时间
        /// </summary>
        public DateTime ApplyTime { get; set; }
        /// <summary>
        /// 授权过期时间
        /// </summary>
        public DateTime AuthExpiredTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        
        /// <summary>
        /// 关联用户Id
        /// </summary>
        public Guid RelationUserId { get; set; }
        /// <summary>
        /// 关联用户名称
        /// </summary>
        public string RelationUserRealName { get; set; }
    }
}
