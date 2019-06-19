using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwSale.Model
{
    /// <summary>
    /// �����ˣ�LTL
    /// ��  �ڣ�2019.06.11 10:12
    /// ��  ����FollowInfoʵ��
    /// </summary>
    [Table("FollowInfo")]
    public class FollowInfo: Entity
    {
       
        /// <summary>
        /// ������ƷId
        /// </summary>
        public string ProductId { get; set; }
        /// <summary>
        /// ������Ʒ����
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// �ͻ�Id
        /// </summary>
        public Guid CustomerInfoId { get; set; }
        /// <summary>
        /// �ͻ�����
        /// </summary>
        public string CsutomerInfoName { get; set; }
        /// <summary>
        /// ������Id
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// ����������
        /// </summary>
        public string UserRealName { get; set; }
        /// <summary>
        /// ��������Id
        /// </summary>
        public Guid TypeId { get; set; }
        /// <summary>
        /// ������������
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        public string Remark { get; set; }
        
    }
}
