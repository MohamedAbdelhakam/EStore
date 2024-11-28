using EStore.Core.Models;
using EStore.Repositories.Interfaces;
using EStore.Services.SharedResponces;
using EStore.Services.UserServices.Dtos;
using EStore.Services.Helper;
using UserServices.Orders;

namespace EStore.Services.UserServices.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ServiceResponce> GetOrders(string UserId)
        {
            try
            {
                var orders=await _unitOfWork.OrderRepository.GetUserOrdersAsync(UserId);
                List<OrderDto> orderDtos = new List<OrderDto>();
                foreach (var order in orders) 
                {
                    orderDtos.Add((OrderDto)order);
                }
                return new ServiceResponce
                {
                    Messege = "UserOrders",
                    IsSucceed = true,
                    Values =
                    {
                        ["Orders"]=orderDtos
                    }

                };
            }
            catch (Exception ex) 
            {
                return this.ExeptionResponce(ex.Message);
            }
        }

        public async Task<ServiceResponce> MakeOrder(int CartId, string ShippingAddress)
        {
            try
            {
                var Succsess=await _unitOfWork.OrderRepository.OrderAsync(CartId,ShippingAddress);
                if (Succsess)
                {
                    return new ServiceResponce
                    {
                        IsSucceed = true,
                        Messege = "Succsess process"
                    };
                }
                return new ServiceResponce
                {
                    IsSucceed = true,
                };
            }
            catch(Exception ex)
            {
                return this.ExeptionResponce(ex.Message);    
            }
        }
    }
}
