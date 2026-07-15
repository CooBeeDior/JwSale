using System;
using System.Text;

namespace JwSale.Model.DbModel
{
    public class SafetySystemFile : Entity
    {
        public string DirectoryId { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }

        public string FileExtension { get; set; }

        public long FileSize { get; set; }

        public string FileType { get; set; }

        public string Remark { get; set; }

        public string FileUrl { get; set; }

        public int FileVersion { get; set; } = 1;

        public bool IsActive { get; set; } = true;
    }
}