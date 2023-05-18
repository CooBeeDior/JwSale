using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwSale.Model
{
    /// <summary>
    /// �����ˣ�LTL
    /// ��  �ڣ�2019.06.11 10:13
    /// ��  ����UserPermissionInfoʵ��
    /// </summary>
    [Table("UserPermissionInfo")]
    public class UserPermissionInfo: Entity
    {
        
        /// <summary>
        /// �û�Id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// ����Id
        /// </summary>
        public string FunctionId { get; set; }
        /// <summary>
        /// ���ͣ�0���� 1������
        /// </summary>
        public short Type { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        public string Remark { get; set; }
       
    }
}
