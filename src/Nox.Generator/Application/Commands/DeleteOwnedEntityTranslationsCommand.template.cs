﻿{{- func keysToString(keys, prefix = "key")
	keyNameWithPrefix(name) = ("{" + prefix + name + ".ToString()}")	
	ret (keys | array.map "Name" | array.each @keyNameWithPrefix | array.join ", ")
end -}}
// Generated

#nullable enable
using MediatR;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Nox.Application.Commands;
using Nox.Solution;
using Nox.Types;
using Nox.Exceptions;
using Nox.Extensions;
using System.CodeDom;
using {{codeGenConventions.PersistenceNameSpace}};
using {{codeGenConventions.DomainNameSpace}};
using {{entity.Name}}LocalizedEntity = {{codeGenConventions.DomainNameSpace}}.{{entity.Name}}Localized;

namespace {{codeGenConventions.ApplicationNameSpace}}.Commands;

public partial record  {{className}}({{parentPrimaryKeys}}, Nox.Types.CultureCode {{codeGenConventions.LocalizationCultureField}}) : IRequest<bool>;

internal partial class {{ className}}Handler : {{ className}}HandlerBase
{
    public {{className}}Handler(
           AppDbContext dbContext,
                  NoxSolution noxSolution) : base(dbContext, noxSolution)
    {
    }
}

internal abstract class {{ className}}HandlerBase : {{if relationship.WithSingleEntity}}CommandBase{{else}}CommandCollectionBase{{end}}<{{ className}}, {{entity.Name}}LocalizedEntity>, IRequestHandler<{{ className}}, bool>
{
    public AppDbContext DbContext { get; }

    public {{ className}}HandlerBase(
           AppDbContext dbContext,
                  NoxSolution noxSolution) : base(noxSolution)
    {
        DbContext = dbContext;
    }

    public virtual async Task<bool> Handle({{ className}} command, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await OnExecutingAsync(command);
        
        {{- for key in parent.Keys }}
		var parentKey{{key.Name}} = Dto.{{parent.Name}}Metadata.Create{{key.Name}}(command.key{{key.Name}});
		{{- end }}
        var parentEntity = await DbContext.{{parent.PluralName}}.FindAsync({{parentPrimaryKeysFindQuery}});

        EntityNotFoundException.ThrowIfNull(parentEntity, "{{parent.Name}}", $"{{keysToString parent.Keys 'parentKey' }}");

        {{~if relationship.WithSingleEntity ~}}
        var entity = await DbContext.{{entity.PluralName}}Localized.SingleOrDefaultAsync(x => {{for key in parent.Keys}}x.{{parent.Name}}{{key.Name}} == parentEntity.{{key.Name}} && {{end}}x.CultureCode == command.CultureCode, cancellationToken);
        EntityLocalizationNotFoundException.ThrowIfNull(entity, "{{parent.Name}}.{{GetNavigationPropertyName parent relationship}}", String.Empty, command.{{codeGenConventions.LocalizationCultureField}}.ToString());

        await OnCompletedAsync(command, entity);

        DbContext.Remove(entity);
        {{~else~}}
        await DbContext.Entry(parentEntity).Collection(p => p.{{GetNavigationPropertyName parent relationship}}).LoadAsync(cancellationToken);
        var entityKeys = parentEntity.{{GetNavigationPropertyName parent relationship}}.Select(x => x.{{entity.Keys[0].Name}}).ToList();
        var entities = await DbContext.{{entity.PluralName}}Localized.Where(x => entityKeys.Contains(x.{{entity.Keys[0].Name}}) && x.CultureCode == command.CultureCode).ToListAsync(cancellationToken);
        
        if (!entities.Any())
        {
            throw new EntityLocalizationNotFoundException("{{parent.Name}}.{{GetNavigationPropertyName parent relationship}}",  String.Empty, command.{{codeGenConventions.LocalizationCultureField}}.ToString());
        }

        await OnCompletedAsync(command, entities);

        DbContext.RemoveRange(entities);
        {{end}}

        await DbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}

public class {{className}}Validator : AbstractValidator<{{className}}>
{
    public {{className}}Validator(NoxSolution noxSolution)
    {
        var defaultCultureCode = Nox.Types.CultureCode.From(noxSolution!.Application!.Localization!.DefaultCulture);

		RuleFor(x => x.{{codeGenConventions.LocalizationCultureField}})
			.Must(x => x != defaultCultureCode)
			.WithMessage($"{%{{}%}nameof({{className}}){%{}}%} : {%{{}%}nameof({{className}}.{{codeGenConventions.LocalizationCultureField}}){%{}}%} cannot be the default culture code: {defaultCultureCode.Value}.");
			
    }
}