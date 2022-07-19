using BTT.Api.Infrastructure.Filters;

namespace BTT.Api.Infrastructure;

[ApiController]
[Route("api/[controller]")]
[ApiResultFilter]
public class BaseController : ControllerBase
{
}
