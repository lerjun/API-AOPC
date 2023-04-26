using AuthSystem.Manager;
using AuthSystem.Models;
using AuthSystem.ViewModel;
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
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AuthSystem.Data.Controller
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize("ApiKey")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApiCorporatePrivilegeController : ControllerBase
    {
        DbManager db = new DbManager();
        private readonly AppSettings _appSettings;
        private ApplicationDbContext _context;
        private ApiGlobalModel _global = new ApiGlobalModel();
        private readonly JwtAuthenticationManager jwtAuthenticationManager;


        public ApiCorporatePrivilegeController(ApplicationDbContext context)
        {
   
            _context = context;

        }
  

        [HttpGet]
        public async Task<IActionResult> CorporatePrivilegeLsit()
        {

            var result = new List<CorporatePrivilegeVM>();
            DataTable table = db.SelectDb_SP("Corporate_SP").Tables[0];

            foreach (DataRow dr in table.Rows)
            {
                var item = new CorporatePrivilegeVM();
                item.Id = int.Parse(dr["id"].ToString());
                item.Fullname = dr["Fullname"].ToString();
                item.Businessname = dr["Businessname"].ToString();
                item.Vendorname = dr["Vendorname"].ToString();
                item.Corporatename = dr["Corporatename"].ToString();
                item.Privilegename = dr["Privilegename"].ToString() ;
                item.Country = dr["Country"].ToString();
                item.Businesstype = dr["Businesstypename"].ToString();
                item.NoOfVisit = int.Parse(dr["No_Of_visit"].ToString());
                result.Add(item);
            }
             
                return Ok(result);
        }

        //[HttpPost]
        //public async  Task<IActionResult> SaveNewMembership(MembershipModel data)
        //{
        //    try
        //    {
        //        string result = "";
        //        GlobalVariables gv = new GlobalVariables();
        //        _global.Status = gv.MemberShipRegistration(data,_context);
        //    }

        //    catch (Exception ex)
        //    {
        //        string status = ex.GetBaseException().ToString();
        //    }
        //     return Content(_global.Status);
        //}

        //[HttpPut]
        //public async Task<IActionResult> UpdateMembershipInfo(MembershipModel data)
        //{
        //    try
        //    {
        //        string result = "";
        //        GlobalVariables gv = new GlobalVariables();
        //        _global.Status = gv.MembershipUpdateInfo(data ,_context);
        //    }

        //    catch (Exception ex)
        //    {
        //        string status = ex.GetBaseException().ToString();
        //    }
        //    return Content(_global.Status);
        //}
        //[HttpDelete]
        //public async Task<IActionResult> DeleteMembershipInfo(int id)
        //{
        //    try
        //    {
        //        var result = await _context.tbl_MembershipModel.FindAsync(id);
        //        _context.tbl_MembershipModel.Remove(result);
        //        await _context.SaveChangesAsync();
        //        _global.Status = "Successfully Deleted.";

        //    }
        //    catch (Exception ex)
        //    {
        //        _global.Status = ex.GetBaseException().ToString();
        //    }

        //    return Content(_global.Status);
        //}
    }
}
