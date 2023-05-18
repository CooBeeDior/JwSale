using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwSale.Model
{
    /// <summary>
    /// �����ˣ�LTL
    /// ��  �ڣ�2019.06.11 10:13
    /// ��  ����UserRoleDataPermissionInfoʵ��
    /// </summary>
    [Table("UserRoleDataPermissionInfo")]
    public class UserRoleDataPermissionInfo: Entity
    { 
        /// <summary>
        /// �û�Id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// ��ɫId
        /// </summary>
        public string RoleId { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        public string Remark { get; set; }
      
    }
}
