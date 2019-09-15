using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Common
{
   public class ReadArticleRequest
    {
        public string FullUrl { get; set; }

        public IList<ArticleHttpHeader> HttpHeader { get; set; }
    }

    public class ArticleHttpHeader
    {
        public string Key { get; set; }

        public string Value { get; set; }
    }
}
