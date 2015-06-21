using System.Threading.Tasks;
using System.Web.Http;

namespace TagStreamer.Controllers
{
    public class UserController : ApiController
    {
		[HttpGet]
	    public Task<IHttpActionResult> Connect()
	    {
	    }

		[HttpGet]
	    public Task<IHttpActionResult> NewPhoto(string connectionToken)
	    {
	    }

		[HttpGet]
	    public Task<IHttpActionResult> Disconnect(string connectionToken)
	    {
	    }
    }
}
