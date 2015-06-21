using System.Web.Http;
using TagStreamer.Models;

namespace TagStreamer.Controllers
{
    public class UserController : ApiController
    {
	    public UserController(UserFeedItemService userFeedItemService)
	    {
		    _userFeedItemService = userFeedItemService;
	    }

	    [HttpGet]
	    public IHttpActionResult Connect()
	    {
		    var sessionId = _userFeedItemService.CreateNewSession();
		    return Ok(sessionId);
	    }

		[HttpGet]
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
	    public IHttpActionResult Disconnect(string connectionToken)
		{
			_userFeedItemService.CloseSession(connectionToken);
			return Ok();
		}
		
		private readonly UserFeedItemService _userFeedItemService;
    }
}
