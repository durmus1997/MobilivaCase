using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using MobilivaCase.Services.Abstract;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace MobilivaCase.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IDistributedCache _distributedCache;
        private readonly DataContext _context;

        public ProductService(DataContext context, IDistributedCache distributedCache)
        {
            this._context = context;
            this._distributedCache = distributedCache;
        }
        public async Task<ActionResult<ApiResponse>> GetProducts(string? category)
        {
            List<Product> allProducts = new List<Product>();
            ApiResponse apiResponse = new ApiResponse();
            List<ProductDto> productDtos = new List<ProductDto>();

            var redisMemory = await _distributedCache.GetAsync("");
            if (redisMemory != null)
            {
                string serializedProducts = Encoding.UTF8.GetString(redisMemory);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                allProducts = JsonConvert.DeserializeObject<List<Product>>(serializedProducts);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            }
            else
            {
                allProducts = await _context.Products.ToListAsync();
                var serializedProducts = JsonConvert.SerializeObject(allProducts);
                var cacheEntryOptions = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(150));
                await _distributedCache.SetAsync("", Encoding.UTF8.GetBytes(serializedProducts), cacheEntryOptions);
            }

            if (allProducts == null)
            {
                apiResponse.ResultMessage = "No products found";
                apiResponse.Status = ApiResponse.StatusCode.Failed;
                apiResponse.ErrorCode = 404;
                return apiResponse;
            }

            if (category != null)
            {
                List<Product> productsWithCategory = allProducts.Where(predicate: x => x.Category.Contains(category)).ToList();

                foreach (Product product in productsWithCategory)
                {
                    ProductDto newProduct = new ProductDto();
                    newProduct.UnitPrice = product.UnitPrice;
                    newProduct.Unit = product.Unit;
                    newProduct.Description = product.Description;
                    newProduct.Category = product.Category;

                    productDtos.Add(newProduct);
                }

                if (productDtos.Count == 0)
                {
                    apiResponse.ResultMessage = "Failed with category: " + category;
                    apiResponse.Status = ApiResponse.StatusCode.Failed;
                    apiResponse.ErrorCode = 404;
                    return apiResponse;
                }
                else
                {
                    apiResponse.Data = productDtos;
                    apiResponse.ResultMessage = "Success with Category: " + category;
                    apiResponse.Status = ApiResponse.StatusCode.Success;
                    apiResponse.ErrorCode = 0;
                    return apiResponse;
                }
            }

            foreach (Product product in allProducts)
            {
                ProductDto newProduct = new ProductDto();
                newProduct.UnitPrice = product.UnitPrice;
                newProduct.Unit = product.Unit;
                newProduct.Description = product.Description;
                newProduct.Category = product.Category;

                productDtos.Add(newProduct);
            }

            if (productDtos.Count == 0)
            {
                apiResponse.ResultMessage = "Failed";
                apiResponse.Status = ApiResponse.StatusCode.Failed;
                apiResponse.ErrorCode = 404;
                return apiResponse;
            }
            else
            {
                apiResponse.Data = productDtos;
                apiResponse.ResultMessage = "Success";
                apiResponse.Status = ApiResponse.StatusCode.Success;
                apiResponse.ErrorCode = 0;
                return apiResponse;
            }
        }
    }
}
