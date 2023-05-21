using JwSale.Util.Attributes;
using JwSale.Util.Excels;
using JwSale.Util.Properties;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.International.Converters.PinYinConverter;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;

namespace JwSale.Util.Extensions
{
    public static class Extension
    {


        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time">时间</param>
        /// <param name="pow"></param>
        /// <returns></returns>
        public static long ToTimeStamp(this DateTime time, int pow)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (time.Ticks - startTime.Ticks) / (long)Math.Pow(10, pow);
            return t;
        }

        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long To10TimeStamp(this DateTime time)
        {
            return ToTimeStamp(time, 7);
        }
        /// <summary>
        /// 转base64图片
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static string ToBase64Img(this byte[] buffer)
        {
            return $"data:img/jpg;base64,{Convert.ToBase64String(buffer)}";
        }

        /// <summary>
        /// 字符串转16进制字节数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] StrToHexBuffer(this string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }


        /// <summary>
        /// 字节数组转16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string HexBufferToStr(this byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    sb.Append(bytes[i].ToString("X2"));
                }
            }
            return sb.ToString();
        }




        public static string HexToBin(this string input, int toType = 8)
        {
            StringBuilder sb = new StringBuilder();
            input.Select(o =>
            {
                sb.Append(Convert.ToString(o, 8));
                return o;
            }).ToList();

            return sb.ToString();
        }





        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static IEnumerable<T> ToPage<T>(this IEnumerable<T> source, int pageIndex, int pageSize)
        {
            return source.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }

        /// <summary>
        /// 汉字转化为拼音
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToPinYin(this string source)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char obj in source)
            {
                try
                {
                    ChineseChar chineseChar = new ChineseChar(obj);
                    string t = chineseChar.Pinyins[0].ToString();
                    sb.Append(t.Substring(0, t.Length - 1));
                }
                catch
                {
                    sb.Append(obj.ToString());
                }
            }
            return sb.ToString();

        }

        /// <summary>
        /// 汉字转化为拼音首字母
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToFirstPinYin(this string source)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char obj in source)
            {
                try
                {
                    ChineseChar chineseChar = new ChineseChar(obj);
                    string t = chineseChar.Pinyins[0].ToString();
                    sb.Append(t.Substring(0, t.Length - 1));
                }
                catch
                {
                    sb.Append(obj.ToString());
                }
            }
            return sb.ToString();
        }

        public static string ToDes(this string encryptString, string key)
        {
            try
            {
                key = key.Substring(0, 8);
                byte[] rgbKey = Encoding.UTF8.GetBytes(key);
                //用于对称算法的初始化向量（默认值）。
                byte[] rgbIV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            { }
            return null;

        }

        public static string FromDes(this string decryptString, string key)
        {
            try
            {
                key = key.Substring(0, 8);
                //用于对称算法的初始化向量（默认值）
                byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
                byte[] rgbKey = Encoding.UTF8.GetBytes(key);
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            { }
            return null;
        }




        public static string ToMd5(this string strText)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = System.Text.Encoding.UTF8.GetBytes(strText);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x");
            }

            return byte2String;
        }


        public static TOptions GetOptions<TOptions>(this IConfiguration configuration) where TOptions : class, new()
        {
            return default(TOptions);
        }

        public static string ToJson(this object obj)
        {
            if (obj.GetType().Equals(typeof(string)))
            {
                return obj.ToString();
            }
            return JsonConvert.SerializeObject(obj);
        }

        public static T ToObj<T>(this string soucre)
        {
            return JsonConvert.DeserializeObject<T>(soucre);
        }
        public static object ToObj(this string soucre)
        {
            try
            {
                return JsonConvert.DeserializeObject(soucre);
            }
            catch
            {

            }
            return soucre;
        }


        public static IEnumerable<Type> ContainAttributeType(this IEnumerable<Type> source, Type attribute)
        {
            return source.Where(o => o.GetCustomAttributes(attribute, true) != null);
        }

        public static IEnumerable<ServiceDescriptor> GetServices(this IServiceCollection services, Type type)
        {
            return services.Where(m => type.IsAssignableFrom(m.ServiceType));
        }

        /// <summary>
        /// 如果指定服务不存在，添加指定服务
        /// </summary>
        public static ServiceDescriptor GetOrAdd(this IServiceCollection services, ServiceDescriptor toAdDescriptor)
        {
            ServiceDescriptor descriptor = services.FirstOrDefault(m => m.ServiceType == toAdDescriptor.ServiceType);
            if (descriptor != null)
            {
                return descriptor;
            }

            services.Add(toAdDescriptor);
            return toAdDescriptor;
        }


        /// <summary>
        /// 如果指定服务不存在，创建实例并添加
        /// </summary>
        public static TServiceType GetOrAddSingletonInstance<TServiceType>(this IServiceCollection services, Func<TServiceType> factory) where TServiceType : class
        {
            TServiceType item = (TServiceType)services.FirstOrDefault(m => m.ServiceType == typeof(TServiceType) && m.Lifetime == ServiceLifetime.Singleton)?.ImplementationInstance;
            if (item == null)
            {
                item = factory();
                services.AddSingleton<TServiceType>(item);
            }
            return item;
        }


        /// <summary>
        /// 如果指定服务不存在，创建实例并添加
        /// </summary>
        public static TServiceType GetSingletonInstance<TServiceType>(this IServiceCollection services, Func<TServiceType> factory) where TServiceType : class
        {
            TServiceType item = (TServiceType)services.FirstOrDefault(m => m.ServiceType == typeof(TServiceType) && m.Lifetime == ServiceLifetime.Singleton)?.ImplementationInstance;
            return item;
        }

        /// <summary>
        /// 获取单例注册服务对象
        /// </summary>
        public static T GetSingletonInstanceOrNull<T>(this IServiceCollection services)
        {
            return (T)services.FirstOrDefault(d => d.ServiceType == typeof(T))?.ImplementationInstance;
        }

        /// <summary>
        /// 获取单例注册服务对象
        /// </summary>
        public static T GetSingletonInstance<T>(this IServiceCollection services)
        {
            var instance = services.GetSingletonInstanceOrNull<T>();
            if (instance == null)
            {
                throw new InvalidOperationException($"无法找到已注册的单例服务：{typeof(T).AssemblyQualifiedName}");
            }

            return instance;
        }



        /// <summary>
        /// 如果条件成立，添加项
        /// </summary>
        public static void AddIf<T>(this ICollection<T> collection, T value, bool flag)
        {
            collection.CheckNotNull("collection");
            if (flag)
            {
                collection.Add(value);
            }
        }

        /// <summary>
        /// 如果条件成立，添加项
        /// </summary>
        public static void AddIf<T>(this ICollection<T> collection, T value, Func<bool> func)
        {
            collection.CheckNotNull("collection");
            if (func())
            {
                collection.Add(value);
            }
        }

        /// <summary>
        /// 如果不存在，添加项
        /// </summary>
        public static void AddIfNotExist<T>(this ICollection<T> collection, T value, Func<T, bool> existFunc = null)
        {
            collection.CheckNotNull("collection");
            bool exists = existFunc == null ? collection.Contains(value) : existFunc(value);
            if (!exists)
            {
                collection.Add(value);
            }
        }

        /// <summary>
        /// 如果不为空，添加项
        /// </summary>
        public static void AddIfNotNull<T>(this ICollection<T> collection, T value) where T : class
        {
            collection.CheckNotNull("collection");
            if (value != null)
            {
                collection.Add(value);
            }
        }
        /// <summary>
        /// 如何不为空 并且不存在,添加项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="value"></param>
        /// <param name=""></param>
        /// <param name="existFunc"></param>
        public static void AddIfNotNullAndNotExsit<T>(this ICollection<T> collection, T value, Func<T, bool> existFunc = null) where T : class
        {
            collection.CheckNotNull("collection");
            bool exists = existFunc == null ? collection.Contains(value) : existFunc(value);
            if (value != null && !exists)
            {
                collection.Add(value);
            }
        }

        /// <summary>
        /// 如何不为空 并且不存在,添加项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="values"></param>
        /// <param name="existFunc"></param>
        public static void AddIfNotNullAndNotExsit<T>(this ICollection<T> collection, IList<T> values, Func<T, bool> existFunc = null) where T : class
        {
            collection.CheckNotNull("collection");
            if (values != null)
            {
                foreach (var value in values)
                {
                    bool exists = existFunc == null ? collection.Contains(value) : existFunc(value);
                    if (value != null && !exists)
                    {
                        collection.Add(value);
                    }
                }
            }

        }

        /// <summary>
        /// 获取对象，不存在对使用委托添加对象
        /// </summary>
        public static T GetOrAdd<T>(this ICollection<T> collection, Func<T, bool> selector, Func<T> factory)
        {
            collection.CheckNotNull("collection");
            T item = collection.FirstOrDefault(selector);
            if (item == null)
            {
                item = factory();
                collection.Add(item);
            }

            return item;
        }

        /// <summary>
        /// 根据第三方条件是否为真来决定是否执行指定条件的查询
        /// </summary>
        /// <param name="source"> 要查询的源 </param>
        /// <param name="predicate"> 查询条件 </param>
        /// <param name="condition"> 第三方条件 </param>
        /// <typeparam name="T"> 动态类型 </typeparam>
        /// <returns> 查询的结果 </returns>
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, Func<T, bool> predicate, bool condition)
        {
            predicate.CheckNotNull("predicate");
            source = source as IList<T> ?? source.ToList();

            return condition ? source.Where(predicate) : source;
        }
        /// <summary>
        /// 根据第三方条件是否为真来决定是否执行指定条件的查询
        /// </summary>
        /// <param name="source"> 要查询的源 </param>
        /// <param name="predicate"> 查询条件 </param>
        /// <param name="condition"> 第三方条件 </param>
        /// <typeparam name="T"> 动态类型 </typeparam>
        /// <returns> 查询的结果 </returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate, bool condition)
        {
            source.CheckNotNull("source");
            predicate.CheckNotNull("predicate");

            return condition ? source.Where(predicate) : source;
        }


        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        ///反射 深拷贝
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T DeepCopyByReflection<T>(this object obj) where T : new()
        {


            object retval = Activator.CreateInstance(typeof(T));
            FieldInfo[] fields = typeof(T).GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
            foreach (var field in fields)
            {
                try
                {
                    field.SetValue(retval, DeepCopyByReflection<object>(field.GetValue(obj)));
                }
                catch { }
            }

            return (T)retval;
        }

        /// <summary>
        /// 二进制序列化和反序列化 深拷贝
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T DeepCopyByBinary<T>(this object obj) where T : new()
        {
            object retval;
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                retval = bf.Deserialize(ms);
                ms.Close();
            }
            return (T)retval;
        }


        /// <summary>
        /// 验证指定值的断言<paramref name="assertion"/>是否为真，如果不为真，抛出指定消息<paramref name="message"/>的指定类型<typeparamref name="TException"/>异常
        /// </summary>
        /// <typeparam name="TException">异常类型</typeparam>
        /// <param name="assertion">要验证的断言。</param>
        /// <param name="message">异常消息。</param>
        private static void Require<TException>(bool assertion, string message) where TException : Exception
        {
            if (assertion)
            {
                return;
            }
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException("message");
            }
            TException exception = (TException)Activator.CreateInstance(typeof(TException), message);
            throw exception;
        }

        /// <summary>
        /// 验证指定值的断言表达式是否为真，不为值抛出<see cref="Exception"/>异常
        /// </summary>
        /// <param name="value"></param>
        /// <param name="assertionFunc">要验证的断言表达式</param>
        /// <param name="message">异常消息</param>
        public static void Required<T>(this T value, Func<T, bool> assertionFunc, string message)
        {
            if (assertionFunc == null)
            {
                throw new ArgumentNullException("assertionFunc");
            }
            Require<Exception>(assertionFunc(value), message);
        }

        /// <summary>
        /// 验证指定值的断言表达式是否为真，不为真抛出<typeparamref name="TException"/>异常
        /// </summary>
        /// <typeparam name="T">要判断的值的类型</typeparam>
        /// <typeparam name="TException">抛出的异常类型</typeparam>
        /// <param name="value">要判断的值</param>
        /// <param name="assertionFunc">要验证的断言表达式</param>
        /// <param name="message">异常消息</param>
        public static void Required<T, TException>(this T value, Func<T, bool> assertionFunc, string message) where TException : Exception
        {
            if (assertionFunc == null)
            {
                throw new ArgumentNullException("assertionFunc");
            }
            Require<TException>(assertionFunc(value), message);
        }

        /// <summary>
        /// 检查参数不能为空引用，否则抛出<see cref="ArgumentNullException"/>异常。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="paramName">参数名称</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void CheckNotNull<T>(this T value, string paramName) where T : class
        {
            Require<ArgumentNullException>(value != null, string.Format(Resources.ParameterCheck_NotNull, paramName));
        }

        /// <summary>
        /// 检查字符串不能为空引用或空字符串，否则抛出<see cref="ArgumentNullException"/>异常或<see cref="ArgumentException"/>异常。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="paramName">参数名称。</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void CheckNotNullOrEmpty(this string value, string paramName)
        {
            value.CheckNotNull(paramName);
            Require<ArgumentException>(value.Length > 0, string.Format(Resources.ParameterCheck_NotNullOrEmpty_String, paramName));
        }

        /// <summary>
        /// 检查Guid值不能为Guid.Empty，否则抛出<see cref="ArgumentException"/>异常。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="paramName">参数名称。</param>
        /// <exception cref="ArgumentException"></exception>
        public static void CheckNotEmpty(this Guid value, string paramName)
        {
            Require<ArgumentException>(value != Guid.Empty, string.Format(Resources.ParameterCheck_NotEmpty_Guid, paramName));
        }

        /// <summary>
        /// 检查集合不能为空引用或空集合，否则抛出<see cref="ArgumentNullException"/>异常或<see cref="ArgumentException"/>异常。
        /// </summary>
        /// <typeparam name="T">集合项的类型。</typeparam>
        /// <param name="collection"></param>
        /// <param name="paramName">参数名称。</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void CheckNotNullOrEmpty<T>(this IEnumerable<T> collection, string paramName)
        {
            collection.CheckNotNull(paramName);
            Require<ArgumentException>(collection.Any(), string.Format(Resources.ParameterCheck_NotNullOrEmpty_Collection, paramName));
        }

        /// <summary>
        /// 检查参数必须小于[或可等于，参数canEqual]指定值，否则抛出<see cref="ArgumentOutOfRangeException"/>异常。
        /// </summary>
        /// <typeparam name="T">参数类型。</typeparam>
        /// <param name="value"></param>
        /// <param name="paramName">参数名称。</param>
        /// <param name="target">要比较的值。</param>
        /// <param name="canEqual">是否可等于。</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void CheckLessThan<T>(this T value, string paramName, T target, bool canEqual = false) where T : IComparable<T>
        {
            bool flag = canEqual ? value.CompareTo(target) <= 0 : value.CompareTo(target) < 0;
            string format = canEqual ? Resources.ParameterCheck_NotLessThanOrEqual : Resources.ParameterCheck_NotLessThan;
            Require<ArgumentOutOfRangeException>(flag, string.Format(format, paramName, target));
        }

        /// <summary>
        /// 检查参数必须大于[或可等于，参数canEqual]指定值，否则抛出<see cref="ArgumentOutOfRangeException"/>异常。
        /// </summary>
        /// <typeparam name="T">参数类型。</typeparam>
        /// <param name="value"></param>
        /// <param name="paramName">参数名称。</param>
        /// <param name="target">要比较的值。</param>
        /// <param name="canEqual">是否可等于。</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void CheckGreaterThan<T>(this T value, string paramName, T target, bool canEqual = false) where T : IComparable<T>
        {
            bool flag = canEqual ? value.CompareTo(target) >= 0 : value.CompareTo(target) > 0;
            string format = canEqual ? Resources.ParameterCheck_NotGreaterThanOrEqual : Resources.ParameterCheck_NotGreaterThan;
            Require<ArgumentOutOfRangeException>(flag, string.Format(format, paramName, target));
        }

        /// <summary>
        /// 检查参数必须在指定范围之间，否则抛出<see cref="ArgumentOutOfRangeException"/>异常。
        /// </summary>
        /// <typeparam name="T">参数类型。</typeparam>
        /// <param name="value"></param>
        /// <param name="paramName">参数名称。</param>
        /// <param name="start">比较范围的起始值。</param>
        /// <param name="end">比较范围的结束值。</param>
        /// <param name="startEqual">是否可等于起始值</param>
        /// <param name="endEqual">是否可等于结束值</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void CheckBetween<T>(this T value, string paramName, T start, T end, bool startEqual = false, bool endEqual = false)
            where T : IComparable<T>
        {
            bool flag = startEqual ? value.CompareTo(start) >= 0 : value.CompareTo(start) > 0;
            string message = startEqual
                ? string.Format(Resources.ParameterCheck_Between, paramName, start, end)
                : string.Format(Resources.ParameterCheck_BetweenNotEqual, paramName, start, end, start);
            Require<ArgumentOutOfRangeException>(flag, message);

            flag = endEqual ? value.CompareTo(end) <= 0 : value.CompareTo(end) < 0;
            message = endEqual
                ? string.Format(Resources.ParameterCheck_Between, paramName, start, end)
                : string.Format(Resources.ParameterCheck_BetweenNotEqual, paramName, start, end, end);
            Require<ArgumentOutOfRangeException>(flag, message);
        }

        /// <summary>
        /// 检查指定路径的文件夹必须存在，否则抛出<see cref="DirectoryNotFoundException"/>异常。
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="paramName">参数名称。</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        public static void CheckDirectoryExists(this string directory, string paramName = null)
        {
            CheckNotNull(directory, paramName);
            Require<DirectoryNotFoundException>(Directory.Exists(directory), string.Format(Resources.ParameterCheck_DirectoryNotExists, directory));
        }

        /// <summary>
        /// 检查指定路径的文件必须存在，否则抛出<see cref="FileNotFoundException"/>异常。
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="paramName">参数名称。</param>
        /// <exception cref="ArgumentNullException">当文件路径为null时</exception>
        /// <exception cref="FileNotFoundException">当文件路径不存在时</exception>
        public static void CheckFileExists(this string filename, string paramName = null)
        {
            CheckNotNull(filename, paramName);
            Require<FileNotFoundException>(File.Exists(filename), string.Format(Resources.ParameterCheck_FileNotExists, filename));
        }
        public static byte[] ToBuffer(this Stream sm)
        {
            sm.Seek(0, SeekOrigin.Begin);
            byte[] buffer = new byte[sm.Length];
            sm.Read(buffer, 0, buffer.Length);
            sm.Seek(0, SeekOrigin.Begin);
            return buffer;
        }

        public static async Task<byte[]> ToBufferAsync(this Stream sm)
        {
            sm.Seek(0, SeekOrigin.Begin);
            byte[] buffer = new byte[sm.Length];
            await sm.ReadAsync(buffer, 0, buffer.Length);
            sm.Seek(0, SeekOrigin.Begin);
            return buffer;
        }

        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="url"></param>
        /// <param name="pixel"></param>
        /// <returns></returns>
        public static Bitmap CreateQRCode(this string url, int pixel = 5)
        {
            QRCodeEncoder qRCodeEncoder = new QRCodeEncoder();
            //设置二维码编码格式
            qRCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            //设置编码测量度
            qRCodeEncoder.QRCodeScale = 80;
            //设置编码版本
            qRCodeEncoder.QRCodeVersion = 7;
            //设置错误校验
            qRCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;

            Bitmap img = qRCodeEncoder.Encode(url);

            return img;


        }



        /// <summary>
        /// 解码二维码
        /// </summary>
        /// <param name="buffer">待解码的二维码图片</param>
        /// <returns>扫码结果</returns>
        public static string DecodeQrCode(this byte[] buffer)
        {
            MemoryStream sm = new MemoryStream();
            sm.Write(buffer, 0, buffer.Length);
            Bitmap bitMap = new Bitmap(sm);//实例化位图对象，把文件实例化为带有颜色信息的位图对象  
            QRCodeDecoder decoder = new QRCodeDecoder();//实例化QRCodeDecoder  
            //通过.decoder方法把颜色信息转换成字符串信息  
            var decoderStr = decoder.decode(new QRCodeBitmapImage(bitMap), System.Text.Encoding.UTF8);
            return decoderStr;
        }

        public static string DecodeQrCode(this Stream sm)
        {
            Bitmap bitMap = new Bitmap(sm);//实例化位图对象，把文件实例化为带有颜色信息的位图对象  
            QRCodeDecoder decoder = new QRCodeDecoder();//实例化QRCodeDecoder  
            //通过.decoder方法把颜色信息转换成字符串信息  
            var decoderStr = decoder.decode(new QRCodeBitmapImage(bitMap), System.Text.Encoding.UTF8);
            return decoderStr;
        }


        public static NameValueCollection ParseUrl(this string url)
        {
            if (url == null)
            {
                throw new ArgumentNullException("url");
            }
            NameValueCollection nvc = new NameValueCollection();

            if (url == "")
                return nvc;
            int questionMarkIndex = url.IndexOf('?');
            if (questionMarkIndex == -1)
            {
                return nvc;
            }

            if (questionMarkIndex == url.Length - 1)
                return nvc;
            string ps = url.Substring(questionMarkIndex + 1);
            // 开始分析参数对  
            Regex re = new Regex(@"(^|&)?(\w+)=([^&]+)(&|$)?", RegexOptions.Compiled);
            MatchCollection mc = re.Matches(ps);
            foreach (Match m in mc)
            {
                nvc.Add(m.Result("$2").ToLower(), m.Result("$3"));
            }
            return nvc;
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="sheetName"></param>
        /// <param name="isNo"></param>
        public static ExcelPackage AddSheet<T>(this ExcelPackage ep, IList<T> list) where T : class, new()
        {
            ExcelWorkbook wb = ep.Workbook;
            string sheetName = null;
            IExcelTypeFormater defaultExcelTypeFormater = null;
            var excelAttribute = typeof(T).GetCustomAttribute<ExcelAttribute>();
            if (excelAttribute == null)
            {
                sheetName = typeof(T).Name;
                defaultExcelTypeFormater = new DefaultExcelTypeFormater();
            }
            else
            {
                if (excelAttribute.IsIncrease)
                {
                    sheetName = $"{excelAttribute.SheetName}{wb.Worksheets.Count + 1}";
                }
                else
                {
                    sheetName = excelAttribute.SheetName;
                }
                if (excelAttribute.ExcelType != null)
                {
                    defaultExcelTypeFormater = Activator.CreateInstance(excelAttribute.ExcelType) as IExcelTypeFormater;
                }
                else
                {
                    defaultExcelTypeFormater = new DefaultExcelTypeFormater();
                }
            }

            ExcelWorksheet ws1 = wb.Worksheets.Add(sheetName);
            Dictionary<PropertyInfo, ExportColumnAttribute> mainDic = new Dictionary<PropertyInfo, ExportColumnAttribute>();

            typeof(T).GetProperties().ToList().ForEach(o =>
            {
                var attribute = o.GetCustomAttribute<ExportColumnAttribute>();
                if (attribute != null)
                {
                    mainDic.Add(o, attribute);
                }
            });
            var mainPropertieList = mainDic.OrderBy(o => o.Value.Order).ToList();


            IList<IExcelTypeFormater> excelTypes = new List<IExcelTypeFormater>();
            int row = 1;
            int column = 1;

            //表头行
            foreach (var item in mainPropertieList)
            {
                IExcelTypeFormater excelType = null;
                if (item.Value.ExcelType != null)
                {
                    excelType = excelTypes.Where(o => o.GetType().FullName == item.Value.ExcelType.FullName).FirstOrDefault();
                    if (excelType == null)
                    {
                        excelType = Activator.CreateInstance(item.Value.ExcelType) as IExcelTypeFormater;
                        excelTypes.Add(excelType);
                    }
                }
                else
                {
                    excelType = defaultExcelTypeFormater;
                }
                excelType.SetHeaderCell()?.Invoke(ws1.Cells[row, column], item.Value.Name);
                column++;
            }

            row++;

            //数据行 
            foreach (var item in list)
            {
                column = 1;
                foreach (var mainPropertie in mainPropertieList)
                {
                    IExcelTypeFormater excelType = null;
                    var mainValue = mainPropertie.Key.GetValue(item);
                    if (mainPropertie.Value.ExcelType != null)
                    {
                        excelType = excelTypes.Where(o => o.GetType().FullName == mainPropertie.Value.ExcelType.FullName).FirstOrDefault();
                        if (excelType == null)
                        {
                            excelType = Activator.CreateInstance(mainPropertie.Value.ExcelType) as IExcelTypeFormater;
                            excelTypes.Add(excelType);
                        }
                    }
                    else
                    {
                        excelType = defaultExcelTypeFormater;
                    }
                    excelType.SetBodyCell()?.Invoke(ws1.Cells[row, column], mainValue);
                    column++;
                }
                row++;
            }

            return ep;

        }

        public static IList<T> ToList<T>(this ExcelPackage ep, string sheetName = null) where T : class, new()
        {
            ExcelWorkbook wb = ep.Workbook;
            ExcelWorksheet ws1 = null;
            if (string.IsNullOrEmpty(sheetName))
            {
                ws1 = wb.Worksheets[1];
            }
            else
            {
                ws1 = wb.Worksheets[sheetName];
            }


            Dictionary<PropertyInfo, ExportColumnAttribute> mainDic = new Dictionary<PropertyInfo, ExportColumnAttribute>();

            typeof(T).GetProperties().ToList().ForEach(o =>
            {
                var attribute = o.GetCustomAttribute<ExportColumnAttribute>();
                if (attribute != null)
                {
                    mainDic.Add(o, attribute);
                }
            });
            var mainPropertieList = mainDic.OrderBy(o => o.Value.Order).ToList();

            int totalRows = ws1.Dimension.Rows;
            int totalColums = ws1.Dimension.Columns;

            IList<T> list = new List<T>();
            //表头行
            int row = 1;
            IList<PropertyInfo> propertyInfos = new List<PropertyInfo>();
            for (int i = 1; i <= totalColums; i++)
            {
                var property = mainPropertieList.Where(o => o.Value.Name.Equals(ws1.Cells[row, i].Value?.ToString()?.Trim()) || o.Key.Name.Equals(ws1.Cells[row, i].Value?.ToString()?.Trim())).FirstOrDefault().Key;
                propertyInfos.Add(property);
            }

            row++;

            for (int i = row; i <= totalRows; i++)
            {
                T t = new T();
                int column = 1;
                foreach (var property in propertyInfos)
                {
                    if (property != null)
                    {
                        object cellValue = ws1.GetValue(row, column);
                        if (cellValue == null)
                        {
                            cellValue = "";
                        }
                        else if (property.PropertyType == typeof(string))
                        {
                            cellValue = cellValue.ToString();
                        }
                        else if (property.PropertyType == typeof(int))
                        {
                            cellValue = Convert.ToInt32(cellValue);
                        }
                        else if (property.PropertyType == typeof(long))
                        {
                            cellValue = Convert.ToInt64(cellValue);
                        }
                        else if (property.PropertyType == typeof(double))
                        {
                            cellValue = Convert.ToDecimal(cellValue);
                        }
                        else if (property.PropertyType == typeof(DateTime))
                        {
                            cellValue = Convert.ToDateTime(cellValue);

                        }
                        else
                        {
                            cellValue = cellValue.ToString();
                        }
                        property?.SetValue(t, cellValue);
                    }


                    column++;
                }
                list.Add(t);
            }
            return list;

        }


        public static (string, int) ToIpPort(this string url)
        {
            var urlArr = url.Split(":");

            int port = 80;
            if (urlArr.Length == 2)
            {
                port = Convert.ToInt32(urlArr[1]);
            }
            return (urlArr[0], port);
        }

  

        /// <summary>
        /// 获取微信号openId
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static string WxOpenId(this HttpContext httpContext)
        {
            string wxOpenId = httpContext.Request.Headers[CacheKeyHelper.WXOPENID].ToString();

            return wxOpenId;
        }

        /// <summary>
        /// 获取登录设备
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static string LoginDevice(this HttpContext httpContext)
        {
            string LoginDevice = httpContext.Request.Headers[CacheKeyHelper.LOGINDEVICE].ToString();

            return LoginDevice;
        }

        public static int ToTotalPage(this long totalCount, int pageSize)
        {
            int totalPage = 0;
            if (totalCount % pageSize == 0)
            {
                totalPage = (int)totalCount / pageSize;
            }
            else
            {
                totalPage = (int)totalCount / pageSize + 1;
            }
            return totalPage;

        }

    }
}
