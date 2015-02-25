using System.Web.Http;

namespace SmsService.Controllers
{
    public class BaseController<TContract, TRepository> : ApiController
    {
        protected readonly TRepository Repository;

        protected BaseController(TRepository repository)
        {
            Repository = repository;
        }
    }
}