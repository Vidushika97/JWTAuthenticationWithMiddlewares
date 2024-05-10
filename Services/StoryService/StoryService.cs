using JWTAuthenticationWithMiddlewares.DTOs.Requests;
using JWTAuthenticationWithMiddlewares.DTOs.Responses;
using JWTAuthenticationWithMiddlewares.Services.StoryService;
using JWTAuthenticationWithMiddlewares.Models;
using static JWTAuthenticationWithMiddlewares.Services.StoryService.StoryService;
using JWTAuthenticationWithMiddlewares.DTOs;

namespace JWTAuthenticationWithMiddlewares.Services.StoryService
{
    public class StoryService : IStoryService
    {
        //variable to store application db context
        private readonly ApplicationDbContext context;

        public StoryService(ApplicationDbContext applicationDbContext)
        {
            //get db context through DI
            context = applicationDbContext;
        }
        public BaseResponse CreateStory(CreateStoryRequest request)
        {
            BaseResponse response;
            try
            {

                // create new instace of student model

                StoryModel newStory = new StoryModel();
                newStory.title = request.title;
                newStory.description = request.description;

                using (context)
                {
                    context.Add(newStory);
                    context.SaveChanges();

                }

                response = new BaseResponse
                {
                    status_code = StatusCodes.Status200OK,
                    data = new { message = "Successfully created the new story" }
                };

                return response;
            }
            catch (Exception ex)
            {
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status500InternalServerError,
                    data = new { message = "Internal server error : " + ex.Message }
                };

                return response;
            }
        }



        public BaseResponse StoryList()
        {
            BaseResponse response;

            try
            {
                List<StoryDTO> stories = new List<StoryDTO>();

                using (context)
                {
                    Console.WriteLine(context.Stories);
                    // get all students fron database and add to student list after convert them to student dto
                    context.Stories.ToList().ForEach(story => stories.Add(new StoryDTO
                    {
                        id = story.id,
                        title = story.title,
                        description = story.description,

                    }));

                }

                response = new BaseResponse
                {
                    status_code = StatusCodes.Status200OK,
                    data = new { stories }
                };

                return response;

            }
            catch (Exception ex)
            {
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status500InternalServerError,
                    data = new { message = "Internal server error : " + ex.Message }
                };

                return response;
            }
        }
    }
}
