using FreeSql;
using JwSale.Api.Util;
using JwSale.Model;
using JwSale.Model.Dto;
using JwSale.Model.Dto.Common;
using JwSale.Util.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JwSale.Api.Extensions
{
    public static class Extension
    {
        public static string ToAnalysis(this string cgiType)
        {
            return $"-{cgiType}";
        }

        public static StringContent ToStringContent(this object obj)
        {
            var content = new StringContent(obj.ToJson(), Encoding.UTF8, "application/json");
            return content;
        }

        public static ByteArrayContent ToByteArrayContent(this byte[] buffer)
        {
            var content = new ByteArrayContent(buffer);
            return content;
        }

        public static ByteArrayContent ToByteArrayContent(this string hexStr)
        {
            var buffer = hexStr.StrToHexBuffer();
            var content = new ByteArrayContent(buffer);
            return content;
        }


        public static IEnumerable<T> AsEnumerable<T>(this IQueryable<T> source, string userId) where T : Entity
        {
            if (userId == null || userId == string.Empty)
            {
                return source;
            }
            else
            {
                return source.Where(o => o.AddUserId == userId).AsEnumerable();
            }

        }


        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> source, IList<OrderByBase> orderbys, string defaultOrderby = "AddTime")
        {
            if (orderbys == null || orderbys.Count == 0)
            {
                if (string.IsNullOrEmpty(defaultOrderby))
                {
                    return source;
                }
                else
                {
                    source = source.OrderByDescending(o => defaultOrderby);
                }

            }
            else
            {
                foreach (var item in orderbys)
                {
                    if (item.IsAsc)
                    {
                        source = source.OrderBy(o => item.Name);
                    }
                    else
                    {
                        source = source.OrderByDescending(o => item.Name);
                    }
                }
            }
            return source;
        }

        public static HttpResponseMessage ToHttpResponse(this string response, Dictionary<string, string> headers = null)
        {
            return HttpResponseMessageHelper.GetJsonOk(response, headers);
        }

        public static Task<HttpResponseMessage> ToHttpResponseAsync(this string response, Dictionary<string, string> headers = null)
        {
            return Task.FromResult(HttpResponseMessageHelper.GetJsonOk(response, headers));
        }
        public static HttpResponseMessage ToHttpResponse(this ResponseBase response, Dictionary<string, string> headers = null)
        {
            return HttpResponseMessageHelper.GetJsonOk(response, headers);
        }

        public static Task<HttpResponseMessage> ToHttpResponseAsync(this object response, Dictionary<string, string> headers = null)
        {
            return Task.FromResult(HttpResponseMessageHelper.GetJsonOk(response, headers));
        }

        public static JsonResult ToJsonResult(this object response)
        {
            return new JsonResult(response);
        }
        public static Task<JsonResult> ToJsonResultAsync(this object response)
        {
            return Task.FromResult(new JsonResult(response));
        }




        public static IQueryable<T> AsQueryable<T>(this IQueryable<T> source, string userId) where T : Entity
        {
            if (userId == null || userId == string.Empty)
            {
                return source;
            }
            else
            {
                return source.Where(o => o.AddUserId == userId).AsQueryable();
            }

        }

        public static IQueryable<T> ToPage<T>(this IQueryable<T> source, RequestPageBase requestPageBase) where T : Entity
        {
            source = source.Skip((requestPageBase.PageIndex - 1) * requestPageBase.PageSize).Take(requestPageBase.PageSize).OrderBy(requestPageBase.OrderBys);
            return source;
        }
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, IList<OrderByBase> orderbys, string defaultOrderby = "AddTime") where T : class, IEntity
        {
            if (orderbys == null || orderbys.Count == 0)
            {
                if (string.IsNullOrEmpty(defaultOrderby))
                {
                    return source;
                }
                else
                {
                    source = source.OrderByDescending(o => defaultOrderby);
                }

            }
            else
            {
                foreach (var item in orderbys)
                {
                    if (item.IsAsc)
                    {
                        source = source.OrderBy(o => item.Name);
                    }
                    else
                    {
                        source = source.OrderByDescending(o => item.Name);
                    }
                }
            }
            return source;
        }



    
        public static IUpdate<T> InitUpdateBaseEntityData<T>(this IUpdate<T> update, UserInfo userinfo) where T : Entity
        {
            var now = DateTime.Now;
            update = update.Set(a => a.UpdateTime == now)
               .Set(a => a.UpdateUserId == userinfo.Id)
               .Set(a => a.UpdateUserRealName == userinfo.RealName);

            return update;
        }

     

        public static T InitAddBaseEntityData<T>(this T entity, UserInfo userinfo) where T : Entity
        {
            entity.AddTime = DateTime.Now;
            entity.AddUserId = userinfo.Id;
            entity.AddUserRealName = userinfo.RealName;
            entity.UpdateTime = DateTime.Now;
            entity.UpdateUserId = userinfo.Id;
            entity.UpdateUserRealName = userinfo.RealName;
            return entity;
        }
        public static T InitUpdateBaseEntityData<T>(this T entity, UserInfo userinfo) where T : Entity
        {
            entity.UpdateTime = DateTime.Now;
            entity.UpdateUserId = userinfo.Id;
            entity.UpdateUserRealName = userinfo.RealName;
            return entity;
        }
        public static ISelect<T> OrderBy<T>(this ISelect<T> select, IList<OrderByBase> orderBys) where T : Entity
        {
            if (orderBys != null)
            {
                foreach (var item in orderBys)
                {
                    if (item.IsAsc)
                    {
                        select = select.OrderByIf(!item.Name.IsNullOrWhiteSpace(), o => item.Name);
                    }
                    else
                    {
                        select = select.OrderByDescending(!item.Name.IsNullOrWhiteSpace(), o => item.Name);
                    }

                }
            }
            return select;
        }

        public static ISelect<T1, T2> OrderBy<T1, T2>(this ISelect<T1, T2> select, IList<OrderByBase> orderBys) where T1 : Entity where T2 : Entity
        {
            if (orderBys != null)
            {
                foreach (var item in orderBys)
                {
                    select = select.OrderByPropertyNameIf(!item.Name.IsNullOrWhiteSpace(), item.Name, item.IsAsc);

                }
            }
            return select;
        }

        public static ISelect<T1, T2, T3> OrderBy<T1, T2, T3>(this ISelect<T1, T2, T3> select, IList<OrderByBase> orderBys) where T1 : Entity where T2 : Entity where T3 : Entity
        {
            if (orderBys != null)
            {
                foreach (var item in orderBys)
                {
                    select = select.OrderByPropertyNameIf(!item.Name.IsNullOrWhiteSpace(), item.Name, item.IsAsc);

                }
            }
            return select;
        }



    }
}
