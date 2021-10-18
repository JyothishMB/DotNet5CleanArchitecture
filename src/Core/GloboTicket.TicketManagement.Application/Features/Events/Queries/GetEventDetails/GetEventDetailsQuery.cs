using System;
using System.Collections.Generic;
using MediatR;

namespace GloboTicket.TicketManagement.Application.Features.Events.Queries.GetEventDetails
{
    public class GetEventDetailsQuery : IRequest<GetEventDetailsQueryResponse>
    {
        public Guid Id { get; set; }
    }
}