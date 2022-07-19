using BusinessLogicLayer.Entity;
using BusinessLogicLayer.Repository;
using Microsoft.AspNetCore.Mvc;

namespace TryCats6.Controllers
{
    [ApiController]

    [Route("[controller]")]
    public class CatsController : ControllerBase
    {
        private readonly ILogger<CatsController> _logger;

        private readonly ICatRepository _catRepository;

        public CatsController(ILogger<CatsController> logger, ICatRepository catRepository)
        {
            _logger = logger;
            _catRepository = catRepository;
            _catRepository.SeedData();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cat>>> Get()
        {
            var cats = await _catRepository.GetAll();
            return await _catRepository.Count() !=0 ? new ActionResult<IEnumerable<Cat>>(cats) : NotFound();
        }

        [HttpGet("{name}")]
        public Task<ActionResult<Cat>> Get(string name)
        {
            var getCat = _catRepository.Find(x => x.Name == name);
            return Task.FromResult<ActionResult<Cat>>(getCat != null ? new ObjectResult(getCat) : NotFound());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cat>> Get(int id)
        {
            var getCat = await _catRepository.Get(id);
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
                await _catRepository.Create(cat);
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

            var getCat = await _catRepository.Get(cat.Id);
            if (getCat != null)
            {
                await _catRepository.Update(cat);
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
            Cat cat = await _catRepository.Get(id);
            if (cat == null)
            {
                return NotFound();
            }
            else
            {
                await _catRepository.Delete(id);
                return Ok(cat);
            }
        }
    }
}