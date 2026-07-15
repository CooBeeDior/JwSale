using JwSale.Model.DbModel;
using JwSale.Model.Dto.Response.SafetySystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace JsSaleService
{
    public interface ISafetySystemService
    {
        DirectoryTreeResponse GetDirectoryTree(string parentId);

        List<FileInfoResponse> GetFilesByDirectory(string directoryId);

        SafetySystemDirectory AddDirectory(string name, string parentId, string remark);

        SafetySystemDirectory UpdateDirectory(string id, string name, string remark);

        void DeleteDirectory(string id);

        FileInfoResponse UploadFile(string directoryId, string fileName, string filePath, string fileExtension, long fileSize, string fileType, string fileUrl, string remark);

        void DeleteFile(string id);

        List<FileVersionResponse> GetFileVersions(string fileId);
    }
}