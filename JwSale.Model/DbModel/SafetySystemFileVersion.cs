using System;
using System.Text;

namespace JwSale.Model.DbModel
{
    public class SafetySystemFileVersion : Entity
    {
        public string FileId { get; set; }

        public int FileVersion { get; set; }

        public string FilePath { get; set; }

        public string FileUrl { get; set; }

        public long FileSize { get; set; }

        public string ChangeRemark { get; set; }
    }
}