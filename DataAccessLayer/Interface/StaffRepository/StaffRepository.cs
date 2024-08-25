using DataAccessLayer.Data.ADO.Net;
using DataAccessLayer.Data.IDataAccess;
using DataAccessLayer.Entities;
using DataAccessLayer.Interface;
using DataAccessLayer.Interface.StaffRepository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.Interface.StaffRepository;

public class StaffRepository : IStaffRepository
{
    private readonly SqlConnection connection;
    public StaffRepository(IConfiguration configuration)
    {
        connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }
    public Staff? Find(int key)
    {
        throw new NotImplementedException();
    }
    public IEnumerable<Staff> GetAll()
    {
        List<Staff> staffs = new List<Staff>();
        using (var conn = connection)
        {
            SqlCommand command = conn.CreateCommand();
            command.CommandType=CommandType.Text;
            command.CommandText = "select ss.staff_id,ss.first_name,ss.last_name,ss.email,ISNULL(ss.[phone],'') AS phone," +
                "ss.active,ss.store_id,ISNULL(ss.[manager_id],0) AS manager_id,st.store_name," +
                "ISNULL(tblStaff.first_name,'') +' '+ ISNULL(tblStaff.last_name,'') AS manager_name from sales.staffs AS ss " +
                "LEFT JOIN SALES.stores st ON ss.store_id = st.store_id " +
                "LEFT JOIN sales.staffs tblStaff on ss.manager_id=tblStaff.staff_id";
            conn.Open();
            SqlDataReader reader = command.ExecuteReader();
            InjectData(staffs, reader);
        }
        return staffs;
    }
    private void InjectData(List<Staff> staffs, SqlDataReader reader)
    {
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                staffs.Add(new Staff
                {
                    StaffId = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    Email = reader.GetString(3),
                    Phone = reader.GetString(4),
                    Active = reader.GetByte(5),
                    StoreId = reader.GetInt32(6),
                    ManagerId = reader.GetInt32(7),
                    StoreName = reader.GetString(8),
                    ManagerName = reader.GetString(9)
                });
            }
        }
    }
    public IEnumerable<Staff> GetAll(string keyName,int key)
    {
        List<Staff> staffs = new List<Staff>();
        using (var conn = connection)
        {
            SqlCommand command = conn.CreateCommand();
            command.CommandType=CommandType.Text;
            command.CommandText = "select ss.staff_id,ss.first_name,ss.last_name,ss.email,ISNULL(ss.[phone],'') AS phone," +
                "ss.active,ss.store_id,ISNULL(ss.[manager_id],0) AS manager_id,st.store_name," +
                "ISNULL(tblStaff.first_name,'') +' '+ ISNULL(tblStaff.last_name,'') AS manager_name from sales.staffs AS ss " +
                "LEFT JOIN SALES.stores st ON ss.store_id = st.store_id " +
                "LEFT JOIN sales.staffs tblStaff on ss.manager_id=tblStaff.staff_id " +
                $"WHERE ss.{keyName} = {key}";
            conn.Open();
            SqlDataReader reader = command.ExecuteReader();
            InjectData(staffs, reader);
        }
        return staffs;
    }
    public IEnumerable<Staff> GetWithManagerId(int managerId)
    {
        throw new NotImplementedException();
    }
}
