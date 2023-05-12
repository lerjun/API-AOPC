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
using AuthSystem.ViewModel;
using Microsoft.Data.SqlClient;
using static AuthSystem.Data.Controller.ApiVendorController;

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
WHERE        (tbl_OfferingModel.OfferingID = '" +data.OfferingID + "')";
            
        

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
        [HttpPost]
        public IActionResult SaveOffering(OfferingVM data)
        {

            string sql = "";

            sql = $@"select * from tbl_OfferingModel where id ='" + data.Id + "'";
            DataTable dt = db.SelectDb(sql).Tables[0];
            var result = new Registerstats();
            string imgfile = "";
            string FeaturedImage = "";
            if (data.ImgUrl == null || data.ImgUrl == string.Empty)
            {
                FeaturedImage = "https://www.alfardanoysterprivilegeclub.com/assets/img/defaultavatar.png";
            }
            else
            {
                FeaturedImage = "https://www.alfardanoysterprivilegeclub.com/assets/img/" + data.ImgUrl;
            }
            if (dt.Rows.Count == 0)
            {
                sql = $@"select * from tbl_OfferingModel where OfferingName='" + data.OfferingName + "'";
                DataTable dt2 = db.SelectDb(sql).Tables[0];
                if (dt2.Rows.Count  != 0)
                {
                    result.Status = "Duplicate Offering Name";
                    return BadRequest(result);
                }
                else
                {
                
                    //if (data.MembershipID == "ALL")
                    //{
                    //    sql = $@"select * from tbl_MembershipModel where Status = 5";
                    //    DataTable table = db.SelectDb(sql).Tables[0];
                    //    foreach (DataRow dr in table.Rows)
                    //    {
                    //        string Insert = $@"insert into tbl_OfferingModel (VendorID,MembershipID,BusinessTypeID,OfferingName,PromoDesc,PromoReleaseText,ImgUrl,StatusID,PrivilegeID,Url,OfferDays,StartDate,EndDate,FromTime,ToTime) values 
                    //               ('" + data.VendorID + "','" + dr["Id"].ToString() + "','" + data.BusinessTypeID + "','" + data.OfferingName + "','" + data.PromoDesc + "','" + data.PromoReleaseText + "','" + FeaturedImage + "',5,'" + data.PrivilegeID + "'" +
                    //               ",'" + data.URL + "','" + data.Offerdays + "','" + data.StartDateTime + "','" + data.EndDateTime + "','" + data.FromTime + "','" + data.ToTime + "')";
                    //        db.AUIDB_WithParam(Insert);
                    //    }
                    //}
                    //else
                    //{
                        string Insert = $@"insert into tbl_OfferingModel (VendorID,MembershipID,BusinessTypeID,OfferingName,PromoDesc,PromoReleaseText,ImgUrl,StatusID,PrivilegeID,Url,OfferDays,StartDate,EndDate,FromTime,ToTime) values 
                                   ('" + data.VendorID + "','" + data.MembershipID + "','" + data.BusinessTypeID + "','" + data.OfferingName + "','" + data.PromoDesc + "','" + data.PromoReleaseText + "','" + FeaturedImage + "',5,'" + data.PrivilegeID + "'" +
                                    ",'" + data.URL + "','" + data.Offerdays + "','" + data.StartDateTime + "','" + data.EndDateTime + "','" + data.FromTime + "','" + data.ToTime + "')";
                        db.AUIDB_WithParam(Insert);
                  
      
                    result.Status = "Successfully Added";

                    return Ok(result);
                    //}
                }
      

            }
            else
            {

                string OTPInsert = $@"update tbl_OfferingModel set VendorID='"+data.VendorID+"' ,MembershipID='"+data.MembershipID + "' ,BusinessTypeID='"+data.BusinessTypeID + "' ,OfferingName='"+data.OfferingName + "' ,PromoDesc='"
                    +data.PromoDesc + "' ,PromoReleaseText='' ,ImgUrl='"+ FeaturedImage + "' ,StatusID='5' ,PrivilegeID='' ,Url='"+data.URL + "' ,OfferDays='"+data.Offerdays + "' ,StartDate='"
                    +data.StartDateTime+"' ,EndDate='"+data.EndDateTime+"' ,FromTime='"+data.FromTime+"' ,ToTime='"+data.ToTime+"'  where id =" + data.Id + "";
                db.AUIDB_WithParam(OTPInsert);
                result.Status = "Successfully Updated";

                return Ok(result);
            }


            return Ok(result);
        }
        public class DeleteOffer
        {

            public int Id { get; set; }
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
