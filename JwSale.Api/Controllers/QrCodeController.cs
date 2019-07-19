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
using Microsoft.AspNetCore.Http;
using JwSale.Api.Util;
using System.ComponentModel;
using StackExchange.Profiling;
using System.Threading;
using System.Drawing.Imaging;

namespace JwSale.Api.Controllers
{
    /// <summary>
    /// 二维码
    /// </summary> 

    public class QrCodeController : JwSaleControllerBase
    {

        public QrCodeController(JwSaleDbContext jwSaleDbContext) : base(jwSaleDbContext)
        {


        }
        /// <summary>
        /// 上传二维码
        /// </summary>
        /// <returns></returns>
        [HttpPost("api/qrcode/uploadqrcode")]
        [Description("上传二维码")]
        public async Task<ActionResult<ResponseBase>> UploadQrCode(UploadQrCode uploadQrCode)
        {
            ResponseBase<IList<QrCodeInfo>> response = new ResponseBase<IList<QrCodeInfo>>();
            IList<QrCodeInfo> list = new List<QrCodeInfo>();
            //var fileCount = formFiles.Count;
            if (uploadQrCode == null || uploadQrCode.QrCodes == null)
            {
                response.Success = false;
                response.Code = HttpStatusCode.BadRequest;
                response.Message = "请上传二维码链接";
                return await response.ToJsonResultAsync();
            }

            foreach (var item in uploadQrCode.QrCodes)
            {
                Guid id = Guid.NewGuid();
                QrCodeInfo qrCodeInfo = new QrCodeInfo()
                {
                    Id = id,
                    Content = item,
                    Path = $"{id}.png",
                    Status = 0,
                    AddUserId = UserHelper.UserInfo.Id,
                    AddUserRealName = UserHelper.UserInfo.AddUserRealName,
                    UpdateUserId = UserHelper.UserInfo.Id,
                    UpdateUserRealName = UserHelper.UserInfo.AddUserRealName,
                    AddTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                };
                list.Add(qrCodeInfo);
                DbContext.Add(qrCodeInfo);
                var bitMap = item.CreateQRCode();
                bitMap.Save(Path.Combine("wwwroot", qrCodeInfo.Path), ImageFormat.Png);
            }




            int count = await DbContext.SaveChangesAsync();
            if (count == 0)
            {
                response.Success = false;
                response.Code = HttpStatusCode.InternalServerError;
                response.Message = "添加失败";
            }
            response.Data = list;
            return await response.ToJsonResultAsync();

        }

        /// <summary>
        /// 上传二维码
        /// </summary>
        /// <returns></returns>
        [HttpPost("api/qrcode/uploadqrcodeform")]
        [Description("上传二维码")]
        public async Task<ActionResult<ResponseBase>> UploadQrCodeForm([FromForm]IList<IFormFile> files)
        {

            ResponseBase<IList<QrCodeInfo>> response = new ResponseBase<IList<QrCodeInfo>>();
            IList<QrCodeInfo> list = new List<QrCodeInfo>();
            //var fileCount = formFiles.Count;
            if (files == null || files.Count == 0)
            {
                response.Success = false;
                response.Code = HttpStatusCode.BadRequest;
                response.Message = "请上传文件";
                return await response.ToJsonResultAsync();
            }

            foreach (var file in files)
            {
                Guid id = Guid.NewGuid();
                var buffer = file.OpenReadStream().ToBuffer();
                QrCodeInfo qrCodeInfo = new QrCodeInfo()
                {
                    Id = id,
                    Content = buffer.DecodeQrCode(),
                    Path = $"{id}{Path.GetExtension(file.FileName)}",
                    Status = 0,
                    AddUserId = UserHelper.UserInfo.Id,
                    AddUserRealName = UserHelper.UserInfo.AddUserRealName,
                    UpdateUserId = UserHelper.UserInfo.Id,
                    UpdateUserRealName = UserHelper.UserInfo.AddUserRealName,
                    AddTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                };
                DbContext.Add(qrCodeInfo);
                list.Add(qrCodeInfo);
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
            response.Data = list;
            return await response.ToJsonResultAsync();

        }


        /// <summary>
        /// 获取二维码列表
        /// </summary>
        /// <param name="getQrCodeList"></param>
        /// <returns></returns>
        [HttpPost("api/qrcode/getqrcodelist")]
        [Description("获取二维码列表")]
        public async Task<ActionResult<ResponseBase<IQueryable<QrCodeInfo>>>> GetQrCodeList(GetQrCodeList getQrCodeList)
        {
            ResponseBase<IQueryable<QrCodeInfo>> response = new ResponseBase<IQueryable<QrCodeInfo>>();

            var qrcodeInfos = DbContext.QrCodeInfos.AsQueryable();
            if (!getQrCodeList.Status.IsNull())
            {
                qrcodeInfos = qrcodeInfos.Where(o => o.Status == getQrCodeList.Status);
            }
            qrcodeInfos = qrcodeInfos.Skip((getQrCodeList.PageIndex - 1) * getQrCodeList.PageSize).Take(getQrCodeList.PageSize).OrderBy(o => o.AddTime);
            response.Data = qrcodeInfos;
            return await response.ToJsonResultAsync();
        }

        /// <summary>
        /// 获取二维码详情
        /// </summary>
        /// <param name="id">二维码id</param>
        /// <returns></returns>
        [HttpPost("api/QrCode/GetQrCodeDetail/{id}")]
        [Description("获取二维码详情")]
        public async Task<ActionResult<ResponseBase<QrCodeInfo>>> GetQrCodeDetail(Guid id)
        {
            ResponseBase<QrCodeInfo> response = new ResponseBase<QrCodeInfo>();
            var qrcodeInfo = DbContext.QrCodeInfos.Where(o => o.Id == id).FirstOrDefault();
            if (qrcodeInfo == null)
            {
                response.Success = false;
                response.Code = HttpStatusCode.NotFound;
                response.Message = "记录不存在";
            }
            else
            {
                response.Data = qrcodeInfo;
            }
            return await response.ToJsonResultAsync();
        }

        /// <summary>
        /// 修改二维码状态
        /// </summary>
        /// <param name="updateQrCodeStatus"></param>
        /// <returns></returns>
        [HttpPost("api/QrCode/UpdateQrCodeStatus")]
        [Description("修改二维码状态")]
        public async Task<ActionResult<ResponseBase<QrCodeInfo>>> UpdateQrCodeStatus(UpdateQrCodeStatus updateQrCodeStatus)
        {
            ResponseBase<QrCodeInfo> response = new ResponseBase<QrCodeInfo>();
            var qrcodeInfo = DbContext.QrCodeInfos.Where(o => o.Content == updateQrCodeStatus.QrCode).FirstOrDefault();
            if (qrcodeInfo == null)
            {
                response.Success = false;
                response.Code = HttpStatusCode.NotFound;
                response.Message = "记录不存在";
            }
            else
            {
                qrcodeInfo.Status = updateQrCodeStatus.Status;
                qrcodeInfo.UpdateUserId = UserHelper.UserInfo.Id;
                qrcodeInfo.UpdateUserRealName = UserHelper.UserInfo.RealName;
                qrcodeInfo.UpdateTime = DateTime.Now;
                await DbContext.SaveChangesAsync();
            }

            response.Data = qrcodeInfo;
            return await response.ToJsonResultAsync();
        }


    }
}
