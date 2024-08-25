using DataAccessLayer.Data.IDataAccess;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data.ADO.Net
{
    public class ADODataAccess : IDataAccessRepo
    {
        private readonly IConfiguration _configuration;

        public ADODataAccess(IConfiguration configuration)
        {
           _configuration = configuration;
        }
        public  SqlConnection CreateConnectionForAdo()
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return connection;
        }
    }
}
