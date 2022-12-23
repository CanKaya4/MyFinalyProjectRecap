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
using Business.CCS;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _ProductDal;
        ILogger _logger;
        public ProductManager(IProductDal productDal, ILogger logger)
        {
            _ProductDal = productDal;
            _logger = logger;
        }
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
            //Aynı isimde ürün eklenemez.
            //Bir kategoride en fazla 10 ürün olabilir.
            if (CheckIfProducyCountOfCategoryCorrect(product.CategoryId).Success)
            {
                if (ChechIfProductNameExits(product.ProductName).Success)
                {
                    _ProductDal.Add(product);

                    return new SuccessResult(Messages.ProductAdded);
                }
             
            }
            return new ErrorResult(Messages.ProductCountOfCategoryError);
        }

        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour == 23)
            {
                return new ErrorDataResult<List<Product>>(_ProductDal.GetAll(), Messages.MaintanceTime);
            }
            return new SuccessDataResult<List<Product>>(_ProductDal.GetAll(), Messages.ProductList);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_ProductDal.GetAll(p => p.CategoryId == id), Messages.GetAllByCategoryListed);
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_ProductDal.Get(p => p.ProductId == productId), Messages.ProductNameInValid);
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal max, decimal min)
        {
            return new SuccessDataResult<List<Product>>(_ProductDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_ProductDal.GetProductDetails(), Messages.ProductAdded);
        }
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product product)
        {
            //iş kurallarını bu şekilde yazarsan, kod bir süre sonra spagettiye döner.
            //var result = _ProductDal.GetAll(p => p.CategoryId == product.CategoryId).Count;
            //if (result >= 10)
            //{
            //    new ErrorResult(Messages.ProductCountOfCategoryError);
            //}
            _ProductDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated);
        }
        //Private : iş kuralı parçacıkları Private yazılır.
        private IResult CheckIfProducyCountOfCategoryCorrect(int categoryId)
        {
            var result = _ProductDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result >= 10)
            {
                new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();

        }

        private IResult ChechIfProductNameExits(string productName)
        {
            //Result'a uyan kayıt var mı
            var result = _ProductDal.GetAll(p => p.ProductName == productName).Any();
            if (result)
            {
                new ErrorResult(Messages.ProductNameAlreadyExits);
            }
            return new SuccessResult();
        }
    }
}
