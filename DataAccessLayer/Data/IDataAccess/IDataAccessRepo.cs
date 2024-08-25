using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data.IDataAccess
{
    public interface IDataAccessRepo
    {
        SqlConnection CreateConnectionForAdo();
        
    }
}
