using EStore.Core.Constants;
using EStore.Repositories.Interfaces;
using EStore.Services.Helper;
using EStore.Services.SharedResponces;
namespace EStore.Services.AdminServices.RepositoryServices.Order
{
    public class OrderAdminService : IOrderAdminService
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderAdminService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ServiceResponce> ChangeOrderStatus(int OrderId,OrderStatus orderStatus)
        {
            try
            {
                var Succsess=await _unitOfWork.OrderRepository.ChangeOrderStatusAsync(OrderId,orderStatus);
                if (Succsess) 
                {
                    return new ServiceResponce
                    {
                        IsSucceed = true,
                        Messege = "Status Changed"
                    };
                }
                return new ServiceResponce
                {
                    IsSucceed=false,
                    Messege="Invalid Operation"
                    
                };
            }
            catch (Exception ex)
            {
                return this.ExeptionResponce(ex.Message);
            }
        }
    }
}
