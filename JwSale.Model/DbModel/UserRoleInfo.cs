using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwSale.Model
{
    /// <summary>
    /// �����ˣ�LTL
    /// ��  �ڣ�2019.06.11 10:13
    /// ��  ����UserRoleInfoʵ��
    /// </summary>
    [Table("UserRoleInfoEntity")]
    public class UserRoleInfoEntity : Entity
    {
        /// <summary>
        /// �û�Id
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// ��ɫId
        /// </summary>
        public Guid RoleId { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        public string Remark { get; set; }

    }
}
