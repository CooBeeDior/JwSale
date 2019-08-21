using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class AnalysisData
    {
        public AnalysisData()
        {
        }
        public AnalysisData(string token, string data)
        {
            this.token = token;
            this.data = data;
        }
        public string token { get; set; }

        public string data { get; set; }
    }
}
