using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizerCore
{
    [Table(Name = "LocalizedString")]
    public class MsSqlStringLocalizedTable
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public string Id { get; set; }

        public string Name { get; set; }
        public string TypeName { get; set; }

        public string LocalizedStringTemplate { get; set; }

        public string Culture { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

    }
}
