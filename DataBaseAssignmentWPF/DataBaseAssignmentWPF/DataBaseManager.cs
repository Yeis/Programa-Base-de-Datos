using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseAssignmentWPF
{
    public class DataBaseManager
    {
        public List<DataBase> GetDataBases()
        {
            List<DataBase> temp = new List<DataBase>();
            using (var connection =  new SqlConnection("Data Source=SQLEXPRESS;User ID=sa;Password=ec0t1j;"))
            {
                connection.Open();
                DataTable databases = connection.GetSchema("Databases");
                foreach (DataRow database in databases.Rows)
                {
                    String Name = database.Field<String>("database_name");
                    short dbID = database.Field<short>("dbid");
                    DateTime created = database.Field<DateTime>("create_date");
                    temp.Add(new DataBase(Name, dbID, created));
                }
                return temp;
            }
        }
    }
}
