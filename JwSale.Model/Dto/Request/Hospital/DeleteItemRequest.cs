using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JwSale.Model.Dto.Request.Hospital
{
    public class DeleteItemRequest : RequestBase
    {
        [Required]
        public string Id { get; set; }
    }
}
