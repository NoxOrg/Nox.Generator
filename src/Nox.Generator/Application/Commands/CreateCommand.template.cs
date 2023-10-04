﻿{{- func fieldFactoryName
	ret (string.downcase $0 + "Factory")
end -}}
{{- func relatedKeyName
	ret ("relatedKey" + $0)
end -}}
{{- func keysQuery(keyNames)	
	ret (keyNames | array.each @relatedKeyName | array.join ", ")
end -}}
﻿// Generated

#nullable enable

using MediatR;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
{{- if (entity.Persistence?.IsAudited ?? true)}}
using Nox.Abstractions;
{{- end}}
using Nox.Application;
using Nox.Application.Commands;
using Nox.Exceptions;
using Nox.Extensions;
using Nox.Factories;
using Nox.Solution;

using {{codeGeneratorState.PersistenceNameSpace}};
using {{codeGeneratorState.DomainNameSpace}};
using {{codeGeneratorState.ApplicationNameSpace}}.Dto;
using {{entity.Name}}Entity = {{codeGeneratorState.DomainNameSpace}}.{{entity.Name}};

namespace {{codeGeneratorState.ApplicationNameSpace}}.Commands;

public record Create{{entity.Name}}Command({{entity.Name}}CreateDto EntityDto) : IRequest<{{entity.Name}}KeyDto>;

internal partial class Create{{entity.Name}}CommandHandler : Create{{entity.Name}}CommandHandlerBase
{
	public Create{{entity.Name}}CommandHandler(
		{{codeGeneratorState.Solution.Name}}DbContext dbContext,
		NoxSolution noxSolution,
		{{- for relatedEntity in relatedEntities }}
		IEntityFactory<{{codeGeneratorState.DomainNameSpace}}.{{relatedEntity}}, {{relatedEntity}}CreateDto, {{relatedEntity}}UpdateDto> {{fieldFactoryName relatedEntity}},
		{{- end }}
		IEntityFactory<{{entity.Name}}Entity, {{entity.Name}}CreateDto, {{entity.Name}}UpdateDto> entityFactory)
		: base(dbContext, noxSolution, {{- for relatedEntity in relatedEntities}}{{fieldFactoryName relatedEntity}}, {{end}}entityFactory)
	{
	}
}


internal abstract class Create{{entity.Name}}CommandHandlerBase : CommandBase<Create{{entity.Name}}Command,{{entity.Name}}Entity>, IRequestHandler <Create{{entity.Name}}Command, {{entity.Name}}KeyDto>
{
	private readonly {{codeGeneratorState.Solution.Name}}DbContext _dbContext;
	private readonly IEntityFactory<{{entity.Name}}Entity, {{entity.Name}}CreateDto, {{entity.Name}}UpdateDto> _entityFactory;
	{{- for relatedEntity in relatedEntities }}
	private readonly IEntityFactory<{{codeGeneratorState.DomainNameSpace}}.{{relatedEntity}}, {{relatedEntity}}CreateDto, {{relatedEntity}}UpdateDto> _{{fieldFactoryName relatedEntity}};
	{{- end }}

	public Create{{entity.Name}}CommandHandlerBase(
		{{codeGeneratorState.Solution.Name}}DbContext dbContext,
		NoxSolution noxSolution,
		{{- for relatedEntity in relatedEntities }}
		IEntityFactory<{{codeGeneratorState.DomainNameSpace}}.{{relatedEntity}}, {{relatedEntity}}CreateDto, {{relatedEntity}}UpdateDto> {{fieldFactoryName relatedEntity}},
		{{- end }}
		IEntityFactory<{{entity.Name}}Entity, {{entity.Name}}CreateDto, {{entity.Name}}UpdateDto> entityFactory): base(noxSolution)
	{
		_dbContext = dbContext;
		_entityFactory = entityFactory;
		{{- for relatedEntity in relatedEntities }}
		_{{fieldFactoryName relatedEntity}} = {{fieldFactoryName relatedEntity}};
		{{- end }}
	}

	public virtual async Task<{{entity.Name}}KeyDto> Handle(Create{{entity.Name}}Command request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		OnExecuting(request);

		var entityToCreate = _entityFactory.CreateEntity(request.EntityDto);

	{{- for relationship in entity.Relationships }}
		{{- if relationship.WithSingleEntity }}
		if(request.EntityDto.{{relationship.Name}}Id is not null)
		{
			{{- relatedEntity =  relationship.Related.Entity }}
			{{- if (array.size relatedEntity.Keys) == 1 }}

			{{- key = array.first relatedEntity.Keys }}
			var relatedKey = {{codeGeneratorState.DomainNameSpace}}.{{relatedEntity.Name}}Metadata.Create{{key.Name}}(request.EntityDto.{{relationship.Name}}Id.NonNullValue<{{relationship.ForeignKeyPrimitiveType}}>());
			var relatedEntity = await _dbContext.{{relatedEntity.PluralName}}.FindAsync(relatedKey);
			
			{{- else }}

			{{- for key in relatedEntity.Keys }}
			var relatedKey{{key.Name}} = {{codeGeneratorState.DomainNameSpace}}.{{relatedEntity.Name}}Metadata.Create{{key.Name}}request.EntityDto.{{relationship.Name}}Id!.key{{key.Name}});
			{{- end }}
			var relatedEntity = await _dbContext.{{relatedEntity.PluralName}}.FindAsync({{relatedEntity.Keys | array.map "Name" | keysQuery}});
			
			{{- end }}
			if(relatedEntity is not null)
				entityToCreate.CreateRefTo{{relationship.Name}}(relatedEntity);
			else
				throw new RelatedEntityNotFoundException("{{relationship.Name}}", request.EntityDto.{{relationship.Name}}Id.NonNullValue<{{relationship.ForeignKeyPrimitiveType}}>().ToString());
		}
		else if(request.EntityDto.{{relationship.Name}} is not null)
		{
			var relatedEntity = _{{fieldFactoryName relationship.Entity}}.CreateEntity(request.EntityDto.{{relationship.Name}});
			entityToCreate.CreateRefTo{{relationship.Name}}(relatedEntity);
		}
		{{- else}}
		foreach(var relatedCreateDto in request.EntityDto.{{relationship.Name}})
		{
			var relatedEntity = _{{fieldFactoryName relationship.Entity}}.CreateEntity(relatedCreateDto);
			entityToCreate.CreateRefTo{{relationship.Name}}(relatedEntity);
		}
		{{-end}}
	{{- end }}

		OnCompleted(request, entityToCreate);
		_dbContext.{{entity.PluralName}}.Add(entityToCreate);
		await _dbContext.SaveChangesAsync();
		return new {{entity.Name}}KeyDto({{primaryKeysQuery}});
	}
}