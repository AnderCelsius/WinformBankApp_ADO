using Celsius.Core.Interfaces;
using Celsius.Data;
using Celsius.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celsius.Core
{
    public class CustomerRepository : ICustomerRepository
    {
        /// <summary>
        /// Adds customer information to the database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> AddCustomerInfoToDB(Customer model)
        {
            string sp = "sp_RegisterCustomer";

            IDataParameter pId, pFirstName, pLastName, pFullName, pEmail, pPhoneNumber, pPasswordHash, pPasswordSalt, pDateCreated; // instance of sqlparameter;


            pId = new SqlParameter("@Id", model.Id);

            pFirstName = new SqlParameter("@FirstName", model.FirstName);

            pLastName = new SqlParameter("@LastName", model.LastName);

            pFullName = new SqlParameter("@FullName", model.LastName);

            pEmail = new SqlParameter("@Email", model.Email);

            pPhoneNumber = new SqlParameter("@PhoneNumber", model.PhoneNumber);

            pPasswordSalt = new SqlParameter("@PasswordSalt", model.PasswordSalt);

            pPasswordHash = new SqlParameter("@PasswordHash", model.PasswordHash);

            pDateCreated = new SqlParameter("@DateCreated", model.DateCreated);

            var result = await DataStore.WriteToDataTbl(sp, new IDataParameter[]{ pId, pFirstName, pLastName, pFullName, pEmail, pPhoneNumber, pPasswordSalt, pPasswordHash, pDateCreated});


            return result;
   
        }

        /// <summary>
        /// Searches the database for the email passed and retrieves it if found. 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<Customer> GetCustomerDetails(string email)
        {
            string sp = "sp_GetCustomerByEmail";

            IDataParameter spParameters = new SqlParameter
            {
                ParameterName = "CustomerEmail",
                Value = email
            };

            Customer customer = new Customer();
            using (var reader = await DataStore.ReadFromDataTbl(sp, new IDataParameter[] { spParameters }))
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        customer.Id = (string)reader["Id"];
                        customer.FirstName = (string)reader["FirstName"];
                        customer.LastName = (string)reader["LastName"];
                        customer.Email = (string)reader["Email"];
                        customer.PhoneNumber = (string)reader["PhoneNumber"];
                        customer.PasswordSalt = (byte[])reader["PasswordSalt"];
                        customer.PasswordHash = (byte[])reader["PasswordHash"];
                        customer.DateCreated = (DateTime)reader["DateCreated"];
                    }
                }

                return customer;
            }

        }

        /// <summary>
        /// Creates an instance of the customer and adds the customer information to database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> RegisterCustomer(Customer model)
        {
            string message = string.Empty;

            if (model != null)
            {
                if (await SearchCustomer(model.Email) == false)
                {
                    var status = AddCustomerInfoToDB(model);
                    message = status.Equals(1) ? "Registration Successful" : "Registration Failed";
                }
                else
                {
                    message += "An account with this email already exist. Please login instead";
                }
            }

            else
            {
                message += "No record inserted";
            }
            return message;
        }

        /// <summary>
        /// Searches if customer with the passed in email exists in record. 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<bool> SearchCustomer(string email)
        {
            string sp = "sp_GetCustomerByEmail";

            IDataParameter spParameters = new SqlParameter
            {
                ParameterName = "CustomerEmail",
                Value = email
            };

            using (var reader = await DataStore.ReadFromDataTbl(sp, new IDataParameter[] {spParameters}))
            {
                if(reader != null)
                {
                    while (reader.Read())
                    {
                        string customerId = (string)reader["CustomerId"];

                        if (!string.IsNullOrWhiteSpace(customerId))
                        {
                            return true;
                        }
                    }
                }

               return false;
            }
            

        }

        public async Task<string> GetCustomerIdAsync(string accountNumber)
        {
            string sp = "sp_GetCustomerIdByAccountNumber";

            IDataParameter spParameters = new SqlParameter
            {
                ParameterName = "AccountNumber",
                Value = accountNumber
            };

            using (var reader = await DataStore.ReadFromDataTbl(sp, new IDataParameter[] { spParameters }))
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        string customerId = (string)reader["CustomerId"];

                        if (!string.IsNullOrWhiteSpace(customerId))
                        {
                            return customerId;
                        }
                    }
                }

                return null;
            }

        }
    }
}
