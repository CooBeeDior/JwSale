using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwSale.Model
{
    /// <summary>
    /// �����ˣ�LTL
    /// ��  �ڣ�2019.06.11 10:12
    /// ��  ����GeneralizeInfoʵ��
    /// </summary>
    [Table("GeneralizeInfo")]
    public class GeneralizeInfo: Entity
    { 
        /// <summary>
        /// Ͷ��Ŀ�ĵ�Id���ֵ䣩
        /// </summary>
        public Guid DestinationId { get; set; }
        /// <summary>
        /// Ͷ��Ŀ�ĵ����ƣ��ֵ䣩
        /// </summary>
        public string DestinationName { get; set; }
        /// <summary>
        /// �ؼ���
        /// </summary>
        public string Keywords { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string Message { get; set; }
   
    }
}
