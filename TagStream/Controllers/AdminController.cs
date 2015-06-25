using System;
using System.Threading.Tasks;
using System.Web.Http;
using TagStream.Models;

namespace TagStream.Controllers
{
    public class AdminController : ApiController
    {
	    public AdminController(AdminService adminService)
	    {
		    _adminService = adminService;
	    }

	    [HttpGet]
		[Route("api/Admin/Connect")]
		public IHttpActionResult Connect(string login, string password)
	    {
		    var token = _adminService.RegisterNewAdmin(login, password);
		    if (token == null)
		    {
			    return Unauthorized();
		    }

		    return Ok(token);
	    }

		[HttpGet]
		[Route("api/Admin/NewPhoto")]
		public async Task<IHttpActionResult> NewPhoto(string connectionToken)
		{
			var item = await _adminService.GetLastItemAsync(connectionToken);
			if (item == null)
			{
				return NotFound();
			}

			return Ok(item);
		}

	    [HttpGet]
		[Route("api/Admin/PhotoProcessed")]
	    public IHttpActionResult PhotoProcessed(string connectionToken, Guid itemGuid, bool accepted)
	    {
			_adminService.ProcessItem(connectionToken, itemGuid, accepted);
		    return Ok();
	    }

		[HttpGet]
		[Route("api/Admin/Disconnect")]
		public IHttpActionResult Disconnect(string connectionToken)
		{
			_adminService.DisconnectAdmin(connectionToken);
			return Ok();
		}

		private readonly AdminService _adminService;
    }
}
