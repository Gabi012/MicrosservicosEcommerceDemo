using System.Net.Http.Json;
using OrderApi.Application.Converstion;
using OrderApi.Application.DTOs;
using OrderApi.Application.Interface;
using Polly;
using Polly.Registry;

namespace OrderApi.Application.Services
{
    public class OrderService(IOrder orderInterface ,HttpClient httpClient, ResiliencePipelineProvider<string> resiliencePipeline) : IOrderService
    {
        public async Task<ProductDTO> GetProduct(int productId)
        {
            // Call Product ApI using HttpClient
            // Redirect this call to the API Gateway since product Api is not response to outsiders. 
            var getProduct = await httpClient.GetAsync($"api/products/{productId}");
            if (!getProduct.IsSuccessStatusCode) { return null; }
            var product = await getProduct.Content.ReadFromJsonAsync<ProductDTO>();
            return product!;
        }

        public async Task<AppUserDTO> GetUser(int userId)
        {
            // Call Product ApI using HttpClient
            // Redirect this call to the API Gateway since product Api is not response to outsiders. 
            var getUser = await httpClient.GetAsync($"api/authentication/{userId}");
            if (!getUser.IsSuccessStatusCode) return null;
            var user = await getUser.Content.ReadFromJsonAsync<AppUserDTO>();
            return user;
        }

        public async Task<OrderDetailsDTO> GetOrderDetails(int orderId)
        {
            // Prepared Order
            var order = await orderInterface.FindByIdAsync(orderId);
            if(order is null || order.Id <= 0)
            {
                return null;
            }
            // Retry pipeline
            var retryPipiline = resiliencePipeline.GetPipeline("my-retry-pipeline");
            // Prepare Product
            var productDTO = await retryPipiline.ExecuteAsync(async token => await GetProduct(order.ProductId));

            // Prepared User
            var appUserDto = await retryPipiline.ExecuteAsync(async token => await GetUser(order.ClientId));

            // Populate OrderDto

            //return new OrderDetailsDTO(
            //    orderId = order.Id,
            //    productDTO.Id,
            //    appUserDto.Id,
            //    appUserDto.Name,
            //    appUserDto.Email,
            //    appUserDto.Address,
            //    appUserDto.TelephoneNumber,
            //    productDTO.Name,
            //    order.PurchaseQuantity,
            //    productDTO.Price,
            //    productDTO.Price * order.PurchaseQuantity,
            //    order.OrderedDate
            //    );
            return new OrderDetailsDTO(
    order.Id,
    productDTO?.Id ?? 0,
    appUserDto?.Id ?? 0,
    appUserDto?.Name ?? string.Empty,
    appUserDto?.Email ?? string.Empty,
    appUserDto?.Address ?? string.Empty,
    appUserDto?.TelephoneNumber ?? string.Empty,
    productDTO?.Name ?? string.Empty,
    order.PurchaseQuantity,
    productDTO?.Price ?? 0,
    (productDTO?.Price ?? 0) * order.PurchaseQuantity,
    order.OrderedDate
);
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersByClientId(int clientId)
        {
            // Get all Client's orders
            var orders = await orderInterface.GetOrdersAsync(o => o.ClientId == clientId);
            if (!orders.Any()) return null!;

            // Convert from entity to DTO
            var (_, _orders) = OrderConversion.FromEntity(null, orders); 
            return _orders!;
        }
    }
}
