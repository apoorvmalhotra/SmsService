using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Emails.Data;
using EmailService.Models;
using EmailService.Utilities;

namespace EmailService.Controllers
{
    public class MessagesController : BaseApiController
    {
        private readonly IMessageRepository _repository;
        private ModelFactory _modelFactory;

        public MessagesController(IMessageRepository repository)
        {
            _repository = repository;
        }

        protected ModelFactory ModelFactory
        {
            get
            {
                if (_modelFactory == null)
                {
                    _modelFactory = new ModelFactory(Request, _repository);
                }
                return _modelFactory;
            }
        }

        [HttpPost]
        public virtual HttpResponseMessage Post(MessageModel message)
        {
            try
            {
                var entity = ModelFactory.Parse(message);

//                if (entity == null) Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Could not read the message");

                var insertedEntity = _repository.Insert(entity);
                if (insertedEntity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, ModelFactory.Map(insertedEntity));
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Could not save to the database.");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
        
    }
}
