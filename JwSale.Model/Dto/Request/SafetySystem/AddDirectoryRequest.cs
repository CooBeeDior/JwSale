using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Request.SafetySystem
{
    public class AddDirectoryRequest
    {
        public string Name { get; set; }

        public string ParentId { get; set; }

        public string Remark { get; set; }
    }
}