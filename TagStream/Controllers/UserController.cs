using System.Web.Http;
using TagStream.Models;

namespace TagStream.Controllers
{
    public class UserController : ApiController
    {
	    public UserController(UserFeedItemService userFeedItemService)
	    {
		    _userFeedItemService = userFeedItemService;
	    }

	    [HttpGet]
		[Route("api/User/Connect")]
	    public IHttpActionResult Connect()
	    {
		    var sessionId = _userFeedItemService.CreateNewSession();
		    return Ok(sessionId);
	    }

		[HttpGet]
		[Route("api/User/NewPhoto")]
	    public IHttpActionResult NewPhoto(string connectionToken)
		{
			var item = _userFeedItemService.GetNewItem(connectionToken);
			if (item == null)
			{
				return NotFound();
			}

			return Ok(item);
		}

		[HttpGet]
		[Route("api/User/Disconnect")]
	    public IHttpActionResult Disconnect(string connectionToken)
		{
			_userFeedItemService.CloseSession(connectionToken);
			return Ok();
		}
		
		private readonly UserFeedItemService _userFeedItemService;
    }
}
