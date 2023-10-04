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
using BankNoteEntity = Cryptocash.Domain.BankNote;

namespace Cryptocash.Application.Commands;
public record CreateBankNoteForCurrencyCommand(CurrencyKeyDto ParentKeyDto, BankNoteCreateDto EntityDto, System.Guid? Etag) : IRequest <BankNoteKeyDto?>;

internal partial class CreateBankNoteForCurrencyCommandHandler : CreateBankNoteForCurrencyCommandHandlerBase
{
	public CreateBankNoteForCurrencyCommandHandler(
		CryptocashDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<BankNoteEntity, BankNoteCreateDto, BankNoteUpdateDto> entityFactory)
		: base(dbContext, noxSolution, entityFactory)
	{
	}
}
internal abstract class CreateBankNoteForCurrencyCommandHandlerBase : CommandBase<CreateBankNoteForCurrencyCommand, BankNoteEntity>, IRequestHandler<CreateBankNoteForCurrencyCommand, BankNoteKeyDto?>
{
	private readonly CryptocashDbContext _dbContext;
	private readonly IEntityFactory<BankNoteEntity, BankNoteCreateDto, BankNoteUpdateDto> _entityFactory;

	public CreateBankNoteForCurrencyCommandHandlerBase(
		CryptocashDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<BankNoteEntity, BankNoteCreateDto, BankNoteUpdateDto> entityFactory) : base(noxSolution)
	{
		_dbContext = dbContext;
		_entityFactory = entityFactory;
	}

	public virtual  async Task<BankNoteKeyDto?> Handle(CreateBankNoteForCurrencyCommand request, CancellationToken cancellationToken)
	{
		OnExecuting(request);
		var keyId = Cryptocash.Domain.CurrencyMetadata.CreateId(request.ParentKeyDto.keyId);

		var parentEntity = await _dbContext.Currencies.FindAsync(keyId);
		if (parentEntity == null)
		{
			return null;
		}

		var entity = _entityFactory.CreateEntity(request.EntityDto);
		parentEntity.CurrencyCommonBankNotes.Add(entity);
		parentEntity.Etag = request.Etag.HasValue ? request.Etag.Value : System.Guid.Empty;
		OnCompleted(request, entity);

		_dbContext.Entry(parentEntity).State = EntityState.Modified;
		var result = await _dbContext.SaveChangesAsync();
		if (result < 1)
		{
			return null;
		}

		return new BankNoteKeyDto(entity.Id.Value);
	}
}