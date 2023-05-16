using JwSale.Model.Dto.Request.Wechat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwSale.Api.Const
{
    public class SendSnsConst
    {
        public static string GetContentTemplate(string wxId, string content, string title, string contentUrl, string description)
        {
            string template = $"<TimelineObject><id>1</id><username>{wxId}</username><createTime>1</createTime><contentDesc>{content}</contentDesc><contentDescShowType>0</contentDescShowType><contentDescScene>3</contentDescScene><private>0</private><sightFolded>0</sightFolded><appInfo><id></id><version></version><appName></appName><installUrl></installUrl><fromUrl></fromUrl><isForceUpdate>0</isForceUpdate></appInfo><sourceUserName></sourceUserName><sourceNickName></sourceNickName><statisticsData></statisticsData><statExtStr></statExtStr><ContentObject><contentStyle>2</contentStyle><title>{title}</title><description>{description}</description><mediaList></mediaList><contentUrl>{contentUrl}</contentUrl></ContentObject><actionInfo><appMsg><messageAction></messageAction></appMsg></actionInfo><location city=\"\" poiClassifyId=\"\" poiName=\"\" poiAddress=\"\" poiClassifyType=\"1\"></location><publicUserName></publicUserName></TimelineObject>";
            return template;
        }

        public static string GetImageTemplate3(string wxId, string content, IList<MediaInfo> mediaInfos, string title, string contentUrl, string description)
        {
            StringBuilder sb = new StringBuilder();
            if (mediaInfos != null && mediaInfos.Count > 0)
            {
                foreach (var item in mediaInfos)
                {

                    string media = $"<media><id>1</id><type>3</type><title></title><description></description><private>0</private><url type=\"1\">{item.ImageUrl}</url><thumb type=\"1\">{item.ImageUrl}</thumb><size height=\"{item.Height}\" width=\"{item.Width}\" totalSize=\"{item.TotalSize}\"></size></media>";
                    sb.Append(media);
                }
            }
            string template = $"<TimelineObject><id>1</id><username>{wxId}</username><createTime>1</createTime><contentDescShowType>0</contentDescShowType><contentDescScene>0</contentDescScene><private>0</private><contentDesc>{content}</contentDesc><contentattr>0</contentattr><sourceUserName></sourceUserName><sourceNickName></sourceNickName><statisticsData></statisticsData><weappInfo><appUserName></appUserName><pagePath></pagePath></weappInfo><canvasInfoXml></canvasInfoXml><ContentObject><contentStyle>1</contentStyle><contentSubStyle>0</contentSubStyle><title></title><description></description><contentUrl></contentUrl><mediaList>{sb}</mediaList></ContentObject><actionInfo><appMsg><mediaTagName></mediaTagName><messageExt></messageExt><messageAction></messageAction></appMsg></actionInfo><appInfo><id></id></appInfo><location poiClassifyId=\"\" poiName=\"\" poiAddress=\"\" poiClassifyType=\"0\" city=\"\"></location><publicUserName></publicUserName><streamvideo><streamvideourl></streamvideourl><streamvideothumburl></streamvideothumburl><streamvideoweburl></streamvideoweburl></streamvideo></TimelineObject>";
            return template;
        }
        public static string GetImageTemplate4(string wxId, string content, IList<MediaInfo> mediaInfos, string title, string contentUrl, string description)
        {
            StringBuilder sb = new StringBuilder();
            if (mediaInfos != null && mediaInfos.Count > 0)
            {
                foreach (var item in mediaInfos)
                {
                    string media = $"<media><id>1</id><type>2</type><title></title><description></description><private>0</private><url type=\"1\">{item.ImageUrl}</url><thumb type=\"1\">{item.ImageUrl}</thumb><size height=\"{item.Height}\" width=\"{item.Width}\" totalSize=\"{item.TotalSize}\"></size></media>";
                    sb.Append(media);
                }
            }
            string template = $"<TimelineObject><id>1</id><username>{wxId}</username><createTime>1</createTime><contentDescShowType>0</contentDescShowType><contentDescScene>0</contentDescScene><private>0</private><contentDesc>{content}</contentDesc><contentattr>0</contentattr><sourceUserName></sourceUserName><sourceNickName></sourceNickName><statisticsData></statisticsData><weappInfo><appUserName></appUserName><pagePath></pagePath></weappInfo><canvasInfoXml></canvasInfoXml><ContentObject><contentStyle>1</contentStyle><contentSubStyle>0</contentSubStyle><title></title><description></description><contentUrl></contentUrl><mediaList>{sb}</mediaList></ContentObject><actionInfo><appMsg><mediaTagName></mediaTagName><messageExt></messageExt><messageAction></messageAction></appMsg></actionInfo><appInfo><id></id></appInfo><location poiClassifyId=\"\" poiName=\"\" poiAddress=\"\" poiClassifyType=\"0\" city=\"\"></location><publicUserName></publicUserName><streamvideo><streamvideourl></streamvideourl><streamvideothumburl></streamvideothumburl><streamvideoweburl></streamvideoweburl></streamvideo></TimelineObject>";
            return template;
        }
        public static string GetImageTemplate5(string wxId, string content, IList<MediaInfo> mediaInfos, string title, string contentUrl, string description)
        {
            StringBuilder sb = new StringBuilder();
            if (mediaInfos != null && mediaInfos.Count > 0)
            {
                foreach (var item in mediaInfos)
                {

                    string media = $"<media><id>1</id><type>2</type><title></title><description></description><private>0</private><url type=\"1\">{item.ImageUrl}</url><thumb type=\"1\">{item.ImageUrl}</thumb><size height=\"{item.Height}\" width=\"{item.Width}\" totalSize=\"{item.TotalSize}\"></size></media>";
                    sb.Append(media);
                }
            }
            string template = $"<TimelineObject><id>1</id><username>{wxId}</username><createTime>1</createTime><contentDescShowType>0</contentDescShowType><contentDescScene>0</contentDescScene><private>0</private><contentDesc>{content}</contentDesc><contentattr>0</contentattr><sourceUserName></sourceUserName><sourceNickName></sourceNickName><statisticsData></statisticsData><weappInfo><appUserName></appUserName><pagePath></pagePath></weappInfo><canvasInfoXml></canvasInfoXml><ContentObject><contentStyle>1</contentStyle><contentSubStyle>0</contentSubStyle><title></title><description></description><contentUrl></contentUrl><mediaList>{sb}</mediaList></ContentObject><actionInfo><appMsg><mediaTagName></mediaTagName><messageExt></messageExt><messageAction></messageAction></appMsg></actionInfo><appInfo><id></id></appInfo><location poiClassifyId=\"\" poiName=\"\" poiAddress=\"\" poiClassifyType=\"0\" city=\"\"></location><publicUserName></publicUserName><streamvideo><streamvideourl></streamvideourl><streamvideothumburl></streamvideothumburl><streamvideoweburl></streamvideoweburl></streamvideo></TimelineObject>";
            return template;
        }

        public static string GetImageTemplate(string wxId, string content, IList<MediaInfo> mediaInfos, string title, string contentUrl, string description)
        {
            StringBuilder sb = new StringBuilder();
            if (mediaInfos != null && mediaInfos.Count > 0)
            {
                foreach (var item in mediaInfos)
                {
                    string media = $"<media><id>1</id><type>2</type><title></title><description></description><private>0</private><url type=\"1\">{item.ImageUrl}</url><thumb type=\"1\">{item.ImageUrl}</thumb><size height=\"{item.Height}\" width=\"{item.Width}\" totalSize=\"{item.TotalSize}\"></size></media>";
                    sb.Append(media);

                }
            }
            string template = $"<TimelineObject><id>1</id><username>{wxId}</username><createTime>1</createTime><contentDescShowType>0</contentDescShowType><contentDescScene>0</contentDescScene><private>0</private><contentDesc>{content}</contentDesc><contentattr>0</contentattr><sourceUserName></sourceUserName><sourceNickName></sourceNickName><statisticsData></statisticsData><weappInfo><appUserName></appUserName><pagePath></pagePath></weappInfo><canvasInfoXml></canvasInfoXml><ContentObject><contentStyle>1</contentStyle><contentSubStyle>0</contentSubStyle><title>{title}</title><description>{description}</description><contentUrl></contentUrl><mediaList>{sb}</mediaList></ContentObject><actionInfo><appMsg><mediaTagName></mediaTagName><messageExt></messageExt><messageAction></messageAction></appMsg></actionInfo><appInfo><id></id></appInfo><location poiClassifyId=\"\" poiName=\"\" poiAddress=\"\" poiClassifyType=\"0\" city=\"\"></location><publicUserName></publicUserName><streamvideo><streamvideourl></streamvideourl><streamvideothumburl></streamvideothumburl><streamvideoweburl></streamvideoweburl></streamvideo></TimelineObject> ";
            return template;
        }


        public static string GetVideoTemplate(string wxId, string content, IList<MediaInfo> mediaInfos, string title, string contentUrl, string description)
        {
            StringBuilder sb = new StringBuilder();
            if (mediaInfos != null && mediaInfos.Count > 0)
            {
                foreach (var item in mediaInfos)
                {
                    string media = $"<media><id>1</id><type>6</type><title>{content}</title><description>{content}</description><private>0</private><url type=\"1\">{item.ImageUrl}</url><thumb type=\"1\">{item.ImageUrl}</thumb><size height=\"{item.Height}\" width=\"{item.Width}\" totalSize=\"{item.TotalSize}\"></size></media>";

                    sb.Append(media);
                }
            }
            string template = $"<TimelineObject><id>1</id><username>{wxId}</username><createTime>1</createTime><contentDescShowType>0</contentDescShowType><contentDescScene>0</contentDescScene><private>0</private><contentDesc>{content}</contentDesc><contentattr>0</contentattr><sourceUserName></sourceUserName><sourceNickName></sourceNickName><statisticsData></statisticsData><weappInfo><appUserName></appUserName><pagePath></pagePath></weappInfo><canvasInfoXml></canvasInfoXml><ContentObject><contentStyle>15</contentStyle><contentSubStyle>0</contentSubStyle><title>{title}</title><description>{description}</description><contentUrl>{contentUrl}</contentUrl><mediaList>{sb}</mediaList></ContentObject><actionInfo><appMsg><mediaTagName></mediaTagName><messageExt></messageExt><messageAction></messageAction></appMsg></actionInfo><appInfo><id></id></appInfo><location poiClassifyId=\"\" poiName=\"\" poiAddress=\"\" poiClassifyType=\"0\" city=\"\"></location><publicUserName></publicUserName><streamvideo><streamvideourl></streamvideourl><streamvideothumburl></streamvideothumburl><streamvideoweburl></streamvideoweburl></streamvideo></TimelineObject>";
            return template;
        }


        public static string GetLinkTemplate(string wxId, string content, IList<MediaInfo> mediaInfos, string title, string contentUrl, string description)
        {
            StringBuilder sb = new StringBuilder();
            if (mediaInfos != null && mediaInfos.Count > 0)
            {
                foreach (var item in mediaInfos)
                {
                    string media = $"<media><id>1</id><type>2</type><title></title><description></description><private>0</private><url type=\"1\">{item.ImageUrl}</url><thumb type=\"1\">{item.ImageUrl}</thumb><size height=\"{item.Height}\" width=\"{item.Width}\" totalSize=\"{item.TotalSize}\"></size></media>";
                    sb.Append(media);
                }
            }
            string template = $"<TimelineObject><id>1</id><username>{wxId}</username><createTime>1</createTime><contentDescShowType>0</contentDescShowType><contentDescScene>0</contentDescScene><private>0</private><contentDesc>{content}</contentDesc><contentattr>0</contentattr><sourceUserName></sourceUserName><sourceNickName></sourceNickName><statisticsData></statisticsData><weappInfo><appUserName></appUserName><pagePath></pagePath></weappInfo><canvasInfoXml></canvasInfoXml><ContentObject><contentStyle>3</contentStyle><contentSubStyle>0</contentSubStyle><title>{title}</title><description>{description}</description><contentUrl>{contentUrl}</contentUrl><mediaList>{sb}</mediaList></ContentObject><actionInfo><appMsg><mediaTagName></mediaTagName><messageExt></messageExt><messageAction></messageAction></appMsg></actionInfo><statExtStr></statExtStr><appInfo><id></id></appInfo><location poiClassifyId=\"\" poiName=\"\" poiAddress=\"\" poiClassifyType=\"0\" city=\"\"></location><publicUserName>gh_a45e6bdccb14</publicUserName><streamvideo><streamvideourl></streamvideourl><streamvideothumburl></streamvideothumburl><streamvideoweburl></streamvideoweburl></streamvideo></TimelineObject>"; return template;
        }









    }

}
