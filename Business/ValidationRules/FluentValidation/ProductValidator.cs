using Entites.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    //Business kodlarımmız ile validation kodlarımızı ayırmak için bu  classı açtım.
    //AbstractValidator'dan inherit ediyoruz ve hangi sınıf ile çalıaşcaksak onu veriyoruz. Bu class product içib olduğundan dolayı Product verdim.
    public class ProductValidator:AbstractValidator<Product>
    {
        //Validation kuralları bir contructor içerisine yazılır.
        public ProductValidator()
        {
            //Ürün başlığı boş olamaz.
            RuleFor(p => p.ProductName).NotEmpty();
            //Ürün başlığının min 2 karakter olması gerekmekte
            RuleFor(p => p.ProductName).MinimumLength(2);
            //Ürünün fiyatı boş olamaz.
            RuleFor(p=>p.UnitPrice).NotEmpty();
            //Ürünün fiyatı 0 dan büyük olmalıdır.
            RuleFor(p => p.UnitPrice).GreaterThan(0);
            //CategoryId'si 1 olan bir ürünün fiyatı minumum 10 birim olmalıdır.
            RuleFor(p => p.UnitPrice).GreaterThan(10).When(p => p.CategoryId == 1);
            //Ürün ismi a harfi ile başlamalı.
            RuleFor(p => p.ProductName).Must(StartWithA).WithMessage("Ürün ismi A harfi ile başlamalı.");
            
        }

        private bool StartWithA(string arg)
        {
            return arg.StartsWith("A");
        }
    }
}
