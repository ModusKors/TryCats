using BusinessLogicLayer;
using BusinessLogicLayer.Entity;
using BusinessLogicLayer.Finder;
using BusinessLogicLayer.Repository;
using BusinessLogicLayer.Service;
using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;

namespace TryCats6.Controllers
{
    [ApiController]

    [Route("[controller]")]
    public class CatsController : ControllerBase
    {
        private readonly ILogger<CatsController> _logger;

        private CatService _catService;

        public CatsController(ILogger<CatsController> logger, IRepository<Cat> iRepository, ICatFinder iCatFinder, IUnitOfWork iUnitOfWork)
        {
            _logger = logger;
            _catService = new CatService(iRepository, iCatFinder, iUnitOfWork);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cat>>> Get()
        {
            var cats = await _catService.Get();
            return await _catService.Count() !=0 ? new ActionResult<IEnumerable<Cat>>(cats) : NotFound();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cat>> Get(int id)
        {
            var getCat = await _catService.Get(id);
            return getCat != null ? new ObjectResult(getCat) : NotFound();
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<Cat>> Get(string name)
        {
            var getCat = await _catService.Get(name);
            return (getCat != null ? new ObjectResult(getCat) : NotFound());
        }
       

        [HttpPost]
        public async Task<ActionResult<Cat>> Post(Cat cat)
        {
            if (cat == null)
            {
                return BadRequest();
            }
            else
            {
                await _catService.Post(cat);
                return Ok(cat);
            }
        }

        [HttpPut]
        public async Task<ActionResult<Cat>> Put(Cat cat)
        {
            if (cat == null)
            {
                return BadRequest();
            }
            else
            {
                return await _catService.Put(cat) ? Ok(cat) : NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task <ActionResult<Cat>> Delete(int id)
        {
            return await _catService.Delete(id) ? Ok() : NotFound();
        }
    }
}