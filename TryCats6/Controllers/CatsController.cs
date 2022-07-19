using BusinessLogicLayer.Entity;
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

        public CatsController(ILogger<CatsController> logger, CatService unitOfWork)
        {
            _logger = logger;
            _catService = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cat>>> Get()
        {
            var cats = await _catService.Cats.GetAll();
            return await _catService.CatsFinder.Count() !=0 ? new ActionResult<IEnumerable<Cat>>(cats) : NotFound();
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<Cat>> Get(string name)
        {
            var getCat = await _catService.CatsFinder.FindByName(name);
            return (getCat != null ? new ObjectResult(getCat) : NotFound());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cat>> Get(int id)
        {
            var getCat = await _catService.Cats.Get(id);
            return getCat != null ? new ObjectResult(getCat) : NotFound();
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
                _catService.Cats.Create(cat);
                await _catService.Commit();
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

            var getCat = await _catService.Cats.Get(cat.Id);
            if (getCat != null)
            {
                _catService.Cats.Update(cat);
                await _catService.Commit();
                return Ok(cat);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task <ActionResult<Cat>> Delete(int id)
        {
            Cat cat = await _catService.Cats.Get(id);
            if (cat == null)
            {
                return NotFound();
            }
            else
            {
                _catService.Cats.Delete(id);
                await _catService.Commit();
                return Ok(cat);
            }
        }
    }
}