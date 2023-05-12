using AuthSystem.Models;
using AuthSystem.Manager;
using AuthSystem.Services;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Net;
using System.Web.Http;
using Newtonsoft.Json.Linq;

namespace AuthSystem.Data.Class
{

    public class GlobalVariables
    {
        DbManager dbm = new DbManager();

        DbManager db = new DbManager();
        private readonly AppSettings _appSettings;
        private ApiGlobalModel _global = new ApiGlobalModel();
        private readonly JwtAuthenticationManager jwtAuthenticationManager;


        #region Convert TABLE to LIST
        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
        #endregion
      
        #region Add Pre-Registration
        public string EmployeeRegistration(UsersModel data, string token, ApplicationDbContext dbContext)
        {
       
            bool tblcount = dbContext.tbl_UsersModel.ToList().Count() > 0;

            if (!tblcount)
            {
                _global.Status = AddNewUser(data, token, dbContext);
            }
            else
            {
                var exist_EmployeeID = dbContext.tbl_UsersModel.Where(a => a.EmployeeID == data.EmployeeID).ToList().Count();
                if (exist_EmployeeID == 0)
                {
                    _global.Status = AddNewUser(data, token, dbContext);

                }
                else
                {
                    _global.Status = "Employee ID is already used.";
                     //AudittrailLogIn(_global.Status, "User Registration Maintenance", data.Username, 8);
                }

            }


            return _global.Status;
        }
        public string AddNewUser(UsersModel data, string token, ApplicationDbContext dbContext)
        {
            string EncryptPword = Cryptography.Encrypt(data.Password);
            var model = new UsersModel()
            {
                Username = data.Username,
                Password = EncryptPword,
                Fullname = data.Fullname,
                Fname = data.Fname,
                Lname = data.Lname,
                Gender = data.Gender,
                Email = data.Email,
                CorporateID = data.CorporateID,
                PositionID = data.PositionID,
                JWToken = token,
                FilePath = data.FilePath,
                Type = 3,
                Active = 2,
                EmployeeID = data.EmployeeID,
                


            };
            dbContext.tbl_UsersModel.Add(model);
            dbContext.SaveChanges();
            _global.Status = "Successfully Saved.";
             //AudittrailLogIn(_global.Status, "User Registration Maintenance", data.Username, 7);
            return _global.Status;
        }
        #endregion

        #region UpdateUserInfo
        public string EmployeeUpdateInfo(UsersModel data, string token, ApplicationDbContext dbContext)
        {
            _global.Status = UpdateUser(data, token, dbContext);

            return _global.Status;
        }
        public string UpdateUser(UsersModel data, string token, ApplicationDbContext dbContext)
        {
            string EncryptPword = Cryptography.Encrypt(data.Password);
            var model = new UsersModel()
            {
                Username = data.Username,
                Password = EncryptPword,
                Fullname = data.Fullname,
                Fname=data.Fname,
                Lname=data.Lname,
                Gender = data.Gender,
                Email = data.Email,
                CorporateID = data.CorporateID,
                PositionID = data.PositionID,
                JWToken = token,
                FilePath = data.FilePath,
                Type = data.Type,
                Active = data.Active,
                EmployeeID = data.EmployeeID,
                Id = data.Id,
                isVIP= data.isVIP
            };
            if (data.Id == 0)
            {
                dbContext.tbl_UsersModel.Add(model);
                _global.Status = "Successfully Saved New User";
                 //AudittrailLogIn(_global.Status, "User Registration Maintenance", data.Username, 7);
            }
            else
            {
                model.Id = data.Id;
                dbContext.tbl_UsersModel.Update(model);
                _global.Status = "Successfully Updated User.";
                 //AudittrailLogIn(_global.Status, "User Registration Maintenance",data.Username, 7);
            }
            dbContext.SaveChanges();
            return _global.Status;
        }
        #endregion

        #region ValidateUserLogin
        public string ValidationUser(string username, string password, ApplicationDbContext dbContext)
        {
            bool compr_user = false;
            //var userinfo = dbContext.tbl_UsersModel.Where(a => EF.Functions.Collate(a.Username, "Latin1_General_CI_AI") == username).ToList();
            string sql = $@"SELECT        UsersModel.Id, UsersModel.Username, UsersModel.Password, UsersModel.Fullname, UsersModel.Active, tbl_UserTypeModel.UserType, tbl_CorporateModel.CorporateName, tbl_PositionModel.Name, UsersModel.JWToken
                        FROM            UsersModel INNER JOIN
                                                 tbl_UserTypeModel ON UsersModel.Type = tbl_UserTypeModel.Id INNER JOIN
                                                 tbl_CorporateModel ON UsersModel.CorporateID = tbl_CorporateModel.Id INNER JOIN
                                                 tbl_PositionModel ON UsersModel.PositionID = tbl_PositionModel.Id
                        WHERE       UsersModel.Username = '" + username + "' AND (UsersModel.Active = 1)";
            DataTable dt = db.SelectDb(sql).Tables[0];
            if (username.Length != 0 || password.Length != 0)
            {
                if (dt.Rows.Count != 0)
                {
                    compr_user = String.Equals(dt.Rows[0]["username"].ToString().Trim(), username, StringComparison.Ordinal);
                }

                if (compr_user)
                {
                    string pass = Cryptography.Decrypt(dt.Rows[0]["password"].ToString().Trim());
                    if ((pass).Equals(password))
                    {
                        _global.Status = "Success";
                         //AudittrailLogIn(_global.Status, "User Registration Maintenance", dt.Rows[0]["username"].ToString(), 7);
                    }
                    else
                    {
                     
                    }

                }
                else
                {
                    _global.Status = "Username does not exist";
                     //AudittrailLogIn(_global.Status, "User Registration Maintenance", dt.Rows[0]["username"].ToString(), 7);
                }
            }
            else
            {
                _global.Status = "Invalid Log In";
                 //AudittrailLogIn(_global.Status, "User Registration Maintenance", dt.Rows[0]["username"].ToString(), 8);
            }

            return _global.Status;

        }
        #endregion

        #region Add new Membership
        public string MemberShipRegistration(MembershipModel data, ApplicationDbContext dbContext)
        {

            bool tblcount = dbContext.tbl_MembershipModel.ToList().Count() > 0;
            if (!tblcount)
            {
                _global.Status = AddnewMembership(data, dbContext);
            }
            else
            {
                var exist_membership = dbContext.tbl_MembershipModel.Where(a => a.Name == data.Name).ToList().Count();
                if (exist_membership == 0)
                {
                    _global.Status = AddnewMembership(data, dbContext);

                }
                else
                {
                    _global.Status = "Membership Name already exist.";
                     //AudittrailLogIn(_global.Status, "Membership Maintenance", data.Name, 8);
                }

            }


            return _global.Status;
        }
        public string AddnewMembership(MembershipModel data, ApplicationDbContext dbContext)
        {

            string? datecreated = Convert.ToDateTime(DateTime.Now).ToString("MM/dd/yyyy hh:mm:ss tt");
            var model = new MembershipModel()
            {
                Name = data.Name,
                Description = data.Description,
                DateCreated = DateTime.Parse(datecreated),
                Status = 1,
            };
            dbContext.tbl_MembershipModel.Add(model);
            dbContext.SaveChanges();
            _global.Status = "Successfully Saved New Membership.";
             //AudittrailLogIn(_global.Status, "Membership Maintenance", data.Name, 7);
            return _global.Status;
        }
        #endregion

        #region UpdateMembershipInfo
        public string MembershipUpdateInfo(MembershipModel data, ApplicationDbContext dbContext)
        {
            _global.Status = UpdateMembership(data, dbContext);

            return _global.Status;
        }
        public string UpdateMembership(MembershipModel data, ApplicationDbContext dbContext)
        {
            string datecreated = Convert.ToDateTime(data.DateCreated).ToString("MM/dd/yyyy hh:mm:ss tt");
            var model = new MembershipModel()
            {
                Id = data.Id,
                Name = data.Name,
                Description = data.Description,
                DateCreated = DateTime.Parse(datecreated),
                Status = data.Status,
            };

            if (data.Id == 0)
            {
                dbContext.tbl_MembershipModel.Add(model);
                _global.Status = "Successfully Saved New Membership";

            }
            else
            {
                model.Id = data.Id;
                dbContext.tbl_MembershipModel.Update(model);
                _global.Status = "Successfully Updated.";
            }
            dbContext.SaveChanges();
             //AudittrailLogIn(_global.Status, "Membership Maintenance", data.Name, 7);
            return _global.Status;
        }
        #endregion

        #region Add new BusinessLocation
        public string BusinessLocationRegister(BusinessLocation data, ApplicationDbContext dbContext)
        {

            bool tblcount = dbContext.tbl_MembershipModel.ToList().Count() > 0;
            if (!tblcount)
            {
                _global.Status = AddnewBusinessLocatio(data, dbContext);
            }
            else
            {
                var exist_businessloc = dbContext.tbl_BusinessLocationModel.Where(a => a.Country.ToLower() == data.Country.ToLower()).ToList().Count();
                if (exist_businessloc == 0)
                {
                    _global.Status = AddnewBusinessLocatio(data, dbContext);

                }
                else
                {
                    _global.Status = "Business Location already exist.";
                     //AudittrailLogIn(_global.Status, "Business Location Maintenance", data.Country,8);
                }

            }
            return _global.Status;
        }
        public string AddnewBusinessLocatio(BusinessLocation data, ApplicationDbContext dbContext)
        {

            var model = new BusinessLocation()
            {
              Country    =data.Country, 
              City       =data.City,
              PostalCode =data.PostalCode,
                Active = data.Active
            };
            dbContext.tbl_BusinessLocationModel.Add(model);
            dbContext.SaveChanges();
            _global.Status = "Successfully Saved.";
             //AudittrailLogIn(_global.Status, "Business Location Maintenance", data.Country, 7);
            return _global.Status;
        }
        #endregion


        #region UpdateBusinessLocation
        public string BusinessLocationUpdateInfo(BusinessLocation data, ApplicationDbContext dbContext)
        {
            _global.Status = UpdateBusinessLocation(data, dbContext);

            return _global.Status;
        }
        public string UpdateBusinessLocation(BusinessLocation data, ApplicationDbContext dbContext)
        {
            
            var model = new BusinessLocation()
            {
                Id = data.Id,
                Country= data.Country,
                City= data.City,
                PostalCode=data.PostalCode,
                Active = data.Active
            };
           
            if (data.Id == 0)
            {
                var exist_businessloc = dbContext.tbl_BusinessLocationModel.Where(a => a.Country.ToLower() == data.Country.ToLower()).ToList().Count();
                if (exist_businessloc == 0)
                {
                    dbContext.tbl_BusinessLocationModel.Add(model);
                    _global.Status = "Successfully Saved New Business Location.";

                }
                else
                {
                    _global.Status = "Business Location already exist.";
                    //AudittrailLogIn(_global.Status, "Business Location Maintenance", data.Country,8);
                }
       
                 //AudittrailLogIn(_global.Status, "Business Location Maintenance", data.Country, 7);
            }
            else
            {
                model.Id = data.Id;
                dbContext.tbl_BusinessLocationModel.Update(model);
                _global.Status = "Successfully Updated.";
                 //AudittrailLogIn(_global.Status, "Business Location Maintenance", data.Country, 7);
            }
            dbContext.SaveChanges();

            return _global.Status;
        }
        #endregion

        #region Add new Business
        public string BusinessRegister(BusinessModel data, ApplicationDbContext dbContext)
        {

            bool tblcount = dbContext.tbl_MembershipModel.ToList().Count() > 0;
            if (!tblcount)
            {
                _global.Status = AddnewBusiness(data, dbContext);
            }
            else
            {
                var exist_businessloc = dbContext.tbl_BusinessModel.Where(a => a.BusinessName.ToLower() == data.BusinessName.ToLower()).ToList().Count();
                if (exist_businessloc == 0)
                {
                    _global.Status = AddnewBusiness(data, dbContext);

                }
                else
                {
                    _global.Status = "Business already exist.";
                     //AudittrailLogIn(_global.Status, "Business Maintenance", data.BusinessName, 8);
                }

            }
            return _global.Status;
        }
        public string AddnewBusiness (BusinessModel data, ApplicationDbContext dbContext)
        {

            var model = new BusinessModel()
            {
                BusinessName = data.BusinessName,
                TypeId = data.TypeId,
                LocationID = data.LocationID,
                Description = data.Description,
                Address = data.Address,
                Cno = data.Cno,
                Email = data.Email,
                Url = data.Url,
                Services = data.Services,
                FeatureImg = data.FeatureImg,
                Gallery = data.Gallery,
                Active = data.Active,
                FilePath = data.FilePath,
                Map = data.Map,


            };
            dbContext.tbl_BusinessModel.Add(model);
            dbContext.SaveChanges();
            _global.Status = "Successfully Saved.";
             //AudittrailLogIn(_global.Status, "Business Maintenance", data.BusinessName, 7);
            return _global.Status;
        }
        #endregion

        #region UpdateBusiness
        public string BusinessUpdateInfo(BusinessModel data, ApplicationDbContext dbContext)
        {
            _global.Status = UpdateBusiness(data, dbContext);

            return _global.Status;
        }
        public string UpdateBusiness(BusinessModel data, ApplicationDbContext dbContext)
        {

            var model = new BusinessModel()
            {
               BusinessName= data.BusinessName,
               TypeId = data.TypeId,
               LocationID = data.LocationID,
               Description = data.Description,
               Address = data.Address,
               Cno = data.Cno,
               Email = data.Email,
               Url = data.Url,
               Services = data.Services,
               FeatureImg = data.FeatureImg,
               Gallery = data.Gallery,
               Active = data.Active,
               FilePath= data.FilePath,
               Map = data.Map
            };

            if (data.Id == 0)
            {
                dbContext.tbl_BusinessModel.Add(model);
                _global.Status = "Successfully Saved New Business .";
                 //AudittrailLogIn(_global.Status, "Business Maintenance", data.BusinessName, 7);
            }
            else
            {
                model.Id = data.Id;
                dbContext.tbl_BusinessModel.Update(model);
                _global.Status = "Successfully Updated.";
                 //AudittrailLogIn(_global.Status, "Business Maintenance", data.BusinessName, 7);
            }
            dbContext.SaveChanges();

            return _global.Status;
        }
        #endregion

        #region Add new BusinessType
        public string BusinessTypeRegister(BusinessTypeModel data, ApplicationDbContext dbContext)
        {

            bool tblcount = dbContext.tbl_BusinessTypeModel.ToList().Count() > 0;
            if (!tblcount)
            {
                _global.Status = AddnewBusinessType(data, dbContext);
            }
            else
            {
                var exist_businessloc = dbContext.tbl_BusinessTypeModel.Where(a => a.BusinessTypeName.ToLower() == data.BusinessTypeName.ToLower()).ToList().Count();
                if (exist_businessloc == 0)
                {
                    _global.Status = AddnewBusinessType(data, dbContext);

                }
                else
                {
                    _global.Status = "Business Type already exist.";
                     //AudittrailLogIn(_global.Status, "BusinessType Maintenance", data.BusinessTypeName,8);
                }

            }
            return _global.Status;
        }
        public string AddnewBusinessType(BusinessTypeModel data, ApplicationDbContext dbContext)
        {

            var model = new BusinessTypeModel()
            {
               BusinessTypeName= data.BusinessTypeName,
               Description= data.Description,
                Status = data.Status


            };
            dbContext.tbl_BusinessTypeModel.Add(model);
            dbContext.SaveChanges();
            _global.Status = "Successfully Saved.";
             //AudittrailLogIn(_global.Status, "BusinessType Maintenance", data.BusinessTypeName, 7);
            return _global.Status;
        }
        #endregion

        #region UpdateBusinessType
        public string BusinessTypeUpdateInfo(BusinessTypeModel data, ApplicationDbContext dbContext)
        {
            _global.Status = UpdateBusinessType(data, dbContext);

            return _global.Status;
        }
        public string UpdateBusinessType(BusinessTypeModel data, ApplicationDbContext dbContext)
        {

            var model = new BusinessTypeModel()
            {
                BusinessTypeName = data.BusinessTypeName,
                Description = data.Description,
                Status = data.Status
            };

            if (data.Id == 0)
            {  
                dbContext.tbl_BusinessTypeModel.Add(model);
                _global.Status = "Successfully Saved New Business Type.";
                 //AudittrailLogIn(_global.Status, "BusinessType Maintenance", data.BusinessTypeName, 7);
            }
            else
            {
                model.Id = data.Id;
                dbContext.tbl_BusinessTypeModel.Update(model);
                _global.Status = "Successfully Updated.";
                 //AudittrailLogIn(_global.Status, "BusinessType Maintenance", data.BusinessTypeName, 7);
            }
            dbContext.SaveChanges();

            return _global.Status;
        }
        #endregion

        #region Add new Corporate
        public string CorporateRegister(CorporateVM data, ApplicationDbContext dbContext)
        {

            bool tblcount = dbContext.tbl_CorporateModel.ToList().Count() > 0;
            if (!tblcount)
            {
                _global.Status = AddnewCorporate(data, dbContext);
            }
            else
            {
                var exist_businessloc = dbContext.tbl_CorporateModel.Where(a => a.CorporateName.ToLower() == data.CorporateName.ToLower()).ToList().Count();
                if (exist_businessloc == 0)
                {
                    _global.Status = AddnewCorporate(data, dbContext);

                }
                else
                {
                    _global.Status = "Business Type already exist.";
                     //AudittrailLogIn(_global.Status, "Corporate Maintenance", data.CorporateName, 8);
                }

            }
            return _global.Status;
        }
        public string AddnewCorporate(CorporateVM data, ApplicationDbContext dbContext)
        {

            var model = new CorporateModel()
            {
             CorporateName= data.CorporateName,
             Address= data.Address,
             CNo= data.CNo,
             EmailAddress= data.EmailAddress,
             Status= 2,


            };
            dbContext.tbl_CorporateModel.Add(model);
            dbContext.SaveChanges();
            _global.Status = "Successfully Saved.";
             //AudittrailLogIn(_global.Status, "Corporate Maintenance", data.CorporateName, 7);
            return _global.Status;
        }
        #endregion

        #region UpdateCoporate
        public string CorporateUpdateInfo(CorporateModel data, ApplicationDbContext dbContext)
        {
            _global.Status = UpdateCorporatee(data, dbContext);

            return _global.Status;
        }
        public string UpdateCorporatee(CorporateModel data, ApplicationDbContext dbContext)
        {

            var model = new CorporateModel()
            {
                CorporateName = data.CorporateName,
                Address = data.Address,
                CNo = data.CNo,
                EmailAddress = data.EmailAddress,
                Status = data.Status,

            };

            if (data.Id == 0)
            {
                var exist_corporate = dbContext.tbl_CorporateModel.Where(a => a.CorporateName == data.CorporateName).ToList().Count();
                if (exist_corporate != 0)
                {
                    _global.Status = "Duplicate Entry.";
                    //AudittrailLogIn(_global.Status, "Corporate Maintenance", data.CorporateName, 8);
                }
                else

                {
                    dbContext.tbl_CorporateModel.Add(model);
                    _global.Status = "Successfully Saved New Corporate.";
                    //AudittrailLogIn(_global.Status,"Corporate Maintenance", data.CorporateName, 7);

                }
            }
            else
            {
                model.Id = data.Id;
                dbContext.tbl_CorporateModel.Update(model);
                _global.Status = "Successfully Updated.";
                //AudittrailLogIn(_global.Status, "Corporate Maintenance", data.CorporateName, 7);
            }
            dbContext.SaveChanges();

            return _global.Status;
        }
        #endregion
        #region AuditTrail
        public void AudittrailLogIn(string module,string stats,string id ,int statsid)
        {
            string query = $@"insert into tbl_audittrailModel (Actions,Module,UserId,status) values ('LogIn "+ stats +" at "+DateTime.Now.ToString("hh:mm:ss")+"','"+module+"','1','"+ statsid + "')";
            db.AUIDB_WithParam(query);
        }

            #endregion
        }
}
