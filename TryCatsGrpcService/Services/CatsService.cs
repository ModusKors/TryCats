using BusinessLogicLayer;
using BusinessLogicLayer.Finder;
using BusinessLogicLayer.Repository;
using BusinessLogicLayer.Service;
using Google.Protobuf.Collections;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.CompilerServices;
using TryCats6.Controllers;
using TryCatsGrpcService;

using ControllerCat = BusinessLogicLayer.Entity.Cat;


namespace TryCatsGrpcService.Services
{
    public class CatsService : Cats.CatsBase
    {
        private readonly ILogger<CatsService> _logger;

        private CatsController _catsController;

        public CatsService(ILogger<CatsService> logger, IRepository<ControllerCat> iRepository, ICatFinder iCatFinder, IUnitOfWork iUnitOfWork)
        {
            _logger = logger;

            _catsController = new CatsController(iRepository, iCatFinder, iUnitOfWork);
        }

        //Code 1
        public override async Task<GetAllCatsReply> GetAllCats(GetAllCatsRequest request, ServerCallContext context)
        {
            ActionResult<IEnumerable<ControllerCat>> result = await _catsController.Get();

            List<Cat> listCats = result.Value.Select(cat => new Cat() { Id = cat.Id, Name = cat.Name, Summary = cat.Summary, }).ToList();

            GetAllCatsReply catsReply = new GetAllCatsReply { Cats = { listCats } };

            return catsReply;
        }

        private T EjectFromActionResult<T>(ActionResult<T> actionResult)
        {
            var result = (ObjectResult)actionResult.Result; // <-- Cast is before using it.
            
            //if (actionResult.Result is NotFoundObjectResult)

            return (T)result.Value; //<-- Then you'll get no error here.
        }

        //Code 2
        public override async Task<CatReply> GetCatById(GetCatByIdRequest request, ServerCallContext context)
        {
            var result = await _catsController.Get(request.Id);

            var valueResult = EjectFromActionResult(result);

            CatReply catReply = new CatReply
            {
               Cat = CatConverter(valueResult)
            };

            return catReply;
        }

        private Cat CatConverter(ControllerCat controllerCat)
        {
            if (controllerCat != null)
            {
                Cat cat =  new Cat()
                {
                    Id = controllerCat.Id,
                    Name = controllerCat.Name,
                    Summary = controllerCat.Summary ?? "normal"
                };

                return cat;
            }
            else
            {
                return new Cat();
            }
        }

        private ControllerCat CatConverter(Cat cat)
        {
            if (cat != null)
            {
                ControllerCat controllerCat = new ()
                {
                    Id = cat.Id,
                    Name = cat.Name,
                    Summary = cat.Summary ?? "normal"
                };

                return controllerCat;
            }
            else
            {
                return new ();
            }
        }

        //Code 3
        public override async Task<CatReply> GetCatByName(GetCatByNameRequest request, ServerCallContext context)
        {
            var result = await _catsController.Get(request.Name);

            var valueResult = EjectFromActionResult(result);

            CatReply catReply = new CatReply
            {
                Cat = CatConverter(valueResult)
            };

            return catReply;
        }

        //Code 4
        public override async Task<CatReply> PostCat(PostCatRequest request, ServerCallContext context)
        {
            var result = await _catsController.Post(CatConverter(request.Cat));

            var valueResult = EjectFromActionResult(result);

            CatReply catReply = new CatReply
            {
                Cat = CatConverter(valueResult)
            };

            return catReply;
        }

        //Code 5
        public override async Task<CatReply> PutCat(PutCatRequest request, ServerCallContext context)
        {
            var result = await _catsController.Put(CatConverter(request.Cat));

            var valueResult = EjectFromActionResult(result);

            CatReply catReply = new CatReply
            {
                Cat = CatConverter(valueResult)
            };

            return catReply;
        }

        //Code 6
        public override async Task<BoolResultReply> DeleteCat(DeleteCatRequest request, ServerCallContext context)
        {
            var result = await _catsController.Delete(request.Id);

            var valueResult = EjectFromActionResult(result);

            BoolResultReply boolResultReply = new BoolResultReply() { Result = valueResult };

            return boolResultReply;
        }

    }
}