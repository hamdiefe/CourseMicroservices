using Course.Services.Order.Application.Dtos;
using Course.Services.Order.Application.Mapping;
using Course.Services.Order.Application.Queries;
using Course.Services.Order.Domain.OrderAggregate;
using Course.Services.Order.Infrastructure;
using Course.Shared.Dtos;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Course.Services.Order.Application.Handlers
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Response<CreatedOrderDto>>
    {
        private readonly OrderDbContext _dbContext;

        public CreateOrderHandler(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Response<CreatedOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var address = ObjectMapper.Mapper.Map<Address>(request.Address);

            var order = new Domain.OrderAggregate.Order(address, request.BuyerId);

            request.OrderItems.ForEach(x =>
            {
                order.AddOrderItem(x.ProductId, x.ProductName, x.PictureUrl, x.Price);
            });

            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();

            return Response<CreatedOrderDto>.Success(new CreatedOrderDto { Id = order.Id }, 200);
        }
    }
}
