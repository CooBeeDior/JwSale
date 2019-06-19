using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwSale.Model
{
    /// <summary>
    /// �����ˣ�LTL
    /// ��  �ڣ�2019.06.11 10:12
    /// ��  ����OrderInfoʵ��
    /// </summary>
    [Table("OrderInfo")]
    public class OrderInfo: Entity
    {
         
        /// <summary>
        /// �ͻ�Id
        /// </summary>
        public Guid CustomerInfoId { get; set; }
        /// <summary>
        /// �ͻ�����
        /// </summary>
        public string CsutomerInfoName { get; set; }
        /// <summary>
        /// ����״̬
        /// </summary>
        public short Status { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime OrderTime { get; set; }
        /// <summary>
        /// ����ʵ���ܽ��
        /// </summary>
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// �������� 0���׵� 1������
        /// </summary>
        public short OrderType { get; set; }
        /// <summary>
        /// ���ۼ۸�
        /// </summary>
        public decimal Discount { get; set; }
        /// <summary>
        /// �ջ���
        /// </summary>
        public string Consignee { get; set; }
        /// <summary>
        /// �ջ����ֻ���
        /// </summary>
        public string ConsigneePhone { get; set; }
        /// <summary>
        /// ConsigneeCountry
        /// </summary>
        public string ConsigneeCountry { get; set; }
        /// <summary>
        /// ConsigneeProvince
        /// </summary>
        public string ConsigneeProvince { get; set; }
        /// <summary>
        /// ConsigneeCiry
        /// </summary>
        public string ConsigneeCiry { get; set; }
        /// <summary>
        /// ConsigneeArea
        /// </summary>
        public string ConsigneeArea { get; set; }
        /// <summary>
        /// ConsigneeAddress
        /// </summary>
        public string ConsigneeAddress { get; set; }
        /// <summary>
        /// ��� �ֵ�
        /// </summary>
        public Guid ExpressTypeId { get; set; }
        /// <summary>
        /// ������� �ֵ�
        /// </summary>
        public string ExpressTypeName { get; set; }
        /// <summary>
        /// ��ݼ۸�
        /// </summary>
        public decimal ExpressPrice { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        public string Remark { get; set; }
      
    }
}
