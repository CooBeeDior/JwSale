using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Common
{
    public class LikeArticleRequest
    {
        public string FullUrl { get; set; }

        public IList<ArticleHttpHeader> HttpHeader { get; set; }
    }
}
