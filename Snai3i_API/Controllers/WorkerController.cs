using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Snai3i_BLL.DTO.WorkerDto;
using Snai3i_BLL.Manager.WorkerManager;

namespace Snai3i_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly IworkerManager workermanager;

        public WorkerController(IworkerManager workermanager)
        {
            this.workermanager = workermanager;
        }

        [HttpGet]   
        public async Task<ActionResult> GetAllworkers([FromQuery] WorkerParamters workerparameters)
        {
            var workerdata = await workermanager.GetAllWorkers(workerparameters);

            return Ok(workerdata);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await workermanager.GetWorkerByCraft(id));
        }

    }
}
