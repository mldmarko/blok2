using Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Database
    {
        public static InternModel InternModel = new InternModel(100, 1000, 10000, "alarms.txt");
    }
}
