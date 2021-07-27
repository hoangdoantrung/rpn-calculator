using Microsoft.AspNetCore.Mvc;
using RpnModels;
using RpnServices;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;

namespace RpnApi.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class StacksController : ControllerBase
    {
        private readonly IStackService _stackService;

        public StacksController(IStackService stackService)
        {
            this._stackService = stackService;
        }
        
        /// <summary>
        /// Create a stack.
        /// </summary>
        /// <returns>New stack id.</returns>
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
        [Route("{id}")]
        [SwaggerResponse(200, "Get stack sucessfully", typeof(StackModel))]
        public IActionResult GetById(int id)
        {
            if (id < 1)
            {
                return BadRequest("Id must be positive");
            }

            return Ok(_stackService.GetById(id));
        }

        /// <summary>
        /// Get all stacks.
        /// </summary>
        /// <returns>Stack with given id.</returns>
        [HttpGet]
        [SwaggerResponse(200, "Get all stacks sucessfully", typeof(IList<StackModel>))]
        public IActionResult GetAll()
        {
            return Ok(_stackService.GetAll());
        }

        /// <summary>
        /// Clear operands of a stack.
        /// </summary>
        /// <param name="id">Stack's id.</param>
        /// <returns>True if succeed.</returns>
        [HttpPut]
        [SwaggerResponse(200, "Clear stack sucessfully", typeof(bool))]
        public IActionResult Clear(int id)
        {
            if (id < 1)
            {
                return BadRequest("Id must be positive");
            }

            return Ok(_stackService.Clear(id));
        }

        /// <summary>
        /// Delete a stack.
        /// </summary>
        /// <param name="id">Stack's id.</param>
        /// <returns>True if succeed.</returns>
        [HttpDelete]
        [SwaggerResponse(200, "Delete stack sucessfully", typeof(bool))]
        public IActionResult Delete(int id)
        {
            if (id < 1)
            {
                return BadRequest("Id must be positive");
            }

            return Ok(_stackService.Delete(id));
        }

        /// <summary>
        /// Push an operand into stack.
        /// </summary>
        /// <param name="id">Stack's id.</param>
        /// /// <param name="operand">Operand to push.</param>
        /// <returns>Stack with new operand.</returns>
        [HttpPatch]
        [SwaggerResponse(200, "Push an operand into stack sucessfully", typeof(StackModel))]
        public IActionResult Push(int id, decimal operand)
        {
            if (id < 1)
            {
                return BadRequest("Id must be positive");
            }

            return Ok(_stackService.Push(id, operand));
        }

        /// <summary>
        /// Create an operator in stack.
        /// </summary>
        /// <param name="id">Stack's id.</param>
        /// /// <param name="op">Operator to create.</param>
        /// <returns>Stack with new operator.</returns>
        [HttpPost]
        [Route("{id}")]
        [SwaggerResponse(200, "Create an operator in stack sucessfully", typeof(StackModel))]
        public IActionResult CreateOperator(int id, Operator op)
        {
            if (id < 1)
            {
                return BadRequest("Id must be positive");
            }

            return Ok(_stackService.CreateOperator(id, op));
        }
    }
}
