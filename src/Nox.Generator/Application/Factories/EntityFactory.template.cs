using System;

{{- ownedEntities = entity.OwnedRelationships | array.map "Entity" }}
{{- func fieldFactoryName 
    ret (string.downcase $0 + "Factory")
end -}}
// Generated

#nullable enable

using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using MediatR;

using Nox.Abstractions;
using Nox.Solution;
using Nox.Domain;
using Nox.Factories;
using Nox.Types;
using Nox.Application;
using Nox.Extensions;
using Nox.Exceptions;

using {{codeGeneratorState.ApplicationNameSpace}}.Dto;
using {{codeGeneratorState.DomainNameSpace}};
using {{entity.Name}} = {{codeGeneratorState.DomainNameSpace}}.{{entity.Name}};

namespace {{codeGeneratorState.ApplicationNameSpace}}.Factories;

public abstract class {{className}}Base: IEntityFactory<{{entity.Name}},{{entity.Name}}CreateDto>
{
    {{- for ownedEntity in ownedEntities #Factories Properties for owned entitites}}
    protected IEntityFactory<{{ownedEntity}},{{ownedEntity}}CreateDto> {{ownedEntity}}Factory {get;}
    {{- end }}

    public {{className}}Base
    (
        {{- for ownedEntity in ownedEntities #Factories Properties for owned entitites}}
        IEntityFactory<{{ownedEntity}},{{ownedEntity}}CreateDto> {{fieldFactoryName ownedEntity}}{{if !for.last}},{{end}}
        {{- end }}
        )
    {
        {{- for ownedEntity in ownedEntities #Factories Properties for owned entitites}}        
        {{ ownedEntity}}Factory = {{fieldFactoryName ownedEntity}};
        {{- end }}
    }

    public virtual {{entity.Name}} CreateEntity({{entity.Name}}CreateDto createDto)
    {
        return ToEntity(createDto);
    }
    private {{codeGeneratorState.DomainNameSpace}}.{{ entity.Name }} ToEntity({{entity.Name}}CreateDto createDto)
    {
        var entity = new {{codeGeneratorState.DomainNameSpace}}.{{entity.Name}}();
        {{- for key in entity.Keys }}
            {{- if key.Type == "Nuid" || key.Type == "AutoNumber" || key.Type == "Guid" -}}
                {{ continue; -}}
            {{- end }}
        entity.{{key.Name}} = {{ entity.Name }}.Create{{key.Name}}(createDto.{{key.Name}});
        {{- end }}
        {{- for key in entity.Keys ~}}
            {{- if key.Type == "Nuid" }}
        entity.Ensure{{key.Name}}();
            {{- end }}
            {{- if key.Type == "Guid" }}
        entity.{{key.Name}} = ( createDto.{{key.Name}} == null || System.Guid.Empty.Equals(createDto.{{key.Name}}) ) ? Nox.Types.Guid.From(System.Guid.NewGuid()) : {{ entity.Name }}.Create{{key.Name}}(createDto.{{key.Name}});  
            {{- end }}
        {{- end }}
        {{- for attribute in entity.Attributes }}
            {{- if !IsNoxTypeReadable attribute.Type || attribute.Type == "Formula" -}}
                {{ continue; }}
            {{- end}}            
        {{- if !attribute.IsRequired }}
        if (createDto.{{attribute.Name}} is not null)
            {{- if IsNoxTypeSimpleType attribute.Type -}}
        entity.{{attribute.Name}} = {{codeGeneratorState.DomainNameSpace}}.{{entity.Name}}.Create{{attribute.Name}}(createDto.{{attribute.Name}}.NonNullValue<{{SinglePrimitiveTypeForKey attribute}}>());
            {{- else -}}
        entity.{{attribute.Name}} = {{codeGeneratorState.DomainNameSpace}}.{{entity.Name}}.Create{{attribute.Name}}(createDto.{{attribute.Name}}.NonNullValue<{{attribute.Type}}Dto>());
            {{- end}}
        {{- else }}
        entity.{{attribute.Name}} = {{codeGeneratorState.DomainNameSpace}}.{{entity.Name}}.Create{{attribute.Name}}(createDto.{{attribute.Name}});
        {{- end }}
        {{- end }}

        {{- for relationship in entity.Relationships }}
            {{- if relationship.Relationship == "ZeroOrMany" || relationship.Relationship == "OneOrMany"}}
        //entity.{{relationship.EntityPlural}} = {{relationship.EntityPlural}}.Select(dto => dto.ToEntity()).ToList();
            {{- else}}
        //entity.{{relationship.Entity}} = {{relationship.Entity}}{{if relationship.Relationship == "ZeroOrOne"}}?{{end}}.ToEntity();
            {{-end}}
        {{- end }}

        {{- for relationship in entity.OwnedRelationships }}
            {{- if relationship.Relationship == "ZeroOrMany" || relationship.Relationship == "OneOrMany"}}
        entity.{{relationship.EntityPlural}} = createDto.{{relationship.EntityPlural}}.Select(dto => {{relationship.Entity}}Factory.CreateEntity(dto)).ToList();
            {{- else}}
        if(createDto.{{relationship.Entity}} is not null)
        {
            entity.{{relationship.Entity}} = {{relationship.Entity}}Factory.CreateEntity(createDto.{{relationship.Entity}});
        }        
            {{-end}}
        {{- end }}
        return entity;
    }
}

public partial class {{className}} : {{className}}Base
{
    {{- if ownedEntities | array.size > 0 #Factories for owned entitites}}
    public {{className}}
    (
        {{- for ownedEntity in ownedEntities #Factories Properties for owned entitites}}
        IEntityFactory<{{ownedEntity}},{{ownedEntity}}CreateDto> {{fieldFactoryName ownedEntity}}{{if !for.last}},{{end}}
        {{- end }}
    ): base({{ ownedEntities | array.each @fieldFactoryName | array.join "," }})                      
    {}
    {{- end }}
}