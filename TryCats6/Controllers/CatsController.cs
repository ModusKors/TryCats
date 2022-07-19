using BusinessLogicLayer.Entity;
using BusinessLogicLayer.Repository;
using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;

namespace TryCats6.Controllers
{
    [ApiController]

    [Route("[controller]")]
    public class CatsController : ControllerBase
    {
        private readonly ILogger<CatsController> _logger;

        //private readonly ICatRepository _catRepository;

        private UnitOfWork unitOfWork;

        public CatsController(ILogger<CatsController> logger, CatsContext catsContext)
        {
            _logger = logger;
            unitOfWork = new UnitOfWork(catsContext);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cat>>> Get()
        {
            var cats = await unitOfWork.Cats.GetAll();
            return await unitOfWork.CatsFinder.Count() !=0 ? new ActionResult<IEnumerable<Cat>>(cats) : NotFound();
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<Cat>> Get(string name)
        {
            var getCat = await unitOfWork.CatsFinder.FindByName(name);
            return (getCat != null ? new ObjectResult(getCat) : NotFound());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cat>> Get(int id)
        {
            var getCat = await unitOfWork.Cats.Get(id);
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
                unitOfWork.Cats.Create(cat);
                await unitOfWork.Commit();
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

            var getCat = await unitOfWork.Cats.Get(cat.Id);
            if (getCat != null)
            {
                unitOfWork.Cats.Update(cat);
                await unitOfWork.Commit();
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
            Cat cat = await unitOfWork.Cats.Get(id);
            if (cat == null)
            {
                return NotFound();
            }
            else
            {
                unitOfWork.Cats.Delete(id);
                await unitOfWork.Commit();
                return Ok(cat);
            }
        }
    }
}