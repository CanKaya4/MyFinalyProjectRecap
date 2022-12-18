using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constans
{
    //sürekli newlenmesine gerek kalmaması için static keywordünü veriyorum.
    //static verdiğimizde direkt olarak Messages. dediğimizde değerler geliyor.
    public static class Messages
    {
        public static string ProductAdded = "Ürün Eklendi."; 
        public static string ProductNameInValid = "Ürün ismi geçersiz.";
        public static string GetAllByCategoryListed = "Kategoriler Listelendi.";
        public static string ProductList = "Ürünler Listelendi.";
        public static string MaintanceTime = "Sistem Bakımda.";
    }
}
