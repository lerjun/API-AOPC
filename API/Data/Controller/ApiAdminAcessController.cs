using AuthSystem.Manager;
using AuthSystem.Models;
using AuthSystem.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Net;

namespace AuthSystem.Data.Controller
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApiAdminAcessController : ControllerBase
    {
        DbManager db = new DbManager();
        private readonly AppSettings _appSettings;
        private ApplicationDbContext _context;
        private ApiGlobalModel _global = new ApiGlobalModel();
        private readonly JwtAuthenticationManager jwtAuthenticationManager;

        public ApiAdminAcessController(JwtAuthenticationManager jwtAuthenticationManager, ApplicationDbContext context)
        {
            this.jwtAuthenticationManager = jwtAuthenticationManager;
            _context = context;
        }
        public class Registerstats
        {
            public string Status { get; set; }

        }
        [HttpPost]
        public IActionResult FinalUserRegistration(UsersModel data)
        {



            string sql = $@"SELECT        UsersModel.Id, UsersModel.Username, UsersModel.Password, UsersModel.Fullname, UsersModel.Active, tbl_UserTypeModel.UserType, tbl_CorporateModel.CorporateName, tbl_PositionModel.Name, UsersModel.JWToken
                        FROM            UsersModel INNER JOIN
                                                 tbl_UserTypeModel ON UsersModel.Type = tbl_UserTypeModel.Id INNER JOIN
                                                 tbl_CorporateModel ON UsersModel.CorporateID = tbl_CorporateModel.Id INNER JOIN
                                                 tbl_PositionModel ON UsersModel.PositionID = tbl_PositionModel.Id
                        WHERE     (UsersModel.Active in(9,10, 2) and LOWER(Email) ='" + data.Email.ToLower() + "' and LOWER(Fname) ='" + data.Fname.ToLower() + "' and LOWER(Lname) ='" + data.Lname.ToLower() + "')";
            DataTable dt = db.SelectDb(sql).Tables[0];
            var result = new Registerstats();
            if (dt.Rows.Count > 0)
            {
                string EncryptPword = Cryptography.Encrypt(data.Password);

                string query = $@"update  UsersModel set Username='" + data.Username + "',Password='" + EncryptPword + "', Fname='" + data.Fname + "',Lname='" + data.Lname + "',cno='" + data.Cno + "', Active=10 , Address ='" + data.Address + "' where  Id='" + dt.Rows[0]["Id"].ToString() + "' ";
                db.AUIDB_WithParam(query);
                string message = "Welcome to Alfardan Oyster Privilege Application to confirm your registration here's your one time password " + data.OTP + ". Please do not share.";
                string username = "Carlo26378";
                string password = "d35HV7kqQ8Hsf24";
                string sid = "Oyster Club";
                string type = "N";

                var url = "https://api.smscountry.com/SMSCwebservice_bulk.aspx?User=" + username + "&passwd=" + password + "&mobilenumber=" + data.Cno + "&message=" + message + "&sid=" + sid + "&mtype=" + type + "";
                //    string response = url;

                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //optional
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                Stream stream = response.GetResponseStream();
                ////gv.AudittrailLogIn("Successfully Registered", "User Registration Form",data.Id.ToString(),7);
                string OTPInsert = $@"insert into tbl_RegistrationOTPModel (email,OTP,status) values ('" + data.Email + "','" + data.OTP + "','10')";
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

    }
}
