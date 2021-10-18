using GloboTicket.TicketManagement.Application.Responses;

namespace GloboTicket.TicketManagement.Application.Features.Events.Queries.GetEventDetails
{
    public class GetEventDetailsQueryResponse : BaseResponse
    {
        public GetEventDetailsQueryResponse() : base()
        {
            
        }

        public EventDetailVm eventDetailVm { get; set; }
    }
}