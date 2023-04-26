namespace AuthSystem.Manager
{
    public class DBConn
    {
        private static string GetConnectionString()
        {
            // Return My.Settings.ConnString.ToString
            //  return "Data Source=192.168.0.84,36832;Initial Catalog=EQMS;User ID=randy;Password=otik"; //test
            //return "Data Source=192.168.0.222,36832;Initial Catalog=EQMS;User ID=randy;Password=otik"; //live
           return "Data Source=LERJUN-PC;Initial Catalog=AOPCDB;User ID=test;Password=1234"; //live
          //  return "Data Source=EC2AMAZ-AN808JE\\MSSQLSERVER01;Initial Catalog=AOPCDB;User ID=test;Password=1234"; //server
        }

        public static string ConnectionString
        {
            get
            {
                return GetConnectionString();
            }
        }
    }
}
