using filmsApi.Models;
using filmsApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace filmsApi.Controllers;

[Route("[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly IUserInfoService _userInfoService;
    public TokenController(IUserInfoService userInfoService) => _userInfoService = userInfoService;
    
    [HttpPost]
    public IActionResult Post([FromBody]UserInfo user)
    {        
        var (userData, userInfoResponse) = _userInfoService.GetTokenForUser(user);

        return userInfoResponse switch
        {
            UserInfoResponse.Good => Ok(userData),
            UserInfoResponse.Invalid => BadRequest("Invalid email or password"),
            UserInfoResponse.DoesNotExist => BadRequest("User does not exist"),
            _ => BadRequest()
        };
    }
}
