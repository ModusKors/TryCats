using BusinessLogicLayer;
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

        private IUnitOfWork _unitOfWork;

        public CatsController(ILogger<CatsController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cat>>> Get()
        {
            var cats = await _unitOfWork.Cats.GetAll();
            return await _unitOfWork.CatsFinder.Count() !=0 ? new ActionResult<IEnumerable<Cat>>(cats) : NotFound();
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<Cat>> Get(string name)
        {
            var getCat = await _unitOfWork.CatsFinder.FindByName(name);
            return (getCat != null ? new ObjectResult(getCat) : NotFound());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cat>> Get(int id)
        {
            var getCat = await _unitOfWork.Cats.Get(id);
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
                _unitOfWork.Cats.Create(cat);
                await _unitOfWork.Commit();
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

            var getCat = await _unitOfWork.Cats.Get(cat.Id);
            if (getCat != null)
            {
                _unitOfWork.Cats.Update(cat);
                await _unitOfWork.Commit();
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
            Cat cat = await _unitOfWork.Cats.Get(id);
            if (cat == null)
            {
                return NotFound();
            }
            else
            {
                _unitOfWork.Cats.Delete(id);
                await _unitOfWork.Commit();
                return Ok(cat);
            }
        }
    }
}