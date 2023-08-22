using System;

namespace PMS.Exceptions
{
    public class CustomException:Exception
    {
        public CustomException():base()
        {

        }

        public CustomException(string errorMessage):base(errorMessage)
        {

        }
    }
}
