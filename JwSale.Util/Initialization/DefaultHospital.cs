using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Util.Initialization
{
    public class DefaultHospital
    {
        public static DefaultHospital Hospital
        {
            get
            {
                return new DefaultHospital()
                {
                    Id = new Guid("B025749E-2B58-4702-9EC2-97549169F888").ToString(),
                    Code = "JIUJIANGFUSHUYIYUAN",
                    Name = "九江附属医院",
                    FullName = "九江学院附属医院"

                };
            }
        }
        public string Id { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 全称
        /// </summary>
        public string FullName { get; set; }
    }
}
