﻿{{- relationshipName = GetNavigationPropertyName parent relationship }}﻿
{{- func keyType(key)
   ret (key.Type == "EntityId") ? (SingleKeyPrimitiveTypeForEntity key.EntityIdTypeOptions.Entity) : (SinglePrimitiveTypeForKey key)
end -}}
﻿// Generated

#nullable enable

using MediatR;
using Microsoft.EntityFrameworkCore;
using Nox.Application.Commands;
using Nox.Solution;
using Nox.Types;
using Nox.Application.Factories;
using Nox.Extensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using {{codeGeneratorState.PersistenceNameSpace}};
using {{codeGeneratorState.DomainNameSpace}};
using {{codeGeneratorState.ApplicationNameSpace}}.Dto;
using {{entity.Name}}Entity = {{codeGeneratorState.DomainNameSpace}}.{{entity.Name}};

namespace {{codeGeneratorState.ApplicationNameSpace}}.Commands;

public partial record Update{{relationshipName}}For{{parent.Name}}Command({{parent.Name}}KeyDto ParentKeyDto, {{entity.Name}}UpsertDto EntityDto, System.Guid? Etag) : IRequest <{{entity.Name}}KeyDto?>;

internal partial class Update{{relationshipName}}For{{parent.Name}}CommandHandler : Update{{relationshipName}}For{{parent.Name}}CommandHandlerBase
{
	public Update{{relationshipName}}For{{parent.Name}}CommandHandler(
        AppDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<{{entity.Name}}Entity, {{entity.Name}}UpsertDto, {{entity.Name}}UpsertDto> entityFactory)
		: base(dbContext, noxSolution, entityFactory)
	{
	}
}

internal partial class Update{{relationshipName}}For{{parent.Name}}CommandHandlerBase : CommandBase<Update{{relationshipName}}For{{parent.Name}}Command, {{entity.Name}}Entity>, IRequestHandler <Update{{relationshipName}}For{{parent.Name}}Command, {{entity.Name}}KeyDto?>
{
	public AppDbContext DbContext { get; }
	private readonly IEntityFactory<{{entity.Name}}Entity, {{entity.Name}}UpsertDto, {{entity.Name}}UpsertDto> _entityFactory;

	public Update{{relationshipName}}For{{parent.Name}}CommandHandlerBase(
        AppDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<{{entity.Name}}Entity, {{entity.Name}}UpsertDto, {{entity.Name}}UpsertDto> entityFactory) : base(noxSolution)
	{
		DbContext = dbContext;
		_entityFactory = entityFactory;
	}

	public virtual async Task<{{entity.Name}}KeyDto?> Handle(Update{{relationshipName}}For{{parent.Name}}Command request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		await OnExecutingAsync(request);

		{{- for key in parent.Keys }}
		var key{{key.Name}} = {{codeGeneratorState.DomainNameSpace}}.{{parent.Name}}Metadata.Create{{key.Name}}(request.ParentKeyDto.key{{key.Name}});
		{{- end }}
		var parentEntity = await DbContext.{{parent.PluralName}}.FindAsync({{parentKeysFindQuery}});
		if (parentEntity == null)
		{
			return null;
		}

		{{- if relationship.WithSingleEntity }}
		await DbContext.Entry(parentEntity).Reference(e => e.{{relationshipName}}).LoadAsync(cancellationToken);
		var entity = parentEntity.{{relationshipName}};
		{{ else }}
		await DbContext.Entry(parentEntity).Collection(p => p.{{relationshipName}}).LoadAsync(cancellationToken);
		{{- for key in entity.Keys }}
		var owned{{key.Name}} = {{codeGeneratorState.DomainNameSpace}}.{{entity.Name}}Metadata.Create{{key.Name}}(request.EntityDto.{{key.Name}}.NonNullValue<{{keyType key}}>());
		{{- end }}
		var entity = parentEntity.{{relationshipName}}.SingleOrDefault(x => {{ownedKeysFindQuery}});
		{{- end }}
		if (entity == null)
		{
			return null;
		}

		_entityFactory.UpdateEntity(entity, request.EntityDto, DefaultCultureCode);
		parentEntity.Etag = request.Etag.HasValue ? request.Etag.Value : System.Guid.Empty;
		await OnCompletedAsync(request, entity);

		DbContext.Entry(parentEntity).State = EntityState.Modified;
		var result = await DbContext.SaveChangesAsync();
		if (result < 1)
		{
			return null;
		}

		return new {{entity.Name}}KeyDto({{primaryKeysReturnQuery}});
	}
}

{{- if (entity.Keys | array.size) > 0 }}

public class Update{{relationshipName}}For{{parent.Name}}Validator : AbstractValidator<Update{{relationshipName}}For{{parent.Name}}Command>
{
    public Update{{relationshipName}}For{{parent.Name}}Validator(ILogger<Update{{relationshipName}}For{{parent.Name}}Command> logger)
    {
		{{- for key in entity.Keys }}
		RuleFor(x => x.EntityDto.{{key.Name}}).NotNull().WithMessage("{{key.Name}} is required.");
        {{- end }}
    }
}
{{- end }}