using Microsoft.AspNetCore.Mvc;

namespace MobilivaCase.Services.Abstract
{
    public interface IOrderService
    {
        Task<ActionResult<ApiResponse>> CreateOrder([FromBody] CreateOrderRequest createOrderRequest);
    }
}
