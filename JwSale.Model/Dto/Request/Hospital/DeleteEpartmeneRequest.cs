using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JwSale.Model.Dto.Request.Hospital
{ 
    public class DeleteEpartmeneRequest : RequestBase
    {
        [Required]
        public string Id { get; set; }
    }
}
