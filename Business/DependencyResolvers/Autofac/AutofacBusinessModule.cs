using Autofac;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        //Load isimli hazır metodu ovveride ile ezip içerisine kendi kodlarımızı yazıyoruz.
        protected override void Load(ContainerBuilder builder)
        {
            //BU şu demek.
            //Birisi senden IProductService isterse ProductManager'î register et, yani Productmanager intstance'si ver demek.
            //SingleInstance : tek bir instance oluştur demek.
            builder.RegisterType<ProductManager >().As<IProductService>().SingleInstance();
            builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();
        }
    }
}
