using Microsoft.AspNetCore.Mvc;
using MobilivaCase.Services.Abstract;

namespace MobilivaCase.Services.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly DataContext _context;

        public OrderService(DataContext context)
        {
            this._context = context;
        }

        public async Task<ActionResult<ApiResponse>> CreateOrder([FromBody] CreateOrderRequest createOrderRequest)
        {
            ApiResponse apiResponse = new ApiResponse();
            Order order = new Order();
            order.CustomerName = createOrderRequest.CustomerName;
            order.CustomerEmail = createOrderRequest.CustomerEmail;
            order.CustomerGSM = createOrderRequest.CustomerGSM;
            order.Id = new Guid();

            if (createOrderRequest.ProductDetail == null)
            {
                apiResponse.ResultMessage = "ProductDetail is required";
                apiResponse.Status = ApiResponse.StatusCode.Failed;
                apiResponse.ErrorCode = 400;
                return apiResponse;
            }

            order.TotalAmount = createOrderRequest.ProductDetail.Sum(x => x.Amount);

            _context.Orders.Add(order);

            foreach (var productDetail in createOrderRequest.ProductDetail)
            {
                if (productDetail.ProductId == 0 || productDetail.Amount == 0 || productDetail.UnitPrice == 0)
                {
                    apiResponse.ResultMessage = "ProductDetail is invalid";
                    apiResponse.Status = ApiResponse.StatusCode.Failed;
                    apiResponse.ErrorCode = 400;
                    return apiResponse;
                }
                
                OrderDetail orderDetail = new OrderDetail();
                orderDetail.ProductId = productDetail.ProductId;
                orderDetail.UnitPrice = productDetail.UnitPrice;
                orderDetail.OrderId = order.Id;

                _context.OrderDetails.Add(orderDetail);
            }

            await _context.SaveChangesAsync();

            apiResponse.ResultMessage = "Success";
            apiResponse.Status = ApiResponse.StatusCode.Success;
            apiResponse.ErrorCode = 0;
            apiResponse.Data = order.Id;

            return apiResponse;
        }
    }
}
