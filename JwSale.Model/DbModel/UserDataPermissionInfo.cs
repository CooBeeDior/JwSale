using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwSale.Model
{
    /// <summary>
    /// �����ˣ�LTL
    /// ��  �ڣ�2019.06.11 10:13
    /// ��  ����UserDataPermissionInfoʵ��
    /// </summary>
    [Table("UserDataPermissionInfo")]
    public class UserDataPermissionInfo: Entity
    { 
        /// <summary>
        /// �û�Id
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// ������ʵ��û�Id
        /// </summary>
        public Guid ToUserId { get; set; }
        /// <summary>
        /// ���� 0�����Է��� 1�����ܷ���
        /// </summary>
        public short Type { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        public string Remark { get; set; }
    
    }
}
