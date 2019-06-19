using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwSale.Model
{
    /// <summary>
    /// �����ˣ�LTL
    /// ��  �ڣ�2019.06.11 10:13
    /// ��  ����ProjectInfoʵ��
    /// </summary>
    [Table("ProjectInfo")]
    public class ProjectInfo: Entity
    {
        
        /// <summary>
        /// ���
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// ȫ��
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        public string UserRealName { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        public string Remark { get; set; }
   
    }
}
