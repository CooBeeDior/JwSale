using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Common
{

 


    public class BaseRet
    {
        public int ret { get; set; }

        public ErrMsg errMsg { get; set; }

      


    }

    public class ErrMsg
    {

        public string str { get; set; }
    }

  
}
