using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwSale.Model
{
    /// <summary>
    /// �����ˣ�LTL
    /// ��  �ڣ�2019.06.11 10:12
    /// ��  ����FunctionInfoʵ��
    /// </summary>
    [Table("FunctionInfo")]
    public class FunctionInfo : Entity
    {
        /// <summary>
        /// ��������
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// ���ܱ���
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// ·��
        /// </summary>
        public string Path { get; set; }
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
