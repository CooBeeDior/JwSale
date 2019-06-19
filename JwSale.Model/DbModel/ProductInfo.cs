using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwSale.Model
{
    /// <summary>
    /// �����ˣ�LTL
    /// ��  �ڣ�2019.06.11 10:13
    /// ��  ����ProductInfoʵ��
    /// </summary>
    [Table("ProductInfo")]
    public class ProductInfo: Entity
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
        /// ��ĿId
        /// </summary>
        public Guid ProjectInfoId { get; set; }
        /// <summary>
        /// ��Ŀ����
        /// </summary>
        public string ProjectInfoName { get; set; }
        /// <summary>
        /// ��ƷͼƬ
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// ���� �ֵ�  ��� Ӳ�� ����
        /// </summary>
        public Guid TypeId { get; set; }
        /// <summary>
        /// �������� �ֵ�
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public decimal Discount { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        public string Remark { get; set; }
     
    }
}
