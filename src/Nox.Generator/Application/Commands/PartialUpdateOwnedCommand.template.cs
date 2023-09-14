﻿﻿// Generated

#nullable enable

using MediatR;
using Microsoft.EntityFrameworkCore;
using Nox.Application.Commands;
using Nox.Solution;
using Nox.Types;
using Nox.Factories;
using {{codeGeneratorState.PersistenceNameSpace}};
using {{codeGeneratorState.DomainNameSpace}};
using {{codeGeneratorState.ApplicationNameSpace}}.Dto;

namespace {{codeGeneratorState.ApplicationNameSpace}}.Commands;
{{- if isSingleRelationship }}
public record PartialUpdate{{entity.Name}}Command({{parent.Name}}KeyDto ParentKeyDto, Dictionary<string, dynamic> UpdatedProperties, System.Guid? Etag) : IRequest <{{entity.Name}}KeyDto?>;
{{ else }}
public record PartialUpdate{{entity.Name}}Command({{parent.Name}}KeyDto ParentKeyDto, {{entity.Name}}KeyDto EntityKeyDto, Dictionary<string, dynamic> UpdatedProperties, System.Guid? Etag) : IRequest <{{entity.Name}}KeyDto?>;
{{- end }}

public partial class PartialUpdate{{entity.Name}}CommandHandler: CommandBase<PartialUpdate{{entity.Name}}Command, {{entity.Name}}>, IRequestHandler <PartialUpdate{{entity.Name}}Command, {{entity.Name}}KeyDto?>
{
	public {{codeGeneratorState.Solution.Name}}DbContext DbContext { get; }
	public IEntityMapper<{{entity.Name}}> EntityMapper { get; }

	public PartialUpdate{{entity.Name}}CommandHandler(
		{{codeGeneratorState.Solution.Name}}DbContext dbContext,
		NoxSolution noxSolution,
		IServiceProvider serviceProvider,
		IEntityMapper<{{entity.Name}}> entityMapper): base(noxSolution, serviceProvider)
	{
		DbContext = dbContext;
		EntityMapper = entityMapper;
	}

	public async Task<{{entity.Name}}KeyDto?> Handle(PartialUpdate{{entity.Name}}Command request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		OnExecuting(request);

		{{- for key in parent.Keys }}
		var key{{key.Name}} = CreateNoxTypeForKey<{{parent.Name}},{{SingleTypeForKey key}}>("{{key.Name}}", request.ParentKeyDto.key{{key.Name}});
		{{- end }}

		var parentEntity = await DbContext.{{parent.PluralName}}.FindAsync({{parentKeysFindQuery}});
		if (parentEntity == null)
		{
			return null;
		}	

		{{- if isSingleRelationship }}
		var entity = parentEntity.{{entity.Name}};
		{{ else }}
		{{- for key in entity.Keys }}
		var owned{{key.Name}} = CreateNoxTypeForKey<{{entity.Name}},{{SingleTypeForKey key}}>("{{key.Name}}", request.EntityKeyDto.key{{key.Name}});
		{{- end }}
		var entity = parentEntity.{{entity.PluralName}}.SingleOrDefault(x => {{ownedKeysFindQuery}});
		{{- end }}	
		if (entity == null)
		{
			return null;
		}

		EntityMapper.PartialMapToEntity(entity, GetEntityDefinition<{{entity.Name}}>(), request.UpdatedProperties);
		parentEntity.Etag = request.Etag.HasValue ? request.Etag.Value : System.Guid.Empty;

		OnCompleted(request, entity);
	
		DbContext.Entry(parentEntity).State = EntityState.Modified;
		var result = await DbContext.SaveChangesAsync();
		if (result < 1)
		{
			return null;
		}

		return new {{entity.Name}}KeyDto({{primaryKeysReturnQuery}});
	}
}