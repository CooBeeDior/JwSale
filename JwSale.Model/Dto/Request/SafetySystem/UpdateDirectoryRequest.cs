using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Request.SafetySystem
{
    public class UpdateDirectoryRequest
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Remark { get; set; }
    }
}