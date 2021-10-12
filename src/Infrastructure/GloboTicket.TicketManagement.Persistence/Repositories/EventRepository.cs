using System;
using System.Linq;
using System.Threading.Tasks;
using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Domain.Entities;

namespace GloboTicket.TicketManagement.Persistence.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        private readonly GloboTicketDbContext _context;
        public EventRepository(GloboTicketDbContext context) : base(context)
        {
            _context = context;

        }
        public Task<bool> IsEventNameAndDateUnique(string Name, DateTime Date)
        {
            var matches = _context.Events.Any(e => e.Name.Equals(Name) && e.Date.Date.Equals(Date.Date));
            return Task.FromResult(matches);
        }
    }
}