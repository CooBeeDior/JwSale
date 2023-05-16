using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Common
{
    public class ReadArticleRequest
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

    public class ArticleHttpHeader
    {
        /// <summary>
        /// 键
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
    }
}
