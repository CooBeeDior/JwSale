using JwSale.Util.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JwSale.Model
{
    public interface IEntity
    {

    }

    public interface IEntity<T> : IEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        T Id { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        [Timestamp]
        byte[] Version { get; set; }
        /// <summary>
        /// UpdateTime
        /// </summary>
        DateTime UpdateTime { get; set; }
        /// <summary>
        /// AddTime
        /// </summary>
        DateTime AddTime { get; set; }
        /// <summary>
        /// AddUserId
        /// </summary>
        Guid AddUserId { get; set; }
        /// <summary>
        /// UpdateUserId
        /// </summary>
        Guid UpdateUserId { get; set; }
        /// <summary>
        /// AddUserRealName
        /// </summary>
        string AddUserRealName { get; set; }
        /// <summary>
        /// UpdateUserRealName
        /// </summary>
        string UpdateUserRealName { get; set; }
    }


    [Entity]
    public class Entity : IEntity<Guid>
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        [Timestamp]
        public byte[] Version { get; set; }
        /// <summary>
        /// UpdateTime
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// AddTime
        /// </summary>
        public DateTime AddTime { get; set; }
        /// <summary>
        /// AddUserId
        /// </summary>
        public Guid AddUserId { get; set; }
        /// <summary>
        /// UpdateUserId
        /// </summary>
        public Guid UpdateUserId { get; set; }
        /// <summary>
        /// AddUserRealName
        /// </summary>
        public string AddUserRealName { get; set; }
        /// <summary>
        /// UpdateUserRealName
        /// </summary>
        public string UpdateUserRealName { get; set; }


        public override bool Equals(object obj)
        {
            var t = obj as Entity;
            return t.Id == this.Id;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
