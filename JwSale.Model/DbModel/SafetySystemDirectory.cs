using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.DbModel
{
    public class SafetySystemDirectory : Entity
    {
        public string Name { get; set; }

        public string ParentId { get; set; }

        public int Level { get; set; }

        public int SortOrder { get; set; }

        public string Remark { get; set; }

        public bool IsActive { get; set; } = true;
    }
}