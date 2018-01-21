using Abp.Runtime.Caching;
using Aliyun.OSS;
using Aliyun.OSS.Util;
using NetCommunitySolution.Common;
using NetCommunitySolution.Domain.Configuration;
using NetCommunitySolution.Media;
using System;
using System.IO;

namespace NetCommunitySolution.Media
{
    public class OssService : NetCommunitySolutionAppServiceBase, IOssService
    {
        #region Fields &&Ctor
        private readonly ICacheManager _cacheManager;
        private readonly ISettingService _settingService;
        private readonly System.Threading.AutoResetEvent _event;
        private readonly MediaSetting mediaSetting;

        public OssService(ICacheManager cacheManager, ISettingService settingService)
        {
            this._settingService = settingService;
            this._cacheManager = cacheManager;
            _event = new System.Threading.AutoResetEvent(false);
            mediaSetting = _settingService.GetMediaSettings();
        }
        #endregion


        #region Nested classes       
        

        private string GetImageName(string suffix = ".jpg",string prefix = "")
        {
            var name = DateTime.Now.ToString("yyyyMMddhhmmssfff") +CommonHelper.GenerateCode(3);
            if (!String.IsNullOrWhiteSpace(prefix))
            {
                name = string.Join("_", prefix, name);
            }
            return string.Format("{0}{1}", name, suffix);
        }

        #endregion

        
        private void ThumbnailOssPutObject(string bucketName, string imageName, byte[] images,OssClient ossClient)
        {
            string graphFileName = string.Format("graph_{0}", imageName);
            byte[] listGraphSizeByte = ThumbnailHelper.MakeThumbnail(ThumbnailHelper.ReturnPhoto(images), "", 200, 200, "Cut");            
            var objStream = new MemoryStream(listGraphSizeByte);
            ossClient.PutObject(mediaSetting.Bucket, graphFileName, objStream);
            objStream.Close();

        }

        #region Method

        public bool DeleteImage(string fileName)
        {
            var ossClient = new OssClient(mediaSetting.Endpoint, mediaSetting.AccessKeyId, mediaSetting.AccessKeySecret);
            try
            {
                ossClient.DeleteObject(mediaSetting.Bucket, fileName);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("删除文件失败, {0}", ex.Message);
                return false;
            }
        }

        public string UploadImage(byte[] images, bool isBuildThumbnail = false, string upload = "")
        {
            // config.endpoint = "oss-cn-beijing.aliyuncs.com" 我是华北2的
            var ossClient = new OssClient(mediaSetting.Endpoint, mediaSetting.AccessKeyId, mediaSetting.AccessKeySecret);
            try
            {
                //判定bucket是否存在
                //var result = ossClient.DoesBucketExist(config.Bucket);
                //重置文件名称

                var imageName = GetImageName(prefix: upload);
                //bytes 转换为Stream
                var objStream = new MemoryStream(images);
                objStream.Seek(0, SeekOrigin.Begin);
                //这个是否需要？
                string md5 = OssUtils.ComputeContentMd5(objStream, objStream.Length);
                var objectMeta = new ObjectMetadata
                {
                    ContentMd5 = md5,
                    ContentType = "image/png",

                };
                PutObjectResult result = ossClient.PutObject(mediaSetting.Bucket, imageName, objStream, objectMeta);
                objStream.Close();
                objStream.Dispose();

                string imgurl = "http://" + string.Join(".", mediaSetting.Bucket, mediaSetting.Endpoint) + "/" + imageName;
                
                #region 生成缩略图
                if (isBuildThumbnail)
                {
                    ThumbnailOssPutObject(mediaSetting.Bucket, imageName, images, ossClient);
                }
                #endregion

                return imgurl;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex.Message);
                return null;
            }
        }

        
        #endregion

        
        
    }
}
