using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwSale.Model
{
    /// <summary>
    /// �����ˣ�LTL
    /// ��  �ڣ�2019.06.11 10:13
    /// ��  ����SysLogʵ��
    /// </summary>
    [Table("SysLog")]
    public class SysLog: Entity
    { 
        /// <summary>
        /// ����
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string Message { get; set; }
  
    }
}
