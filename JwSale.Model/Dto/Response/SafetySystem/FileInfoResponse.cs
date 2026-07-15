using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Response.SafetySystem
{
    public class FileInfoResponse
    {
        public string Id { get; set; }

        public string DirectoryId { get; set; }

        public string DirectoryName { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }

        public string FileExtension { get; set; }

        public long FileSize { get; set; }

        public string FileSizeText { get; set; }

        public string FileType { get; set; }

        public string FileUrl { get; set; }

        public string Remark { get; set; }

        public int Version { get; set; }

        public DateTime AddTime { get; set; }

        public string AddUserRealName { get; set; }

        public DateTime UpdateTime { get; set; }

        public string UpdateUserRealName { get; set; }
    }
}