using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    //Temel Voidler için başlangıç
    //Void metotları IResult yapısı ile süsleyeceğiz.
    //IResult interface'imde bir tane işlem sonucu bir tane de Kullanıcıyı bilgilendirmek için mesajı olacak.
    // {get} : Sadece okunabilir demek.
    //IResult bir interace, şimdi ise bunun somut class'ını yazalım.
    public interface IResult
    {
        bool Success { get; }
        string Message { get; }  
    }
}
