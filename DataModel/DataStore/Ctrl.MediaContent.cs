﻿using DataModel.DataEntity;
using DataModel.Extension;
using DataModel.DataViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Collections;

namespace DataModel.DataStore
{
    public partial class Ctrl : Core
    {
        public static string PrefixImageName = "phong-lan-gia-si";
        public static string PrefixImageContentURL = "phong-lan-gia-si";
        public static string DefaulMediaContentURL = "";
        public static string FolderImage = "images";
        public static string FolderFrontEnd = "Media";
        public static string FolderFTPImage = "ftp://103.3.250.22/BDSAdmin/images/media";
        public static string FTPUser = "NGUYENHUY";
        public static string FTPPass = "123456";
        public static string FTPFolder = "CMSADMIN";
        public static string FTPURL = "ftp://103.3.250.22";
        public static string WaterMask = "";


        public async Task<long> AddNewMediaContent(MediaContentViewModels mediaContent)
        {
            try
            {
                var result = db.MediaContents.Add(mediaContent.objMediaContent);
                await db.SaveChangesAsync();
                return result.MediaContentId;

            }
            catch (Exception e)
            {
                return (long)EnumCore.Result.action_false;
            }

        }

        public MediaContent AddNewMediaNoAsync(MediaContentViewModels mediaContent)
        {
            try
            {
                var result = db.MediaContents.Add(mediaContent.objMediaContent);
                 db.SaveChanges();
                 return mediaContent.objMediaContent;

            }
            catch (Exception e)
            {
                return null;
            }

        }


        public async Task<MediaContent> AddObjMediaContent(MediaContent media)
        {
            MediaContent result = db.MediaContents.Add(media);
            await db.SaveChangesAsync();
            return result;
        }

        public async Task<int> AddRangeMediaContent(List<MediaContent> lstMediaContent)
        {
            var obj = db.MediaContents.AddRange(lstMediaContent);
            return await db.SaveChangesAsync();
        }
        public async Task<int> EditMediaContent(MediaContentViewModels mediaContent)
        {
            db.Entry(mediaContent.objMediaContent).State = EntityState.Modified;
            return await db.SaveChangesAsync();
        }
        public async Task<int> UpdateMediaContent(MediaContent mediaContent)
        {
            db.Entry(mediaContent).State = EntityState.Modified;
            return await db.SaveChangesAsync();
        }
        public async Task<int> DeleteMediaContent(long? id)
        {


            var objMediaContent = db.MediaContents.FirstOrDefault(c => c.MediaContentId == id);
            db.MediaContents.Remove(objMediaContent);
            if (!string.IsNullOrEmpty(objMediaContent.FullURL))
            {
                string _ServerPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, objMediaContent.FullURL);
                bool rs = this.DeleteFileNoFTP(_ServerPath);
            }
            if (!string.IsNullOrEmpty(objMediaContent.ThumbURL))
            {
                string _ServerPath2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, objMediaContent.ThumbURL);
                bool rs2 = this.DeleteFileNoFTP(_ServerPath2);
            }

            int result = await db.SaveChangesAsync();
            return result;
        }
        public async Task<int> DeletelstMediaContent(long[] lstid)
        {
            List<MediaContent> LstMediaContent = db.MediaContents.Where(s => lstid.Contains(s.MediaContentId)).ToList();
            foreach (MediaContent _val in LstMediaContent)
            {
               await this.DeleteMediaContent(_val.MediaContentId);
            
            }
            return (int)EnumCore.Result.action_true;
        }

        public bool DeleteFileNoFTP(string PathFile)
        {
            try
            {
                if (System.IO.File.Exists(PathFile))
                {
                    System.IO.File.Delete(PathFile);
                    return true;
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }


        }
        public IQueryable<MediaContent> GetLstMediaContent()
        {
            IQueryable<MediaContent> lstMediaContent = db.MediaContents.OrderByDescending(c => c.MediaContentId);
            return lstMediaContent;
        }

        public IQueryable<MediaContent> GetLstMediaContentByKey(long? MediaContentId, int? MediaTypeId, int? ObjTypeId, long? ContentObjId)
        {
            IQueryable<MediaContent> lstMediaContent = db.MediaContents.OrderByDescending(c => c.MediaContentId);
            if (MediaContentId.HasValue)
              lstMediaContent=  lstMediaContent.Where(s => s.MediaContentId == MediaContentId);
            if (MediaTypeId.HasValue)
             lstMediaContent=   lstMediaContent.Where(s => s.MediaTypeId == MediaTypeId);
            if (ObjTypeId.HasValue)
             lstMediaContent=   lstMediaContent.Where(s => s.ObjTypeId == ObjTypeId);
            if (ContentObjId.HasValue)
             lstMediaContent=   lstMediaContent.Where(s => s.ContentObjId == ContentObjId);
            return lstMediaContent;
        }

        public IQueryable<MediaContent> GetLstMediaContent(long ObjId, int ObjTypeId)
        {
            IQueryable<MediaContent> lstMediaContent = db.MediaContents.Where(s => s.ContentObjId == ObjId
                                            && s.ObjTypeId == ObjTypeId).OrderByDescending(c => c.MediaContentId);
            return lstMediaContent;
        }

        public IEnumerable GetBannerListForSelectlist()
        {
            try
            {
                var lstobj = (from c in this.GetLstMediaContent().Where(s => s.ObjTypeId == (int)EnumCore.ObjTypeId.banner)
                              select new { c.MediaContentId, c.Filename }).ToList();
                return lstobj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<MediaContent> GetlstMediaContentByObjId(Product p)
        {
            return db.MediaContents.Where(x => x.ObjTypeId == p.ProductId && x.FullURL != p.MediaUrl && x.ThumbURL != p.MediaThumb).ToList();
        }

        public List<MediaContent> GetLstMediaContentByAlbumId(int AlbumId)
        {
            List<MediaContent> lstMediaContent = this.GetLstMediaContent().
                    Where(c => c.MediaTypeId == (int)EnumCore.mediatype.hinh_anh && c.ObjTypeId == (int)EnumCore.ObjTypeId.album && c.ContentObjId == AlbumId).ToList();
            return lstMediaContent;
        }
        public List<MediaContentViewModels> GetLstMediaContentViewModel()
        {
            List<MediaContent> lstMediaContent = this.GetLstMediaContent().ToList();
            List<MediaContentViewModels> lstMediaContentViewModel = new List<MediaContentViewModels>();
            foreach (MediaContent _val in lstMediaContent)
            {
                //lstMediaContentViewModel.Add(new MediaContentViewModels(_val));
            }
            return lstMediaContentViewModel;
        }
        public MediaContent GetObjMediaContent(long? id)
        {
            if (id.HasValue)
            {
                MediaContent _Obj = db.MediaContents.Find(id.Value);
                return _Obj;
            }
            else
            {
                return null;
            }

        }
        public MediaContent GetObjDefaultMediaContent(long DefaultImage)
        {
            MediaContent _Obj = db.MediaContents.Find(DefaultImage);
            return _Obj;
        }
        public MediaContent GetObjDefaultMediaByContentIdvsType(long contentid, int type)
        {
            MediaContent _Obj = db.MediaContents.Where(s => s.ContentObjId == contentid && s.ObjTypeId == type).FirstOrDefault();
            return _Obj;
        }
        public MediaContent GetObjMediaContentProduct(Product p)
        {
            MediaContent media = db.MediaContents.SingleOrDefault(x => x.ContentObjId == p.ProductId && x.ObjTypeId == (int)EnumCore.ObjTypeId.san_pham);
            return media;
        }
        public IQueryable<MediaContent> GetObjMedia()
        {
            IQueryable<MediaContent> _Obj = db.MediaContents;
            return _Obj;
        }

        public MediaContent GetObjMediaContentForMicrosite(long idMicroste)
        {
            return db.MediaContents.SingleOrDefault(x=>x.ContentObjId == idMicroste && x.ObjTypeId ==(int)EnumCore.ObjTypeId.Microsite);
        }

        public ImageUploadViewModel UploadHttpPostedFileBase(HttpPostedFileBase Request)
        {
            HttpPostedFileBase desimg = Request;
            ImageUploadViewModel imgUpload = new ImageUploadViewModel();
            long _ImgId = 0;
            string coderandom = DateTime.UtcNow.Ticks.ToString();
            coderandom = PrefixImageName + coderandom;
            var _Filename = Path.GetFileName(desimg.FileName);
            string _ServerPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FolderImage);
            string _NameRandom = coderandom + _Filename;
            var path = Path.Combine(_ServerPath, _NameRandom);
            if (!System.IO.Directory.Exists(_ServerPath))
                System.IO.Directory.CreateDirectory(_ServerPath);
            desimg.SaveAs(path);
            FileHelper.SaveResizedImage(_ServerPath, _NameRandom, "thumb" + _NameRandom, 200, 200);
            imgUpload.ImageName = _NameRandom;
            imgUpload.ImageThumbUrl = FolderImage + "/thumb" + _NameRandom;
            imgUpload.ImageUrl = FolderImage + "/" + _NameRandom;
            imgUpload.MediaContentId = _ImgId;
            return imgUpload;
        }
        public async Task<ImageUploadViewModel> UploadOneImageToServer(HttpRequestBase Request)
        {
            HttpPostedFileBase desimg = Request.Files[0];
            ImageUploadViewModel _File = UploadHttpPostedFileBase(desimg);
            MediaContentViewModels _Media = new MediaContentViewModels();
            _Media.Filename = _File.ImageName;
            _Media.FullURL = _File.ImageUrl;
            _Media.ThumbURL = _File.ImageThumbUrl;
            _Media.ContentObjId = null;
            _Media.ObjTypeId = null;
            _Media.CrtdUID = long.Parse(HttpContext.Current.User.Identity.GetUserId());
            _Media.CrtdDT = DateTime.UtcNow;
            _Media.MediaTypeId = (int)EnumCore.mediatype.hinh_anh;
            long _ImgId = await AddNewMediaContent(_Media);
            _File.MediaContentId = _Media.MediaContentId;
            return _File;
        }
        #region UPLOAD BY CKEDITOR
        public ImageUploadViewModel UploadHttpPostedFileBaseForCK(HttpPostedFileBase Request)
        {
            HttpPostedFileBase desimg = Request;
            ImageUploadViewModel imgUpload = new ImageUploadViewModel();
            string coderandom = PrefixImageName;
            var _Filename = Path.GetFileName(desimg.FileName);
            string _ServerPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FolderImage);
            string _NameRandom = coderandom + _Filename;
            var path = Path.Combine(_ServerPath, _NameRandom);
            desimg.SaveAs(path);
            imgUpload.ImageName = _NameRandom;
            imgUpload.ImageUrl = FolderImage + "/" + _NameRandom;
            return imgUpload;
        }
        public async Task<List<ImageUploadViewModel>> UploadImageServerForCK(HttpRequestBase Request)
        {
            List<ImageUploadViewModel> lstimgUpload = new List<ImageUploadViewModel>();
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase desimg = Request.Files[i];
                ImageUploadViewModel File = UploadHttpPostedFileBaseForCK(desimg);

                MediaContentViewModels _Media = new MediaContentViewModels();
                _Media.Filename = File.ImageName;
                _Media.FullURL = File.ImageUrl;
                _Media.ContentObjId = null;
                _Media.ObjTypeId = (int)EnumCore.ObjTypeId.img_in_document;
                _Media.CrtdUID = long.Parse(HttpContext.Current.User.Identity.GetUserId());
                _Media.CrtdDT = DateTime.UtcNow;
                _Media.MediaTypeId = (int)EnumCore.mediatype.hinh_anh;
                long _ImgId = await AddNewMediaContent(_Media);
                File.MediaContentId = _Media.MediaContentId;
                lstimgUpload.Add(File);
            }
            return lstimgUpload;
        }
        #endregion
        #region FUNCTION WITH FTP
        //public static List<string> GetImageSrcFromHTMLString(string Values)
        //{
        //    List<string> lstImage = new List<string>();
        //    foreach (Match m in Regex.Matches(Values, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase | RegexOptions.Multiline))
        //    {
        //        lstImage.Add(m.Groups[1].Value);
        //        // add src to some array
        //    }
        //    return lstImage;
        //}
        /// </summary>
        /// <param name="filename"></param>
        public static void UploadFTP(string filename)
        {

            FileInfo fileInf = new FileInfo(filename);
            string uri = FolderFTPImage + fileInf.Name;
            FtpWebRequest reqFTP;

            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(FolderFTPImage + fileInf.Name));
            reqFTP.Credentials = new NetworkCredential(FTPUser, FTPPass);
            reqFTP.KeepAlive = false;
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
            reqFTP.UseBinary = true;
            reqFTP.ContentLength = fileInf.Length;
            //Set max 2mb
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;
            FileStream fs = fileInf.OpenRead();
            try
            {
                // Stream to which the file to be upload is written
                Stream strm = reqFTP.GetRequestStream();
                // Read from the file stream 2kb at a time
                contentLen = fs.Read(buff, 0, buffLength);
                // Till Stream content ends
                while (contentLen != 0)
                {
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }
                // Close the file stream and the Request Stream
                strm.Close();
                fs.Close();
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// Use for fontEnd Upload bang FTP
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public ImageUploadViewModel UploadHttpPostedFileBaseWithFTP(HttpPostedFileBase Request)
        {
            HttpPostedFileBase desimg = Request;
            ImageUploadViewModel imgUpload = new ImageUploadViewModel();
            long _ImgId = 0;

            string coderandom = DateTime.UtcNow.Ticks.ToString();
            var _Filename = Path.GetFileName(desimg.FileName);
            string _ServerPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Admin/images/media/");
            string _ServerPathReplace = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images/media/HPB");
            string _NameRandom = coderandom + _Filename;
            var path = Path.Combine(_ServerPath, _NameRandom);
            string _OnputFullname = Path.Combine(PrefixImageContentURL, "images/media/" + _NameRandom);
            desimg.SaveAs(path);
            UploadFTP(path);
            imgUpload.ImageName = _NameRandom;
            imgUpload.ImageUrl = _OnputFullname;
            imgUpload.MediaContentId = _ImgId;

            return imgUpload;
        }

        public async Task<bool> DeleteMediaContentByListMediaId(ICollection<MediaContent> lstMediaContentId)
        {
            foreach (MediaContent _val in lstMediaContentId)
            {
                db.MediaContents.Remove(_val);
                DeleteFile(_val.Filename);
                await db.SaveChangesAsync();
            }
            return true;
        }
        public static bool DeleteFileByFTP(string ftpURL, string UserName, string Password, string ftpDirectory, string FileName)
        {
            try
            {
                FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(ftpURL + "/" + ftpDirectory + "/" + FileName);
                ftpRequest.Credentials = new NetworkCredential(UserName, Password);
                ftpRequest.Method = WebRequestMethods.Ftp.DeleteFile;
                FtpWebResponse responseFileDelete = (FtpWebResponse)ftpRequest.GetResponse();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public static bool CheckFileExistOrNot(string ftpURL, string UserName, string Password, string ftpDirectory, string FileName)
        {
            FtpWebRequest ftpRequest = null;
            FtpWebResponse ftpResponse = null;
            bool IsExists = true;
            try
            {
                ftpRequest = (FtpWebRequest)WebRequest.Create(ftpURL + "/" + ftpDirectory + "/" + FileName);
                ftpRequest.Credentials = new NetworkCredential(UserName, Password);
                ftpRequest.Method = WebRequestMethods.Ftp.GetFileSize;
                ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                ftpResponse.Close();
                ftpRequest = null;
            }
            catch (Exception ex)
            {
                IsExists = false;
            }
            return IsExists;
        }
        public static bool DeleteFile(string photoFileName)
        {
            bool Exist = CheckFileExistOrNot(FTPURL, FTPUser, FTPPass, FTPFolder, photoFileName);
            if (Exist == true)
            {
                DeleteFileByFTP(FTPURL, FTPUser, FTPPass, FTPFolder, photoFileName);
                return true;
            }
            else
            {
                return true;
            }

        }
        #endregion
        #region EXT FUNCTION
        public static string GetImageUrl(MediaContent InputMediaContent)
        {
            return InputMediaContent == null
                                    ? DefaulMediaContentURL
                                    : InputMediaContent.FullURL;

        }
        public string GetImageUrl(long MediaContentId)
        {
            MediaContent _objMediaContent = db.MediaContents.Where(s => s.MediaContentId == MediaContentId).FirstOrDefault();
            if (_objMediaContent != null)
                return _objMediaContent.FullURL;
            return "";
        }

        #endregion




        #region FRONTEND

        public ImageUploadViewModel UploadHttpPostedFileBaseFrond(HttpPostedFileBase Request)
        {
            HttpPostedFileBase desimg = Request;
            ImageUploadViewModel imgUpload = new ImageUploadViewModel();
            long _ImgId = 0;
            string coderandom = DateTime.UtcNow.Ticks.ToString();
            coderandom = "botogiasi" + coderandom;
            var _Filename = Path.GetFileName(desimg.FileName);
            string _ServerPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FolderFrontEnd);
            string _NameRandom = coderandom + _Filename;
            var path = Path.Combine(_ServerPath, _NameRandom);
            if (!System.IO.Directory.Exists(_ServerPath))
                System.IO.Directory.CreateDirectory(_ServerPath);
            desimg.SaveAs(path);
            FileHelper.SaveResizedImage(_ServerPath, _NameRandom, "thumb" + _NameRandom, 200, 200);
            imgUpload.ImageName = _NameRandom;
            imgUpload.ImageThumbUrl = FolderFrontEnd + "/thumb" + _NameRandom;
            imgUpload.ImageUrl = FolderFrontEnd + "/" + _NameRandom;
            imgUpload.MediaContentId = _ImgId;
            return imgUpload;
        }

        public async Task<List<ImageUploadViewModel>> UploadIMGForCKFrontEnd(HttpRequestBase Request)
        {
            List<ImageUploadViewModel> lstimgUpload = new List<ImageUploadViewModel>();
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase desimg = Request.Files[i];
                ImageUploadViewModel File = UpFileBaseForCKFrondEnd(desimg);

                MediaContentViewModels _Media = new MediaContentViewModels();
                _Media.Filename = File.ImageName;
                _Media.FullURL = File.ImageUrl;
                _Media.ContentObjId = null;
                _Media.ObjTypeId = (int)EnumCore.ObjTypeId.img_in_document;
                _Media.CrtdUID = long.Parse(HttpContext.Current.User.Identity.GetUserId());
                _Media.CrtdDT = DateTime.UtcNow;
                _Media.MediaTypeId = (int)EnumCore.mediatype.hinh_anh;
                long _ImgId = await AddNewMediaContent(_Media);
                File.MediaContentId = _Media.MediaContentId;
                lstimgUpload.Add(File);
            }
            return lstimgUpload;
        }

        public ImageUploadViewModel UpFileBaseForCKFrondEnd(HttpPostedFileBase Request)
        {
            HttpPostedFileBase desimg = Request;
            ImageUploadViewModel imgUpload = new ImageUploadViewModel();
            string coderandom = "botogiasi";
            var _Filename = Path.GetFileName(desimg.FileName);
            string _ServerPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FolderFrontEnd);
            string _NameRandom = coderandom + _Filename;
            var path = Path.Combine(_ServerPath, _NameRandom);
            desimg.SaveAs(path);
            imgUpload.ImageName = _NameRandom;
            imgUpload.ImageUrl = FolderFrontEnd + "/" + _NameRandom;
            return imgUpload;
        }


        #endregion FrontEND

    }

    public static class FileHelper
    {
        public static void SaveResizedImage(string filePath, string filename, string resizedFilename, int percent)
        {
            Image origImg = Image.FromFile(Path.Combine(filePath, filename));
            Image resizedImg = ScaleByPercent(origImg, percent);

            resizedImg.Save(Path.Combine(filePath, resizedFilename), ImageFormat.Jpeg);
            resizedImg.Dispose();
            origImg.Dispose();
        }

        public static void SaveResizedImage(string filePath, string filename, string resizedFilename, int width, int height)
        {
            FileStream fileStream = new FileStream(filePath +"\\"+ filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            //Image origImg = Image.FromFile(Path.Combine(filePath, filename));
            Image origImg = Image.FromStream(fileStream);
            Image resizedImg = FixedSize(origImg, width, height);

            resizedImg.Save(Path.Combine(filePath, resizedFilename), ImageFormat.Jpeg);
            resizedImg.Dispose();
            origImg.Dispose();
        }

        public static Image ScaleByPercent(Image imgPhoto, int percent)
        {
            float nPercent = ((float)percent / 100);
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;

            int destX = 0;
            int destY = 0;
            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(destWidth, destHeight, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
            grPhoto.DrawImage(imgPhoto, new Rectangle(destX, destY, destWidth, destHeight), new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight), GraphicsUnit.Pixel);

            grPhoto.Dispose();

            return bmPhoto;
        }

        public static Image FixedSize(Image imgPhoto, int Width, int Height)
        {
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)Width / (float)sourceWidth);
            nPercentH = ((float)Height / (float)sourceHeight);
            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = System.Convert.ToInt16((Width - (sourceWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = System.Convert.ToInt16((Height - (sourceHeight * nPercent)) / 2);
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.Clear(Color.White);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
            grPhoto.DrawImage(imgPhoto, new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight), GraphicsUnit.Pixel);
            grPhoto.Dispose();

            return bmPhoto;
        }
    }

}
