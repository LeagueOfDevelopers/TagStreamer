using System;
using System.Threading.Tasks;
using System.Web.Http;
using TagStreamer.Models;

namespace TagStreamer.Controllers
{
    public class AdminController : ApiController
    {
	    public AdminController(AdminService adminService)
	    {
		    _adminService = adminService;
	    }

	    [HttpGet]
		public IHttpActionResult Connect(string login, string password)
	    {
		    var token = _adminService.RegisterNewAdmin(login, password);
		    return Ok(token);
	    }

		[HttpGet]
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
	    public IHttpActionResult PhotoProcessed(string connectionToken, Guid itemGuid, bool accepted)
	    {
			_adminService.ProcessItem(connectionToken, itemGuid, accepted);
		    return Ok();
	    }

		[HttpGet]
		public IHttpActionResult Disconnect(string connectionToken)
		{
			_adminService.DisconnectAdmin(connectionToken);
			return Ok();
		}

		private readonly AdminService _adminService;
    }
}
