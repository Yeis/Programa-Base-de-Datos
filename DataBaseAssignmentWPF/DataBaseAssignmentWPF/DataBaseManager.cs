using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.SqlServer.Management.Smo;

namespace DataBaseAssignmentWPF
{
    public class DataBaseManager
    {
        public List<Database> GetDataBases()
        {
            //List<DataBase> temp = new List<DataBase>();
            //using (var connection =  new SqlConnection("Data Source=(local); Integrated Security = true"))
            //{
            //    try
            //    {
            //        connection.Open();
            //        DataTable databases = connection.GetSchema("Databases");
            //        foreach (DataRow database in databases.Rows)
            //        {
            //            string Name = database.Field<string>("database_name");
            //            short dbID = database.Field<short>("dbid");
            //            DateTime created = database.Field<DateTime>("create_date");
            //            temp.Add(new DataBase(Name, dbID, created));
            //        }
            //    }
            //    catch (Exception e)
            //    {
            //        MessageBox.Show(e.Message);
            //    }

            //    return temp;
            //}


            List<Database> dbs = new List<Database>();

            Server myServer = new Server(@"(local)");
            myServer.ConnectionContext.LoginSecure = true;
            myServer.ConnectionContext.Connect();
            
            foreach (Database myDB in myServer.Databases)
            {
                dbs.Add(myDB);
            }

            if (myServer.ConnectionContext.IsOpen)
                myServer.ConnectionContext.Disconnect();
            
            return dbs;
        }

        public void BackupDataBase(Database db)
        {
            Backup buFull = new Backup();
            buFull.Action = BackupActionType.Database;
            buFull.Database = db.Name;
            buFull.Devices.AddDevice(@"C:\" + db.Name + ".bak", DeviceType.File);
            buFull.BackupSetName = db.Name + " Backup";
            buFull.BackupSetDescription = db.Name + " - Full Backup";

            Server myServer = new Server(@"(local)");
            myServer.ConnectionContext.LoginSecure = true;
            myServer.ConnectionContext.Connect();

            buFull.SqlBackup(myServer);

            if (myServer.ConnectionContext.IsOpen)
                myServer.ConnectionContext.Disconnect();

        }
    }
}
