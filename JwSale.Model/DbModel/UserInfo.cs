using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwSale.Model
{
    /// <summary>
    /// �����ˣ�LTL
    /// ��  �ڣ�2019.06.11 10:13
    /// ��  ����UserInfoʵ��
    /// </summary>
    [Table("UserInfo")]
    public class UserInfo: Entity
    {
    
        /// <summary>
        /// �û���
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// �ֻ�����
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// ��ʵ����
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// ��ʵ����ƴ��
        /// </summary>
        public string RealNamePin { get; set; }
        /// <summary>
        /// qq��
        /// </summary>
        public string Qq { get; set; }
        /// <summary>
        /// ΢�ź�
        /// </summary>
        public string WxNo { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string TelPhone { get; set; }
        /// <summary>
        /// ְλ����
        /// </summary>
        public string PositionName { get; set; }
        /// <summary>
        /// ʡ
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// ��
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// ��
        /// </summary>
        public string Area { get; set; }
        /// <summary>
        /// ��ϸ��ַ
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// ͷ��
        /// </summary>
        public string HeadImageUrl { get; set; }
        /// <summary>
        /// ״̬��0������ 1��ͣ��
        /// </summary>
        public short Status { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        public string Remark { get; set; }
     
    }
}
