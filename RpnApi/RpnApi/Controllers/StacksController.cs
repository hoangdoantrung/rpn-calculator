using Microsoft.AspNetCore.Mvc;
using RpnServices;

namespace RpnApi.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class StacksController: ControllerBase
    {
        private readonly IStackService _stackService;

        public StacksController(IStackService stackService)
        {
            this._stackService = stackService;
        }
        
        [HttpPost]
        public int CreateStack()
        {
            return _stackService.CreateStack();
        }
    }
}
