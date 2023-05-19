using JsSaleService;
using JwSale.Api.Extensions;
using JwSale.Model;
using JwSale.Model.DbModel;
using JwSale.Model.Dto;
using JwSale.Model.Dto.Request.Hospital;
using JwSale.Model.Dto.Response.Hospital;
using JwSale.Model.Dto.Response.User;
using JwSale.Packs.Options;
using JwSale.Repository.Context;
using JwSale.Util;
using JwSale.Util.Attributes;
using JwSale.Util.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JwSale.Api.Controllers
{
    [MoudleInfo("医生管理", 1, IsFunction = false)]
    public class ManagementController : ManageControllerBase
    {
        private readonly IUserService _userService;
        private IDistributedCache _cache;

        private JwSaleOptions jwSaleOptions;
        private readonly IFreeSql _freeSql;


        public ManagementController(JwSaleDbContext context, IUserService userService,
            IDistributedCache cache, IOptions<JwSaleOptions> jwSaleOptions,
          IFreeSql freeSql) : base(context)
        {
            this._userService = userService;
            this._cache = cache;
            this.jwSaleOptions = jwSaleOptions.Value;
            this._freeSql = freeSql;


        }

        /// <summary>
        /// 新增收费类别
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [MoudleInfo("新增收费类别")]
        [HttpPost("api/Management/AddItemType")]
        public async Task<ActionResult<ResponseBase>> AddItemType(ItemTypeRequest request)
        {
            ResponseBase<string> response = new ResponseBase<string>();

            ItemType itemType = new ItemType()
            {
                Id = Guid.NewGuid().ToString(),
                Code = string.IsNullOrWhiteSpace(request.Code) ? request.Name.ToPinYin() : request.Code,
                Name = request.Name,
                AddTime = DateTime.Now,
                AddUserId = UserInfo.Id,
                AddUserRealName = UserInfo.RealName,
                UpdateTime = DateTime.Now,
                UpdateUserId = UserInfo.Id,
                UpdateUserRealName = UserInfo.RealName,
                Remark = request.Remark,
                HospitalId = HttpContext.HospitalId()
            };

            int count = await _freeSql.Insert<ItemType>(itemType).ExecuteAffrowsAsync();
            response.Data = itemType.Id;
            return await response.ToJsonResultAsync();
        }



        /// <summary>
        /// 修改收费类别
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [MoudleInfo("修改收费类别")]
        [HttpPost("api/Management/UpdateItemType")]
        public async Task<ActionResult<ResponseBase>> UpdateItemType(UpdateItemTypeRequest request)
        {
            ResponseBase response = new ResponseBase();
            int count = await _freeSql.Update<ItemType>(request.Id)
                .Set(a => a.Name == request.Name)
                .Set(a => a.Code == (request.Code ?? request.Name.ToPinYin()))
                .Set(a => a.Remark == request.Remark)
                .Set(a => a.UpdateUserId == UserInfo.Id)
                .Set(a => a.UpdateUserRealName == UserInfo.UpdateUserRealName)
                .Set(a => a.UpdateTime == UserInfo.UpdateTime)
                .ExecuteAffrowsAsync();
            if (count == 0)
            {
                response.Message = "收费类别不存在";
                response.Code = HttpStatusCode.BadRequest;
                response.Success = false;
            }

            return await response.ToJsonResultAsync();
        }


        /// <summary>
        /// 删除收费类别
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [MoudleInfo("删除收费类别")]
        [HttpPost("api/Management/DeleteItemType")]
        public async Task<ActionResult<ResponseBase>> DeleteItemType(DeleteItemTypeRequest request)
        {
            ResponseBase response = new ResponseBase();
            bool isExistItem = _freeSql.Select<Item>().Where(o => o.ItemTypeId == request.Id).Any();
            if (isExistItem)
            {
                response.Message = "收费类别下有收费项目";
                response.Code = HttpStatusCode.BadRequest;
                response.Success = false;
            }
            else
            {
                int count = await _freeSql.Delete<ItemType>().Where(o => o.Id == request.Id).ExecuteAffrowsAsync();
                if (count == 0)
                {
                    response.Message = "收费类别不存在";
                    response.Code = HttpStatusCode.BadRequest;
                    response.Success = false;
                }
            }
            return await response.ToJsonResultAsync();
        }

        /// <summary>
        /// 获取收费类别列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [MoudleInfo("获取收费类别列表")]
        [HttpPost("api/Management/GetItemTypes")]
        public async Task<ActionResult<ResponseBase>> GetItemTypes(GetItemTypesRequest request)
        {
            ResponsePage<IList<GetItemTypeResponse>> response = new ResponsePage<IList<GetItemTypeResponse>>();

            request = request ?? new GetItemTypesRequest();
            var selectItemType = _freeSql.Select<ItemType>();
            if (!string.IsNullOrEmpty(request?.Name))
            {
                selectItemType = selectItemType.Where(o => o.Name.Contains(request.Name));

            }
            if (!string.IsNullOrEmpty(request?.Code))
            {
                selectItemType = selectItemType.Where(o => o.Code.Contains(request.Code));

            }
            if (!string.IsNullOrEmpty(request?.Remark))
            {
                selectItemType = selectItemType.Where(o => o.Remark.Contains(request.Remark));
            }
            long totalCount = await selectItemType.CountAsync();
            var totalPage = totalCount.ToTotalPage(request.PageSize);
            response.Page.TotalPage = totalPage;
            response.Page.TotalCount = totalCount;
            selectItemType = selectItemType.Page(request.PageIndex, request.PageSize);

            foreach (var item in request.OrderBys)
            {
                string orderBy = item.IsAsc ? "ASC" : "DESC";
                selectItemType = selectItemType.OrderBy($"{item.Name} {orderBy}");

            }


            var result = await selectItemType.ToListAsync(o => new GetItemTypeResponse()
            {
                Id = o.Id,
                Code = o.Code,
                Name = o.Name,
                Remark = o.Remark,
                UpdateTime = o.UpdateTime,
                AddTime = o.AddTime,
                AddUserId = o.AddUserId,
                UpdateUserId = o.UpdateUserId,
                AddUserRealName = o.AddUserRealName,
                UpdateUserRealName = o.UpdateUserRealName,
                ItemCount = (int)_freeSql.Select<Item>().Where(i => i.ItemTypeId == o.Id).Count()

            });



            response.Data = result;

            return await response.ToJsonResultAsync();
        }

        /// <summary>
        /// 获取收费类别详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MoudleInfo("获取收费类别详情")]
        [HttpGet("api/Management/GetItemType")]
        public async Task<ActionResult<ResponseBase>> GetItemType(string id)
        {
            ResponseBase<GetItemTypeResponse> response = new ResponseBase<GetItemTypeResponse>();
            if (string.IsNullOrWhiteSpace(id))
            {
                response.Message = "id不能为空";
                response.Code = HttpStatusCode.BadRequest;
                response.Success = false;
                return await response.ToJsonResultAsync();
            }
            var result = await _freeSql.Select<ItemType>().Where(o => o.Id == id).ToOneAsync(o => new GetItemTypeResponse()
            {
                Id = o.Id,
                Code = o.Code,
                Name = o.Name,
                Remark = o.Remark,
                UpdateTime = o.UpdateTime,
                AddTime = o.AddTime,
                AddUserId = o.AddUserId,
                UpdateUserId = o.UpdateUserId,
                AddUserRealName = o.AddUserRealName,
                UpdateUserRealName = o.UpdateUserRealName,
                ItemCount = (int)_freeSql.Select<Item>().Where(i => i.ItemTypeId == o.Id).Count()

            });
            response.Data = result;

            return await response.ToJsonResultAsync();
        }

        /// <summary>
        /// 新增收费项目
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [MoudleInfo("新增收费项目")]
        [HttpPost("api/Management/AddItem")]
        public async Task<ActionResult<ResponseBase>> AddItem(ItemRequest request)
        {
            ResponseBase<string> response = new ResponseBase<string>();

            Item item = new Item()
            {
                Id = Guid.NewGuid().ToString(),

                Name = request.Name,
                ItemTypeId = request.ItemTypeId,
                Price = request.Price,
                ImageUrl = request.ImageUrl,
                Status = request.Status,
                AddTime = DateTime.Now,
                AddUserId = UserInfo.Id,
                AddUserRealName = UserInfo.RealName,
                UpdateTime = DateTime.Now,
                UpdateUserId = UserInfo.Id,
                UpdateUserRealName = UserInfo.RealName,
                Remark = request.Remark,
                HospitalId = HttpContext.HospitalId()
            };

            int count = await _freeSql.Insert<Item>(item).ExecuteAffrowsAsync();
            response.Data = item.Id;
            return await response.ToJsonResultAsync();
        }


        /// <summary>
        /// 修改收费类别
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [MoudleInfo("修改收费项目")]
        [HttpPost("api/Management/UpdateItem")]
        public async Task<ActionResult<ResponseBase>> UpdateItem(UpdateItemRequest request)
        {
            ResponseBase response = new ResponseBase();
            int count = await _freeSql.Update<Item>(request.Id)
                .Set(a => a.Name == request.Name)
                .Set(a => a.ItemTypeId == request.ItemTypeId)
                .Set(a => a.Price == request.Price)
                .Set(a => a.ImageUrl == request.ImageUrl)
                .Set(a => a.Status == request.Status)
                .Set(a => a.Remark == request.Remark)
                .Set(a => a.UpdateUserId == UserInfo.Id)
                .Set(a => a.UpdateUserRealName == UserInfo.UpdateUserRealName)
                .Set(a => a.UpdateTime == UserInfo.UpdateTime)
                .ExecuteAffrowsAsync();
            if (count == 0)
            {
                response.Message = "收费项目不存在";
                response.Code = HttpStatusCode.BadRequest;
                response.Success = false;
            }

            return await response.ToJsonResultAsync();
        }




        /// <summary>
        /// 删除收费项目
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [MoudleInfo("删除收费项目")]
        [HttpPost("api/Management/DeleteItem")]
        public async Task<ActionResult<ResponseBase>> DeleteItem(DeleteItemRequest request)
        {
            ResponseBase response = new ResponseBase();
            int count = await _freeSql.Delete<Item>().Where(o => o.Id == request.Id).ExecuteAffrowsAsync();
            if (count == 0)
            {
                response.Message = "收费项目不存在";
                response.Code = HttpStatusCode.BadRequest;
                response.Success = false;
            }
            return await response.ToJsonResultAsync();
        }

        /// <summary>
        /// 获取收费项目列表
        /// </summary>
        /// <param name="request"></param> 
        /// <returns></returns>
        [MoudleInfo("获取收费项目列表")]
        [HttpPost("api/Management/GetItems")]
        public async Task<ActionResult<ResponseBase>> GetItems(GetItemsRequest request)
        {
            ResponsePage<IList<GetItemResponse>> response = new ResponsePage<IList<GetItemResponse>>();

            request = request ?? new GetItemsRequest();
            string alias = "i";
            var selectItem = _freeSql.Select<Item>().As(alias);
            if (!string.IsNullOrEmpty(request?.Name))
            {
                selectItem = selectItem.Where(o => o.Name.Contains(request.Name));

            }
            if (!string.IsNullOrEmpty(request?.ItemTypeId))
            {
                selectItem = selectItem.Where(o => o.ItemTypeId == request.ItemTypeId);

            }
            if (!string.IsNullOrEmpty(request?.Remark))
            {
                selectItem = selectItem.Where(o => o.Remark.Contains(request.Remark));
            }
            long totalCount = await selectItem.CountAsync();
            var totalPage = totalCount.ToTotalPage(request.PageSize);
            response.Page.TotalPage = totalPage;
            response.Page.TotalCount = totalCount;
            selectItem = selectItem.Page(request.PageIndex, request.PageSize);

            foreach (var item in request.OrderBys)
            {
                string orderBy = item.IsAsc ? "ASC" : "DESC";
                selectItem = selectItem.OrderBy($"{alias}.{item.Name} {orderBy}");
            }
            var result = await selectItem.From<ItemType>((i, t) => i.LeftJoin(a => a.ItemTypeId == t.Id)).ToListAsync((i, t) => new GetItemResponse()
            {
                Id = i.Id,
                Name = i.Name,
                ItemTypeId = t.Id,
                ItemTypeName = t.Name,
                ItemTypeCode = t.Code,
                Price = i.Price,
                ImageUrl = i.ImageUrl,
                Status = i.Status,
                Remark = i.Remark,
                UpdateTime = i.UpdateTime,
                AddTime = i.AddTime,
                AddUserId = i.AddUserId,
                UpdateUserId = i.UpdateUserId,
                AddUserRealName = i.AddUserRealName,
                UpdateUserRealName = i.UpdateUserRealName
            });

            response.Data = result;

            return await response.ToJsonResultAsync();
        }

        /// <summary>
        /// 获取收费项目详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MoudleInfo("获取收费项目详情")]
        [HttpGet("api/Management/GetItem")]
        public async Task<ActionResult<ResponseBase>> GetItem(string id)
        {
            ResponseBase<GetItemResponse> response = new ResponseBase<GetItemResponse>();
            if (string.IsNullOrWhiteSpace(id))
            {
                response.Message = "id不能为空";
                response.Code = HttpStatusCode.BadRequest;
                response.Success = false;
                return await response.ToJsonResultAsync();
            }
            var result = await _freeSql.Select<Item>().From<ItemType>((i, t) => i.LeftJoin(a => a.ItemTypeId == t.Id))
                .Where((i, t) => t.Id == id).ToOneAsync((i, t) => new GetItemResponse()
                {
                    Id = i.Id,
                    Name = i.Name,
                    ItemTypeId = t.Id,
                    ItemTypeName = t.Name,
                    ItemTypeCode = t.Code,
                    Price = i.Price,
                    ImageUrl = i.ImageUrl,
                    Status = i.Status,
                    Remark = i.Remark,
                    UpdateTime = i.UpdateTime,
                    AddTime = i.AddTime,
                    AddUserId = i.AddUserId,
                    UpdateUserId = i.UpdateUserId,
                    AddUserRealName = i.AddUserRealName,
                    UpdateUserRealName = i.UpdateUserRealName
                });

            response.Data = result;

            return await response.ToJsonResultAsync();
        }

        /// <summary>
        /// 新增科室
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [MoudleInfo("新增科室")]
        [HttpPost("api/Management/AddEpartmene")]
        public async Task<ActionResult<ResponseBase>> AddEpartmene(EpartmeneRequest request)
        {
            ResponseBase<string> response = new ResponseBase<string>();

            Epartmene epartmene = new Epartmene()
            {
                Id = Guid.NewGuid().ToString(),

                Name = request.Name,
                Code = string.IsNullOrWhiteSpace(request.Code) ? request.Name.ToPinYin() : request.Code,

                Location = request.Location,
                Remark = request.Remark,
                Status = request.Status,
                AddTime = DateTime.Now,
                AddUserId = UserInfo.Id,
                AddUserRealName = UserInfo.RealName,
                UpdateTime = DateTime.Now,
                UpdateUserId = UserInfo.Id,
                UpdateUserRealName = UserInfo.RealName,
                HospitalId = string.IsNullOrWhiteSpace(request.HospitalId) ? HttpContext.HospitalId() : request.HospitalId
            };

            int count = await _freeSql.Insert<Epartmene>(epartmene).ExecuteAffrowsAsync();
            response.Data = epartmene.Id;
            return await response.ToJsonResultAsync();
        }


        /// <summary>
        /// 修改科室
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [MoudleInfo("修改科室")]
        [HttpPost("api/Management/UpdateEpartmene")]
        public async Task<ActionResult<ResponseBase>> UpdateEpartmene(UpdateEpartmeneRequest request)
        {
            ResponseBase response = new ResponseBase();
            int count = await _freeSql.Update<Epartmene>(request.Id)
                .Set(a => a.Name == request.Name)
                .Set(a => a.Code == (request.Code ?? request.Name.ToPinYin()))
                .Set(a => a.Location == request.Location)
                .Set(a => a.Status == request.Status)
                .Set(a => a.Remark == request.Remark)
                .Set(a => a.UpdateUserId == UserInfo.Id)
                .Set(a => a.UpdateUserRealName == UserInfo.UpdateUserRealName)
                .Set(a => a.UpdateTime == UserInfo.UpdateTime)
                .ExecuteAffrowsAsync();
            if (count == 0)
            {
                response.Message = "科室不存在";
                response.Code = HttpStatusCode.BadRequest;
                response.Success = false;
            }

            return await response.ToJsonResultAsync();
        }



        /// <summary>
        /// 删除科室
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [MoudleInfo("删除科室")]
        [HttpPost("api/Management/DeleteEpartmene")]
        public async Task<ActionResult<ResponseBase>> DeleteEpartmene(DeleteEpartmeneRequest request)
        {
            ResponseBase response = new ResponseBase();
            int count = await _freeSql.Delete<Epartmene>().Where(o => o.Id == request.Id).ExecuteAffrowsAsync();
            if (count == 0)
            {
                response.Message = "科室不存在";
                response.Code = HttpStatusCode.BadRequest;
                response.Success = false;
            }
            return await response.ToJsonResultAsync();
        }


        /// <summary>
        /// 获取科室列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [MoudleInfo("获取科室列表")]
        [HttpPost("api/Management/GetEpartmenes")]
        public async Task<ActionResult<ResponseBase>> GetEpartmenes(GetEpartmenesRequest request)
        {
            ResponsePage<IList<GetEpartmeneResponse>> response = new ResponsePage<IList<GetEpartmeneResponse>>();

            request = request ?? new GetEpartmenesRequest();
            string alias = "e";
            var selectEpartmene = _freeSql.Select<Epartmene>().As(alias);
            if (!string.IsNullOrEmpty(request?.Name))
            {
                selectEpartmene = selectEpartmene.Where(o => o.Name.Contains(request.Name));

            }
            if (!string.IsNullOrEmpty(request?.Code))
            {
                selectEpartmene = selectEpartmene.Where(o => o.Code == request.Code);

            }
            if (!string.IsNullOrEmpty(request?.Remark))
            {
                selectEpartmene = selectEpartmene.Where(o => o.Remark.Contains(request.Remark));
            }
            if (request.Status != 0)
            {
                selectEpartmene = selectEpartmene.Where(o => o.Status == request.Status);
            }
            long totalCount = await selectEpartmene.CountAsync();
            var totalPage = totalCount.ToTotalPage(request.PageSize);
            response.Page.TotalPage = totalPage;
            response.Page.TotalCount = totalCount;
            selectEpartmene = selectEpartmene.Page(request.PageIndex, request.PageSize);

            foreach (var item in request.OrderBys)
            {
                string orderBy = item.IsAsc ? "ASC" : "DESC";
                selectEpartmene = selectEpartmene.OrderBy($"{alias}.{item.Name} {orderBy}");
            }
            var result = await selectEpartmene.From<Hospital>((e, h) => e.LeftJoin(a => a.HospitalId == h.Id)).ToListAsync((e, h) => new GetEpartmeneResponse()
            {
                Id = e.Id,
                Code = e.Code,
                Name = e.Name,
                Location = e.Location,
                Status = e.Status,
                Remark = e.Remark,
                HospitalId = h.Id,
                HospitalCode = h.Code,
                HospitalName = h.Name,
                HospitalFullName = h.FullName,
                UpdateTime = e.UpdateTime,
                AddTime = e.AddTime,
                AddUserId = e.AddUserId,
                UpdateUserId = e.UpdateUserId,
                AddUserRealName = e.AddUserRealName,
                UpdateUserRealName = e.UpdateUserRealName
            });

            response.Data = result;

            return await response.ToJsonResultAsync();
        }

        /// <summary>
        /// 获取科室详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MoudleInfo("获取科室详情")]
        [HttpGet("api/Management/GetEpartmene")]
        public async Task<ActionResult<ResponseBase>> GetEpartmene(string id)
        {
            ResponseBase<GetEpartmeneResponse> response = new ResponseBase<GetEpartmeneResponse>();
            if (string.IsNullOrWhiteSpace(id))
            {
                response.Message = "id不能为空";
                response.Code = HttpStatusCode.BadRequest;
                response.Success = false;
                return await response.ToJsonResultAsync();
            }
            var result = await _freeSql.Select<Epartmene>().From<Hospital>((e, h) => e.LeftJoin(a => a.HospitalId == h.Id))
                .Where((e, h) => e.Id == id).ToOneAsync((e, h) => new GetEpartmeneResponse()
                {
                    Id = e.Id,
                    Code = e.Code,
                    Name = e.Name,
                    Location = e.Location,
                    Status = e.Status,
                    Remark = e.Remark,
                    HospitalId = h.Id,
                    HospitalCode = h.Code,
                    HospitalName = h.Name,
                    HospitalFullName = h.FullName,
                    UpdateTime = e.UpdateTime,
                    AddTime = e.AddTime,
                    AddUserId = e.AddUserId,
                    UpdateUserId = e.UpdateUserId,
                    AddUserRealName = e.AddUserRealName,
                    UpdateUserRealName = e.UpdateUserRealName
                });

            response.Data = result;

            return await response.ToJsonResultAsync();
        }



        /// <summary>
        /// 新增医生信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [MoudleInfo("新增医生")]
        [HttpPost("api/Management/AddDoctor")]
        public async Task<ActionResult<ResponseBase>> AddDoctor(DoctorRequest request)
        {
            ResponseBase<string> response = new ResponseBase<string>();
            var epartmene = await _freeSql.Select<Epartmene>().Where(o => o.Id == request.EpartmeneId).ToOneAsync();
            if (epartmene == null)
            {
                response.Message = "科室不存在";
                response.Code = HttpStatusCode.BadRequest;
                response.Success = false;
            }
            else
            {
                UserInfo exsitUserInfo = await _freeSql.Select<UserInfo>().Where(o => o.Phone == request.Phone).ToOneAsync();

                if (exsitUserInfo != null)
                {
                    var isExsitDoctor = await _freeSql.Select<Doctor>().Where(o => o.BelongHospitalId == HttpContext.HospitalId() && o.UserId == exsitUserInfo.Id).AnyAsync();
                    if (isExsitDoctor)
                    {
                        response.Message = "存在当前医生用户";
                        response.Code = HttpStatusCode.BadRequest;
                        response.Success = false;
                    }
                    else
                    {
                        _freeSql.Transaction(() =>
                        {
                            string realNamePinYin = (request.RealName?.ToPinYin());
                            int count = _freeSql.Update<UserInfo>(exsitUserInfo.Id)                               
                                    .SetIf(!string.IsNullOrWhiteSpace(request.Phone), a => a.Phone == request.Phone)
                                    .SetIf(!string.IsNullOrWhiteSpace(request.Email), a => a.Email == request.Email)
                                    .SetIf(!string.IsNullOrWhiteSpace(request.RealName), a => a.RealName == request.RealName)
                                    .SetIf(request.Sex != 0, a => a.Sex == request.Sex)
                                    .SetIf(request.BirthDay != null, a => a.BirthDay == request.BirthDay)
                                    .SetIf(!string.IsNullOrWhiteSpace(request.IdCard), a => a.IdCard == request.IdCard)
                                    .SetIf(!string.IsNullOrWhiteSpace(realNamePinYin), a => a.RealNamePin == realNamePinYin)
                                    .SetIf(!string.IsNullOrWhiteSpace(request.Qq), a => a.Qq == request.Qq)
                                    .SetIf(!string.IsNullOrWhiteSpace(request.WxNo), a => a.WxNo == request.WxNo)
                                    .SetIf(!string.IsNullOrWhiteSpace(request.TelPhone), a => a.TelPhone == request.TelPhone)
                                    .SetIf(!string.IsNullOrWhiteSpace(request.PositionName), a => a.PositionName == request.PositionName)
                                    .SetIf(!string.IsNullOrWhiteSpace(request.Province), a => a.Province == request.Province)
                                    .SetIf(!string.IsNullOrWhiteSpace(request.City), a => a.City == request.City)
                                    .SetIf(!string.IsNullOrWhiteSpace(request.Area), a => a.Area == request.Area)
                                    .SetIf(!string.IsNullOrWhiteSpace(request.Address), a => a.Address == request.Address)
                                    .SetIf(!string.IsNullOrWhiteSpace(request.HeadImageUrl), a => a.HeadImageUrl == request.HeadImageUrl)
                                    .SetIf(!string.IsNullOrWhiteSpace(request.Remark), a => a.Remark == request.Remark)
                                    .Set(a => a.UpdateUserId == UserInfo.Id)
                                    .Set(a => a.UpdateUserRealName == UserInfo.UpdateUserRealName)
                                    .Set(a => a.UpdateTime == UserInfo.UpdateTime)
                                    .ExecuteAffrows();

                            Doctor doctor = new Doctor()
                            {
                                Id = Guid.NewGuid().ToString(),
                                EpartmeneId = request.EpartmeneId,
                                BelongHospitalId = epartmene.HospitalId,
                                Professional = request.PositionName,
                                Remark = request.Remark,
                                UserId = exsitUserInfo.Id,
                                AddTime = DateTime.Now,
                                AddUserId = UserInfo.Id,
                                AddUserRealName = UserInfo.RealName,
                                UpdateTime = DateTime.Now,
                                UpdateUserId = UserInfo.Id,
                                UpdateUserRealName = UserInfo.RealName

                            };
                            count = _freeSql.Insert<Doctor>(doctor).ExecuteAffrows();
                            response.Data = doctor.Id;
                        });

                    }


                }
                else
                {
                    _freeSql.Transaction(() =>
                    {

                        UserInfo userInfo = new UserInfo()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Password = DefaultPassword.PASSWORD.ToMd5(),
                            UserName = request.Phone,
                            Phone = request.Phone,
                            Email = request.Email,
                            RealName = request.RealName,
                            Sex = request.Sex,
                            BirthDay = request.BirthDay,
                            IdCard = request.IdCard,
                            RealNamePin = request.RealName?.ToPinYin(),
                            Qq = request.Qq,
                            WxNo = request.WxNo,
                            TelPhone = request.TelPhone,
                            PositionName = request.PositionName,
                            Province = request.Province,
                            City = request.City,
                            Area = request.Area,
                            Address = request.Address,
                            ExpiredTime = DateTime.Now.AddYears(100),
                            Status = 0,
                            Type = 1,
                            HeadImageUrl = request.HeadImageUrl,
                            Remark = request.Remark,
                            AddTime = DateTime.Now,
                            AddUserId = UserInfo.Id,
                            AddUserRealName = UserInfo.RealName,
                            UpdateTime = DateTime.Now,
                            UpdateUserId = UserInfo.Id,
                            UpdateUserRealName = UserInfo.RealName
                        };
                        int count = _freeSql.Insert<UserInfo>(userInfo).ExecuteAffrows();
                        Doctor doctor = new Doctor()
                        {
                            Id = Guid.NewGuid().ToString(),
                            EpartmeneId = request.EpartmeneId,
                            BelongHospitalId = epartmene.HospitalId,
                            Professional = request.PositionName,
                            Remark = request.Remark,
                            UserId = userInfo.Id,
                            AddTime = DateTime.Now,
                            AddUserId = UserInfo.Id,
                            AddUserRealName = UserInfo.RealName,
                            UpdateTime = DateTime.Now,
                            UpdateUserId = UserInfo.Id,
                            UpdateUserRealName = UserInfo.RealName

                        };
                        count = _freeSql.Insert<Doctor>(doctor).ExecuteAffrows();

                        response.Data = doctor.Id;
                    });
                }


            }


            return await response.ToJsonResultAsync();
        }



        /// <summary>
        /// 修改医生信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [MoudleInfo("修改医生")]
        [HttpPost("api/Management/UpdateDoctor")]
        public async Task<ActionResult<ResponseBase>> UpdateDoctor(UpdateDoctorRequest request)
        {
            ResponseBase<string> response = new ResponseBase<string>();
            var epartmene = await _freeSql.Select<Epartmene>().Where(o => o.Id == request.EpartmeneId).ToOneAsync();
            if (epartmene == null)
            {
                response.Message = "科室不存在";
                response.Code = HttpStatusCode.BadRequest;
                response.Success = false;
            }
            else
            {
                _freeSql.Transaction(() =>
                {

                    var doctor = _freeSql.Select<Doctor>().Where(o => o.Id == request.Id).ToOne();
                    if (doctor == null)
                    {
                        throw new Exception("医生信息不存在");
                    }
                    var userInfo = _freeSql.Select<UserInfo>().Where(o => o.Id == doctor.UserId).ToOne();
                    if (userInfo == null)
                    {
                        throw new Exception("医生用户信息不存在");
                    }
                    var exsitUserInfo = _freeSql.Select<UserInfo>().Where(o => o.Id != doctor.UserId && o.Phone == request.Phone).ToOne();
                    string realNamePinYin = (request.RealName?.ToPinYin());
                    if (exsitUserInfo != null)
                    {
                        int count = _freeSql.Update<Doctor>(request.Id)
                              .Set(a => a.BelongHospitalId == epartmene.HospitalId)
                              .Set(a => a.EpartmeneId == epartmene.Id)
                              .Set(a => a.Professional == request.PositionName)
                              .Set(a => a.UserId == exsitUserInfo.Id)
                              .Set(a => a.Remark == request.Remark)
                              .Set(a => a.UpdateUserId == UserInfo.Id)
                              .Set(a => a.UpdateUserRealName == UserInfo.UpdateUserRealName)
                              .Set(a => a.UpdateTime == UserInfo.UpdateTime)
                              .ExecuteAffrows();

                        int uCount = _freeSql.Update<UserInfo>(exsitUserInfo.Id)                       
                                .SetIf(!string.IsNullOrWhiteSpace(request.Phone), a => a.Phone == request.Phone)
                                .SetIf(!string.IsNullOrWhiteSpace(request.Email), a => a.Email == request.Email)
                                .SetIf(!string.IsNullOrWhiteSpace(request.RealName), a => a.RealName == request.RealName)
                                .SetIf(request.Sex != 0, a => a.Sex == request.Sex)
                                .SetIf(request.BirthDay != null, a => a.BirthDay == request.BirthDay)
                                .SetIf(!string.IsNullOrWhiteSpace(request.IdCard), a => a.IdCard == request.IdCard)
                                .SetIf(!string.IsNullOrWhiteSpace(realNamePinYin), a => a.RealNamePin == realNamePinYin)
                                .SetIf(!string.IsNullOrWhiteSpace(request.Qq), a => a.Qq == request.Qq)
                                .SetIf(!string.IsNullOrWhiteSpace(request.WxNo), a => a.WxNo == request.WxNo)
                                .SetIf(!string.IsNullOrWhiteSpace(request.TelPhone), a => a.TelPhone == request.TelPhone)
                                .SetIf(!string.IsNullOrWhiteSpace(request.PositionName), a => a.PositionName == request.PositionName)
                                .SetIf(!string.IsNullOrWhiteSpace(request.Province), a => a.Province == request.Province)
                                .SetIf(!string.IsNullOrWhiteSpace(request.City), a => a.City == request.City)
                                .SetIf(!string.IsNullOrWhiteSpace(request.Area), a => a.Area == request.Area)
                                .SetIf(!string.IsNullOrWhiteSpace(request.Address), a => a.Address == request.Address)
                                .SetIf(!string.IsNullOrWhiteSpace(request.HeadImageUrl), a => a.HeadImageUrl == request.HeadImageUrl)
                                .SetIf(!string.IsNullOrWhiteSpace(request.Remark), a => a.Remark == request.Remark)
                                .Set(a => a.UpdateUserId == UserInfo.Id)
                                .Set(a => a.UpdateUserRealName == UserInfo.UpdateUserRealName)
                                .Set(a => a.UpdateTime == UserInfo.UpdateTime)
                                .ExecuteAffrows();
                    }
                    else
                    {
                        int count = _freeSql.Update<Doctor>(request.Id)
                              .Set(a => a.BelongHospitalId == epartmene.HospitalId)
                              .Set(a => a.EpartmeneId == epartmene.Id)
                              .Set(a => a.Professional == request.PositionName)
                              .Set(a => a.Remark == request.Remark)
                              .Set(a => a.UpdateUserId == UserInfo.Id)
                              .Set(a => a.UpdateUserRealName == UserInfo.UpdateUserRealName)
                              .Set(a => a.UpdateTime == UserInfo.UpdateTime)
                              .ExecuteAffrows();

                        count = _freeSql.Update<UserInfo>(userInfo.Id)                     
                           .Set(a => a.Phone == request.Phone)
                           .Set(a => a.Email == request.Email)
                           .Set(a => a.RealName == request.RealName)
                           .Set(a => a.Sex == request.Sex)
                           .Set(a => a.BirthDay == request.BirthDay)
                           .Set(a => a.IdCard == request.IdCard)
                           .Set(a => a.RealNamePin == realNamePinYin)
                           .Set(a => a.Qq == request.Qq)
                           .Set(a => a.WxNo == request.WxNo)
                           .Set(a => a.TelPhone == request.TelPhone)
                           .Set(a => a.PositionName == request.PositionName)
                           .Set(a => a.Province == request.Province)
                           .Set(a => a.City == request.City)
                           .Set(a => a.Area == request.Area)
                           .Set(a => a.Address == request.Address)
                           .Set(a => a.HeadImageUrl == request.HeadImageUrl)
                           .Set(a => a.Remark == request.Remark)
                           .Set(a => a.UpdateUserId == UserInfo.Id)
                           .Set(a => a.UpdateUserRealName == UserInfo.UpdateUserRealName)
                           .Set(a => a.UpdateTime == UserInfo.UpdateTime)
                           .ExecuteAffrows();
                    }


                    response.Data = doctor.Id;
                });


            }


            return await response.ToJsonResultAsync();
        }


        /// <summary>
        /// 删除医生信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [MoudleInfo("删除医生")]
        [HttpPost("api/Management/DeleteDoctor")]
        public async Task<ActionResult<ResponseBase>> DeleteDoctor(DeleteDoctorRequest request)
        {
            ResponseBase response = new ResponseBase();
            _freeSql.Transaction(() =>
            {

                var doctor = _freeSql.Select<Doctor>().Where(o => o.Id == request.Id).ToOne();
                if (doctor == null)
                {
                    throw new Exception("医生信息不存在");
                }

              
                int count = _freeSql.Delete<Doctor>().Where(o => o.Id == request.Id).ExecuteAffrows();

                bool isExistDoctor = _freeSql.Select<Doctor>().Where(o => o.UserId == doctor.UserId).Any();
                if (!isExistDoctor)
                {
                    count += _freeSql.Delete<UserInfo>().Where(o => o.Id == doctor.UserId).ExecuteAffrows();
                }
                if (count == 0)
                {
                    throw new Exception("医生用户信息不存在");
                }

            });

            return await response.ToJsonResultAsync();
        }


        /// <summary>
        /// 获取医生信息列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [MoudleInfo("获取医生列表")]
        [HttpPost("api/Management/GetDoctors")]
        public async Task<ActionResult<ResponseBase>> GetDoctors(GetDoctorsRequest request)
        {
            ResponsePage<IList<GetDoctorResponse>> response = new ResponsePage<IList<GetDoctorResponse>>();

            request = request ?? new GetDoctorsRequest();
            var selectDoctor = _freeSql.Select<Doctor, UserInfo, Epartmene, Hospital>()
                 .LeftJoin((d, u, e, h) => d.UserId == u.Id)
                 .LeftJoin((d, u, e, h) => d.EpartmeneId == e.Id)
                 .LeftJoin((d, u, e, h) => d.BelongHospitalId == h.Id);
            if (!string.IsNullOrEmpty(request?.HospitalId))
            {
                selectDoctor = selectDoctor.Where((d, u, e, h) => d.BelongHospitalId == request.HospitalId);
            }
            if (!string.IsNullOrEmpty(request?.EpartmeneId))
            {
                selectDoctor = selectDoctor.Where((d, u, e, h) => d.EpartmeneId == request.EpartmeneId);
            }
            if (!string.IsNullOrEmpty(request?.Phone))
            {
                selectDoctor = selectDoctor.Where((d, u, e, h) => u.Phone.Contains(request.Phone));
            }
            if (!string.IsNullOrEmpty(request?.Email))
            {
                selectDoctor = selectDoctor.Where((d, u, e, h) => u.Email.Contains(request.Email));
            }
            if (!string.IsNullOrEmpty(request?.RealName))
            {
                selectDoctor = selectDoctor.Where((d, u, e, h) => u.RealName.Contains(request.RealName));
            }
            if (request?.BirthDayStart != null)
            {
                selectDoctor = selectDoctor.Where((d, u, e, h) => u.BirthDay >= request.BirthDayStart.Value);
            }
            if (request?.BirthDayEnd != null)
            {
                selectDoctor = selectDoctor.Where((d, u, e, h) => u.BirthDay <= request.BirthDayEnd.Value);
            }

            if (!string.IsNullOrEmpty(request?.IdCard))
            {
                selectDoctor = selectDoctor.Where((d, u, e, h) => u.IdCard.Contains(request.IdCard));
            }

            if (!string.IsNullOrEmpty(request?.Qq))
            {
                selectDoctor = selectDoctor.Where((d, u, e, h) => u.Qq.Contains(request.Qq));

            }
            if (!string.IsNullOrEmpty(request?.WxNo))
            {
                selectDoctor = selectDoctor.Where((d, u, e, h) => u.WxNo.Contains(request.WxNo));

            }
            if (!string.IsNullOrEmpty(request?.TelPhone))
            {
                selectDoctor = selectDoctor.Where((d, u, e, h) => u.TelPhone.Contains(request.TelPhone));

            }
            if (!string.IsNullOrEmpty(request?.PositionName))
            {
                selectDoctor = selectDoctor.Where((d, u, e, h) => u.Email.Contains(request.PositionName));
            }
            if (!string.IsNullOrEmpty(request?.Province))
            {
                selectDoctor = selectDoctor.Where((d, u, e, h) => u.Province == request.Province);
            }
            if (!string.IsNullOrEmpty(request?.City))
            {
                selectDoctor = selectDoctor.Where((d, u, e, h) => u.City == request.City);
            }
            if (!string.IsNullOrEmpty(request?.Area))
            {
                selectDoctor = selectDoctor.Where((d, u, e, h) => u.Area == request.Area);
            }
            if (!string.IsNullOrEmpty(request?.Address))
            {
                selectDoctor = selectDoctor.Where((d, u, e, h) => u.Address.Contains(request.Address));
            }
            if (!string.IsNullOrEmpty(request?.Remark))
            {
                selectDoctor = selectDoctor.Where((d, u, e, h) => u.Remark.Contains(request.Remark));
            }
            long totalCount = await selectDoctor.CountAsync();
            var totalPage = totalCount.ToTotalPage(request.PageSize);
            response.Page.TotalPage = totalPage;
            response.Page.TotalCount = totalCount;
            selectDoctor = selectDoctor.Page(request.PageIndex, request.PageSize);

            foreach (var item in request.OrderBys)
            {
                string orderBy = item.IsAsc ? "ASC" : "DESC";
                selectDoctor = selectDoctor.OrderBy($"{item.Name} {orderBy}");
            }
            var result = await selectDoctor.ToListAsync((d, u, e, h) => new GetDoctorResponse()
            {
                Id = d.Id,
                EpartmeneId = e.Id,
                EpartmeneName = e.Name,
                HospitalId = h.Id,
                HospitalCode = h.Code,
                HospitalName = h.Name,
                HospitalFullName = h.FullName,
                Remark = u.Remark,
                UserId = u.Id,
                UserName = u.UserName,
                Phone = u.Phone,
                Email = u.Email,
                RealName = u.RealName,
                BirthDay = u.BirthDay,
                IdCard = u.IdCard,
                Qq = u.Qq,
                WxNo = u.WxNo,
                TelPhone = u.TelPhone,
                PositionName = u.PositionName,
                Province = u.Province,
                City = u.UserName,
                Area = u.Area,
                Address = u.Address,
                Status = u.Status,
                Type = u.Type,
                WxOpenId = u.WxOpenId,
                WxUnionId = u.WxUnionId,
                UpdateTime = d.UpdateTime,
                AddTime = d.AddTime,
                AddUserId = d.AddUserId,
                UpdateUserId = d.UpdateUserId,
                AddUserRealName = d.AddUserRealName,
                UpdateUserRealName = d.UpdateUserRealName
            });

            response.Data = result;

            return await response.ToJsonResultAsync();
        }

        /// <summary>
        /// 获取医生详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MoudleInfo("获取医生详情")]
        [HttpGet("api/Management/GetDoctor")]
        public async Task<ActionResult<ResponseBase>> GetDoctor(string id)
        {
            ResponseBase<GetDoctorResponse> response = new ResponseBase<GetDoctorResponse>();
            if (string.IsNullOrWhiteSpace(id))
            {
                response.Message = "id不能为空";
                response.Code = HttpStatusCode.BadRequest;
                response.Success = false;
                return await response.ToJsonResultAsync();
            }
            var result = await _freeSql.Select<Doctor, UserInfo, Epartmene, Hospital>()
                 .LeftJoin((d, u, e, h) => d.UserId == u.Id)
                 .LeftJoin((d, u, e, h) => d.EpartmeneId == e.Id)
                 .LeftJoin((d, u, e, h) => d.BelongHospitalId == h.Id).Where((d, u, e, h) => d.Id == id).ToOneAsync((d, u, e, h) => new GetDoctorResponse()
                 {
                     Id = d.Id,
                     EpartmeneId = e.Id,
                     EpartmeneName = e.Name,
                     HospitalId = h.Id,
                     HospitalCode = h.Code,
                     HospitalName = h.Name,
                     HospitalFullName = h.FullName,
                     Remark = u.Remark,
                     UserId = u.Id,
                     UserName = u.UserName,
                     Phone = u.Phone,
                     Email = u.Email,
                     RealName = u.RealName,
                     BirthDay = u.BirthDay,
                     IdCard = u.IdCard,
                     Qq = u.Qq,
                     WxNo = u.WxNo,
                     TelPhone = u.TelPhone,
                     PositionName = u.PositionName,
                     Province = u.Province,
                     City = u.UserName,
                     Area = u.Area,
                     Address = u.Address,
                     Status = u.Status,
                     Type = u.Type,
                     WxOpenId = u.WxOpenId,
                     WxUnionId = u.WxUnionId,
                     UpdateTime = d.UpdateTime,
                     AddTime = d.AddTime,
                     AddUserId = d.AddUserId,
                     UpdateUserId = d.UpdateUserId,
                     AddUserRealName = d.AddUserRealName,
                     UpdateUserRealName = d.UpdateUserRealName
                 });

            response.Data = result;

            return await response.ToJsonResultAsync();
        }




    }
}
