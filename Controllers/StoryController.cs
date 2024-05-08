using JWTAuthenticationWithMiddlewares.DTOs.Requests;
using JWTAuthenticationWithMiddlewares.DTOs.Responses;
using JWTAuthenticationWithMiddlewares.Services.StoryService;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuthenticationWithMiddlewares.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoryController : ControllerBase
    {
        private readonly IStoryService storyService;

        //controller
        public StoryController(IStoryService storyService)
        {
            this.storyService = storyService;
        }

        //endpoints

        [HttpPost("addStory")]
        public BaseResponse CreateStory(CreateStoryRequest request)
        {
            return storyService.CreateStory(request);
        }

        [HttpGet("getAll")]
        public BaseResponse StoryList()
        {
            return storyService.StoryList();
        }

    }
}
