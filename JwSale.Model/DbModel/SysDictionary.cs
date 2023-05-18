using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwSale.Model
{
    /// <summary>
    /// �����ˣ�LTL
    /// ��  �ڣ�2019.06.11 10:13
    /// ��  ����SysDictionaryʵ��
    /// </summary>
    [Table("SysDictionary")]
    public class SysDictionary: Entity
    { 
        /// <summary>
        /// ����
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// ������Id
        /// </summary>
        public string ParentId { get; set; }
   
    }
}
