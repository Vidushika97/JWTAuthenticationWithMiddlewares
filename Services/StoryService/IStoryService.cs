using JWTAuthenticationWithMiddlewares.DTOs.Requests;
using JWTAuthenticationWithMiddlewares.DTOs.Responses;

namespace JWTAuthenticationWithMiddlewares.Services.StoryService
{
    public interface IStoryService
    {
        BaseResponse CreateStory(CreateStoryRequest request);

        BaseResponse StoryList();
    }
}
