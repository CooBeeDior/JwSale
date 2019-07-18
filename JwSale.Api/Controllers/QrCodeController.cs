using JwSale.Api.Extensions;
using JwSale.Model;
using JwSale.Model.Dto;
using JwSale.Repository.Context;
using JwSale.Util.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using JwSale.Model.Dto.Request.QrCode;

namespace JwSale.Api.Controllers
{
    /// <summary>
    /// 二维码
    /// </summary> 
    public class QrCodeController : JwSaleControllerBase
    {
        public QrCodeController(JwSaleDbContext context) : base(context)
        {
        }


        /// <summary>
        /// 上传二维码
        /// </summary>
        /// <returns></returns>
        [HttpPost("api/QrCode/UploadQrCodeForm")]
        public async Task<HttpResponseMessage> UploadQrCodeForm()
        {
            ResponseBase response = new ResponseBase();
            if (!Request.HasFormContentType)
            {
                response.Success = false;
                response.Code = HttpStatusCode.BadRequest;
                response.Message = "请表单提交";
                return await response.ToHttpResponseAsync();
            }
            var fileCount = Request.Form.Files.Count;
            if (fileCount == 0)
            {
                response.Success = false;
                response.Code = HttpStatusCode.BadRequest;
                response.Message = "请上传文件";
                return await response.ToHttpResponseAsync();
            }

            foreach (var item in Request.Form.Files)
            {
                Guid id = Guid.NewGuid();
                var buffer = item.OpenReadStream().ToBuffer();
                QrCodeInfo qrCodeInfo = new QrCodeInfo()
                {
                    Id = id,
                    Content = buffer.DecodeQrCode(),
                    Path = $"{id}{Path.GetExtension(item.Name)}",
                    Status = 0,
                    AddUserId = UserInfo.Id,
                    AddUserRealName = UserInfo.AddUserRealName,
                    UpdateUserId = UserInfo.Id,
                    UpdateUserRealName = UserInfo.AddUserRealName,
                    AddTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                };
                DbContext.Add(qrCodeInfo);

                using (var fs = System.IO.File.Create(Path.Combine("wwwroot", qrCodeInfo.Path)))
                {
                    fs.Write(buffer, 0, buffer.Length);
                }
            }
            int count = await DbContext.SaveChangesAsync();
            if (count == 0)
            {
                response.Success = false;
                response.Code = HttpStatusCode.InternalServerError;
                response.Message = "添加失败";
            }
            return await response.ToHttpResponseAsync();

        }


        /// <summary>
        /// 获取二维码列表
        /// </summary>
        /// <param name="getQrCodeList"></param>
        /// <returns></returns>
        [HttpPost("api/QrCode/GetQrCodeList")]
        public async Task<HttpResponseMessage> GetQrCodeList(GetQrCodeList getQrCodeList)
        {
            ResponseBase<IQueryable<QrCodeInfo>> response = new ResponseBase<IQueryable<QrCodeInfo>>();

            var qrcodeInfos = DbContext.QrCodeInfos.AsQueryable();
            if (!getQrCodeList.Status.IsNull())
            {
                qrcodeInfos = qrcodeInfos.Where(o => o.Status == getQrCodeList.Status);
            }
            qrcodeInfos = qrcodeInfos.Skip((getQrCodeList.PageIndex - 1) * getQrCodeList.PageSize).Take(getQrCodeList.PageSize).OrderBy(o => o.AddTime);
            response.Data = qrcodeInfos;
            return await response.ToHttpResponseAsync();
        }

        /// <summary>
        /// 获取二维码详情
        /// </summary>
        /// <param name="id">二维码id</param>
        /// <returns></returns>
        [HttpPost("api/QrCode/GetQrCodeDetail/{id}")]
        public async Task<HttpResponseMessage> GetQrCodeDetail(Guid id)
        {
            ResponseBase<QrCodeInfo> response = new ResponseBase<QrCodeInfo>();
            var qrcodeInfo = DbContext.QrCodeInfos.Where(o => o.Id == id).FirstOrDefault();
            response.Data = qrcodeInfo;
            return await response.ToHttpResponseAsync();
        }

        /// <summary>
        /// 修改二维码状态
        /// </summary>
        /// <param name="updateQrCodeStatus"></param>
        /// <returns></returns>
        [HttpPost("api/QrCode/UpdateQrCodeStatus")]
        public async Task<HttpResponseMessage> UpdateQrCodeStatus(UpdateQrCodeStatus updateQrCodeStatus)
        {
            ResponseBase<QrCodeInfo> response = new ResponseBase<QrCodeInfo>();
            var qrcodeInfo = DbContext.QrCodeInfos.Where(o => o.Content == updateQrCodeStatus.Url).FirstOrDefault();
            if (qrcodeInfo == null)
            {
                response.Success = false;
                response.Code = HttpStatusCode.NotFound;
                response.Message = "记录不存在";
            }
            else
            {
                qrcodeInfo.Status = updateQrCodeStatus.Status;
                qrcodeInfo.UpdateUserId = UserInfo.Id;
                qrcodeInfo.UpdateUserRealName = UserInfo.RealName;
                qrcodeInfo.UpdateTime = DateTime.Now;
                await DbContext.SaveChangesAsync();
            }

            response.Data = qrcodeInfo;
            return await response.ToHttpResponseAsync();
        }


    }
}
