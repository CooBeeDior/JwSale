using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto
{
    public class BriefInfoWithId : BriefInfo
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
    }
}
