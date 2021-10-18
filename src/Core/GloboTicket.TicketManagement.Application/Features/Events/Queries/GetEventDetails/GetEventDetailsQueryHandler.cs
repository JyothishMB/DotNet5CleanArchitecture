using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Domain.Entities;
using MediatR;

namespace GloboTicket.TicketManagement.Application.Features.Events.Queries.GetEventDetails
{
    public class GetEvenDetailsQueryHandler : IRequestHandler<GetEventDetailsQuery, GetEventDetailsQueryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Event> _eventRepository;
        private readonly IAsyncRepository<Category> _categoryRepository;
        public GetEvenDetailsQueryHandler(IMapper mapper
            , IAsyncRepository<Event> eventRepository
            , IAsyncRepository<Category> categoryRepository)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
            _categoryRepository = categoryRepository;

        }

        public async Task<GetEventDetailsQueryResponse> Handle(GetEventDetailsQuery request, CancellationToken cancellationToken)
        {
            var response = new GetEventDetailsQueryResponse();
            var @event = await _eventRepository.GetByIdAsync(request.Id);
            if(@event != null)
            {
                var eventDetailDto = _mapper.Map<EventDetailVm>(@event);

                var category = await _categoryRepository.GetByIdAsync(@event.CategoryId);
                eventDetailDto.Category = _mapper.Map<CategoryDto>(category);

                response.eventDetailVm = eventDetailDto;
            }
            else
                response.Message = "Event not found";
                
            return response;
        }

    }
}