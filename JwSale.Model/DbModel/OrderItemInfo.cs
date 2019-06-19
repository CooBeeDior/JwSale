using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwSale.Model
{
    /// <summary>
    /// �����ˣ�LTL
    /// ��  �ڣ�2019.06.11 10:12
    /// ��  ����OrderItemInfoʵ��
    /// </summary>
    [Table("OrderItemInfo")]
    public class OrderItemInfo: Entity
    {
     
        /// <summary>
        /// ����Id
        /// </summary>
        public Guid OrderInfoId { get; set; }
        /// <summary>
        /// ProductId
        /// </summary>
        public Guid ProductId { get; set; }
        /// <summary>
        /// ProductName
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// ���ۼ۸�
        /// </summary>
        public decimal Discount { get; set; }
        /// <summary>
        /// �۸�
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// ʵ�ʼ۸�=�۸�-���ۼ۸�
        /// </summary>
        public decimal RealPrice { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        public string Remark { get; set; }
        
    }
}
