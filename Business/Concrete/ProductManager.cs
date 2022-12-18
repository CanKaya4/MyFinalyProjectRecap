using Business.Abstract;
using Business.Constans;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entites.Concrete;
using Entites.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _ProductDal;
        public ProductManager(IProductDal productDal)
        {
            _ProductDal = productDal;
        }

        public IResult Add(Product product)
        {
            //magic strings 
            if (product.ProductName.Length>2)
            {
                return new ErrorResult(Messages.ProductNameInValid);
            }
           _ProductDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IDataResult<List<Product>> GetAll()
        {
            return new SuccessDataResult<List<Product>>(_ProductDal.GetAll());
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
