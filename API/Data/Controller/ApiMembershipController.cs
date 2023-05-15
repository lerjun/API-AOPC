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

namespace AuthSystem.Data.Controller
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize("ApiKey")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApiMembershipController : ControllerBase
    {
        DbManager db = new DbManager();
        private readonly AppSettings _appSettings;
        private ApplicationDbContext _context;
        private ApiGlobalModel _global = new ApiGlobalModel();
        private readonly JwtAuthenticationManager jwtAuthenticationManager;


        public ApiMembershipController(ApplicationDbContext context)
        {
   
            _context = context;
   
        }
      
        [HttpGet]
        public async Task<IActionResult> MembershipList()
        {
            string sql = $@"SELECT        tbl_MembershipModel.Id, tbl_MembershipModel.Name AS MembershipName, tbl_MembershipModel.Description, tbl_MembershipModel.DateUsed AS DateStarted, tbl_MembershipModel.DateEnded, 
                         tbl_MembershipModel.DateCreated, tbl_MembershipModel.MembershipID, tbl_StatusModel.Name AS Status, tbl_MembershipModel.UserCount, tbl_MembershipModel.VIPCount
FROM            tbl_MembershipModel INNER JOIN
                         tbl_StatusModel ON tbl_MembershipModel.Status = tbl_StatusModel.Id
WHERE        (tbl_MembershipModel.Status = 5) order by id desc";
            var result = new List<MembershipVM>();
            DataTable table = db.SelectDb(sql).Tables[0];
            foreach (DataRow dr in table.Rows)
            {
                if (dr["MembershipName"].ToString() !="ALL TIER")
                {
                    var item = new MembershipVM();
                    item.Id = dr["Id"].ToString();
                    item.MembershipName = dr["MembershipName"].ToString();
                    item.Description = dr["Description"].ToString();
                    item.DateStarted = Convert.ToDateTime(dr["DateStarted"].ToString()).ToString("MM/dd/yyyy");
                    item.DateEnded = Convert.ToDateTime(dr["DateEnded"].ToString()).ToString("MM/dd/yyyy");
                    item.DateCreated = Convert.ToDateTime(dr["DateCreated"].ToString()).ToString("MM/dd/yyyy");
                    item.MembershipID = dr["MembershipID"].ToString();
                    item.Status = dr["Status"].ToString();
                    item.UserCount = int.Parse(dr["UserCount"].ToString());
                    item.VIPCount = int.Parse(dr["VIPCount"].ToString());

                    result.Add(item);
                }
                
            }
            return Ok(result);
        }

        [HttpPost]
        public async  Task<IActionResult> SaveNewMembership(MembershipModel data)
        {
            try
            {
                string result = "";
                GlobalVariables gv = new GlobalVariables();
                _global.Status = gv.MemberShipRegistration(data,_context);
            }

            catch (Exception ex)
            {
                string status = ex.GetBaseException().ToString();
            }
             return Content(_global.Status);
        }
        public class MembershipID
        {
            public int Id { get; set; }

        }
        [HttpPost]
        public async  Task<IActionResult> MembershipFilterbyID(MembershipID data)
        {
            var result = new List<MembershipModelVM>();
            try
            {
                string sql = $@"SELECT        Id, Name AS MembershipName, Description, MembershipID, UserCount, VIPCount, DateCreated
                                FROM            tbl_MembershipModel
                                WHERE        (Id = '"+data.Id+"')";

                DataTable table = db.SelectDb(sql).Tables[0];
              
                if (table.Rows.Count != 0)
                {
           
                    foreach (DataRow dr in table.Rows)
                    {
                        var item = new MembershipModelVM();
                        item.Id = int.Parse(dr["Id"].ToString());
                        item.MembershipName = dr["MembershipName"].ToString();
                        item.Description = dr["Description"].ToString();
                        item.MembershipID = dr["MembershipID"].ToString();
                        item.UserCount = dr["UserCount"].ToString();
                        item.VIPCount = dr["VIPCount"].ToString();
                        item.DateCreated = Convert.ToDateTime(dr["DateCreated"].ToString()).ToString("MM/dd/yyyy") ;
                        result.Add(item);

                    }
                }
            }

            catch (Exception ex)
            {
                string status = ex.GetBaseException().ToString();
            }
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMembershipInfo(MembershipModel data)
        {
            try
            {
                string result = "";
                GlobalVariables gv = new GlobalVariables();
                _global.Status = gv.MembershipUpdateInfo(data ,_context);
            }

            catch (Exception ex)
            {
                string status = ex.GetBaseException().ToString();
            }
            return Content(_global.Status);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteMembershipInfo(int id)
        {
            try
            {
                var result = await _context.tbl_MembershipModel.FindAsync(id);
                _context.tbl_MembershipModel.Remove(result);
                await _context.SaveChangesAsync();
                _global.Status = "Successfully Deleted.";

            }
            catch (Exception ex)
            {
                _global.Status = ex.GetBaseException().ToString();
            }

            return Content(_global.Status);
        }
        public class Registerstats
        {
            public string Status { get; set; }

        }
        [HttpPost]
        public IActionResult SaveMembershipTier(MembershipVM data)
        {


            string sql = "";
            sql = $@"select * from tbl_MembershipModel where id ='" + data.Id + "'";
            DataTable dt = db.SelectDb(sql).Tables[0];
            var result = new Registerstats();
            string imgfile = "";
            if (dt.Rows.Count == 0)
            {
                sql = $@"select * from tbl_MembershipModel where Name ='" + data.MembershipName + "'";
                DataTable dt2 = db.SelectDb(sql).Tables[0];
                if (dt2.Rows.Count == 0)
                {
                    string Insert = $@"insert into tbl_MembershipModel (Name,Description,DateUsed,DateEnded,UserCount,VIPCount,Status) values 
                                   ('" + data.MembershipName + "','" + data.Description + "','" + data.DateStarted + "','" + data.DateEnded + "','" + data.UserCount + "','" + data.VIPCount + "',5) ";
                    db.AUIDB_WithParam(Insert);
                    result.Status = "Successfully Added";

                    return Ok(result);
                }
                else
                {

                    result.Status = "Duplicate Entry";

                    return BadRequest(result);
                }
                   

            }
            else
            {

              
                string Update = $@"update tbl_MembershipModel set Name='" + data.MembershipName + "', Description='" + data.Description + "' , DateUsed='" + data.DateStarted + "', DateEnded='" + data.DateEnded +
                    "', UserCount='" + data.UserCount + "', VIPCount='" + data.VIPCount + "' where id='"+data.Id+"'";
                db.AUIDB_WithParam(Update);
                result.Status = "Successfully Updated";

                return Ok(result);
            }


            return Ok(result);
        }
        public class DeleteMem
        {

            public int Id { get; set; }
        }
        [HttpPost]
        public IActionResult DeleteMemship(DeleteMem data)
        {

            string sql = $@"select * from tbl_MembershipModel where id ='" + data.Id + "'";
            DataTable dt = db.SelectDb(sql).Tables[0];
            var result = new Registerstats();
            string imgfile = "";
            if (dt.Rows.Count != 0)
            {

                string OTPInsert = $@"update tbl_MembershipModel set Status = 6 where id ='" + data.Id + "'";
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
