using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Service.Extensions
{
    [Serializable]
    public class CustomException: Exception
    {
        public CustomException()
        {
        }

       
        public CustomException(string message) : base(message)
        {
        }
    }
}
