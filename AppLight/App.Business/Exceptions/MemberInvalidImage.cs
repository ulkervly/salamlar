using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Exceptions
{
    public class MemberInvalidImage : Exception
    {
        public string PropertyName { get; set; }
        public MemberInvalidImage()
        {
        }

        public MemberInvalidImage(string? message) : base(message)
        {
        }
        public MemberInvalidImage(string ex, string? message) : base(message)
        {
            PropertyName = ex;
        }



    }
}
