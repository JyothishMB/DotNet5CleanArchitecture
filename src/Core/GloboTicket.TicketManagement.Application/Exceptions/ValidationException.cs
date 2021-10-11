using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace GloboTicket.TicketManagement.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public List<string> ValidationErrors { get; set; }
        public ValidationException(ValidationResult validationResult)
        {
            ValidationErrors = new List<string>();

            foreach(var ValidationError in validationResult.Errors)
            {
                ValidationErrors.Add(ValidationError.ErrorMessage);
            }
        }
    }
}