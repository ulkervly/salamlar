using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Exceptions
{
    public class InvalidUserCred:Exception
    {
        public string PropertyName { get; set; }
        public InvalidUserCred()
        {
        }

        public InvalidUserCred(string? message) : base(message)
        {
        }
        public InvalidUserCred(string ex, string? message) : base(message)
        {
            PropertyName = ex;
        }
    }
}
