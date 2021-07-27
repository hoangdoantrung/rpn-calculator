using Microsoft.AspNetCore.Mvc;
using RpnModels;
using RpnServices;
using Swashbuckle.AspNetCore.Annotations;

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
        
        /// <summary>
        /// Create a stack.
        /// </summary>
        /// <returns>New stack.</returns>
        [HttpPost]
        [SwaggerResponse(200, "Create new stack sucessfully", typeof(int))]
        public int CreateStack()
        {
            return _stackService.CreateStack();
        }

        /// <summary>
        /// Get a stack by id.
        /// </summary>
        /// <param name="id">Stack's id.</param>
        /// <returns>Stack with given id.</returns>
        [HttpGet]
        [SwaggerResponse(200, "Get stack sucessfully", typeof(StackModel))]
        public IActionResult GetById(int id)
        {
            if (id < 1)
            {
                return BadRequest("Id must be positive");
            }

            return Ok(_stackService.GetById(id));
        }
    }
}
