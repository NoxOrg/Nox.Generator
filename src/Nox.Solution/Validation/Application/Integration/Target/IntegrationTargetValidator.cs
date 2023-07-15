using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Nox.Solution.Extensions;

namespace Nox.Solution.Validation
{
    internal class IntegrationTargetValidator: AbstractValidator<IntegrationTarget>
    {
        private readonly IEnumerable<DataConnection>? _dataConnections;
        public IntegrationTargetValidator(string integrationName, IEnumerable<DataConnection>? dataConnections)
        {
            _dataConnections = dataConnections;
            
            RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage(m => string.Format(ValidationResources.IntegrationTargetNameEmpty, integrationName));

            RuleFor(p => p.TargetType)
                .NotNull()
                .WithMessage(p => string.Format(ValidationResources.IntegrationTargetTypeEmpty, p.Name, integrationName, IntegrationTargetType.Entity.ToNameList()));

            RuleFor(t => t.DataConnectionName)
                .Must(HaveValidDataConnection)
                .WithMessage(t => string.Format(ValidationResources.IntegrationTargetDataConnectionMissing, t.Name, integrationName, t.DataConnectionName))
                .When(t => t.TargetType != IntegrationTargetType.Entity);
        }
        
        private bool HaveValidDataConnection(string? dataConnectionName)
        {
            if (!string.IsNullOrWhiteSpace(dataConnectionName))
            {
                return _dataConnections!.Any(dc => dc.Name == dataConnectionName);    
            }

            return true;
        }
    }
}