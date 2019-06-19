using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwSale.Model
{
    /// <summary>
    /// �����ˣ�LTL
    /// ��  �ڣ�2019.06.11 10:12
    /// ��  ����CustomerInfoʵ��
    /// </summary>
    [Table("CustomerInfo")]
    public class CustomerInfo : Entity 
    { 
        /// <summary>
        /// ��ʵ����
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// ��ʵ����ƴ��
        /// </summary>
        public string RealNamePin { get; set; }
        /// <summary>
        /// �ֻ�����
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// qq��
        /// </summary>
        public string Qq { get; set; }
        /// <summary>
        /// ΢�ź�
        /// </summary>
        public string WxNo { get; set; }
        /// <summary>
        /// ����1��һ��ͻ� 2����ͻ� 3��������
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// �ֵ�
        /// </summary>
        public Guid TypeId { get; set; }
        /// <summary>
        /// ����1�����ˣ�2�����廧��3����ҵ��4������
        /// </summary>
        public string PropertyName { get; set; }
        /// <summary>
        /// �ֵ�
        /// </summary>
        public Guid PropertyId { get; set; }
        /// <summary>
        /// ��Դ1���ƹ� 2���ͻ�ת��3����ͻ��ݷ� 4��չ���л� 5���������� 
        /// </summary>
        public string OriginName { get; set; }
        /// <summary>
        /// �ֵ�
        /// </summary>
        public Guid COriginId { get; set; }
        /// <summary>
        /// �ƹ�Id
        /// </summary>
        public Guid GeneralizeInfoId { get; set; }
        /// <summary>
        /// �Ƿ�ɽ�
        /// </summary>
        public bool IsDeal { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        public int ReBuy { get; set; }
        /// <summary>
        /// ת������Id
        /// </summary>
        public Guid ParentId { get; set; }
        /// <summary>
        /// �Ӵ�ʱ��
        /// </summary>
        public DateTime TouchTime { get; set; }
        /// <summary>
        /// ��ҵ
        /// </summary>
        public string Industry { get; set; }
        /// <summary>
        /// �״γɽ�ʱ��
        /// </summary>
        public DateTime FirstDealTime { get; set; }
        /// <summary>
        /// ������ʱ��
        /// </summary>
        public DateTime LastFollowTime { get; set; }
        /// <summary>
        /// ����������ʱ��
        /// </summary>
        public DateTime ApplyTime { get; set; }
        /// <summary>
        /// ��Ȩ����ʱ��
        /// </summary>
        public DateTime AuthExpiredTime { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        public string Remark { get; set; }
        
        /// <summary>
        /// �����û�Id
        /// </summary>
        public Guid RelationUserId { get; set; }
        /// <summary>
        /// �����û�����
        /// </summary>
        public string RelationUserRealName { get; set; }
    }
}
