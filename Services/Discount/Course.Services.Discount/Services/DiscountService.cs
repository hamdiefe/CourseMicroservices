using Course.Shared.Dtos;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Services.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _connection;

        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<Response<NoContent>> Delete(int id)
        {
            var result = await _connection.ExecuteAsync("Delete top(1) discount where id=@Id", new { Id = id });

            return result > 0 ? Response<NoContent>.Success(204) : Response<NoContent>.Fail("Discount not found", 404);
        }

        public async Task<Response<List<Models.Discount>>> GetAll()
        {
            var discounts = await _connection.QueryAsync<Models.Discount>("select * from discount");

            return Response<List<Models.Discount>>.Success(discounts.ToList(), 200);
        }

        public async Task<Response<Models.Discount>> GetByCodeAndUserId(string code, string userId)
        {
            var discount = await _connection.QuerySingleOrDefaultAsync<Models.Discount>("select top(1) * from discount where userId=@UserId and code=@Code", new { UserId = userId, Code = code });

            return discount == null ? Response<Models.Discount>.Fail("Discount not found", 404)
                                    : Response<Models.Discount>.Success(discount, 200);
        }

        public async Task<Response<Models.Discount>> GetById(int id)
        {
            var discount = await _connection.QuerySingleOrDefaultAsync<Models.Discount>("select top(1) * from discount where id=@Id", new { Id = id });

            return discount == null ? Response<Models.Discount>.Fail("Discount not found", 404)
                                    : Response<Models.Discount>.Success(discount, 200);
        }

        public async Task<Response<NoContent>> Save(Models.Discount discount)
        {
            var result = await _connection.ExecuteAsync("insert into discount values (userId=@UserId,rate=@Rate,code=@Code)", discount);

            return result > 0 ? Response<NoContent>.Success(204) : Response<NoContent>.Fail("Discount couldn't save", 500);
        }

        public async Task<Response<NoContent>> Update(Models.Discount discount)
        {
            var result = await _connection.ExecuteAsync("update top(1) discount set userId=@UserId,rate=@Rate,code=@Code where id = @Id)", discount);

            return result > 0 ? Response<NoContent>.Success(204) : Response<NoContent>.Fail("Discount not found", 404);
        }
    }
}
