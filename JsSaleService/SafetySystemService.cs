using FreeSql;
using JwSale.Model;
using JwSale.Model.DbModel;
using JwSale.Model.Dto.Response.SafetySystem;
using JwSale.Util.Dependencys;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsSaleService
{
    public class SafetySystemService : ISafetySystemService
    {
        private readonly IFreeSql _freeSql;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SafetySystemService(IFreeSql freeSql, IHttpContextAccessor httpContextAccessor)
        {
            _freeSql = freeSql;
            _httpContextAccessor = httpContextAccessor;
        }

        public DirectoryTreeResponse GetDirectoryTree(string parentId)
        {
            var directories = _freeSql.Select<SafetySystemDirectory>()
                .Where(d => d.IsActive)
                .OrderBy(d => d.SortOrder)
                .ToList();

            var files = _freeSql.Select<SafetySystemFile>()
                .Where(f => f.IsActive)
                .ToList();

            var directoryIdsWithFiles = files.Select(f => f.DirectoryId).Distinct().ToList();

            var root = new DirectoryTreeResponse
            {
                Id = "root",
                Name = "安全制度管理",
                ParentId = null,
                Level = 0,
                HasChildren = true
            };

            BuildTree(root, directories, directoryIdsWithFiles);

            return root;
        }

        private void BuildTree(DirectoryTreeResponse parent, List<SafetySystemDirectory> directories, List<string> directoryIdsWithFiles)
        {
            var children = directories.Where(d => d.ParentId == parent.Id).ToList();
            
            foreach (var dir in children)
            {
                var childNode = new DirectoryTreeResponse
                {
                    Id = dir.Id,
                    Name = dir.Name,
                    ParentId = dir.ParentId,
                    Level = dir.Level,
                    Remark = dir.Remark,
                    HasChildren = directories.Any(d => d.ParentId == dir.Id),
                    HasFiles = directoryIdsWithFiles.Contains(dir.Id)
                };

                parent.Children.Add(childNode);
                BuildTree(childNode, directories, directoryIdsWithFiles);
            }
        }

        public List<FileInfoResponse> GetFilesByDirectory(string directoryId)
        {
            var files = _freeSql.Select<SafetySystemFile>()
                .Where(f => f.DirectoryId == directoryId && f.IsActive)
                .OrderByDescending(f => f.UpdateTime)
                .ToList();

            var directoryNames = _freeSql.Select<SafetySystemDirectory>()
                .Where(d => d.IsActive)
                .ToList()
                .ToDictionary(d => d.Id, d => d.Name);

            return files.Select(f => new FileInfoResponse
            {
                Id = f.Id,
                DirectoryId = f.DirectoryId,
                DirectoryName = directoryNames.TryGetValue(f.DirectoryId, out var name) ? name : string.Empty,
                FileName = f.FileName,
                FilePath = f.FilePath,
                FileExtension = f.FileExtension,
                FileSize = f.FileSize,
                FileSizeText = FormatFileSize(f.FileSize),
                FileType = f.FileType,
                FileUrl = f.FileUrl,
                Remark = f.Remark,
                Version = f.FileVersion,
                AddTime = f.AddTime,
                AddUserRealName = f.AddUserRealName,
                UpdateTime = f.UpdateTime,
                UpdateUserRealName = f.UpdateUserRealName
            }).ToList();
        }

        private string FormatFileSize(long bytes)
        {
            if (bytes < 1024) return bytes + " B";
            if (bytes < 1024 * 1024) return (bytes / 1024.0).ToString("F2") + " KB";
            if (bytes < 1024 * 1024 * 1024) return (bytes / (1024.0 * 1024)).ToString("F2") + " MB";
            return (bytes / (1024.0 * 1024 * 1024)).ToString("F2") + " GB";
        }

        public SafetySystemDirectory AddDirectory(string name, string parentId, string remark)
        {
            var parent = string.IsNullOrEmpty(parentId) || parentId == "root" 
                ? null 
                : _freeSql.Select<SafetySystemDirectory>().Where(d => d.Id == parentId).First();

            var newDir = new SafetySystemDirectory
            {
                Id = Guid.NewGuid().ToString(),
                Name = name,
                ParentId = parentId == "root" ? null : parentId,
                Level = parent != null ? parent.Level + 1 : 1,
                SortOrder = 0,
                Remark = remark,
                AddTime = DateTime.Now,
                UpdateTime = DateTime.Now
            };

            SetUserInfo(newDir);

            _freeSql.Insert<SafetySystemDirectory>().AppendData(newDir).ExecuteAffrows();

            return newDir;
        }

        public SafetySystemDirectory UpdateDirectory(string id, string name, string remark)
        {
            var dir = _freeSql.Select<SafetySystemDirectory>().Where(d => d.Id == id).First();
            if (dir == null) throw new Exception("目录不存在");

            dir.Name = name;
            dir.Remark = remark;
            dir.UpdateTime = DateTime.Now;

            SetUpdateUserInfo(dir);

            _freeSql.Update<SafetySystemDirectory>().SetSource(dir).ExecuteAffrows();

            return dir;
        }

        public void DeleteDirectory(string id)
        {
            var dir = _freeSql.Select<SafetySystemDirectory>().Where(d => d.Id == id).First();
            if (dir == null) throw new Exception("目录不存在");

            var hasChildren = _freeSql.Select<SafetySystemDirectory>().Where(d => d.ParentId == id && d.IsActive).Any();
            if (hasChildren) throw new Exception("该目录下存在子目录，无法删除");

            var hasFiles = _freeSql.Select<SafetySystemFile>().Where(f => f.DirectoryId == id && f.IsActive).Any();
            if (hasFiles) throw new Exception("该目录下存在文件，无法删除");

            dir.IsActive = false;
            dir.UpdateTime = DateTime.Now;

            SetUpdateUserInfo(dir);

            _freeSql.Update<SafetySystemDirectory>().SetSource(dir).ExecuteAffrows();
        }

        public FileInfoResponse UploadFile(string directoryId, string fileName, string filePath, string fileExtension, long fileSize, string fileType, string fileUrl, string remark)
        {
            var directory = _freeSql.Select<SafetySystemDirectory>().Where(d => d.Id == directoryId).First();
            if (directory == null) throw new Exception("目录不存在");

            var existingFile = _freeSql.Select<SafetySystemFile>()
                .Where(f => f.DirectoryId == directoryId && f.FileName == fileName && f.IsActive)
                .First();

            SafetySystemFile file;

            if (existingFile != null)
            {
                var versionRecord = new SafetySystemFileVersion
                {
                    Id = Guid.NewGuid().ToString(),
                    FileId = existingFile.Id,
                    FileVersion = existingFile.FileVersion,
                    FilePath = existingFile.FilePath,
                    FileUrl = existingFile.FileUrl,
                    FileSize = existingFile.FileSize,
                    ChangeRemark = remark,
                    AddTime = DateTime.Now
                };

                SetUserInfo(versionRecord);

                _freeSql.Insert<SafetySystemFileVersion>().AppendData(versionRecord).ExecuteAffrows();

                existingFile.FilePath = filePath;
                existingFile.FileUrl = fileUrl;
                existingFile.FileSize = fileSize;
                existingFile.FileExtension = fileExtension;
                existingFile.FileType = fileType;
                existingFile.Remark = remark;
                existingFile.FileVersion += 1;
                existingFile.UpdateTime = DateTime.Now;

                SetUpdateUserInfo(existingFile);

                _freeSql.Update<SafetySystemFile>().SetSource(existingFile).ExecuteAffrows();

                file = existingFile;
            }
            else
            {
                file = new SafetySystemFile
                {
                    Id = Guid.NewGuid().ToString(),
                    DirectoryId = directoryId,
                    FileName = fileName,
                    FilePath = filePath,
                    FileExtension = fileExtension,
                    FileSize = fileSize,
                    FileType = fileType,
                    FileUrl = fileUrl,
                    Remark = remark,
                    FileVersion = 1,
                    AddTime = DateTime.Now,
                    UpdateTime = DateTime.Now
                };

                SetUserInfo(file);

                _freeSql.Insert<SafetySystemFile>().AppendData(file).ExecuteAffrows();
            }

            return new FileInfoResponse
            {
                Id = file.Id,
                DirectoryId = file.DirectoryId,
                DirectoryName = directory.Name,
                FileName = file.FileName,
                FilePath = file.FilePath,
                FileExtension = file.FileExtension,
                FileSize = file.FileSize,
                FileSizeText = FormatFileSize(file.FileSize),
                FileType = file.FileType,
                FileUrl = file.FileUrl,
                Remark = file.Remark,
                Version = file.FileVersion,
                AddTime = file.AddTime,
                AddUserRealName = file.AddUserRealName,
                UpdateTime = file.UpdateTime,
                UpdateUserRealName = file.UpdateUserRealName
            };
        }

        public void DeleteFile(string id)
        {
            var file = _freeSql.Select<SafetySystemFile>().Where(f => f.Id == id).First();
            if (file == null) throw new Exception("文件不存在");

            file.IsActive = false;
            file.UpdateTime = DateTime.Now;

            SetUpdateUserInfo(file);

            _freeSql.Update<SafetySystemFile>().SetSource(file).ExecuteAffrows();
        }

        public List<FileVersionResponse> GetFileVersions(string fileId)
        {
            var versions = _freeSql.Select<SafetySystemFileVersion>()
                .Where(v => v.FileId == fileId)
                .OrderByDescending(v => v.FileVersion)
                .ToList();

            return versions.Select(v => new FileVersionResponse
            {
                Id = v.Id,
                FileId = v.FileId,
                Version = v.FileVersion,
                FilePath = v.FilePath,
                FileUrl = v.FileUrl,
                FileSize = v.FileSize,
                ChangeRemark = v.ChangeRemark,
                AddTime = v.AddTime,
                AddUserRealName = v.AddUserRealName
            }).ToList();
        }

        private void SetUserInfo(Entity entity)
        {
            var user = _httpContextAccessor.HttpContext?.Items["CurrentUser"] as UserInfo;
            if (user != null)
            {
                entity.AddUserId = user.Id;
                entity.AddUserRealName = user.RealName;
                entity.UpdateUserId = user.Id;
                entity.UpdateUserRealName = user.RealName;
            }
        }

        private void SetUpdateUserInfo(Entity entity)
        {
            var user = _httpContextAccessor.HttpContext?.Items["CurrentUser"] as UserInfo;
            if (user != null)
            {
                entity.UpdateUserId = user.Id;
                entity.UpdateUserRealName = user.RealName;
            }
        }
    }
}