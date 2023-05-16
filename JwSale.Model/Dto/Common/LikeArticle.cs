using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Common
{
    public class LikeArticleRequest
    {
        /// <summary>
        /// 完整Url(GetA8Key返回)
        /// </summary>
        public string FullUrl { get; set; }

        /// <summary>
        /// 请求头(GetA8Key返回)
        /// </summary>
        public IList<ArticleHttpHeader> HttpHeader { get; set; }
    }
}
