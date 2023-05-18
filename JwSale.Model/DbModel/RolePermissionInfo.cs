using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwSale.Model
{
    /// <summary>
    /// �����ˣ�LTL
    /// ��  �ڣ�2019.06.11 10:13
    /// ��  ����RolePermissionInfoʵ��
    /// </summary>
    [Table("RolePermissionInfo")]
    public class RolePermissionInfo: Entity
    {
       
        /// <summary>
        /// ��ɫId
        /// </summary>
        public string RoleId { get; set; }
        /// <summary>
        /// ����Id
        /// </summary>
        public string FunctionId { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        public string Remark { get; set; }
      
    }
}
