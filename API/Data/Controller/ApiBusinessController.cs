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
using static AuthSystem.Data.Controller.ApiUserAcessController;
using Serilog;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Web.Http.Results;
using static AuthSystem.Data.Controller.ApiVendorController;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AuthSystem.Data.Controller
{

    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize("ApiKey")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApiBusinessController : ControllerBase
    {
        DbManager db = new DbManager();
        public readonly AppSettings _appSettings;
        public ApplicationDbContext _context;
        public ApiGlobalModel _global = new ApiGlobalModel();
        public readonly JwtAuthenticationManager jwtAuthenticationManager;


        public ApiBusinessController(IOptions<AppSettings> appSettings, ApplicationDbContext context, JwtAuthenticationManager jwtAuthenticationManager)
        {
   
            _context = context;
            _appSettings = appSettings.Value;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
   
        }

        [HttpPost]
        public async Task<IActionResult> GetBusinessFByBID(BusinessIDVM data)
        {
            var param = new IDataParameter[]
              {
               new SqlParameter("@BusinessID",data.BusinessID)
              };
            DataTable table = db.SelectDb_SP("SP_GetBFByBID", param).Tables[0];
            var item = new BusinessCardVM();
            foreach (DataRow dr in table.Rows)
            {
     
                item.Description = dr["Description"].ToString();
                item.FeatureImg = dr["FeatureImg"].ToString();
                item.Status = dr["Status"].ToString();
                item.HotelName = dr["HotelName"].ToString();
                item.Location = dr["Location"].ToString();
                item.BusinessID = dr["BusinessID"].ToString();
                item.Cno = dr["Cno"].ToString();
                item.Email = dr["Email"].ToString();
                item.Url = dr["Url"].ToString();
                item.Gallery = dr["Gallery"].ToString();
                item.FilePath = dr["FilePath"].ToString();
                item.Map = dr["Map"].ToString();
            

            }
            return Ok(item);

        }
        [HttpGet]
        public async Task<IActionResult> BusinessList()
        {
            string sql = $@"SELECT        tbl_BusinessModel.Map, tbl_BusinessModel.FilePath, tbl_BusinessModel.BusinessID, tbl_BusinessModel.DateCreated, tbl_BusinessModel.Gallery, tbl_BusinessModel.FeatureImg, tbl_BusinessModel.Services, 
                         tbl_BusinessModel.Url, tbl_BusinessModel.Email, tbl_BusinessModel.Cno, tbl_BusinessModel.Address, tbl_BusinessModel.Description, tbl_BusinessModel.BusinessName, tbl_BusinessModel.Id, 
                         tbl_BusinessTypeModel.BusinessTypeName, tbl_BusinessLocationModel.Country, tbl_BusinessLocationModel.City, tbl_BusinessLocationModel.PostalCode, tbl_StatusModel.Name AS Status, 
                         tbl_BusinessLocationModel.Id AS blocid, tbl_BusinessTypeModel.Id AS btypeid
                        FROM            tbl_BusinessModel INNER JOIN
                                                 tbl_BusinessTypeModel ON tbl_BusinessModel.TypeId = tbl_BusinessTypeModel.Id LEFT OUTER JOIN
                                                 tbl_BusinessLocationModel ON tbl_BusinessModel.LocationId = tbl_BusinessLocationModel.Id LEFT OUTER JOIN
                                                 tbl_StatusModel ON tbl_BusinessModel.Active = tbl_StatusModel.Id
                        WHERE        (tbl_BusinessModel.Active = 5)
                        ORDER BY tbl_BusinessModel.Id DESC";
            var result = new List<BusinessModelVM>();
            DataTable table = db.SelectDb(sql).Tables[0];
            foreach (DataRow dr in table.Rows)
            {
                var item = new BusinessModelVM();
                item.Id = dr["Id"].ToString();
                item.Map = dr["Map"].ToString();
                item.FilePath = dr["FilePath"].ToString();
                item.BusinessID = dr["BusinessID"].ToString();
                item.DateCreated =Convert.ToDateTime( dr["DateCreated"].ToString()).ToString("MM/dd/yyyy");
                item.Gallery = dr["Gallery"].ToString();
                item.FeatureImg = dr["FeatureImg"].ToString();
                item.Services = dr["Services"].ToString();
                item.Url = dr["Url"].ToString();
                item.Email = dr["Email"].ToString();
                item.Cno = dr["Cno"].ToString();
                item.Address = dr["Address"].ToString();
                item.Description = dr["Description"].ToString();
                item.BusinessName = dr["BusinessName"].ToString();
                item.BusinessTypeName = dr["BusinessTypeName"].ToString();
                item.Country = dr["Country"].ToString();
                item.City = dr["City"].ToString();
                item.PostalCode = dr["PostalCode"].ToString();
                item.Status = dr["Status"].ToString();
                item.blocid = dr["blocid"].ToString();
                item.btypeid = dr["btypeid"].ToString();
                result.Add(item);
            }

            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> BusinessCardList()
        {
            DataTable table = db.SelectDb_SP("SP_BusinessHotelList").Tables[0];
            var result = new List<BusinessCardVM>();
            foreach (DataRow dr in table.Rows)
            {
                var item = new BusinessCardVM();
                item.Description = dr["Description"].ToString();
                item.FeatureImg = dr["FeatureImg"].ToString();
                item.Status = dr["Status"].ToString();
                item.HotelName = dr["HotelName"].ToString();
                item.BusinessID = dr["BusinessID"].ToString();
                item.Location = dr["Location"].ToString();

                result.Add(item);
            }

            return Ok(result);
        }
       
        [HttpPost]
        public async  Task<IActionResult> SaveBusiness(BusinessModel data)
        {
        //    try
        //    {
        //        string result = "";
        //        GlobalVariables gv = new GlobalVariables();
        //        _global.Status = gv.BusinessRegister(data,  _context);
        //    }

        //    catch (Exception ex)
        //    {
        //        string status = ex.GetBaseException().ToString();
        //    }
        //     return Content(_global.Status);

              string result = "";
        string query = "";
            try
            {

                if (data.BusinessName.Length != 0 || data.Description.Length != 0 )
                {
                    string sql = "";
                    string res_image = "";
                
                    string FeaturedImage = "";
                    var image_ = (dynamic)null;
                    if (data.Id != 0)
                    {
                        sql += $@"select Top(1) BusinessID from tbl_BusinessModel where Active =5 and id='" + data.Id + "' order by id desc  ";
                        DataTable table = db.SelectDb(sql).Tables[0];
                        string str = table.Rows[0]["BusinessID"].ToString();
                        res_image = str;
                    }
                    else
                    {
                        sql += $@"select Top(1) BusinessID from tbl_BusinessModel where Active =5   order by id desc   ";
                        DataTable table = db.SelectDb(sql).Tables[0];
                        string str = table.Rows[0]["BusinessID"].ToString();
                        image_ = int.Parse(str.Replace("Hotel-", "")) + 1;
                        res_image = "Hotel-0" + image_;
                    }

          
                    if (data.FeatureImg == null)
                    {
                        FeaturedImage = "https://www.alfardanoysterprivilegeclub.com/assets/img/defaultavatar.png";
                    }
                    else
                    {
                        FeaturedImage = "https://www.alfardanoysterprivilegeclub.com/assets/img/" + res_image +".jpg";
                    }
                  

                    if (data.Id == 0)
                    {
                      sql = $@"select * from tbl_BusinessModel where BusinessName='" + data.BusinessName + "'";
                        DataTable dt = db.SelectDb(sql).Tables[0];
                        if (dt.Rows.Count == 0)
                        {
                            query += $@"insert into tbl_BusinessModel (BusinessName,TypeId,LocationId,Description,Address,Cno,Email,Url,Services,FeatureImg,Gallery,Active,FilePath,Map) values ('"+data.BusinessName
                                +"','"+data.TypeId+"','"+data.LocationID+"','"+data.Description+"','"+data.Address+"','"+data.Cno+"','"+data.Email+"','"+data.Url+"','"+data.Services
                                +"','"+ FeaturedImage + "','',5,'','"+data.Map+"')";
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
                        query += $@"update  tbl_BusinessModel set BusinessName ='"+data.BusinessName+"', TypeId ='"+data.TypeId+"', LocationId ='"+data.LocationID+"', Description ='"+data.Description
                            +"', Address ='"+data.Address+"' , Cno ='"+data.Cno+"', Email ='"+data.Email+"', Url ='"+data.Url+"' , Services ='"+data.Services+"', FeatureImg ='"
                            + FeaturedImage + "', Gallery ='' , Active ='5' ,FilePath ='' , Map ='"+data.Map+"'  where  Id='" + data.Id + "' ";
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

        [HttpPost]
        public async Task<IActionResult> UpdateBusiness(BusinessModel data)
        {
            try
            {
                string result = "";
                GlobalVariables gv = new GlobalVariables();
                _global.Status = gv.BusinessUpdateInfo(data, _context);
            }

            catch (Exception ex)
            {
                string status = ex.GetBaseException().ToString();
            }
            return Content(_global.Status);
        }
        //[HttpDelete]
        //public async Task<IActionResult> DeleteBusiness(int id)
        //{
        //    try
        //    {
        //        var result = await _context.tbl_BusinessModel.FindAsync(id);
        //        _context.tbl_BusinessModel.Remove(result);
        //        await _context.SaveChangesAsync();
        //        _global.Status = "Successfully Deleted.";

        //    }
        //    catch (Exception ex)
        //    {
        //        _global.Status = ex.GetBaseException().ToString();
        //    }

        //    return Content(_global.Status);
        //}
        public class DeleteB
        {

            public int Id { get; set; }
        }
        public class Registerstats
        {
            public string Status { get; set; }

        }
        [HttpPost]
        public IActionResult DeleteBusiness(DeleteB data)
        {

            string sql = $@"select * from tbl_BusinessModel where id ='" + data.Id + "'";
            DataTable dt = db.SelectDb(sql).Tables[0];
            var result = new Registerstats();
            string imgfile = "";
            if (dt.Rows.Count != 0)
            {

                string sql1 = $@"select * from tbl_VendorModel where BusinessLocationID ='" + data.Id + "'";
                DataTable dt1 = db.SelectDb(sql1).Tables[0];
                if (dt1.Rows.Count == 0)
                {
                    string query = $@"update  tbl_BusinessModel set Active='6' where  Id='" + data.Id + "'";
                    db.AUIDB_WithParam(query);
                    result.Status = "Successfully Deleted";
                    return Ok(result);
                }
                else
                {
                    result.Status = "Business Type is Already in Used!";

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
    }
}
