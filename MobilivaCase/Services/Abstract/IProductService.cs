using Microsoft.AspNetCore.Mvc;

namespace MobilivaCase.Services.Abstract
{
    public interface IProductService
    {
        Task<ActionResult<ApiResponse>> GetProducts(string? category);
    }
}
