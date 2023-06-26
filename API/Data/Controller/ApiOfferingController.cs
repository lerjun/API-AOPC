using AuthSystem.Manager;
using AuthSystem.Models;
using AuthSystem.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Data;
using System.Reflection;
using System.IO;
using AuthSystem.Data;
using AuthSystem.Data.Class;
using Newtonsoft.Json.Linq;
using Microsoft.Data.SqlClient;
using AuthSystem.ViewModel;
using static AuthSystem.Data.Controller.ApiRegisterController;
using System.Web.Http.Results;
using MimeKit;
using MailKit.Net.Smtp;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static AuthSystem.Data.Controller.ApiUserAcessController;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Org.BouncyCastle.Ocsp;
using static AuthSystem.Data.Controller.ApiNotifcationController;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
using System.ComponentModel;
using System.Drawing;
using System.Reflection.Metadata;
using System.Text.Unicode;
using System.Web.Http.Services;
using System.Xml.Linq;


namespace AuthSystem.Data.Controller
{

    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize("ApiKey")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApiOfferingController : ControllerBase
    {
        DbManager db = new DbManager();
        private readonly AppSettings _appSettings;
        private ApplicationDbContext _context;
        private ApiGlobalModel _global = new ApiGlobalModel();
        private readonly JwtAuthenticationManager jwtAuthenticationManager;


        public ApiOfferingController(IOptions<AppSettings> appSettings, ApplicationDbContext context, JwtAuthenticationManager jwtAuthenticationManager)
        {
   
            _context = context;
            _appSettings = appSettings.Value;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
   
        }
      
        [HttpGet]
        public async Task<IActionResult> OfferingList()
        {
            DataTable table = db.SelectDb_SP("SP_OfferingList").Tables[0];
            var result = new List<OfferingVM>();
            foreach (DataRow dr in table.Rows)
            {
                var item = new OfferingVM();
                item.Id= int.Parse(dr["Id"].ToString());
                item.BusinessTypeName=dr["BusinessTypeName"].ToString();
                item.VendorName= dr["VendorName"].ToString();
                item.PromoReleaseText= dr["PromoReleaseText"].ToString();
                item.OfferingName=dr["OfferingName"].ToString();
                item.MembershipName=dr["MembershipName"].ToString();
                item.VendorID= dr["VendorID"].ToString();
                item.ImgUrl= dr["ImgUrl"].ToString();
                item.OfferingID= dr["OfferingID"].ToString();
                item.Status= dr["Status"].ToString();
          
                result.Add(item);
            }

            return Ok(result);
        }  
        [HttpGet]
        public async Task<IActionResult> CMSOfferingList()
        {
            string sql = $@"SELECT        tbl_BusinessTypeModel.BusinessTypeName, tbl_VendorModel.VendorName, tbl_OfferingModel.PromoReleaseText, tbl_OfferingModel.OfferingName, tbl_MembershipModel.Name AS MembershipName, 
                         tbl_VendorModel.VendorID, tbl_OfferingModel.ImgUrl, tbl_OfferingModel.Id, tbl_OfferingModel.OfferingID, tbl_StatusModel.Name AS Status, tbl_OfferingModel.PromoDesc, tbl_OfferingModel.Url, tbl_OfferingModel.OfferDays, 
                         tbl_OfferingModel.StartDate, tbl_OfferingModel.EndDate, tbl_OfferingModel.FromTime, tbl_OfferingModel.ToTime, tbl_OfferingModel.DateCreated, tbl_MembershipModel.Id AS memid, tbl_BusinessTypeModel.Id AS btypeid, 
                         tbl_VendorModel.Id AS vid
FROM            tbl_OfferingModel LEFT OUTER JOIN
                         tbl_BusinessTypeModel ON tbl_OfferingModel.BusinessTypeID = tbl_BusinessTypeModel.Id LEFT OUTER JOIN
                         tbl_VendorModel ON tbl_OfferingModel.VendorID = tbl_VendorModel.Id LEFT OUTER JOIN
                         tbl_MembershipModel ON tbl_OfferingModel.MembershipID = tbl_MembershipModel.Id LEFT OUTER JOIN
                         tbl_StatusModel ON tbl_OfferingModel.StatusID = tbl_StatusModel.Id
WHERE        (tbl_OfferingModel.StatusID = 5) order by id desc";

            DataTable table = db.SelectDb(sql).Tables[0];
            var result = new List<OfferingVM>();
            foreach (DataRow dr in table.Rows)
            {
                var item = new OfferingVM();
                item.Id= int.Parse(dr["Id"].ToString());
                item.OfferingName= dr["OfferingName"].ToString();
                item.OfferingID= dr["OfferingID"].ToString();
                item.ImgUrl= dr["ImgUrl"].ToString();
                item.BusinessTypeName= dr["BusinessTypeName"].ToString();
                item.VendorName= dr["VendorName"].ToString();
                item.MembershipName= dr["MembershipName"].ToString();
                item.PromoDesc= dr["PromoDesc"].ToString();
                item.PromoReleaseText= dr["PromoReleaseText"].ToString();
                item.URL= dr["URL"].ToString();
                item.Offerdays= dr["Offerdays"].ToString();
                item.StartDateTime= Convert.ToDateTime(dr["StartDate"].ToString()).ToString("MM-dd-yyyy");
                item.EndDateTime = Convert.ToDateTime(dr["EndDate"].ToString()).ToString("MM-dd-yyyy");
                item.FromTime= dr["FromTime"].ToString();
                item.ToTime= dr["ToTime"].ToString();
                item.Status= dr["Status"].ToString();
                item.DateCreated = Convert.ToDateTime(dr["DateCreated"].ToString()).ToString("MM-dd-yyyy");
                item.BusinessTypeID = dr["btypeid"].ToString();
                item.MembershipID = dr["memid"].ToString();
                item.VendorID = dr["vid"].ToString();

                result.Add(item);
            }

            return Ok(result);
        }
        public class BtypeModel
        {

            public string BusinessTypeName { get; set; }

        }
        [HttpPost]
        public async Task<IActionResult> OfferingsFilteredbyVID(VendorIDVM data)
        {
            var param = new IDataParameter[]
                       {
               new SqlParameter("@VendorID",data.vendorID)
                       };
            DataTable table = db.SelectDb_SP("SP_GetOfferingFilteredbyVID", param).Tables[0];
            var result = new List<OfferingVM>();
            foreach (DataRow dr in table.Rows)
            {
                var item = new OfferingVM();
                item.Id = int.Parse(dr["Id"].ToString());
                item.BusinessTypeName = dr["BusinessTypeName"].ToString();
                item.VendorName = dr["VendorName"].ToString();
                item.PromoReleaseText = dr["PromoReleaseText"].ToString();
                item.OfferingName = dr["OfferingName"].ToString();
                item.MembershipName = dr["MembershipName"].ToString();
                item.VendorID = dr["VendorID"].ToString();
                item.ImgUrl = dr["ImgUrl"].ToString();
                item.OfferingID = dr["OfferingID"].ToString();
                item.Status = dr["Status"].ToString();

                result.Add(item);
            }

            return Ok(result);
        }

        public class offeridID
        {
            public string? OfferingID { get; set; }

        }
        [HttpPost]
        public async Task<IActionResult> OfferingsFilteredbyID(offeridID data)
        {
           
                string sql = $@"SELECT        tbl_BusinessTypeModel.BusinessTypeName, tbl_VendorModel.VendorName, tbl_OfferingModel.PromoReleaseText, tbl_OfferingModel.OfferingName, tbl_MembershipModel.Name AS MembershipName, 
                         tbl_VendorModel.VendorID, tbl_OfferingModel.ImgUrl, tbl_OfferingModel.Id, tbl_OfferingModel.OfferingID, tbl_StatusModel.Name AS Status, tbl_OfferingModel.Url, tbl_OfferingModel.OfferDays, tbl_OfferingModel.StartDate, 
                         tbl_OfferingModel.EndDate, tbl_OfferingModel.PromoDesc, tbl_OfferingModel.FromTime, tbl_OfferingModel.ToTime
FROM            tbl_OfferingModel INNER JOIN
                         tbl_BusinessTypeModel ON tbl_OfferingModel.BusinessTypeID = tbl_BusinessTypeModel.Id INNER JOIN
                         tbl_VendorModel ON tbl_OfferingModel.VendorID = tbl_VendorModel.Id INNER JOIN
                         tbl_MembershipModel ON tbl_OfferingModel.MembershipID = tbl_MembershipModel.Id INNER JOIN
                         tbl_StatusModel ON tbl_OfferingModel.StatusID = tbl_StatusModel.Id
WHERE        (tbl_OfferingModel.OfferingID = '" +data.OfferingID + "') and StatusID=5";
            
        

            DataTable table = db.SelectDb(sql).Tables[0];

            var item = new OfferingVM();
            if (table.Rows.Count != 0 )
            {

        
                foreach (DataRow dr in table.Rows)
                {
            
                item.Id = int.Parse(dr["Id"].ToString());
                item.BusinessTypeName = dr["BusinessTypeName"].ToString();
                item.VendorName = dr["VendorName"].ToString();
                item.PromoReleaseText = dr["PromoReleaseText"].ToString();
                item.OfferingName = dr["OfferingName"].ToString();
                item.MembershipName = dr["MembershipName"].ToString();
                item.VendorID = dr["VendorID"].ToString();
                item.ImgUrl = dr["ImgUrl"].ToString();
                item.OfferingID = dr["OfferingID"].ToString();
                item.Status = dr["Status"].ToString();
                item.URL = dr["URL"].ToString();
                item.Offerdays = dr["Offerdays"].ToString();
                item.StartDateTime = Convert.ToDateTime(dr["StartDate"].ToString()).ToString("MM/dd/yyyy");
                item.EndDateTime = Convert.ToDateTime(dr["EndDate"].ToString()).ToString("MM/dd/yyyy");
                item.PromoDesc = dr["PromoDesc"].ToString();
                item.FromTime = dr["FromTime"].ToString();
                item.ToTime = dr["ToTime"].ToString();

                }
            
            }
            else
            {
                return BadRequest();
            }

                return Ok(item);
        }
        [HttpGet]
        public async Task<IActionResult> UserListEmail()
        {
            string sql = $@"select Concat (Fname , ' ' , Lname) as Fullname, Email from UsersModel where AllowEmailNotif =  1 and active = 1";

            DataTable table = db.SelectDb(sql).Tables[0];
            var result = new List<Userlist>();
            foreach (DataRow dr in table.Rows)
            {
                var item = new Userlist();
                item.Fullname = dr["Fullname"].ToString();
                item.Email = dr["Email"].ToString();
                result.Add(item);
            }

            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> OfferingFilterList(BtypeModel data)
        {
            var param = new IDataParameter[]
                       {
               new SqlParameter("@BusinessTypeName",data.BusinessTypeName)
                       };
            DataTable table = db.SelectDb_SP("SP_GetOfferingFilterList", param).Tables[0];
            var result = new List<OfferingVM>();
            foreach (DataRow dr in table.Rows)
            {
                var item = new OfferingVM();
                item.Id = int.Parse(dr["Id"].ToString());
                item.BusinessTypeName = dr["BusinessTypeName"].ToString();
                item.VendorName = dr["VendorName"].ToString();
                item.PromoReleaseText = dr["PromoReleaseText"].ToString();
                item.OfferingName = dr["OfferingName"].ToString();
                item.MembershipName = dr["MembershipName"].ToString();
                item.VendorID = dr["VendorID"].ToString();
                item.ImgUrl = dr["ImgUrl"].ToString();
                item.OfferingID = dr["OfferingID"].ToString();
                item.Status = dr["Status"].ToString();

                result.Add(item);
            }

            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetVendorOfferingList()
        {
            GlobalVariables gv = new GlobalVariables();

            var result = new List<VendorOfferingVM>();
            DataTable table = db.SelectDb_SP("SP_GetVendorOfferings").Tables[0];

            foreach (DataRow dr in table.Rows)
            {
                var item = new VendorOfferingVM();
                item.Id = int.Parse(dr["id"].ToString());
                item.VendorName = dr["VendorName"].ToString();
                item.Description = dr["Description"].ToString();
                item.Services = dr["Services"].ToString();
                item.WebsiteUrl = dr["WebsiteUrl"].ToString();
                item.FeatureImg = dr["FeatureImg"].ToString();
                item.Gallery = dr["Gallery"].ToString();
                item.Cno = dr["Cno"].ToString();
                item.Email = dr["Email"].ToString();
                item.VideoUrl = dr["VideoUrl"].ToString();
                item.VrUrl = dr["VrUrl"].ToString();
                item.OfferingDesc = dr["OfferingDesc"].ToString();
                item.PromoDesc = dr["PromoDesc"].ToString();
                item.Expiry = dr["Expiry"].ToString();
                item.MembershipName = dr["MembershipName"].ToString();
                item.DateEnded = Convert.ToDateTime(dr["DateEnded"].ToString()).ToString("MM/dd/yyyy");
                item.DateUsed = Convert.ToDateTime(dr["DateUsed"].ToString()).ToString("MM/dd/yyyy");
                item.PromoReleaseText = dr["PromoReleaseText"].ToString();
                item.Country = dr["Country"].ToString();
                item.City = dr["City"].ToString();
                item.PostalCode = dr["PostalCode"].ToString();
                item.BusinessTypeName = dr["BusinessTypeName"].ToString();
                item.BusinessTypeDesc = dr["BusinessTypeDesc"].ToString();
                item.BusinessName = dr["BusinessName"].ToString();
                item.BusinessDesc = dr["BusinessDesc"].ToString();
                item.Address = dr["Address"].ToString();
                item.BusinessEmail = dr["BusinessEmail"].ToString();
                item.BusinessCno = dr["BusinessCno"].ToString();
                item.Url = dr["Url"].ToString();
                item.BusinessServ = dr["BusinessServ"].ToString();
                item.ImageUrl = dr["FeaturedImg"].ToString();
                item.BusinessGallery = dr["BusinessGallery"].ToString();
                item.FileUrl = dr["FileUrl"].ToString();
                item.Map = dr["Map"].ToString();
                item.VendorLogo = dr["VendorLogo"].ToString();
                

                result.Add(item);
            }

            return Ok(result);
        }
        public class Registerstats
        {
            public string Status { get; set; }

        }  
        public class Userlist
        {
            public string Fullname { get; set; }
            public string Email { get; set; }

        }
        [HttpPost]
        public IActionResult SaveOffering(OfferingVM data)
        {

            string sql_ = "";
            string sql= "";

            sql = $@"select * from tbl_OfferingModel where id ='" + data.Id + "'";
            DataTable dt = db.SelectDb(sql).Tables[0];
            var result = new Registerstats();
            string imgfile = "";
            string FeaturedImage = "";
            string res_image = "";
            int image_ = 0;
            if (data.Id != 0)
            {
                sql_ += $@"select Top(1) OfferingID from tbl_OfferingModel where StatusID =5 and id='" + data.Id + "' order by id desc  ";
                DataTable table = db.SelectDb(sql_).Tables[0];
                string str = table.Rows[0]["OfferingID"].ToString();
                res_image = str;
            }
            else
            {
                sql_ += $@"select Top(1) OfferingID from tbl_OfferingModel where StatusID =5  order by id desc  ";
                DataTable table = db.SelectDb(sql_).Tables[0];
                string str = table.Rows[0]["OfferingID"].ToString();
                image_ = int.Parse(str.Replace("Offering-", "")) + 1;
                res_image = "Offering-0" + image_;
            }
            if (data.ImgUrl == null || data.ImgUrl == string.Empty)
            {
                FeaturedImage = "https://www.alfardanoysterprivilegeclub.com/assets/img/defaultavatar.png";
            }
            else
            {
                FeaturedImage = "https://www.alfardanoysterprivilegeclub.com/assets/img/" + data.ImgUrl.Replace(" ", "%20"); ;
            }
            if (dt.Rows.Count == 0)
            {
                sql = $@"select * from tbl_OfferingModel where OfferingName='" + data.OfferingName + "' and StatusID = 5";
                DataTable dt2 = db.SelectDb(sql).Tables[0];
                if (dt2.Rows.Count  != 0)
                {
                    result.Status = "Duplicate Offering Name";
                    return BadRequest(result);
                }
                else
                {

                    if (data.MembershipID == "ALL")
                    {
                       
                            string Insert = $@"insert into tbl_OfferingModel (VendorID,MembershipID,BusinessTypeID,OfferingName,PromoDesc,PromoReleaseText,ImgUrl,StatusID,PrivilegeID,Url,OfferDays,StartDate,EndDate,FromTime,ToTime) values 
                                   ('" + data.VendorID + "','10','" + data.BusinessTypeID + "','" + data.OfferingName + "','" + data.PromoDesc + "','" + data.PromoReleaseText + "','" + FeaturedImage + "',5,'" + data.PrivilegeID + "'" +
                                   ",'" + data.URL + "','" + data.Offerdays + "','" + data.StartDateTime + "','" + data.EndDateTime + "','" + data.FromTime + "','" + data.ToTime + "')";
                           db.AUIDB_WithParam(Insert);
                        
                    }
                    else
                    {
                        string Insert = $@"insert into tbl_OfferingModel (VendorID,MembershipID,BusinessTypeID,OfferingName,PromoDesc,PromoReleaseText,ImgUrl,StatusID,PrivilegeID,Url,OfferDays,StartDate,EndDate,FromTime,ToTime) values 
                                   ('" + data.VendorID + "','" + data.MembershipID + "','" + data.BusinessTypeID + "','" + data.OfferingName + "','" + data.PromoDesc + "','" + data.EndDateTime + "','" + FeaturedImage + "',5,'" + data.PrivilegeID + "'" +
                                    ",'" + data.URL + "','" + data.Offerdays + "','" + data.StartDateTime + "','" + data.EndDateTime + "','" + data.FromTime + "','" + data.ToTime + "')";
                        db.AUIDB_WithParam(Insert);
                  
      
                    result.Status = "Successfully Added";

                    return Ok(result);
                    }
                }
      

            }
            else
            {
                if (data.MembershipID == "ALL")
                {
                    string OTPInsert = $@"update tbl_OfferingModel set VendorID='" + data.VendorID + "' ,MembershipID='10' ,BusinessTypeID='" + data.BusinessTypeID + "' ,OfferingName='" + data.OfferingName + "' ,PromoDesc='"
                + data.PromoDesc + "' ,PromoReleaseText='" + data.EndDateTime + "' ,ImgUrl='" + FeaturedImage + "' ,StatusID='5' ,PrivilegeID='' ,Url='" + data.URL + "' ,OfferDays='" + data.Offerdays + "' ,StartDate='"
                + data.StartDateTime + "' ,EndDate='" + data.EndDateTime + "' ,FromTime='" + data.FromTime + "' ,ToTime='" + data.ToTime + "'  where id =" + data.Id + "";
                    db.AUIDB_WithParam(OTPInsert);
                    result.Status = "Successfully Updated";

                    return Ok(result);
                }
                else
                {
                    string OTPInsert = $@"update tbl_OfferingModel set VendorID='" + data.VendorID + "' ,MembershipID='" + data.MembershipID + "' ,BusinessTypeID='" + data.BusinessTypeID + "' ,OfferingName='" + data.OfferingName + "' ,PromoDesc='"
                    + data.PromoDesc + "' ,PromoReleaseText='" + data.EndDateTime + "' ,ImgUrl='" + FeaturedImage + "' ,StatusID='5' ,PrivilegeID='' ,Url='" + data.URL + "' ,OfferDays='" + data.Offerdays + "' ,StartDate='"
                    + data.StartDateTime + "' ,EndDate='" + data.EndDateTime + "' ,FromTime='" + data.FromTime + "' ,ToTime='" + data.ToTime + "'  where id =" + data.Id + "";
                    db.AUIDB_WithParam(OTPInsert);
                    result.Status = "Successfully Updated";

                    return Ok(result);

                }
            }


            return Ok(result);
        }
        public class DeleteOffer
        {

            public int Id { get; set; }
        }
        [HttpPost]
        public IActionResult DeleteOfferingList(List<DeleteOffer> IdList)
        {
            //string delete = $@"delete tbl_MembershipPrivilegeModel where MembershipID='" + IdList[0].MembershipID + "'";
            //db.AUIDB_WithParam(delete);
            var result = new Registerstats();
            string imgfile = "";

            foreach (var emp in IdList)
            {
                string delete = $@"update tbl_OfferingModel set StatusID = 6 where id ='" + emp.Id + "'";
                db.AUIDB_WithParam(delete);
            }
            result.Status = "Successfully Added";

            

            return Ok(result);
        }
        public class UserEmail
        {

            public string email { get; set; }
            public string offerid { get; set; }
        }
        [HttpPost]
        public IActionResult SendEmail(List<UserEmail> IdList)
        {
            //string delete = $@"delete tbl_MembershipPrivilegeModel where MembershipID='" + IdList[0].MembershipID + "'";
            //db.AUIDB_WithParam(delete);
            var result = new Registerstats();
            string imgfile = "";
            string status = "";
            foreach (var emp in IdList)
            {

                string offerid = emp.offerid;
                string sql = $@"SELECT        tbl_OfferingModel.Id, tbl_OfferingModel.OfferingName, tbl_OfferingModel.ImgUrl, tbl_OfferingModel.PromoDesc, tbl_VendorModel.VendorName
FROM            tbl_OfferingModel LEFT OUTER JOIN
                         tbl_VendorModel ON tbl_OfferingModel.Id = tbl_VendorModel.Id where   tbl_OfferingModel.Id ='" + offerid + "'";
                DataTable dt = db.SelectDb(sql).Tables[0];
                string imgurl = dt.Rows[0]["ImgUrl"].ToString();
                string offername = dt.Rows[0]["OfferingName"].ToString();
                string PromoDesc = dt.Rows[0]["PromoDesc"].ToString();
                string VendorName = dt.Rows[0]["VendorName"].ToString();
                //var emailsend = "https://www.alfardanoysterprivilegeclub.com/change-password/" + emp.email;
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("AOPC New Offering", "app@alfardan.com.qa"));
                message.To.Add(new MailboxAddress("", emp.email));
                message.Subject = "New Offering Added";
                var bodyBuilder = new BodyBuilder();
                string img = "../img/AOPCBlack.jpg";
                bodyBuilder.HtmlBody = @"<!DOCTYPE html>

<html>
  <head>
    <meta charset=""utf-8"" />
    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"" />
    <title></title>
    <meta name=""description"" content="""" />
    <meta name=""viewport"" content=""width=device-width, initial-scale=1"" />
    <link rel=""stylesheet"" href="""" />
  </head>
  <body
    style=""
      display: flex;
      flex-direction: column;
      font-family: Arial, Helvetica, sans-serif;
    ""
  >
    <table style=""width: 500px; margin: 0 auto; border-collapse: collapse"">
      <tr>
        <td style=""text-align: center; padding: 0"">
          <img
            style=""width: 100%;object-fit:cover;height:200px""
            src=""https://www.alfardanoysterprivilegeclub.com/assets/img/AOPC%20Logo%20-%20Black.png""
            alt=""Oyster Privilege""
            width=""100%""
          />
        </td>
      </tr>
      <tr style=""background-color: #c89328; padding-top: 10px"">
        <td style=""text-align: center; padding: 0; color: white"">
          <h1>New Offering</h1>
        </td>
      </tr>
      <tr>
        <td style=""text-align: center; padding: 0"">
          <img
            style=""height: 200px""
            src=" + imgurl+" "+
           " alt= "+
           " width='100%' "+
          "/>" +
          "</td>" +
          " </tr>" +
          "<tr>" +
          "<td>" +
          " <h3 style='text-align: left; color: #c89328'>" +
                    ""+offername+" <br />" +
                    " <span style='font-size: 15px; color: black'>"+ VendorName + "</span>" +
                    " </h3>" +
                    "</td>" +
                    " </tr>" +
                    "<tr>" +
                    "<td style='vertical-align: top'>" +
                    " <p style='font-size: 12px; text-align: left'>" +
                    ""+PromoDesc+" </p>" +
                    "</td>" +
                    "</tr>" +
                    "<tr style='height: 100px'>" +
                    " <td style='padding-top: 20px; text-align: center'>" +
                    "<a style=''" +
                    "background-color: #c89328;" +
                    " padding-left: 20px;" +
                    "padding-right: 20px;" +
                    "padding-top: 10px;" +
                    " padding-bottom: 10px;" +
                    "border-radius: 10px;" +
                    "box-shadow: 3px 2px 5px 3px rgb(220, 220, 220);" +
                    "text-decoration: none;" +
                    "color: white;' "+
           " href='www.alfardanoysterprivilegeclub.com >Visit Oyster Privilege Club</a"+
       " </td>" +
       "</tr>" +
       "<tr style='border-top: 1xp solid grey; padding-top: 20px'>" +
                    "<td style='font-size: 10px; text-align: center'>" +
                    "<a style='text-decoration: none; color: black' href=''>Copyright © Alfardan Properties</a >"+
       " </td>" +
       "<td></td>" +
       "</tr>" +
       " </table>" +
       "<script src='' async defer></script>" +
       "</body>" +
       "</html>";
                message.Body = bodyBuilder.ToMessageBody();
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.office365.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                    client.Authenticate("app@alfardan.com.qa", "Oyster2023!");
                    client.Send(message);
                    client.Disconnect(true);
                    status = "Successfully sent registration email";

                }
                result.Status = "Email Successfully Sent";

            }
            return Ok(result);

        }
        [HttpPost]
        public IActionResult SendEmailVendor(List<UserEmail> IdList)
        {
            //string delete = $@"delete tbl_MembershipPrivilegeModel where MembershipID='" + IdList[0].MembershipID + "'";
            //db.AUIDB_WithParam(delete);
            var result = new Registerstats();
            string imgfile = "";
            string status = "";
            foreach (var emp in IdList)
            {

                string offerid = emp.offerid;
                string sql = $@"SELECT        FeatureImg, VendorName, Id, Description
                            FROM            tbl_VendorModel
                            WHERE        (Id = '" + offerid + "')";
                DataTable dt = db.SelectDb(sql).Tables[0];
                string imgurl = dt.Rows[0]["FeatureImg"].ToString();
                string offername = dt.Rows[0]["VendorName"].ToString();
                string PromoDesc = dt.Rows[0]["Description"].ToString();
                //var emailsend = "https://www.alfardanoysterprivilegeclub.com/change-password/" + emp.email;
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("AOPC New Vendor", "app@alfardan.com.qa"));
                message.To.Add(new MailboxAddress("", emp.email));
                message.Subject = "New Vendor Added";
                var bodyBuilder = new BodyBuilder();
                string img = "../img/AOPCBlack.jpg";
                bodyBuilder.HtmlBody = @"<!DOCTYPE html>

<html>
  <head>
    <meta charset=""utf-8"" />
    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"" />
    <title></title>
    <meta name=""description"" content="""" />
    <meta name=""viewport"" content=""width=device-width, initial-scale=1"" />
    <link rel=""stylesheet"" href="""" />
  </head>
  <body
    style=""
      display: flex;
      flex-direction: column;
      font-family: Arial, Helvetica, sans-serif;
    ""
  >
    <table style=""width: 500px; margin: 0 auto; border-collapse: collapse"">
      <tr>
        <td style=""text-align: center; padding: 0"">
          <img
            style=""width: 300px""
            src=""https://www.alfardanoysterprivilegeclub.com/assets/img/AOPC%20Logo%20-%20Black.png""
            alt=""Oyster Privilege""
            width=""100%""
          />
        </td>
      </tr>
      <tr style=""background-color: #c89328; padding-top: 10px"">
        <td style=""text-align: center; padding: 0; color: white"">
          <h1>New Vendor</h1>
        </td>
      </tr>
      <tr>
        <td style=""text-align: center; padding: 0"">
          <img
            style=""width: 100%;object-fit:cover;height:200px""
            src=" + imgurl + " " +
           " alt= " +
           " width='100%' " +
          "/>" +
          "</td>" +
          " </tr>" +
          "<tr>" +
          "<td>" +
          " <h3 style='text-align: left; color: #c89328'>" +
                    "" + offername + " <br />" +
                    " </h3>" +
                    "</td>" +
                    " </tr>" +
                    "<tr>" +
                    "<td style='vertical-align: top'>" +
                    " <p style='font-size: 12px; text-align: left'>" +
                    "" + PromoDesc + " </p>" +
                    "</td>" +
                    "</tr>" +
                    "<tr style='height: 100px'>" +
                    " <td style='padding-top: 20px; text-align: center'>" +
                    "<a style=''" +
                    "background-color: #c89328;" +
                    " padding-left: 20px;" +
                    "padding-right: 20px;" +
                    "padding-top: 10px;" +
                    " padding-bottom: 10px;" +
                    "border-radius: 10px;" +
                    "box-shadow: 3px 2px 5px 3px rgb(220, 220, 220);" +
                    "text-decoration: none;" +
                    "color: white;' " +
           " href='www.alfardanoysterprivilegeclub.com >Visit Oyster Privilege Club</a" +
       " </td>" +
       "</tr>" +
       "<tr style='border-top: 1xp solid grey; padding-top: 20px'>" +
                    "<td style='font-size: 10px; text-align: center'>" +
                    "<a style='text-decoration: none; color: black' href=''>Copyright © Alfardan Properties</a >" +
       " </td>" +
       "<td></td>" +
       "</tr>" +
       " </table>" +
       "<script src='' async defer></script>" +
       "</body>" +
       "</html>";
                message.Body = bodyBuilder.ToMessageBody();
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.office365.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                    client.Authenticate("app@alfardan.com.qa", "Oyster2023!");
                    client.Send(message);
                    client.Disconnect(true);
                    status = "Successfully sent registration email";

                }
                result.Status = "Email Successfully Sent";

            }
            return Ok(result);

        }
        [HttpPost]
        public IActionResult DeleteOffering(DeleteOffer data)
        {

            string sql = $@"select * from tbl_OfferingModel where id ='" + data.Id + "'";
            DataTable dt = db.SelectDb(sql).Tables[0];
            var result = new Registerstats();
            string imgfile = "";
            if (dt.Rows.Count != 0)
            {

                string OTPInsert = $@"update tbl_OfferingModel set StatusID = 6 where id ='" + data.Id + "'";
                db.AUIDB_WithParam(OTPInsert);
                result.Status = "Succesfully deleted";

                return Ok(result);

            }
            else
            {
                result.Status = "Error";

                return BadRequest(result);

            }


            return Ok(result);
        }
    }
}
