using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwSale.Model
{
    /// <summary>
    /// �����ˣ�LTL
    /// ��  �ڣ�2019.06.11 10:13
    /// ��  ����RoleInfoʵ��
    /// </summary>
    [Table("RoleInfo")]
    public class RoleInfo: Entity
    { 
        /// <summary>
        /// ��������
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// ��Id
        /// </summary>
        public string ParentId { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        public string Remark { get; set; }
      
    }
}
