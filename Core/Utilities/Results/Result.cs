using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        //Get read only'dir yani sadece okunabilir, set gibi yazılamaz.
        //!! ama get constructor içerisinde sett edilebilir.
        public Result(bool success, string message):this(success)
        {
            Message = message;
        }
        //Result consturctor'ını ovverloading ediyorum. sadece success değerini verip mesaj vermek istemezsem diye.
        public Result(bool success)
        {
            Success = success;
        }        

        public bool Success { get; }

        public string Message { get; }
    }
}
