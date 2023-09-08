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

using {{codeGeneratorState.PersistenceNameSpace}};
using {{codeGeneratorState.DomainNameSpace}};
using {{codeGeneratorState.ApplicationNameSpace}}.Dto;
using {{entity.Name}} = {{codeGeneratorState.DomainNameSpace}}.{{entity.Name}};

namespace {{codeGeneratorState.ApplicationNameSpace}}.Commands;
public record Add{{entity.Name}}Command({{parent.Name}}KeyDto ParentKeyDto, {{entity.Name}}CreateDto EntityDto) : IRequest <{{entity.Name}}KeyDto?>;

public partial class Add{{entity.Name}}CommandHandler: CommandBase<Add{{entity.Name}}Command, {{entity.Name}}>, IRequestHandler <Add{{entity.Name}}Command, {{entity.Name}}KeyDto?>
{
	private readonly {{codeGeneratorState.Solution.Name}}DbContext _dbContext;
	private readonly IEntityFactory<{{entity.Name}},{{entity.Name}}CreateDto> _entityFactory;

	public Add{{entity.Name}}CommandHandler(
		{{codeGeneratorState.Solution.Name}}DbContext dbContext,
		NoxSolution noxSolution,
        IEntityFactory<{{entity.Name}},{{entity.Name}}CreateDto> entityFactory,
		IServiceProvider serviceProvider): base(noxSolution, serviceProvider)
	{
		_dbContext = dbContext;
		_entityFactory = entityFactory;	
	}

	public async Task<{{entity.Name}}KeyDto?> Handle(Add{{entity.Name}}Command request, CancellationToken cancellationToken)
	{
		OnExecuting(request);

		{{- for key in parent.Keys }}
		var key{{key.Name}} = CreateNoxTypeForKey<{{parent.Name}},{{SingleTypeForKey key}}>("{{key.Name}}", request.ParentKeyDto.key{{key.Name}});
		{{- end }}

		var parentEntity = await _dbContext.{{parent.PluralName}}.FindAsync({{parentKeysFindQuery}});
		if (parentEntity == null)
		{
			return null;
		}

		var entity = _entityFactory.CreateEntity(request.EntityDto);

		{{- for key in entity.Keys ~}}
		{{- if key.Type == "Nuid" }}
		entityToCreate.Ensure{{key.Name}}();
		{{- end }}
		{{- end }}
		
		parentEntity.{{entity.PluralName}}.Add(entity);

		OnCompleted(entity);
	
		_dbContext.Entry(parentEntity).State = EntityState.Modified;
		var result = await _dbContext.SaveChangesAsync();
		if (result < 1)
		{
			return null;
		}

		return new {{entity.Name}}KeyDto({{primaryKeysReturnQuery}});
	}
}