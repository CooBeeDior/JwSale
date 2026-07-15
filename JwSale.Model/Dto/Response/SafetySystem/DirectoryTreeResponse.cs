using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Response.SafetySystem
{
    public class DirectoryTreeResponse
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ParentId { get; set; }

        public int Level { get; set; }

        public string Remark { get; set; }

        public bool HasChildren { get; set; }

        public bool HasFiles { get; set; }

        public List<DirectoryTreeResponse> Children { get; set; } = new List<DirectoryTreeResponse>();
    }
}