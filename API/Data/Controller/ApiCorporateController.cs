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
using System.Text;
using AuthSystem.ViewModel;
using static AuthSystem.Data.Controller.ApiUserAcessController;
using static AuthSystem.Data.Controller.ApiRegisterController;

namespace AuthSystem.Data.Controller
{

    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize("ApiKey")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApiCorporateController : ControllerBase
    {
        GlobalVariables gv = new GlobalVariables();
        DbManager db = new DbManager();
        private readonly AppSettings _appSettings;
        private ApplicationDbContext _context;
        private ApiGlobalModel _global = new ApiGlobalModel();
        private readonly JwtAuthenticationManager jwtAuthenticationManager;


        public ApiCorporateController(IOptions<AppSettings> appSettings, ApplicationDbContext context, JwtAuthenticationManager jwtAuthenticationManager)
        {
   
            _context = context;
            _appSettings = appSettings.Value;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
   
        }
      
        [HttpGet]
        public async Task<IActionResult> CorporateList()
        {
            var list = (from a in _context.tbl_CorporatePrivilegesModel
                        join b in _context.tbl_MembershipModel
                        on a.MembershipID equals b.Id
                        join c in _context.tbl_CorporateModel
                        on a.CorporateID equals c.Id
                        join e in _context.tbl_UsersModel
                       on c.Id equals e.CorporateID
                        select new
                        {
                            Id = a.Id,
                            Membership = b.Name,
                            CorporateNmae = c.CorporateName,
                            Desc = a.Description,
                            Address = c.Address,
                            Cno = c.CNo,
                            Email = c.EmailAddress,
                            Size = a.Size,
                            Count = a.Count,
                            DateIssued = Convert.ToDateTime(a.DateIssued).ToString("MM/dd/yyyy hh:mm:ss"),
                            DateExpired = Convert.ToDateTime(a.DateExpired).ToString("MM/dd/yyyy hh:mm:ss"),
                            DateCreated = Convert.ToDateTime(a.DateCreated).ToString("MM/dd/yyyy hh:mm:ss"),
                            Fullname = e.Fullname

                        }
                        ).ToList();
            return Ok(list);
        }
        [HttpGet]
        public async Task<IActionResult> CompanyList()
        {
            string sql = $@"SELECT DISTINCT 
                         tbl_MembershipPrivilegeModel.Count AS UserCount, tbl_MembershipPrivilegeModel.VipCount, tbl_CorporateModel.CorporateName, tbl_CorporateModel.Address, tbl_CorporateModel.CNo, tbl_CorporateModel.EmailAddress, 
                         tbl_CorporateModel.CompanyID, tbl_MembershipModel.Name AS Tier, tbl_MembershipModel.Description, tbl_CorporateModel.MembershipID AS memid, tbl_StatusModel.Name AS Status, tbl_CorporateModel.Id, 
                         tbl_CorporateModel.DateCreated
FROM            tbl_MembershipPrivilegeModel LEFT OUTER JOIN
                         tbl_CorporateModel ON tbl_MembershipPrivilegeModel.MembershipID = tbl_CorporateModel.MembershipID LEFT OUTER JOIN
                         tbl_StatusModel ON tbl_CorporateModel.Status = tbl_StatusModel.Id LEFT OUTER JOIN
                         tbl_MembershipModel ON tbl_CorporateModel.MembershipID = tbl_MembershipModel.Id
WHERE        (tbl_CorporateModel.Status = 1)";
            DataTable dt = db.SelectDb(sql).Tables[0];
            var result = new List<CorporateVM>();
            foreach(DataRow dr in dt.Rows)
            {
                var item = new CorporateVM();
                item.Id = int.Parse(dr["Id"].ToString()) ;
                item.DateCreated = Convert.ToDateTime(dr["DateCreated"].ToString()).ToString("MM/dd/yyyy") ;
                item.CNo = dr["CNo"].ToString();
                item.EmailAddress = dr["EmailAddress"].ToString();
                item.CorporateName = dr["CorporateName"].ToString();
                item.Address = dr["Address"].ToString();
                item.CompanyID = dr["CompanyID"].ToString();
                item.Tier = dr["Tier"].ToString();
                item.UserCount = dr["UserCount"].ToString();
                item.VIPCount = dr["VIPCount"].ToString();
                item.Description = dr["Description"].ToString();
                item.Status = dr["Status"].ToString();
                item.memid = dr["memid"].ToString();
                result.Add(item);

            }
            return Ok(result);
        } 
        public class membershiptier
        {

            public string Id { get; set; }
            public string MembershipName { get; set; }
        } 
        public class corporateid
        {

            public string Id { get; set; }
        }
        public class CorporateStatus
        {

            public string status { get; set; }
        }
   
        public class membershipprivilegedata
        {
            public int MembershipID { get; set; }
            public int Count { get; set; }
            public int VipCount { get; set; }
        }
        [HttpPost]
        public async Task<IActionResult> MembershipCorporate(corporateid data)
        {
            var result = new membershiptier();
            var res = new CorporateStatus();
            try
            {

                string sql = $@"SELECT  tbl_CorporateModel.Address, tbl_CorporateModel.CorporateName, tbl_CorporateModel.CNo, tbl_CorporateModel.EmailAddress, tbl_CorporateModel.DateCreated, tbl_CorporateModel.Status, 
                         tbl_CorporateModel.CompanyID, tbl_MembershipModel.Name as MembershipName, tbl_MembershipModel.Id as memid
                         FROM            tbl_CorporateModel INNER JOIN
                         tbl_MembershipModel ON tbl_CorporateModel.MembershipID = tbl_MembershipModel.Id where      (tbl_CorporateModel.Id = '" +data.Id+"')";
                DataTable dt = db.SelectDb(sql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    result.Id = dt.Rows[0]["memid"].ToString() ;
                    result.MembershipName = dt.Rows[0]["MembershipName"].ToString();
                }
                    

                return Ok(result);
            }

            catch (Exception ex)
            {
                return BadRequest("ERROR");
            }
            return Ok(result);
        }
        [HttpPost]
        public async  Task<IActionResult> SaveCorporate(CorporateVM data)
        {
            try
            {
                string result = "";
                GlobalVariables gv = new GlobalVariables();
                _global.Status = gv.CorporateRegister(data,  _context);
            }

            catch (Exception ex)
            {
                string status = ex.GetBaseException().ToString();
            }
             return Content(_global.Status);
        }
    
        [HttpPost]
        public async Task<IActionResult> UpdateCorporate(CorporateModel data)
        {
            string result = "";
            try
            {
                string query = "";

                if (data.CorporateName.Length != 0 || data.Description.Length != 0 || data.CNo.Length != 0 || data.EmailAddress.Length != null )
                {
                    if (data.Id == 0)
                    {
                       
                            query += $@"insert into tbl_CorporateModel ( CorporateName, Address, CNo, EmailAddress, Status, MembershipID) values
                                ('" + data.CorporateName + "','" + data.Address + "','" + data.CNo + "','" + data.EmailAddress + "','1','" + data.MembershipID + "')";
                            db.AUIDB_WithParam(query);
                            result = "Registered Successfully";
                        
                    }
                    else
                    {

                        query += $@"update  tbl_CorporateModel set CorporateName='" + data.CorporateName + "',Address='" + data.Address + "' " +
                               ",CNo='" + data.CNo + "' , EmailAddress='" + data.EmailAddress + "' , MembershipID='" + data.MembershipID + "' where  Id='" + data.Id + "' ";
                        db.AUIDB_WithParam(query);
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
        [HttpPost]
        public async Task<IActionResult> SaveUserMembership(CorporateModel data)
        {
            string result = "";
            try
            {
                string query = "";

                if (data.CorporateName.Length != 0 || data.Description.Length != 0 || data.CNo.Length != 0 || data.EmailAddress.Length != null)
                {
                    if (data.Id == 0)
                    {

                        query += $@"insert into tbl_CorporateModel ( CorporateName, Address, CNo, EmailAddress, Status, MembershipID) values
                                ('" + data.CorporateName + "','" + data.Address + "','" + data.CNo + "','" + data.EmailAddress + "','1','" + data.MembershipID + "')";
                        db.AUIDB_WithParam(query);
                        result = "Registered Successfully";

                    }
                    else
                    {

                        query += $@"update  tbl_CorporateModel set CorporateName='" + data.CorporateName + "',Address='" + data.Address + "'" +
                               ",CNo='" + data.CNo + "' , EmailAddress='" + data.EmailAddress + "' , MembershipID='" + data.MembershipID + "' where  Id='" + data.Id + "' ";
                        db.AUIDB_WithParam(query);
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
        [HttpPost]
        public async Task<IActionResult> DeleteCorproate(DeleteUser data)
        {

            string sql = $@"select * from tbl_CorporateModel where Id='" + data.Id + "'";
            DataTable dt = db.SelectDb(sql).Tables[0];
            var result = new Registerstats();
            if (dt.Rows.Count > 0)
            {
                string sql1 = $@"select * from UsersModel where CorporateID ='" + data.Id + "'";
                DataTable dt1 = db.SelectDb(sql1).Tables[0];
                if (dt1.Rows.Count == 0)
                {
                    string query = $@"update  tbl_CorporateModel set Status='6' where  Id='" + data.Id + "'";
                    db.AUIDB_WithParam(query);
                    result.Status = "Successfully Deleted";
                    return Ok(result);
                }
                else
                {
                    result.Status = "Corporate is Already in Used!";

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
        [HttpPost]
        public async Task<IActionResult> ImportCorporate(List<CorporateModel> list)
        {
            string result = "";
            try
            {

                for (int i = 0; i < list.Count; i++)
                {
                    string sql = $@"select * from tbl_CorporateModel where CorporateName='" + list[i].CorporateName + "' and Status in(1,2)";
                    DataTable dt = db.SelectDb(sql).Tables[0];
                    if (dt.Rows.Count == 0)
                    {
                       
                        string query = $@"insert into tbl_CorporateModel ( CorporateName, Address, CNo, EmailAddress, Status, MembershipID) values
                    ('" + list[i].CorporateName + "','" + list[i].Address + "','" + list[i].CNo + "','" + list[i].EmailAddress + "','1','" + list[i].MembershipID + "')";
                        db.AUIDB_WithParam(query);
     
                        _global.Status = "Successfully Saved.";
                    }
                    else
                    {
         
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
        public async Task<IActionResult> UpdateMembershipPrivilege(membershipprivilegedata data)
        {
            string result = "";
            try
            {

                    string sql = $@"select * from tbl_MembershipPrivilegeModel where MembershipID = '"+data.MembershipID + "'";
                    DataTable dt = db.SelectDb(sql).Tables[0];
                    if (dt.Rows.Count != 0)
                    {

                        string query = $@"update tbl_MembershipPrivilegeModel set VipCount='"+data.VipCount+"' , Count= '"+data.Count+"' where MembershipID = '"+data.MembershipID+"'";
                        db.AUIDB_WithParam(query);

                        return Ok("Success");
                    }
                    else
                    {

                    return BadRequest("There is no Privilege assigned in this Tier ");
                    }

                


            }
            catch (Exception ex)
            {
                _global.Status = ex.GetBaseException().ToString();

            }

            return Ok(db);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCorporate(int id)
        {
            try
            {
                var result = await _context.tbl_BusinessTypeModel.FindAsync(id);
                _context.tbl_BusinessTypeModel.Remove(result);
                await _context.SaveChangesAsync();
                _global.Status = "Successfully Deleted.";

            }
            catch (Exception ex)
            {
                _global.Status = ex.GetBaseException().ToString();
            }

            return Content(_global.Status);
        }
      
        [HttpPost]
        public async Task<IActionResult> FinalCorporateRegistration(CorporateModel data)
        {
            try
            {
                var model = new CorporateModel()
                {
                    CorporateName = data.CorporateName,
                    Address = data.Address,
                    CNo = data.CNo,
                    EmailAddress = data.EmailAddress,
                    Status =1,

                };

                if (data.Id != 0)
                {
                    //var exist_corporate = _context.tbl_CorporateModel.Where(a => a.CorporateName == data.CorporateName && a.Address == data.Address
                    //&& a.EmailAddress == data.EmailAddress && a.Status == 2).ToList().Count();
                    string sql = $@"SELECT        CorporateName, Address, CNo, EmailAddress, Status, CompanyID
                                FROM            tbl_CorporateModel
                                WHERE        (CorporateName = '"+data.CorporateName+"') AND (Address = '"+data.Address+"')  AND (EmailAddress = '"+data.EmailAddress+"') AND (Status = 2)";
                    DataTable dt = db.SelectDb(sql).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        model.Id = data.Id;
                        _context.tbl_CorporateModel.Update(model);
                        _context.SaveChanges();

                        return Ok("Successfully Registered");
                    }
                    else
                    {

                        return BadRequest("Invalid Registration");
                    }
                }
                else
                {
                    return BadRequest("Invalid Registration");
                }

            }

            catch (Exception ex)
            {
                string status = ex.GetBaseException().ToString();
            }

            return Ok("Successfully Registered");
        }
    }
}
