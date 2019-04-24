using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCountry.Data.Base
{
    public class BaseResponseModel<T>
    {
        public bool IsSucess { get; set; }
        public Error Exception { get; set; }
        public T Results { get; set; }
    }

    public class Error
    {
        public int ErrorNumber { get; set; }
        public string ErrorName { get; set; }
    }
}
