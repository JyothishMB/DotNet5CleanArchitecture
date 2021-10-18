using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GloboTicket.TicketManagement.Application.Contracts.Infrastructure;
using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Application.Models.Mail;
using GloboTicket.TicketManagement.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GloboTicket.TicketManagement.Application.Features.Events.Commands.CreateEvent
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IEventRepository _eventRepository;
        private readonly IEmailService _emailService;
        private readonly ILogger<CreateEventCommandHandler> _logger;

        public CreateEventCommandHandler(IMapper mapper
            , IEventRepository eventRepository
            , IEmailService emailService
            , ILogger<CreateEventCommandHandler> logger)
        {
            _emailService = emailService;
            _mapper = mapper;
            _eventRepository = eventRepository;
            _logger = logger;
        }
        public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateEventCommandValidator(_eventRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new Exceptions.ValidationException(validationResult);

            var @event = _mapper.Map<Event>(request);

            @event = await _eventRepository.AddAsync(@event);

            var mail = new Email() 
            { 
                To = "jyothish@jyothish.com", 
                Subject = "Event created", 
                Body = "Event Created" 
            };

            try
            {
                 await _emailService.SendMail(mail);
            }
            catch (Exception ex) 
            {
                _logger.LogError($"Mail sending operation for Event {@event.EventId} failed due to {ex.Message.ToString()}");
            }

            return @event.EventId;
        }
    }
}