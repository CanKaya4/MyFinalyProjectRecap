using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.CCS;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//.net'in içerisinde kendi IoC yapısı olsa da biz bu altyapıyı Interception dediğimiz AOP tekniklerini de kullanabilmek için AutoFac IoC container ile çalışacağız.
    
namespace Business.DependencyResolvers.Autofac
{
    //AutoFacBusinessModule bu projeyi ilgilendiren IoC konfigürasyonu burada yapacağım.
    //Bunun bir de tüm projelerde kullanılabileceğimiz Core katmanı versiyonuda var
    public class AutofacBusinessModule : Module
    {
        //Load isimli hazır metodu ovveride ile ezip içerisine kendi kodlarımızı yazıyoruz.
        protected override void Load(ContainerBuilder builder)
        {
            //BU şu demek.
            //Birisi senden IProductService isterse ProductManager'ı register et, yani Productmanager intstance'si ver demek.
            //SingleInstance : tek bir instance oluştur demek.
            builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();
            builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();
            builder.RegisterType<FileLogger>().As<ILogger>().SingleInstance();
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}
