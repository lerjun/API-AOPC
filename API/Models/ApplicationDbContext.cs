using Microsoft.EntityFrameworkCore;
namespace AuthSystem.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        { }
        public DbSet<UsersModel> tbl_UsersModel { get; set; }
        public DbSet<UsersModel> tbl_UsersModel2 { get; set; }
        public DbSet<MembershipModel> tbl_MembershipModel { get; set; }
        public DbSet<CorporatePrivilegesModel> tbl_CorporatePrivilegesModel { get; set; }
        public DbSet<CorporateModel> tbl_CorporateModel { get; set; }
        public DbSet<StatusModel> tbl_StatusModel { get; set; }
        public DbSet<BusinessLocation> tbl_BusinessLocationModel { get; set; }
        public DbSet<AuditTrailModel> tbl_AuditTrailModel { get; set; }
        public DbSet<BusinessModel> tbl_BusinessModel { get; set; }
        public DbSet<BusinessTypeModel> tbl_BusinessTypeModel { get; set; }
        public DbSet<CorporatePrivilegeLogsModel> tbl_PrivilegeLogsModel { get; set; }
        public DbSet<PositionModel> tbl_PositionModel { get; set; }
        public DbSet<VendorModel> tbl_VendorModel { get; set; }
        public DbSet<PrivilegeModel> tbl_PrivilegeModel { get; set; }
        public DbSet<UserPrivilegeModel> tbl_UserPrivilegeModel { get; set; }
        public DbSet<UserMembershipModel> tbl_UserMembershipModel { get; set; }
        public DbSet<OfferingModel> tbl_OfferingModel { get; set; }
        public DbSet<RegistrationOTPModel> tbl_RegistrationOTPModel { get; set; }
        public DbSet<UserTypeModel> tbl_UserTypeModel { get; set; }
        public DbSet<qrAuditTrailModel> tbl_qrAuditTrailModel { get; set; }
        
        
        
        
    }
}
