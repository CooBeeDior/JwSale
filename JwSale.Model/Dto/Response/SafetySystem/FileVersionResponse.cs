using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Response.SafetySystem
{
    public class FileVersionResponse
    {
        public string Id { get; set; }

        public string FileId { get; set; }

        public int Version { get; set; }

        public string FilePath { get; set; }

        public string FileUrl { get; set; }

        public long FileSize { get; set; }

        public string ChangeRemark { get; set; }

        public DateTime AddTime { get; set; }

        public string AddUserRealName { get; set; }
    }
}