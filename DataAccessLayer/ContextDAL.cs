﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Logging;

namespace DataAccessLayer
{
    //Using IDisposable bc this class of ContextDAL is "heavy"
    //it releases unmanaged resouces 
    public class ContextDAL : IDisposable
    {
        #region Context Stuff
        //this connects to SQL and opens the connection to the database and we are calling this connection _connection
        SqlConnection _connection;
        //creating a constructor 
        public ContextDAL()
        {
            //initializes a new instance of the sqlconnection _connection
            _connection = new SqlConnection();
        }

        public string ConnectionString
        {
            //
            //get = return the property value _connectionstring 
            get { return _connection.ConnectionString; }
            //set = assigning value to _connectionstring
            set { _connection.ConnectionString = value; }
        }
        //new method used to check if connected
        void EnsureConnected()
        {
            if (_connection.State == System.Data.ConnectionState.Open)
            {
                //there is nothing to do if I am connected
            }
            else if (_connection.State == System.Data.ConnectionState.Broken)
            {
                //if brokene, _connection will be closed and then opened
                _connection.Close();
                _connection.Open();
            }
            else if (_connection.State == System.Data.ConnectionState.Closed)
            {
                //if closed, will be opened
                _connection.Open();
            }
            else
            {
                //other states need no processing
            }
        }
        //Logging the excpetion(errors that occur during execution) being called ex
        bool Log(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            //Logging the exception called ex from the calss logger
            Logger.Log(ex);
            //when finished logging will return false to reset bool for next log check
            return false;
        }
        //calling the Dispose method that will release all resources used during this connection
        public void Dispose()
        {
            _connection.Dispose();
        }
        #endregion
        #region Simulate Exceptions for testing
        // this region is to create DAL layer exceptions on purpose in order to test them to see if the exceptions are properly caught and logged
        public int GenerateNotConnected()
        {
            int propsedReturnValue = -1;
            try
            {
                //by commenting out the EnsureConnected below, this method MAY throw an exeception IF it is the first call on ContextDAL
                //EnsureConnected();
                using (SqlCommand command = new SqlCommand("ObtainRoleCount", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    object answer = command.ExecuteScalar();
                    propsedReturnValue = (int)answer;
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return propsedReturnValue;
        }
        public int GenerateStoredProcedureNotFound()
        {
            int proposedReturnValue = -1;
            try
            {
                EnsureConnected();
                //the name of the stored procedure is incorrect, so it should throw and exception
                using (SqlCommand command = new SqlCommand("xxxx", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    object answer = command.ExecuteScalar();
                    proposedReturnValue = (int)answer;
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return proposedReturnValue;
        }
        public int GenerateParametersNotIncluded()
        {
            int proposedReturnValue = -1;
            try
            {
                EnsureConnected();
                //the parameter to the stored procedure is incorrect so it should thrown an exception
                using(SqlCommand command = new SqlCommand("FindRoleByRoleID", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    //the following line is where the parapeter name is incorrect
                    command.Parameters.AddWithValue("@xxxx", 1);
                    object answer = command.ExecuteReader();
                    proposedReturnValue = (int)answer;
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return proposedReturnValue;
        }
        #endregion

        #region Role Stuff
        //Method Called FindRole 
        public RoleDAL FindRoleByRoleID(int RoleID)
        {
            RoleDAL propsosedReturnValue = null;
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("FindRoleByRoleID", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RoleID", RoleID);
                    //need to configure the Text, Type and Parameters. 
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        RoleMapper mapper = new RoleMapper(reader);
                        int count = 0;
                        while (reader.Read())
                        {
                            propsosedReturnValue = mapper.RoleFromReader(reader);
                            count++;
                        }
                        if (count > 1)
                        {
                            throw new Exception($"Found more than 1 Role with key {RoleID}");
                        }
                    }
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return propsosedReturnValue;
        }
        public List<RoleDAL> GetRoles(int skip, int take)
        {
            List<RoleDAL> proposedReturnValue = new List<RoleDAL>();
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("GetRoles", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@skip", skip);
                    command.Parameters.AddWithValue("@take", take);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        RoleMapper mapper = new RoleMapper(reader);
                        while (reader.Read())
                        {
                            RoleDAL read = mapper.RoleFromReader(reader);
                            proposedReturnValue.Add(read);
                        }
                    }

                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return proposedReturnValue;
        }
        public int ObtainRoleCount()
        {
            int proposedReturnValue = -1;
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("ObtainRoleCount", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    object answer = command.ExecuteScalar();
                    proposedReturnValue = (int)answer;
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return proposedReturnValue;
        }
        public int CreateRole(string RoleName)
        {
            int proposedReturnValue = -1;
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("CreateRole", _connection ))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RoleName", RoleName);
                    command.Parameters.AddWithValue("@RoleID", 0);
                    command.Parameters["@RoleID"].Direction = System.Data.ParameterDirection.Output;
                    command.ExecuteNonQuery();
                    proposedReturnValue = Convert.ToInt32(command.Parameters["@RoleID"].Value);
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return proposedReturnValue;
        }
        public void UpdateRole(int RoleID, string RoleName)
        {
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("JustUpdateRole", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RoleName", RoleName);
                    command.Parameters.AddWithValue("@RoleID", RoleID);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
        }
        public void DeleteRole(int RoleID)
        {
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("DeleteRole", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RoleID", RoleID);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
        }
        #endregion
        #region User Stuff
        public int CreateUser(string FirstName, string LastName, string UserName, string Email, int PhoneNumber, string SALT, string HASH, DateTime DateOfBirth, int RoleID)
        { }
        #endregion
        #region Game Stuff
        #endregion
        #region Score Stuff
        #endregion
        #region Comment Stuff
        #endregion
        #region LogEntries
        #endregion
    }
}
