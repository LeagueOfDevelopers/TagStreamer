using System.Threading.Tasks;
using System.Web.Http;

namespace TagStreamer.Controllers
{
    public class AdminController : ApiController
    {
		[HttpGet]
		public Task<IHttpActionResult> Connect(string login, string password)
		{
		}

		[HttpGet]
		public Task<IHttpActionResult> NewPhoto(string connectionToken)
		{
		}

	    [HttpGet]
	    public Task<IHttpActionResult> PhotoProcessed(string connectionToken, bool accepted)
	    {
	    }

		[HttpGet]
		public Task<IHttpActionResult> Disconnect(string connectionToken)
		{
		}
    }
}
