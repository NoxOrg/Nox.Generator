﻿﻿// Generated

#nullable enable

using MediatR;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nox.Application;
using Nox.Application.Commands;
using Nox.Factories;
using Nox.Solution;
using Nox.Types;

using Cryptocash.Infrastructure.Persistence;
using Cryptocash.Domain;
using Cryptocash.Application.Dto;
using EmployeePhoneNumber = Cryptocash.Domain.EmployeePhoneNumber;

namespace Cryptocash.Application.Commands;
public record AddEmployeePhoneNumberCommand(EmployeeKeyDto ParentKeyDto, EmployeePhoneNumberCreateDto EntityDto) : IRequest <EmployeePhoneNumberKeyDto?>;

public partial class AddEmployeePhoneNumberCommandHandler: CommandBase<AddEmployeePhoneNumberCommand, EmployeePhoneNumber>, IRequestHandler <AddEmployeePhoneNumberCommand, EmployeePhoneNumberKeyDto?>
{
	public CryptocashDbContext DbContext { get; }

	public AddEmployeePhoneNumberCommandHandler(
		CryptocashDbContext dbContext,
		NoxSolution noxSolution,
		IServiceProvider serviceProvider): base(noxSolution, serviceProvider)
	{
		DbContext = dbContext;		
	}

	public async Task<EmployeePhoneNumberKeyDto?> Handle(AddEmployeePhoneNumberCommand request, CancellationToken cancellationToken)
	{
		OnExecuting(request);
		var keyId = CreateNoxTypeForKey<Employee,DatabaseNumber>("Id", request.ParentKeyDto.keyId);

		var parentEntity = await DbContext.Employees.FindAsync(keyId);
		if (parentEntity == null)
		{
			return null;
		}

		var entity = request.EntityDto.ToEntity();
		
		parentEntity.EmployeePhoneNumbers.Add(entity);

		OnCompleted(entity);
	
		DbContext.Entry(parentEntity).State = EntityState.Modified;
		var result = await DbContext.SaveChangesAsync();
		return new EmployeePhoneNumberKeyDto(entity.Id.Value);
	}
}