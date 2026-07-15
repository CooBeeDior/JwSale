using JsSaleService;
using JwSale.Api.Attributes;
using JwSale.Model.Dto;
using JwSale.Model.Dto.Response.SafetySystem;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;

namespace JwSale.Api.Controllers
{
    [Route("api/SafetySystem")]
    [ApiController]
    [NoAuthRequired]
    public class SafetySystemController : JwSaleControllerBase
    {
        private readonly ISafetySystemService _safetySystemService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SafetySystemController(ISafetySystemService safetySystemService, IHttpContextAccessor httpContextAccessor)
        {
            _safetySystemService = safetySystemService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("GetDirectoryTree")]
        public ResponseBase<DirectoryTreeResponse> GetDirectoryTree([FromQuery] string parentId = "root")
        {
            var result = _safetySystemService.GetDirectoryTree(parentId);
            return new ResponseBase<DirectoryTreeResponse> { Data = result };
        }

        [HttpGet("GetFilesByDirectory")]
        public ResponseBase<object> GetFilesByDirectory([FromQuery] string directoryId)
        {
            var files = _safetySystemService.GetFilesByDirectory(directoryId);
            return new ResponseBase<object> { Data = files };
        }

        [HttpPost("AddDirectory")]
        public ResponseBase<object> AddDirectory([FromBody] dynamic data)
        {
            string name = data.name;
            string parentId = data.parentId ?? "root";
            string remark = data.remark ?? string.Empty;

            var result = _safetySystemService.AddDirectory(name, parentId, remark);
            return new ResponseBase<object> { Data = new { id = result.Id, name = result.Name } };
        }

        [HttpPost("UpdateDirectory")]
        public ResponseBase<object> UpdateDirectory([FromBody] dynamic data)
        {
            string id = data.id;
            string name = data.name;
            string remark = data.remark ?? string.Empty;

            var result = _safetySystemService.UpdateDirectory(id, name, remark);
            return new ResponseBase<object> { Data = new { id = result.Id, name = result.Name } };
        }

        [HttpPost("DeleteDirectory")]
        public ResponseBase<object> DeleteDirectory([FromBody] dynamic data)
        {
            string id = data.id;
            _safetySystemService.DeleteDirectory(id);
            return new ResponseBase<object>();
        }

        [HttpPost("UploadFile")]
        public ResponseBase<object> UploadFile([FromForm] IFormFile file, [FromForm] string directoryId, [FromForm] string remark)
        {
            if (file == null || file.Length == 0)
                return new ResponseBase<object> { Success = false, Message = "请选择文件" };

            var uploadPath = Path.Combine("wwwroot", "uploads", "safety-system");
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var fileExtension = Path.GetExtension(file.FileName);
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.FileName);
            var newFileName = $"{fileNameWithoutExtension}_{DateTime.Now:yyyyMMddHHmmss}{fileExtension}";
            var filePath = Path.Combine(uploadPath, newFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            var fileUrl = $"/uploads/safety-system/{newFileName}";
            var fileType = GetFileType(fileExtension);

            var result = _safetySystemService.UploadFile(directoryId, file.FileName, filePath, fileExtension, file.Length, fileType, fileUrl, remark);
            return new ResponseBase<object> { Data = result };
        }

        [HttpPost("DeleteFile")]
        public ResponseBase<object> DeleteFile([FromBody] dynamic data)
        {
            string id = data.id;
            _safetySystemService.DeleteFile(id);
            return new ResponseBase<object>();
        }

        [HttpGet("GetFileVersions")]
        public ResponseBase<object> GetFileVersions([FromQuery] string fileId)
        {
            var versions = _safetySystemService.GetFileVersions(fileId);
            return new ResponseBase<object> { Data = versions };
        }

        private string GetFileType(string extension)
        {
            var ext = extension.ToLower();
            if (new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" }.Contains(ext)) return "image";
            if (new[] { ".pdf" }.Contains(ext)) return "pdf";
            if (new[] { ".doc", ".docx" }.Contains(ext)) return "word";
            if (new[] { ".xls", ".xlsx" }.Contains(ext)) return "excel";
            if (new[] { ".ppt", ".pptx" }.Contains(ext)) return "ppt";
            return "other";
        }
    }
}