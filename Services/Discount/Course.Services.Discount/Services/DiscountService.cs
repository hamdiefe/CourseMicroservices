using Course.Services.Discount.Dtos;
using Course.Services.Discount.Models;
using Course.Shared.Dtos;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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
            var result = await _connection.ExecuteAsync("delete from discount where id=@Id", new { Id = id });

            return result > 0 ? Response<NoContent>.Success(204) : Response<NoContent>.Fail("Discount not found", 404);
        }

        public async Task<Response<List<DiscountDto>>> GetAll()
        {
            var discounts = await _connection.QueryAsync<DiscountDto>("select * from discount");

            return Response<List<DiscountDto>>.Success(discounts.ToList(), 200);
        }

        public async Task<Response<DiscountDto>> GetByCodeAndUserId(string code, string userId)
        {
            var discount = await _connection.QuerySingleOrDefaultAsync<DiscountDto>("select * from discount where userId=@UserId and code=@Code", new { UserId = userId, Code = code });

            return discount == null ? Response<DiscountDto>.Fail("Discount not found", 404)
                                    : Response<DiscountDto>.Success(discount, 200);
        }

        public async Task<Response<DiscountDto>> GetById(int id)
        {
            var discount = await _connection.QuerySingleOrDefaultAsync<DiscountDto>("select * from discount where id=@Id", new { Id = id });

            return discount == null ? Response<DiscountDto>.Fail("Discount not found", 404)
                                    : Response<DiscountDto>.Success(discount, 200);
        }

        public async Task<Response<NoContent>> Save(DiscountCreateDto discountCreateDto)
        {
            var result = await _connection.ExecuteAsync("INSERT INTO discount (userid,rate,code) VALUES(@UserId,@Rate,@Code)", discountCreateDto);

            return result > 0 ? Response<NoContent>.Success(204) : Response<NoContent>.Fail("Discount couldn't save", 500);
        }

        public async Task<Response<NoContent>> Update(DiscountUpdateDto discountUpdateDto)
        {
            var result = await _connection.ExecuteAsync("update discount set userid=@UserId, code=@Code, rate=@Rate where id=@Id", discountUpdateDto);

            return result > 0 ? Response<NoContent>.Success(204) : Response<NoContent>.Fail("Discount not found", 404);
        }
    }
}
