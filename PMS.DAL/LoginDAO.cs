using System;
using PMS.Entity;

namespace PMS.DAL
{
    public class LoginDAO
    {
        public LoginDAO()
        {

        }

        public bool RegisterUser(Login userObj)
        {
            return true;
        }

        /// <summary>
        /// Method to Validate User
        /// </summary>
        /// <param name="userObj">object of Login class to provide UserName & Password</param>
        /// <returns>boolean</returns>
        public bool AuthUser(Login userObj)
        {
            return true;
        }
    }
}
