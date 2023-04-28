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
using Newtonsoft.Json;
using AuthSystem.ViewModel;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Web.Http.Results;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Components.Forms;
using static AuthSystem.Data.Controller.ApiAuditTrailController;

namespace AuthSystem.Data.Controller
{

    [Authorize("ApiKey")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApiNotifcationController : ControllerBase
    {
        GlobalVariables gv = new GlobalVariables();
        DbManager db = new DbManager();
        private readonly AppSettings _appSettings;
        private ApplicationDbContext _context;
        private ApiGlobalModel _global = new ApiGlobalModel();
        private readonly JwtAuthenticationManager jwtAuthenticationManager;


        public ApiNotifcationController(IOptions<AppSettings> appSettings, ApplicationDbContext context, JwtAuthenticationManager jwtAuthenticationManager)
        {
   
            _context = context;
            _appSettings = appSettings.Value;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
   
        }

       // for CMS
        [HttpPost]
        public async Task<IActionResult> InsertNotifications(NotificationInsertModel data)
        {
            var result = new Registerstats();
            try
            {
                string sql0 = "";
                string itemid = "";
                string modulename = data.Module == "Vendor" ? "Vendor" : "Offering";
                if(modulename== "Offering")
                {
                    sql0 = $@"SELECT TOP (1) OfferingID FROM tbl_OfferingModel order by id desc";
                    DataTable dt2 = db.SelectDb(sql0).Tables[0];
                    itemid = dt2.Rows[0]["OfferingID"].ToString();

                }
                else
                {
                    sql0 = $@"SELECT TOP (1) VendorID FROM tbl_VendorModel order by id desc";
                    DataTable dt2 = db.SelectDb(sql0).Tables[0];
                    itemid = dt2.Rows[0]["VendorID"].ToString();
                }
             


                string sql1 = $@"SELECT EmployeeID  FROM UsersModel WHERE active=1";
                    DataTable table = db.SelectDb(sql1).Tables[0];
                foreach (DataRow dr in table.Rows)
                {
                    string sql = $@"SELECT  * from UsersModel where EmployeeID='" + dr["EmployeeID"].ToString() + "'";
                    DataTable dt = db.SelectDb(sql).Tables[0];
                    if (dt.Rows.Count != 0)
                    {
                        string Insert = $@"insert into tbl_NotificationModel (EmployeeID,Details,isRead,Module,ItemID,EmailStatus,DateCreated) values
                        ('" + dr["EmployeeID"].ToString() + "','" + data.Details + "','" + data.isRead + "','" + modulename + "','" + itemid + "','" + data.EmailStatus + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "') ";
                        db.AUIDB_WithParam(Insert);
                    }
                
                    else
                    {
                        result.Status = "Error";
                        return BadRequest(result);

                    }
            }
                    result.Status = "New Notifications Added";
                    return Ok(result);
              
            }

            catch (Exception ex)
            {
                return BadRequest(result);
            }
        }
        [HttpPost]
        public async Task<IActionResult> GetNotifEmpId(NotifId data)
        {
            string sql = $@"SELECT        Id, EmployeeID, Details, isRead,DateCreated,Module,ItemID,EmailStatus
                            FROM            tbl_NotificationModel
                            WHERE        (EmployeeID = '" +data.EmployeeID + "') order by id desc";
            var result = new List<NotificationModel>();
            DataTable table = db.SelectDb(sql).Tables[0];
            foreach (DataRow dr in table.Rows)
            {
                var item = new NotificationModel();
                item.Id = dr["Id"].ToString();
                item.EmployeeID = dr["EmployeeID"].ToString();
                item.Details = dr["Details"].ToString();
                item.isRead = dr["isRead"].ToString();
                item.DateCreated = dr["DateCreated"].ToString();
                item.Module = dr["Module"].ToString();
                item.ItemID = dr["ItemID"].ToString();
                item.EmailStatus = dr["EmailStatus"].ToString();
                result.Add(item);
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateNotification(NotifIdUpdate data)
        {
        
            string sql = $@"SELECT * from  tbl_NotificationModel where id='" + data.id + "' and EmployeeID='" + data.EmployeeID + "'";
            DataTable table = db.SelectDb(sql).Tables[0];
            if (table.Rows.Count != 0)
            {
                string Insert = $@" delete tbl_NotificationModel where id='" + data.id + "' and EmployeeID='" + data.EmployeeID + "'";
                db.AUIDB_WithParam(Insert);
                return Ok("Deleted");
            }
            else
            {
                return BadRequest("Error");
            }

        }
        [HttpGet]
        public async Task<IActionResult> NotificationList()
        {
            GlobalVariables gv = new GlobalVariables();
            string sql = $@"SELECT        tbl_NotificationModel.Details, tbl_NotificationModel.isRead, tbl_NotificationModel.DateCreated,Concat(UsersModel.Fname,' ', UsersModel.Lname) as Fullname, tbl_NotificationModel.Id, tbl_NotificationModel.EmployeeID, tbl_NotificationModel.Module, tbl_NotificationModel.ItemID, 
                         tbl_NotificationModel.EmailStatus
                         FROM            tbl_NotificationModel INNER JOIN
                         UsersModel ON tbl_NotificationModel.EmployeeID = UsersModel.EmployeeID";
            var result = new List<NotificationVM>();
            DataTable table = db.SelectDb(sql).Tables[0];

            foreach (DataRow dr in table.Rows)
            {
                string read = dr["isRead"].ToString() == "1" ? "Read" :"Unread";

                var item = new NotificationVM();
                item.Id = dr["Id"].ToString();
                item.EmployeeID = dr["EmployeeID"].ToString();
                item.Fullname = dr["Fullname"].ToString();
                item.Details = dr["Details"].ToString();
                item.Module = dr["Module"].ToString();
                item.ItemID = dr["ItemID"].ToString();
                item.EmailStatus = dr["EmailStatus"].ToString();
                item.isRead = read;
                item.DateCreated = DateTime.Parse(dr["DateCreated"].ToString()).ToString("MM-dd-yyyy");

                result.Add(item);
            }

            return Ok(result);
        }
        #region Model
        public class NotificationModel
        {

            public string? Id { get; set; }
            public string? EmployeeID { get; set; }
            public string? Details { get; set; }
            public string? isRead { get; set; }
            public string? DateCreated { get; set; }
            public string? Module { get; set; }
            public string? ItemID { get; set; }
            public string? EmailStatus { get; set; }

        }   
        public class NotificationVM
        {

            public string? Id { get; set; }
            public string? EmployeeID { get; set; }
            public string? Details { get; set; }
            public string? Fullname { get; set; }
            public string? isRead { get; set; }
            public string? DateCreated { get; set; }
            public string? Module { get; set; }
            public string? ItemID { get; set; }
            public string? EmailStatus { get; set; }

        }

        public class NotificationInsertModel
        {

            public string? Id { get; set; }
            public string? EmployeeID { get; set; }
            public string? Details { get; set; }
            public string? Module { get; set; }
            public string? ItemID { get; set; }
            public int? isRead { get; set; }
            public int? EmailStatus { get; set; }


        }
        public class NotifId
        {
            public string? EmployeeID { get; set; }

        }
        public class NotifIdUpdate
        {
            public int? id { get; set; }
            public string? EmployeeID { get; set; }

        }
        public class DeleteUser
        {

            public int Id { get; set; }
        }
        public class UserID
        {

            public int Id { get; set; }
        }
        public class Registerstats
        {
            public string Status { get; set; }

        }
        #endregion
    }
}
