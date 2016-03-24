using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseAssignmentWPF
{
    public class DataBase
    {
        public short dbID { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }

        public DataBase(){}
        public DataBase(string name , short id , DateTime created)
        {
            this.Created = created;
            this.dbID = id;
            this.Name = name;
        }
    }
}
