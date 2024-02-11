using AutoMapper;
using Course.Services.Catalog;
using Course.Services.Catalog.Dtos;
using Course.Services.Catalog.Models;
using Course.Services.Catalog.Settings;
using Course.Shared.Dtos;
using Mass= MassTransit;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Course.Shared.Messages;

namespace Course.Services.Catalog.Services
{
    public class CourseService : ICourseService
    {
        private readonly IMongoCollection<Models.Course> _courseCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;
        private readonly Mass.IPublishEndpoint _publishEndpoint;


        public CourseService(IMapper mapper,
                             IDatabaseSettings _databaseSettings,
                             Mass.IPublishEndpoint publishEndpoint)
        {

            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _courseCollection = database.GetCollection<Models.Course>(_databaseSettings.CourseCollectionName);
            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }


        public async Task<Response<List<CourseDto>>> GetAllAsync()
        {
            var courses = await _courseCollection.Find(x => true).ToListAsync();
            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    course.Category = _categoryCollection.Find(x => x.Id == course.CategoryId).First();
                }
            }
            else
            {
                courses = new List<Models.Course>();
            }

            var response = Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
            return response;
        }
        public async Task<Response<CourseDto>> GetByIdAsync(string id)
        {
            var course = await _courseCollection.Find<Models.Course>(x => x.Id == id).FirstOrDefaultAsync();

            if (course == null)
            {
                return Response<CourseDto>.Fail("Course not found", 404);
            }
            else
            {
                course.Category = _categoryCollection.Find(x => x.Id == course.CategoryId).First();
            }

            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200); ;
        }
        public async Task<Response<List<CourseDto>>> GetAllByUserIdAsync(string userId)
        {
            var courses = await _courseCollection.Find(x => x.UserId == userId).ToListAsync();
            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    course.Category = _categoryCollection.Find(x => x.Id == course.CategoryId).First();
                }
            }
            else
            {
                courses = new List<Models.Course>();
            }

            var response = Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
            return response;
        }
        public async Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto)
        {
            var newCourse = _mapper.Map<Models.Course>(courseCreateDto);
            await _courseCollection.InsertOneAsync(newCourse);
            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(newCourse), 200);
        }
        public async Task<Response<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto)
        {
            var course = _mapper.Map<Models.Course>(courseUpdateDto);

            var result = await _courseCollection.FindOneAndReplaceAsync(x => x.Id == course.Id, course);

            if (result == null)
            {
                return Response<NoContent>.Fail("Course not found.", 404);
            }

            await _publishEndpoint.Publish<CourseNameChangedEvent>(new CourseNameChangedEvent { CourseId = course.Id, UpdatedName = courseUpdateDto.Name});


            return Response<NoContent>.Success(204);
        }
        public async Task<Response<NoContent>> DeleteAsync(string id)
        {
            var result = await _courseCollection.DeleteOneAsync(x => x.Id == id);

            if (result.DeletedCount > 0)
            {
                return Response<NoContent>.Success(204);
            }

            return Response<NoContent>.Fail("Course not found.", 404);
        }
    }
}
