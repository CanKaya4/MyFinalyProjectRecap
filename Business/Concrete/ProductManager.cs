using Business.Abstract;
using Business.Constans;
    using FluentValidation;
using Business.ValidationRules.FluentValidation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entites.Concrete;
using Entites.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;
using Core.CrossCuttingConcerns.Validation;
using Core.Aspects.Autofac.Validation;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _ProductDal;
        public ProductManager(IProductDal productDal)
        {
            _ProductDal = productDal;
        }

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {

            //ProductValidator'u kullanmak için ProductManager içerisinde yazmamammız gereken kod.
            //Ancak bunu farklı projelerde de kullanacağım ve tekrar tekrar yazmak ile uğraşmak yerine bir tool(araç haline) getişrmtmek istiyorum.
            //Bunun için de Core katmanını kullanacağım.
           //var context = new ValidationContext<Product>(product);
           // ProductValidator productValidator = new ProductValidator();
           // var result = productValidator.Validate(context);
           // if (!result.IsValid)
           // {
           //     throw new ValidationException(result.Errors);
           // }
          
           _ProductDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour == 23)
            {
                return new ErrorDataResult<List<Product>>(_ProductDal.GetAll(), Messages.MaintanceTime);
            }
            return new SuccessDataResult<List<Product>>(_ProductDal.GetAll(),Messages.ProductList);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {   
            return new SuccessDataResult<List<Product>>(_ProductDal.GetAll(p=>p.CategoryId == id), Messages.GetAllByCategoryListed);
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_ProductDal.Get(p=>p.ProductId == productId),Messages.ProductNameInValid);
        }

        public IDataResult<List<Product>>  GetByUnitPrice(decimal max, decimal min)
        {
            return new SuccessDataResult<List<Product>>(_ProductDal.GetAll(p=>p.UnitPrice>=min && p.UnitPrice<=max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_ProductDal.GetProductDetails(), Messages.ProductAdded);
        }

        
    }
}
