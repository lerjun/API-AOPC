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
using static AuthSystem.Data.Controller.ApiVendorController;
using static AuthSystem.Data.Controller.ApiRegisterController;
using System.Security.Cryptography;
using System.Net;

namespace AuthSystem.Data.Controller
{

    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize("ApiKey")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApiRegisterController : ControllerBase
    {
        GlobalVariables gv = new GlobalVariables();
        DbManager db = new DbManager();
        private readonly AppSettings _appSettings;
        private ApplicationDbContext _context;
        private ApiGlobalModel _global = new ApiGlobalModel();
        private readonly JwtAuthenticationManager jwtAuthenticationManager;


        public ApiRegisterController(IOptions<AppSettings> appSettings, ApplicationDbContext context, JwtAuthenticationManager jwtAuthenticationManager)
        {
   
            _context = context;
            _appSettings = appSettings.Value;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
   
        }
      
        [HttpGet]
        public async Task<IActionResult> UserInfoList()
        {
            GlobalVariables gv = new GlobalVariables();

            var result = new List<UserVM>();
            DataTable table = db.SelectDb_SP("SP_UserInfo").Tables[0];

            foreach (DataRow dr in table.Rows)
            {
                var item = new UserVM();
                item.Id = int.Parse(dr["id"].ToString());
                item.Fullname = dr["Fname"].ToString()+" " + dr["Lname"].ToString();
                item.Username = dr["Username"].ToString();
                item.Fname = dr["Fname"].ToString();
                item.Lname = dr["Lname"].ToString();
                item.Email = dr["Email"].ToString();
                item.Gender = dr["Gender"].ToString();
                item.EmployeeID = dr["EmployeeID"].ToString();
                item.Position = dr["Position"].ToString();
                item.Corporatename = dr["Corporatename"].ToString();
                item.UserType = dr["UserType"].ToString();
                item.DateCreated = Convert.ToDateTime(dr["DateCreated"].ToString()).ToString("MM/dd/yyyy");
                item.CorporateID = dr["CorporateID"].ToString();
                item.PositionID = dr["PositionID"].ToString();
                item.status = dr["status"].ToString();

                result.Add(item);
            }

            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> Corporatelist()
        {
            GlobalVariables gv = new GlobalVariables();
            string sql = $@"SELECT        UsersModel.Username, UsersModel.Fname, UsersModel.Lname, UsersModel.Email, UsersModel.Gender, UsersModel.EmployeeID, tbl_PositionModel.Name AS Position, tbl_CorporateModel.CorporateName, 
                         tbl_UserTypeModel.UserType, UsersModel.Fullname, UsersModel.Id, UsersModel.DateCreated, tbl_PositionModel.Id AS PositionID, tbl_CorporateModel.Id AS CorporateID, tbl_StatusModel.Name AS status, UsersModel.isVIP, 
                         UsersModel.FilePath
FROM            UsersModel INNER JOIN
                         tbl_CorporateModel ON UsersModel.CorporateID = tbl_CorporateModel.Id LEFT OUTER JOIN
                         tbl_PositionModel ON UsersModel.PositionID = tbl_PositionModel.Id LEFT OUTER JOIN
                         tbl_UserTypeModel ON UsersModel.Type = tbl_UserTypeModel.Id LEFT OUTER JOIN
                         tbl_StatusModel ON UsersModel.Active = tbl_StatusModel.Id
WHERE        (UsersModel.Active IN (1, 2, 9, 10)) AND (UsersModel.Type = 2)";
            var result = new List<UserVM>();
            DataTable table = db.SelectDb(sql).Tables[0];

            foreach (DataRow dr in table.Rows)
            {
                var item = new UserVM();
                item.Id = int.Parse(dr["id"].ToString());
                item.Fullname = dr["Fname"].ToString() + " " + dr["Lname"].ToString();
                item.Username = dr["Username"].ToString();
                item.Fname = dr["Fname"].ToString();
                item.Lname = dr["Lname"].ToString();
                item.Email = dr["Email"].ToString();
                item.Gender = dr["Gender"].ToString();
                item.EmployeeID = dr["EmployeeID"].ToString();
                item.Position = dr["Position"].ToString();
                item.Corporatename = dr["Corporatename"].ToString();
                item.UserType = dr["UserType"].ToString();
                item.DateCreated = Convert.ToDateTime(dr["DateCreated"].ToString()).ToString("MM/dd/yyyy");
                item.CorporateID = dr["CorporateID"].ToString();
                item.PositionID = dr["PositionID"].ToString();
                item.status = dr["status"].ToString();
                item.FilePath = dr["FilePath"].ToString();
                item.FilePath = dr["FilePath"].ToString();
                item.isVIP = dr["isVIP"].ToString();
                result.Add(item);
            }

            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> UserAllist()
        {
            GlobalVariables gv = new GlobalVariables();

            string sql = $@"SELECT        UsersModel.Username, UsersModel.Fname, UsersModel.Lname, UsersModel.Email, UsersModel.Gender, UsersModel.EmployeeID, tbl_PositionModel.Name AS Position, tbl_CorporateModel.CorporateName, 
                         tbl_UserTypeModel.UserType, UsersModel.Fullname, UsersModel.Id, UsersModel.DateCreated, tbl_PositionModel.Id AS PositionID, tbl_CorporateModel.Id AS CorporateID, tbl_StatusModel.Name AS status, UsersModel.isVIP, 
                         UsersModel.FilePath
FROM            UsersModel LEFT OUTER JOIN
                         tbl_CorporateModel ON UsersModel.CorporateID = tbl_CorporateModel.Id LEFT OUTER JOIN
                         tbl_PositionModel ON UsersModel.PositionID = tbl_PositionModel.Id LEFT OUTER JOIN
                         tbl_UserTypeModel ON UsersModel.Type = tbl_UserTypeModel.Id LEFT OUTER JOIN
                         tbl_StatusModel ON UsersModel.Active = tbl_StatusModel.Id
WHERE        (UsersModel.Active IN (1, 2, 9,10)) AND (UsersModel.Type = 3) ";
            var result = new List<UserVM>();
            DataTable table = db.SelectDb(sql).Tables[0];

            foreach (DataRow dr in table.Rows)
            {
                var item = new UserVM();
                item.Id = int.Parse(dr["id"].ToString());
                item.Fullname = dr["Fname"].ToString() + " " + dr["Lname"].ToString();
                item.Username = dr["Username"].ToString();
                item.Fname = dr["Fname"].ToString();
                item.Lname = dr["Lname"].ToString();
                item.Email = dr["Email"].ToString();
                item.Gender = dr["Gender"].ToString();
                item.EmployeeID = dr["EmployeeID"].ToString();
                item.Position = dr["Position"].ToString();
                item.Corporatename = dr["Corporatename"].ToString();
                item.UserType = dr["UserType"].ToString();
                item.DateCreated = Convert.ToDateTime(dr["DateCreated"].ToString()).ToString("MM/dd/yyyy");
                item.CorporateID = dr["CorporateID"].ToString();
                item.PositionID = dr["PositionID"].ToString();
                item.status = dr["status"].ToString();
                item.FilePath = dr["FilePath"].ToString();
                item.isVIP = dr["isVIP"].ToString();

                result.Add(item);
            }

            return Ok(result);
        }
        public class CorporateID
        {
            public string ID { get; set; }

        }
        [HttpPost]
        public async Task<IActionResult> CorporateAdminUserList(CorporateID data)
        {
            GlobalVariables gv = new GlobalVariables();

            string sql = $@"SELECT        UsersModel.Username, UsersModel.Fname, UsersModel.Lname, UsersModel.Email, UsersModel.Gender, UsersModel.EmployeeID, tbl_PositionModel.Name AS Position, tbl_CorporateModel.CorporateName, 
                         tbl_UserTypeModel.UserType, UsersModel.Fullname, UsersModel.Id, UsersModel.DateCreated, tbl_PositionModel.Id AS PositionID, tbl_CorporateModel.Id AS CorporateID, tbl_StatusModel.Name AS status, UsersModel.isVIP, 
                         UsersModel.FilePath
                         FROM            UsersModel LEFT OUTER JOIN
                         tbl_CorporateModel ON UsersModel.CorporateID = tbl_CorporateModel.Id LEFT OUTER JOIN
                         tbl_PositionModel ON UsersModel.PositionID = tbl_PositionModel.Id LEFT OUTER JOIN
                         tbl_UserTypeModel ON UsersModel.Type = tbl_UserTypeModel.Id LEFT OUTER JOIN
                         tbl_StatusModel ON UsersModel.Active = tbl_StatusModel.Id
                         WHERE        (UsersModel.Active IN (1, 2, 9, 10)) AND (UsersModel.Type = 3) AND (UsersModel.CorporateID = '"+data.ID+"')";
            var result = new List<UserVM>();
            DataTable table = db.SelectDb(sql).Tables[0];

            foreach (DataRow dr in table.Rows)
            {
                var item = new UserVM();
                item.Id = int.Parse(dr["id"].ToString());
                item.Fullname = dr["Fname"].ToString() + " " + dr["Lname"].ToString();
                item.Username = dr["Username"].ToString();
                item.Fname = dr["Fname"].ToString();
                item.Lname = dr["Lname"].ToString();
                item.Email = dr["Email"].ToString();
                item.Gender = dr["Gender"].ToString();
                item.EmployeeID = dr["EmployeeID"].ToString();
                item.Position = dr["Position"].ToString();
                item.Corporatename = dr["Corporatename"].ToString();
                item.UserType = dr["UserType"].ToString();
                item.DateCreated = Convert.ToDateTime(dr["DateCreated"].ToString()).ToString("MM/dd/yyyy");
                item.CorporateID = dr["CorporateID"].ToString();
                item.PositionID = dr["PositionID"].ToString();
                item.status = dr["status"].ToString();
                item.FilePath = dr["FilePath"].ToString();
                item.isVIP = dr["isVIP"].ToString();

                result.Add(item);
            }

            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> AdminList()
        {
            GlobalVariables gv = new GlobalVariables();

   
            string sql = $@"SELECT        UsersModel.Username, UsersModel.Fname, UsersModel.Lname, UsersModel.Email, UsersModel.Gender, UsersModel.EmployeeID, tbl_PositionModel.Name AS Position, tbl_CorporateModel.CorporateName, 
                         tbl_UserTypeModel.UserType, UsersModel.Fullname, UsersModel.Id, UsersModel.DateCreated, tbl_PositionModel.Id AS PositionID, tbl_CorporateModel.Id AS CorporateID, tbl_StatusModel.Name AS status, UsersModel.isVIP,    UsersModel.FilePath
FROM            UsersModel INNER JOIN
                         tbl_CorporateModel ON UsersModel.CorporateID = tbl_CorporateModel.Id INNER JOIN
                         tbl_PositionModel ON UsersModel.PositionID = tbl_PositionModel.Id INNER JOIN
                         tbl_UserTypeModel ON UsersModel.Type = tbl_UserTypeModel.Id INNER JOIN
                         tbl_StatusModel ON UsersModel.Active = tbl_StatusModel.Id
WHERE        (UsersModel.Active IN (1, 2, 9,10)) and Type=1";
            var result = new List<UserVM>();
            DataTable table = db.SelectDb(sql).Tables[0];

            foreach (DataRow dr in table.Rows)
            {
                var item = new UserVM();
                item.Id = int.Parse(dr["id"].ToString());
                item.Fullname = dr["Fname"].ToString() + " " + dr["Lname"].ToString();
                item.Username = dr["Username"].ToString();
                item.Fname = dr["Fname"].ToString();
                item.Lname = dr["Lname"].ToString();
                item.Email = dr["Email"].ToString();
                item.Gender = dr["Gender"].ToString();
                item.EmployeeID = dr["EmployeeID"].ToString();
                item.Position = dr["Position"].ToString();
                item.Corporatename = dr["Corporatename"].ToString();
                item.UserType = dr["UserType"].ToString();
                item.DateCreated = Convert.ToDateTime(dr["DateCreated"].ToString()).ToString("MM/dd/yyyy");
                item.CorporateID = dr["CorporateID"].ToString();
                item.PositionID = dr["PositionID"].ToString();
                item.status = dr["status"].ToString();
                item.FilePath = dr["FilePath"].ToString();
                item.isVIP = dr["isVIP"].ToString();
                result.Add(item);
            }

            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> UserInfoFilteredbyJWToken()
        {
            GlobalVariables gv = new GlobalVariables();

            var result = new List<UserVM>();
            DataTable table = db.SelectDb_SP("SP_UserInfo").Tables[0];

            foreach (DataRow dr in table.Rows)
            {
                var item = new UserVM();
                item.Id = int.Parse(dr["id"].ToString());
                item.Fullname = dr["Fname"].ToString() + " " + dr["Lname"].ToString();
                item.Username = dr["Username"].ToString();
                item.Fname = dr["Fname"].ToString();
                item.Lname = dr["Lname"].ToString();
                item.Email = dr["Email"].ToString();
                item.Gender = dr["Gender"].ToString();
                item.EmployeeID = dr["EmployeeID"].ToString();
                item.Position = dr["Position"].ToString();
                item.Corporatename = dr["Corporatename"].ToString();
                item.UserType = dr["UserType"].ToString();
                item.DateCreated = Convert.ToDateTime(dr["DateCreated"].ToString()).ToString("MM/dd/yyyy");
                item.CorporateID = dr["CorporateID"].ToString();
                item.PositionID = dr["PositionID"].ToString();
          

                result.Add(item);
            }

            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> PositionList()
        {
            string sql = $@"SELECT        tbl_StatusModel.Name AS Status, tbl_PositionModel.Id, tbl_PositionModel.Name AS PositionName, tbl_PositionModel.Description, tbl_PositionModel.DateCreated, tbl_PositionModel.PositionID
                            FROM            tbl_PositionModel INNER JOIN
                                                     tbl_StatusModel ON tbl_PositionModel.Status = tbl_StatusModel.Id
                            WHERE        (tbl_PositionModel.Status = 5)
                            ORDER BY tbl_PositionModel.Id DESC";
            var result = new List<PositionModel>();
            DataTable table = db.SelectDb(sql).Tables[0];
            foreach (DataRow dr in table.Rows)
            {
                var item = new PositionModel();
                item.Id = int.Parse(dr["Id"].ToString());
                item.PositionName = dr["PositionName"].ToString();
                item.PositionID = dr["PositionID"].ToString();
                item.Description = dr["Description"].ToString();
                item.DateCreated = Convert.ToDateTime(dr["DateCreated"].ToString()).ToString("yyyy-MM-dd");
                item.Status = dr["Status"].ToString();
                result.Add(item);

            }

            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> UserTypeList()
        {
            string sql = $@"SELECT    tbl_UserTypeModel.Id, tbl_UserTypeModel.UserType, tbl_UserTypeModel.Description, tbl_UserTypeModel.DateCreated, tbl_StatusModel.Name as Status
                    FROM            tbl_UserTypeModel INNER JOIN
                                             tbl_StatusModel ON tbl_UserTypeModel.Status = tbl_StatusModel.Id
                    WHERE        (tbl_UserTypeModel.Status = 5)";
            var result = new List<UserTypeVM>();
            DataTable table = db.SelectDb(sql).Tables[0];
            foreach (DataRow dr in table.Rows)
            {
                var item = new UserTypeVM();
                item.Id = dr["Id"].ToString();
                item.UserType = dr["UserType"].ToString();
                item.Description = dr["Description"].ToString();
                item.Status = dr["Status"].ToString();
                result.Add(item);

            }

                return Ok(result);
        }
        [HttpPost]
        public async  Task<IActionResult> SaveUserInfo(UsersModel data)
        {
            try
            {
                string result = "";
                GlobalVariables gv = new GlobalVariables();
                _global.Token = _global.GenerateToken(data.Username, _appSettings.Key.ToString());
                _global.Status = gv.EmployeeRegistration(data, _global.Token, _context);
            }

            catch (Exception ex)
            {
                string status = ex.GetBaseException().ToString();
            }
             return Content(_global.Status);
        }
        [HttpPost]
        public async Task<IActionResult> SendOTP(RegistrationOTPModel data)
        {
            try
            {
                var model = new RegistrationOTPModel()
                {
                    Email = data.Email,
                    OTP = data.OTP,
                    Status = 10,

                };
                _context.tbl_RegistrationOTPModel.Add(model);
                _context.SaveChanges();
                _global.Status = "OTP SENT.";
            }

            catch (Exception ex)
            {
                string status = ex.GetBaseException().ToString();
            }
            return Ok(_global.Status);
        }
        [HttpPost]
        public async Task<IActionResult> VerifyOTP(RegistrationOTPModel data)
        {
            var result = new Registerstats();
            try
            {
                string query = "";
          
                string sql = $@"select * from tbl_registrationOTPModel where OTP = '"+data.OTP+ "' and  email ='"+data.Email+"' AND Status in (9,10)";
                DataTable dt = db.SelectDb(sql).Tables[0];
                if(dt.Rows.Count > 0)
                {
                    query += $@"update tbl_RegistrationOTPModel set status = 11 where  email ='" + data.Email + "' and OTP = '" + data.OTP+"'";
                    //-- if email exist - update OTP column--<soon
                    query += $@"update  UsersModel set  Active=1  where Active = 10 and LOWER(Email) ='" + data.Email + "' ";
                    db.AUIDB_WithParam(query);

                    result.Status = "OTP Matched!";
                    return Ok(result);
                }
                else
                {
                   query = $@"update tbl_RegistrationOTPModel set status = 10 where  email ='" + data.Email + "'";
                    db.AUIDB_WithParam(query);
                    result.Status = "OTP UnMatched!";
                    return BadRequest(result);
                }
            }

            catch (Exception ex)
            {
                result.Status = "OTP UnMatched!";
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> ResendOTP(RegistrationOTPModel data)
        {
            var result = new OTP();
            try
            {
                string query = "";

                string sql = $@"select * from tbl_registrationOTPModel where email ='" + data.Email + "' AND Status=10";
                DataTable dt = db.SelectDb(sql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    query += $@"update tbl_RegistrationOTPModel set otp = '"+data.OTP+"' where  email ='" + data.Email + "' ";
                    db.AUIDB_WithParam(query);

                    result.otp = data.OTP;
                    return Ok(result);
                }
                else
                {
                    result.otp = "Error";
                    return BadRequest(result);
                }
            }

            catch (Exception ex)
            {
                result.otp = "Error";
                return BadRequest(result);
            }
            return Ok(result);
        }
  
        [HttpPost]
        public IActionResult FinalUserRegistration(UsersModel data)
        {
            
           

                string sql = $@"SELECT        UsersModel.Id, UsersModel.Username, UsersModel.Password, UsersModel.Fullname, UsersModel.Active, tbl_UserTypeModel.UserType, tbl_CorporateModel.CorporateName, tbl_PositionModel.Name, UsersModel.JWToken
                        FROM            UsersModel INNER JOIN
                                                 tbl_UserTypeModel ON UsersModel.Type = tbl_UserTypeModel.Id INNER JOIN
                                                 tbl_CorporateModel ON UsersModel.CorporateID = tbl_CorporateModel.Id INNER JOIN
                                                 tbl_PositionModel ON UsersModel.PositionID = tbl_PositionModel.Id
                        WHERE     (UsersModel.Active in(9,10, 2) and LOWER(Email) ='" + data.Email.ToLower()+"' and LOWER(Fname) ='"+data.Fname.ToLower()+"' and LOWER(Lname) ='"+data.Lname.ToLower()+"')";
                DataTable dt = db.SelectDb(sql).Tables[0];
                var result = new Registerstats();
                if (dt.Rows.Count > 0)
                 {
                    string EncryptPword = Cryptography.Encrypt(data.Password);

                    string query = $@"update  UsersModel set Username='"+data.Username+"',Password='"+EncryptPword+"', Fname='"+data.Fname+"',Lname='"+data.Lname+"',cno='"+data.Cno+"', Active=10 , Address ='"+data.Address+"' where  Id='" + dt.Rows[0]["Id"].ToString() +"' ";
                    db.AUIDB_WithParam(query);
                string message = "Welcome to Alfardan Oyster Privilege Application to confirm your registration here's your one time password " + data.OTP + ". Please do not share.";
                string username = "Carlo26378";
                string password = "d35HV7kqQ8Hsf24";
                string sid = "Oyster Club";
                string type = "N";

                var url = "https://api.smscountry.com/SMSCwebservice_bulk.aspx?User="+ username + "&passwd="+password+"&mobilenumber="+data.Cno+"&message="+message+"&sid="+sid+"&mtype="+type+"";
                //    string response = url;

                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //optional
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                Stream stream = response.GetResponseStream();
                ////gv.AudittrailLogIn("Successfully Registered", "User Registration Form",data.Id.ToString(),7);
                string OTPInsert = $@"insert into tbl_RegistrationOTPModel (email,OTP,status) values ('"+data.Email+"','"+data.OTP+"','10')";
                    db.AUIDB_WithParam(OTPInsert);
                    result.Status = "Waiting for Verification";

                    return Ok(result);
                    
                }
                else
                {
                //gv.AudittrailLogIn("Failed Registration", "User Registration Form", data.Id.ToString(), 8);

                result.Status = "Invalid Registration";
              
                return BadRequest(result);
            
                }
            

            return Ok(result);
        }     
        [HttpPost]
        public IActionResult FinalUserRegistration2(UsersModel data)
        {
            
           

                string sql = $@"SELECT        UsersModel.Id, UsersModel.Username, UsersModel.Password, UsersModel.Fullname, UsersModel.Active, tbl_UserTypeModel.UserType, tbl_CorporateModel.CorporateName, tbl_PositionModel.Name, UsersModel.JWToken
                        FROM            UsersModel INNER JOIN
                                                 tbl_UserTypeModel ON UsersModel.Type = tbl_UserTypeModel.Id INNER JOIN
                                                 tbl_CorporateModel ON UsersModel.CorporateID = tbl_CorporateModel.Id INNER JOIN
                                                 tbl_PositionModel ON UsersModel.PositionID = tbl_PositionModel.Id
                        WHERE     (UsersModel.Active in(9,10, 2) and LOWER(Email) ='" + data.Email.ToLower()+"' and LOWER(Fname) ='"+data.Fname.ToLower()+"' and LOWER(Lname) ='"+data.Lname.ToLower()+"')";
                DataTable dt = db.SelectDb(sql).Tables[0];
                var result = new Registerstats();
                if (dt.Rows.Count > 0)
                 {
                    string EncryptPword = Cryptography.Encrypt(data.Password);

                    string query = $@"update  UsersModel set Username='"+data.Username+"',Password='"+EncryptPword+"', Fname='"+data.Fname+"',Lname='"+data.Lname+"',cno='"+data.Cno+"', Active=10 , Address ='"+data.Address+"' where  Id='" + dt.Rows[0]["Id"].ToString() +"' ";
                    db.AUIDB_WithParam(query);
                int otp = 0;
                StringBuilder builder = new StringBuilder();
                Random rnd = new Random();
                for (int j = 0; j < 6; j++)
                {
                    otp = rnd.Next(10);
                    builder.Append(otp);//returns random integers < 10
                }
                string OTPInsert = $@"insert into tbl_RegistrationOTPModel (email,OTP,status) values ('"+data.Email+"','"+ builder + "','10')";
                    db.AUIDB_WithParam(OTPInsert);
                    result.Status = "Waiting for Verification";

                    return Ok(result);
                    
                }
                else
                {
                //gv.AudittrailLogIn("Failed Registration", "User Registration Form", data.Id.ToString(), 8);

                result.Status = "Invalid Registration";
              
                return BadRequest(result);
            
                }
            

            return Ok(result);
        }
        public class UpdatePassword
        {
            public string? EmployeeID { get; set; }
            public string? Fname { get; set; }
            public string? Lname { get; set; }
            public string  Address { get; set; }
            public string? Email { get; set; }
            public string  Cno { get; set; }

        }
        [HttpPost]
        public IActionResult UpdateUserInformation(UpdatePassword data)
        {


            string sql = $@"select * from usersmodel where EmployeeID='"+data.EmployeeID+"'";
            DataTable dt = db.SelectDb(sql).Tables[0];
            var result = new Registerstats();
            if (dt.Rows.Count > 0)
            {
                string query = $@"update  UsersModel set Fname='" + data.Fname + "',Lname='" + data.Lname + "'" +
                    ",cno='" + data.Cno + "', Address ='" + data.Address + "' , Email='"+data.Email+"' " +
                    "where  EmployeeID='"+data.EmployeeID+"'";
                db.AUIDB_WithParam(query);
                result.Status = "User Information Updated";

                return Ok(result);

            }
            else
            {
                result.Status = "Invalid User Information";

                return BadRequest(result);

            }


            return Ok(result);
        }
        public class ProfileImge
        {
            public string FilePath { get; set; }
            public string EmployeeID { get; set; }

        }
        [HttpPost]
        public IActionResult UpdateProfileImg(ProfileImge data)
        {


            string sql = $@"select * from usersmodel where EmployeeID='" + data.EmployeeID + "'";
            DataTable dt = db.SelectDb(sql).Tables[0];
            var uploadimage = "https://www.alfardanoysterprivilegeclub.com/assets/img/"+data.FilePath;
            var result = new Registerstats();
            if (dt.Rows.Count > 0)
            {
                string query = $@"update  UsersModel set FilePath='" + uploadimage + "' where  EmployeeID='" + data.EmployeeID + "'";
                db.AUIDB_WithParam(query);
                result.Status = "User Profile Updated";
                return Ok(result);

            }
            else
            {
                result.Status = "Error";

                return BadRequest(result);

            }


            return Ok(result);
        }
        public class Registerstats
        {
            public string Status { get; set; }

        }
        public class OTP
        {
            public string otp { get; set; }

        }
        [HttpPost]
        public async Task<IActionResult> Import(List<UsersModel> list)
        {
            string result = "";
            try
            {

                for (int i = 0; i < list.Count; i++)
                {
                    string sql = $@"select * from usersmodel where EmployeeID='" + list[i].EmployeeID + "' and Active in(1,2)";
                    DataTable dt = db.SelectDb(sql).Tables[0];
                    if (dt.Rows.Count == 0)
                    {
                        StringBuilder str_build = new StringBuilder();
                        Random random = new Random();
                        int length = 8;
                        char letter;

                        for (int x = 0; x < length; x++)
                        {
                            double flt = random.NextDouble();
                            int shift = Convert.ToInt32(Math.Floor(25 * flt));
                            letter = Convert.ToChar(shift + 2);
                            str_build.Append(letter);
                        }

                        var token = Cryptography.Encrypt(str_build.ToString());
                        string strtokenresult = token;
                        string[] charsToRemove = new string[] { "/", ",", ".", ";", "'", "=", "+" };
                        foreach (var c in charsToRemove)
                        {
                            strtokenresult = strtokenresult.Replace(c, string.Empty);
                        }
                        string filepath = "";
                        if (list[i].FilePath == null || list[i].FilePath == "")
                        {
                            filepath = "https://www.alfardanoysterprivilegeclub.com/assets/img/defaultavatar.png";
                        }
                        else
                        {
                            filepath = "https://www.alfardanoysterprivilegeclub.com/assets/img/" + list[i].FilePath;
                        }
                        string EncryptPword = Cryptography.Encrypt(list[i].Password);
                        var fullname = list[i].Fname + " " + list[i].Lname;
                        string query = $@"insert into UsersModel (Username,Password,Fullname,Fname,Lname,Email,Gender,CorporateID,PositionID,JWToken,FilePath,Active,Cno,isVIP,Address,Type,EmployeeID) values
                    ('" + list[i].Username + "','','" + fullname + "','" + list[i].Fname + "','" + list[i].Lname + "','" + list[i].Email + "','" + list[i].Gender + "','" + list[i].CorporateID + "','" + list[i].PositionID + "','" + string.Concat(strtokenresult.TakeLast(15)) + "','" + filepath + "','2','" + list[i].Cno + "','" + list[i].isVIP + "','N/A','" + list[i].Type + "','" + list[i].EmployeeID + "')";
                        db.AUIDB_WithParam(query);
                        gv.AudittrailLogIn("Successfully Import User Pre-Registration ", "User Registration Form", list[i].EmployeeID.ToString(), 7);
                        _global.Status = "Successfully Saved.";
                    }
                    else
                    {
                        gv.AudittrailLogIn("Duplicate Entry.", "User Registration Form", list[i].EmployeeID.ToString(), 8);
                        _global.Status = "Duplicate Entry.";
                    }

                }
                result = "Registered Successfully";


            }
            catch (Exception ex)
            {
                _global.Status = ex.GetBaseException().ToString();

            }

            return Content(_global.Status);
        }
        [HttpPost]
        public IActionResult SavePosition(PositionModel data)
        {


            string result = "";
            string query = "";
            try
            {

                if (data.PositionName.Length != 0 || data.Description.Length != 0)
                {
                
                    if (data.Id == 0)
                    {
                        string sql = $@"select * from tbl_PositionModel where Name='" + data.PositionName + "'";
                        DataTable dt = db.SelectDb(sql).Tables[0];
                        if (dt.Rows.Count == 0)
                        {
                            query += $@"insert into tbl_PositionModel (Name,Description,Status) values ('" +data.PositionName+"','"+data.Description+"','5')"  ;
                            db.AUIDB_WithParam(query);
                            result = "Inserted Successfully";
                            return Ok(result);

                        }
                        else
                        {
                            result = "Vendor Name already Exist";
                            return BadRequest(result);
                        }
                    }
                    else
                    {
                        query += $@"update  tbl_PositionModel set Name='" + data.PositionName + "' , Description='" + data.Description + "' , Status='5'  where  Id='" + data.Id + "' ";
                        db.AUIDB_WithParam(query);

                        result = "Updated Successfully";
                        return BadRequest(result);
                    }


                }
                else
                {
                    result = "Error in Registration";
                    return BadRequest(result);
                }
                return Ok(result);
            }

            catch (Exception ex)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        public class DeletePos
        {

            public int Id { get; set; }
        }
        [HttpPost]
        public IActionResult DeletePosition(DeletePos data)
        {

            string sql = $@"select * from tbl_PositionModel where id ='" + data.Id + "'";
            DataTable dt = db.SelectDb(sql).Tables[0];
            var result = new Registerstats();
            string imgfile = "";
            if (dt.Rows.Count != 0)
            {
                string sql1 = $@"select * from UsersModel where PositionID ='" + data.Id + "'";
                DataTable dt1 = db.SelectDb(sql1).Tables[0];
                if (dt1.Rows.Count == 0)
                {
                    string OTPInsert = $@"update tbl_PositionModel set Status = 6 where id ='" + data.Id + "'";
                    db.AUIDB_WithParam(OTPInsert);
                    result.Status = "Succesfully deleted";

                    return Ok(result);
                }
                else
                {
                    result.Status = "Position is Already in Used!";

                    return BadRequest(result);

                }
       

            }
            else
            {
                result.Status = "Error";

                return BadRequest(result);

            }


            return Ok(result);
        }
        public class StatusResult
        {
            public string Status { get; set; }

        }
        [HttpPost]
        public async Task<IActionResult> UpdateUserInfo(UsersModel data)
        {
            string result="";
            try
            {
    
                if(data.EmployeeID.Length != 0 || data.Fname.Length != 0 || data.Lname.Length != 0 || data.Email.Length != null || data.EmployeeID.Length != null ||
                    data.Email.Length != 0)
                {
           
                  
                        StringBuilder str_build = new StringBuilder();
                    Random random = new Random();
                    int length = 8;
                    char letter;

                    for (int i = 0; i < length; i++)
                    {
                        double flt = random.NextDouble();
                        int shift = Convert.ToInt32(Math.Floor(25 * flt));
                        letter = Convert.ToChar(shift + 2);
                        str_build.Append(letter);
                    }
             
                    var token = Cryptography.Encrypt(str_build.ToString());
                    string strtokenresult = token;
                    string[] charsToRemove = new string[] { "/", ",", ".", ";", "'", "=", "+" };
                    foreach (var c in charsToRemove)
                    {
                        strtokenresult = strtokenresult.Replace(c, string.Empty);
                    }
                    string query = "";
                    string fullname = data.Fname + " " + data.Lname;
                    string filepath = "";
                    if (data.FilePath == null)
                    {
                        filepath = "https://www.alfardanoysterprivilegeclub.com/assets/img/defaultavatar.png";
                    }
                    else
                    {
                        filepath = "https://www.alfardanoysterprivilegeclub.com/assets/img/" + data.FilePath;
                    }
 
                    if(data.Id == 0)
                    {
                        string sql = $@"select * from usersmodel where EmployeeID='" + data.EmployeeID + "'";
                        DataTable dt = db.SelectDb(sql).Tables[0];
                        if (dt.Rows.Count == 0)
                        {
                       
                            query += $@"insert into UsersModel (Username,Fullname,Fname,Lname,Email,Gender,CorporateID,PositionID,JWToken,FilePath,Active,Cno,isVIP,Address,Type,EmployeeID) values
                                     ('" + data.Username + "','" + fullname + "','" + data.Fname + "','" + data.Lname + "','" + data.Email + "','" + data.Gender + "','" + data.CorporateID + "','" + data.PositionID + "','" + string.Concat(strtokenresult.TakeLast(15)) + "','" + filepath + "','"+data.Active+"','" + data.Cno + "','" + data.isVIP + "','N/A','" + data.Type + "','" + data.EmployeeID + "')";
                            db.AUIDB_WithParam(query);

                            string getlastinserted = $@"select Top(1) * from UsersModel order by id desc";
                            DataTable dt2 = db.SelectDb(getlastinserted).Tables[0];
                            if (dt2.Rows.Count > 0)
                            {
                                string getid =  dt2.Rows[0]["Id"].ToString();

                                string sqlmembership = $@"SELECT     tbl_CorporateModel.MembershipID, tbl_MembershipModel.Name, tbl_MembershipModel.Id AS MembershipID, tbl_CorporateModel.CorporateName, tbl_CorporateModel.Id AS CorporateID, 
                                                  tbl_MembershipModel.DateEnded
                                                  FROM            tbl_CorporateModel INNER JOIN
                                                  tbl_MembershipModel ON tbl_CorporateModel.MembershipID = tbl_MembershipModel.Id
                                                WHERE        (tbl_CorporateModel.Id = '" + data.CorporateID + "')";
                                        DataTable dt3 = db.SelectDb(sqlmembership).Tables[0];
                                if (dt3.Rows.Count > 0)
                                {

                                    string sqlprivilege = $@"select * from tbl_MembershipPrivilegeModel where MembershipID = '"+ dt3.Rows[0]["MembershipID"].ToString() + "'";
                                    DataTable dt4 = db.SelectDb(sqlprivilege).Tables[0];
                                    if (dt3.Rows.Count > 0)
                                    {
                                        foreach (DataRow dr4 in dt4.Rows)
                                        {
                                            //item.Id = int.Parse(dr["id"].ToString())
                                            string insertuserprivilege = $@"insert into tbl_UserPrivilegeModel (PrivilegeId,UserID,Validity) values ('" + dr4["PrivilegeID"].ToString() + "','" + getid + "','" + dt3.Rows[0]["DateEnded"].ToString() + "')";
                                            db.AUIDB_WithParam(insertuserprivilege);
                                        }

                                        string insertusermembership = $@"insert into tbl_UserMembershipModel (UserID,MembershipID,Validity) values ('" + getid + "','" + dt3.Rows[0]["MembershipID"].ToString() + "','" + dt3.Rows[0]["DateEnded"].ToString() + "')";
                                        db.AUIDB_WithParam(insertusermembership);
                                        //dt.Rows[0]["MembershipName"].ToString();
                                    }
                                }
                                gv.AudittrailLogIn("Successfully", "Registered New User", data.EmployeeID, 7);
                                result = "Registered Successfully";
                            }
                        }
                            else
                            {
                                result = "User Information Already Used!";
                            }
                        }
                    else
                    {
                        query += $@"update  UsersModel set Fname='" + data.Fname + "',Lname='" + data.Lname + "' ,Username='" + data.Username + "'" +
                               ",cno='" + data.Cno + "' , Email='" + data.Email + "' , CorporateID='" + data.CorporateID + "' , PositionID='" + data.PositionID + "'" +
                               ", Type='" + data.Type + "'  , Gender='" + data.Gender + "', FilePath='" + filepath + "' , EmployeeID='"+data.EmployeeID+"' " +
                               "where  Id='" + data.Id + "' ";
                                    db.AUIDB_WithParam(query);

                        gv.AudittrailLogIn("Successfully", "Registered Updated User Information", data.EmployeeID, 7);

                        result = "Updated Successfully";
                    }
                
           
                }
                else
                {
                    result = "Error in Registration";
                }
                return Ok(result);
            }

            catch (Exception ex)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        public class ChangePW
        {

            public string Email { get; set; }
            public string Password { get; set; }

        }
        public class DeleteUser
        {

            public int Id { get; set; }
        }  
        public class DeleteUserbEMp
        {

            public string EmployeeID { get; set; }
        }
        public class UserID
        {

            public int Id { get; set; }
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePW data)
        {
            var result = new Registerstats();
            try
            {
                string sql = $@"select * from UsersModel where Email ='" + data.Email + "' AND Status=1";
                DataTable dt = db.SelectDb(sql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    string EncryptPword = Cryptography.Encrypt(data.Password);
                    string query = $@"update  UsersModel set Password='" + EncryptPword + "' where Active = 1 and Email ='" + data.Email + "'";
                    db.AUIDB_WithParam(query);
                    result.Status = "Password Successfuly Updated";
                    return Ok(result);
                }
                else
                {
                    result.Status = "Error!";
                    return BadRequest(result);

                }
            }

            catch (Exception ex)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUserStatus(UserID data)
        {

            string sql = $@"select * from usersmodel where Id='" + data.Id + "'";
            DataTable dt = db.SelectDb(sql).Tables[0];
            var result = new Registerstats();
            if (dt.Rows.Count > 0)
            {
                string query = $@"update  UsersModel set Active='9' where  Id='" + data.Id + "'";
                db.AUIDB_WithParam(query);
                result.Status = "Successfully Updated";
                return Ok(result);

            }
            else
            {
                result.Status = "Error";

                return BadRequest(result);

            }


            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUserInfo(DeleteUser data)
        {

            string sql = $@"select * from usersmodel where Id='" + data.Id + "'";
            DataTable dt = db.SelectDb(sql).Tables[0];
            var result = new Registerstats();
            if (dt.Rows.Count > 0)
            {
                string query = $@"update  UsersModel set Active='6' where  Id='"+ data.Id + "'";
                db.AUIDB_WithParam(query);
                result.Status = "Successfully Deleted";
                return Ok(result);

            }
            else
            {
                result.Status = "Error";

                return BadRequest(result);

            }


            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUserInfobyEmpId(DeleteUserbEMp data)
        {

            string sql = $@"select * from usersmodel where EmpoyeeID='" + data.EmployeeID + "'";
            DataTable dt = db.SelectDb(sql).Tables[0];
            var result = new Registerstats();
            if (dt.Rows.Count > 0)
            {
                string query = $@"update  UsersModel set Active='6' where  EmpoyeeID='" + data.EmployeeID + "'";
                db.AUIDB_WithParam(query);
                result.Status = "Successfully Deleted";
                return Ok(result);

            }
            else
            {
                result.Status = "Error";

                return BadRequest(result);

            }


            return Ok(result);
        }

        [HttpPost]
        public string UploadImage([FromForm] IFormFile file, [FromForm] string empid)
        {
            try
            {

                string sql = $@"select * from usersmodel where EmployeeID='" + empid + "'";
                DataTable dt = db.SelectDb(sql).Tables[0];
                var result = new Registerstats();
                if (dt.Rows.Count > 0)
                {
                    //var filePath = "C:\\Files\\";
                    var filePath = "C:\\inetpub\\AOPCAPP\\public\\assets\\img\\";
                    // getting file original name
                    string FileName = file.FileName;
                    string getextension = Path.GetExtension(FileName);
                    string MyUserDetailsIWantToAdd = dt.Rows[0]["EmployeeID"].ToString() + getextension;
                    string files = Path.Combine(filePath, FileName);

                    //var stream = new FileStream(files, FileMode.Create);
                

                    // getting full path inside wwwroot/images
                    //var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/", FileName);
                    var imagePath = Path.Combine(filePath, MyUserDetailsIWantToAdd);
                    //var imagePath = Path.Combine("C:\\inetpub\\AOPCAPP\\public\\assets\\img\\", uniqueFileName);

                    // copying file
                    //file.CopyTo(new FileStream(imagePath, FileMode.Create));
                    using (FileStream streams = new FileStream(Path.Combine(filePath, MyUserDetailsIWantToAdd), FileMode.Create))
                    {
                        file.CopyTo(streams);
                    }
                    string filepath = "";
                    if (file == null)
                    {
                        filepath = "https://www.alfardanoysterprivilegeclub.com/assets/img/defaultavatar.png";
                    }
                    else
                    {
                        filepath = "https://www.alfardanoysterprivilegeclub.com/assets/img/" + MyUserDetailsIWantToAdd;
                    }
                    string query = $@"update  UsersModel set FilePath='" + filepath + "' where  EmployeeID='" + empid + "'";
                    db.AUIDB_WithParam(query);
                    return "File Uploaded Successfully";
                }
                else
                {
                    return "Error!";
                }
   

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
