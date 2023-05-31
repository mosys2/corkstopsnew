using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Common.Constant
{
    public class Messages
    {
        public class ErrorsMessage
        {
            public const string ModelInvalid = "Model invalid!";
            public const string RegisterFeaild = "Register feaild!";
            public const string ValidationError = "Validation error!";
            public const string LoginField = "You aren't login!";
            public const string WrongEmailOrPass = "Wrong email or password!";
            public const string WrongRemove = "Wrong remove!";
            public const string UpdateField = "Update field!";



        }
        public class Message
        {
            public const string RegisterSuccess = "Register successed.";
            public const string LoginSuccess = "Login successed.";
            public const string RemovedSuccess = "Removed successed.";
            public const string UpdateSuccess = "Update successed.";




        }
    }
}
