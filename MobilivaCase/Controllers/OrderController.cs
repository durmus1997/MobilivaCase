using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using MobilivaCase.Services.Abstract;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace MobilivaCase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderService _orderService;
        private IProductService _productService;

        public OrderController(IOrderService orderService, IProductService productService)
        {
            _orderService = orderService;
            _productService = productService;
        }
        /// <summary>
        /// Get Products
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ApiResponse>> Get(string? category)
        {
            var getProducts = await _productService.GetProducts(category);

            if (getProducts.Value != null && getProducts.Value.Data == null)
            {
                return NotFound(getProducts);
            }

            return Ok(getProducts);
        }
        /// <summary>
        /// Create Order
        /// </summary>
        /// <param name="createOrderRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponse>> Post([FromBody] CreateOrderRequest createOrderRequest)
        {
            var apiResponse = await _orderService.CreateOrder(createOrderRequest);

            if (apiResponse.Value != null && apiResponse.Value.ErrorCode == 400)
            {
                return BadRequest(apiResponse);
            }

            return Ok(apiResponse);

        }

    }
}
