using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class UserMapper : Mapper
    {
        int OffsetToUserID; //expected to be 0
        int OffsetToFirstName; //expected to be 1
        int OffsetToLastName; //expected to be 2
        int OffsetToUserName; //expected to be 3
        int OffsetToEmail;
        int OffsetToPhoneNumber;
        int OffsetToSALT;
        int OffsetToHASH;
        int OffsetToDateOfBirth;
        int OffsetToRoleID;
        int OffsetToRoleName; //expected to be 10 

        public UserMapper (System.Data.SqlClient.SqlDataReader reader)
        {
            //checking fields to the sql data base to see if the columns line up
            OffsetToUserID = reader.GetOrdinal("UserID");
            //assert takes a bool cond as parameter, and will show this error message if cond is false 
            Assert(0 == OffsetToUserID, $"UserID is {OffsetToUserID} not 0 as expected");
            //will proceed with no interruption if cond is true
            OffsetToFirstName = reader.GetOrdinal("FirstName");
            Assert(1 == OffsetToFirstName, $"FirstName is {OffsetToFirstName} not 1 as expected");
            OffsetToLastName = reader.GetOrdinal("LastName");
            Assert(2 == OffsetToLastName, $"LastName is {OffsetToLastName} not 2 as expected");
            OffsetToUserName = reader.GetOrdinal("UserName");
            Assert(3 == OffsetToUserName, $"Username is {OffsetToUserName} not 3 as expected");
            OffsetToEmail = reader.GetOrdinal("Email");
            Assert(4 == OffsetToEmail, $"Email is {OffsetToEmail} not 4 as expected");
            OffsetToPhoneNumber = reader.GetOrdinal("PhoneNumber");
            Assert(5 == OffsetToPhoneNumber, $"PhoneNumbe is {OffsetToPhoneNumber} not 5 as expected");
            OffsetToSALT = reader.GetOrdinal("SALT");
            Assert(6 == OffsetToSALT, $"SALT is {OffsetToSALT} not 6 as expected");
            OffsetToHASH = reader.GetOrdinal("HASH");
            Assert(7 == OffsetToHASH, $"HASH is {OffsetToHASH} not 7 as expected");
            OffsetToDateOfBirth = reader.GetOrdinal("DateOfBirth");
            Assert(8 == OffsetToDateOfBirth, $"DateOfBirth is {OffsetToDateOfBirth} not 8 as expected");
            OffsetToRoleID = reader.GetOrdinal("RoleID");
            Assert(9 == OffsetToRoleID, $"RoleID is {OffsetToRoleID} not 9 as expected");
            OffsetToRoleName = reader.GetOrdinal("RoleName");
            Assert(10 == OffsetToRoleName, $"RoleName is {OffsetToRoleName} not 10 as expected");
        }
        public UserDAL UserFromReader(System.Data.SqlClient.SqlDataReader reader)
        {
            UserDAL proposedReturnValue = new UserDAL();
            //reader["UserID"]  is very slow and makes a lot of garbage
            //reader[0] makes a lot of garbage
            //reader.GetInt32(0) is fast, but hard codes the offset to 0
            //reader.GetInt32(OffsetToUserID) is best and allows verification
            //verifing user userdal from reader 
            proposedReturnValue.UserID = reader.GetInt32(OffsetToUserID);
            proposedReturnValue.FirstName = reader.GetString(OffsetToFirstName);
            proposedReturnValue.LastName = reader.GetString(OffsetToLastName);
            proposedReturnValue.UserName = reader.GetString(OffsetToUserName);
            proposedReturnValue.Email = reader.GetString(OffsetToEmail);
            proposedReturnValue.PhoneNumber = reader.GetInt32(OffsetToPhoneNumber);
            proposedReturnValue.SALT = reader.GetString(OffsetToSALT);
            proposedReturnValue.HASH = reader.GetString(OffsetToHASH);
            proposedReturnValue.DateOfBirth = reader.GetDateTime(OffsetToDateOfBirth);
            proposedReturnValue.RoleID = reader.GetInt16(OffsetToRoleID);
            proposedReturnValue.RoleName = reader.GetString(OffsetToRoleID);
            return proposedReturnValue;
        }
    }
}
