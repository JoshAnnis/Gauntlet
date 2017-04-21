using Gauntlet.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gauntlet.Controllers
{
    public class HomeController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            if (User.IsInRole("User"))
            {
                return View("UserIndex");
            }
            else if (User.IsInRole("Admin"))
            {
                return View("AdminIndex");
            }
            else
            {
                return View();
            }


        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult DareMap()
        {
            ViewBag.Message = "Here you can see dares from different places";
            return View();
        }
        public ActionResult UploadFile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {

            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Files"), fileName);
                file.SaveAs(path);
            }

            return RedirectToAction("UploadFile");
        }

        public ActionResult Download()
        {
            string[] files = Directory.GetFiles(Server.MapPath("/Files"));
            for (int i = 0; i < files.Length; i++)
            {
                files[i] = Path.GetFileName(files[i]);
            }
            ViewBag.Files = files;
            return View();
        }
        
        





        public FileResult DownloadFile(string fileName)
        {
            var filepath = System.IO.Path.Combine(Server.MapPath("/Files/"), fileName);
            return File(filepath, MimeMapping.GetMimeMapping(filepath), fileName);
        }
        public FileContentResult UserVideo()
        {
            if (User.Identity.IsAuthenticated)
            {
                String userId = User.Identity.GetUserId();

                if (userId == null)
                {
                    string fileName = HttpContext.Server.MapPath(@"~Images/noImg.png");

                    byte[] imageData = null;
                    FileInfo fileinfo = new FileInfo(fileName);
                    long imageFileLength = fileinfo.Length;
                    FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    imageData = br.ReadBytes((int)imageFileLength);

                    return File(imageData, "video/ mp4");
                }
                var bdUsers = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
                var userImage = bdUsers.Users.Where(x => x.Id == userId).FirstOrDefault();
                var video = bdUsers.Video.Where(x => x.FromUserId == userId).FirstOrDefault();
                return new FileContentResult(video.UserVideo, "video/mp4");
            }
            else
            {
                string fileName = HttpContext.Server.MapPath(@"~/Images/noImg.png");

                byte[] imageData = null;
                FileInfo fileinfo = new FileInfo(fileName);
                long imageFileLength = fileinfo.Length;
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                imageData = br.ReadBytes((int)imageFileLength);
                return File(imageData, "video/mp4");
            }
        }


        public ActionResult UploadVideo()
        {
            return View("UserIndex");
        }
        //POST
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult UploadVideo([Bind(Exclude = "UserVideos")]VideoViewModel model)
        {
            List<Video> all = new List<Video>();

            var video = new Video { };
            byte[] imageData = null;
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase poImgFile = Request.Files["UserVideo"];

                using (var binary = new BinaryReader(poImgFile.InputStream))
                {
                    imageData = binary.ReadBytes(poImgFile.ContentLength);
                }
            }
            using (ApplicationDbContext db = new ApplicationDbContext())

            {
             var UserId = User.Identity.GetUserId();
            video.UserVideo = imageData;
            video.FromUserId = UserId;
            db.Video.Add(video);
            db.SaveChanges();
            }

            return View();
        }




        public void ProcessRequest(HttpContext context)
        {
            int id = int.Parse(context.Request.QueryString["id"]);
            byte[] bytes;
            string contentType;
            string strConnString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            string name;
            using (SqlConnection con = new SqlConnection(strConnString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select Name, Data, ContentType from tblFiles where Id=@Id";
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Connection = con;
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    sdr.Read();
                    bytes = (byte[])sdr["Data"];
                    contentType = sdr["ContentType"].ToString();
                    name = sdr["Name"].ToString();
                    con.Close();
                }
            }
            context.Response.Clear();
            context.Response.Buffer = true;
            context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + name);
            context.Response.ContentType = contentType;
            context.Response.BinaryWrite(bytes);
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    
    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    if (!IsPostBack)
    //    {
    //        BindGrid();
    //    }
    //}

    //private void BindGrid()
    //{
    //    string strConnString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    //    using (SqlConnection con = new SqlConnection(strConnString))
    //    {
    //        using (SqlCommand cmd = new SqlCommand())
    //        {
    //            cmd.CommandText = "select Id, Name from tblFiles";
    //            cmd.Connection = con;
    //            con.Open();
    //            DataList.DataSource = cmd.ExecuteReader();
    //            DataList.DataBind();
    //            con.Close();
    //        }
    //    }
    //}
  }
}
