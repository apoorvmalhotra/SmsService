using System.Data.Entity;

namespace Sms.Data
{
    public interface IMessageHandlerEntities
    {
         int SaveChanges();

         DbSet<Message> Messages { get; set; }
    }
}