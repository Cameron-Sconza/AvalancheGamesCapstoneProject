using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class RoleMapper : Mapper
    {
        int OffsetToRoleID;
        int OffsetToRoleName;
        public RoleMapper(System.Data.SqlClient.SqlDataReader reader)
        {
            OffsetToRoleID = reader.GetOrdinal("RoleID");
            Assert(0 == OffsetToRoleID, "The  RoleID is not 0 as expected");
            OffsetToRoleName = reader.GetOrdinal("RoleName");
            Assert(1 == OffsetToRoleName, "The RoleName is not 1 as expected");
        }
        public RoleDAL RoleFromReader(System.Data.SqlClient.SqlDataReader reader)
        {
            RoleDAL propsedReturnValue = new RoleDAL();
            propsedReturnValue.RoleID = GetInt32OrDefault(reader, OffsetToRoleID);
            propsedReturnValue.RoleName = GetStringOrDefault(reader, OffsetToRoleName);
            return propsedReturnValue;
        }
    }
}
