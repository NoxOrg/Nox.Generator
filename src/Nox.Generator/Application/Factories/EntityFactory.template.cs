{{- ownedEntities = entity.OwnedRelationships | array.map "Entity" }}
{{-entityCreateDto = entity.IsOwnedEntity ? (entity.Name + "UpsertDto") : (entity.Name + "CreateDto") }}
{{-entityUpdateDto = entity.IsOwnedEntity ? (entity.Name + "UpsertDto") : (entity.Name + "UpdateDto") }}
{{- func fieldFactoryName
    ret (string.downcase $0 + "Factory")
end -}}
{{- func keyType(key)
   ret (key.Type == "EntityId") ? (SingleKeyPrimitiveTypeForEntity key.EntityIdTypeOptions.Entity) : (SinglePrimitiveTypeForKey key)
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
using Nox.Application.Factories;
using Nox.Types;
using Nox.Application;
using Nox.Extensions;
using Nox.Exceptions;

using {{codeGeneratorState.ApplicationNameSpace}}.Dto;
using {{codeGeneratorState.DomainNameSpace}};
using {{entity.Name}}Entity = {{codeGeneratorState.DomainNameSpace}}.{{entity.Name}};

namespace {{codeGeneratorState.ApplicationNameSpace}}.Factories;

internal abstract class {{className}}Base : IEntityFactory<{{entity.Name}}Entity, {{entityCreateDto}}, {{entityUpdateDto}}>
{
    private static readonly Nox.Types.CultureCode _defaultCultureCode = Nox.Types.CultureCode.From("{{codeGeneratorState.Solution.Application.Localization.DefaultCulture}}");
    private readonly IRepository _repository;

    {{- for ownedEntity in ownedEntities #Factories Properties for owned entitites}}
    protected IEntityFactory<{{codeGeneratorState.DomainNameSpace}}.{{ownedEntity}}, {{ownedEntity}}UpsertDto, {{ownedEntity}}UpsertDto> {{ownedEntity}}Factory {get;}
    {{- end }}

    public {{className}}Base
    (
        {{- for ownedEntity in ownedEntities #Factories Properties for owned entitites}}
        IEntityFactory<{{codeGeneratorState.DomainNameSpace}}.{{ownedEntity}}, {{ownedEntity}}UpsertDto, {{ownedEntity}}UpsertDto> {{fieldFactoryName ownedEntity}},
        {{- end }}
        IRepository repository
        )
    {
        {{- for ownedEntity in ownedEntities #Factories Properties for owned entitites}}
        {{ ownedEntity}}Factory = {{fieldFactoryName ownedEntity}};
        {{- end }}
        _repository = repository;
    }

    public virtual {{entity.Name}}Entity CreateEntity({{entityCreateDto}} createDto)
    {
        try
        {
            return ToEntity(createDto);
        }
        catch (NoxTypeValidationException ex)
        {
            throw new Nox.Application.Factories.CreateUpdateEntityInvalidDataException(ex);
        }        
    }

    public virtual void UpdateEntity({{entity.Name}}Entity entity, {{entityUpdateDto}} updateDto, Nox.Types.CultureCode cultureCode)
    {
        UpdateEntityInternal(entity, updateDto, cultureCode);
    }

    public virtual void PartialUpdateEntity({{entity.Name}}Entity entity, Dictionary<string, dynamic> updatedProperties, Nox.Types.CultureCode cultureCode)
    {
        PartialUpdateEntityInternal(entity, updatedProperties, cultureCode);
    }

    private {{codeGeneratorState.DomainNameSpace}}.{{ entity.Name }} ToEntity({{entityCreateDto}} createDto)
    {
        var entity = new {{codeGeneratorState.DomainNameSpace}}.{{entity.Name}}();
        {{- for key in entity.Keys }}
            {{- if !IsNoxTypeCreatable key.Type || key.Type == "Guid" -}}
                {{ continue; -}}
            {{- end }}
        entity.{{key.Name}} = {{entity.Name}}Metadata.Create{{key.Name}}(createDto.{{key.Name}}{{if entity.IsOwnedEntity}}.NonNullValue<{{keyType key}}>(){{end}});
        {{- end }}
        {{- for attribute in entity.Attributes }}
            {{- if !IsNoxTypeCreatable attribute.Type -}}
                {{ continue; }}
            {{- end}}
        {{- if !attribute.IsRequired }}
        entity.SetIfNotNull(createDto.{{attribute.Name}}, (entity) => entity.{{attribute.Name}} = 
            {{- if IsNoxTypeSimpleType attribute.Type -}}
        {{codeGeneratorState.DomainNameSpace}}.{{entity.Name}}Metadata.Create{{attribute.Name}}(createDto.{{attribute.Name}}.NonNullValue<{{SinglePrimitiveTypeForKey attribute}}>()));
            {{- else -}}
        {{codeGeneratorState.DomainNameSpace}}.{{entity.Name}}Metadata.Create{{attribute.Name}}(createDto.{{attribute.Name}}.NonNullValue<{{attribute.Type}}Dto>()));
            {{- end}}
        {{- else }}
        entity.{{attribute.Name}} = {{codeGeneratorState.DomainNameSpace}}.{{entity.Name}}Metadata.Create{{attribute.Name}}(createDto.{{attribute.Name}});
        {{- end }}
        {{- end }}

        {{- for key in entity.Keys ~}}
		    {{- if key.Type == "Nuid" }}
		entity.Ensure{{key.Name}}();
		    {{- end }}
            {{- if key.Type == "Guid" }}
        entity.Ensure{{key.Name}}(createDto.{{key.Name}});
            {{- end }}
		{{- end }}

        {{- for relationship in entity.OwnedRelationships }}
            {{- relationshipName = GetNavigationPropertyName entity relationship }}
            {{- if relationship.Relationship == "ZeroOrMany" || relationship.Relationship == "OneOrMany"}}
        createDto.{{relationshipName}}.ForEach(dto => entity.CreateRefTo{{relationshipName}}({{relationship.Entity}}Factory.CreateEntity(dto)));
            {{- else}}
        if (createDto.{{relationshipName}} is not null)
        {
            entity.CreateRefTo{{relationshipName}}({{relationship.Entity}}Factory.CreateEntity(createDto.{{relationshipName}}));
        }
            {{-end}}
        {{- end }}
        return entity;
    }

    private void UpdateEntityInternal({{entity.Name}}Entity entity, {{entityUpdateDto}} updateDto, Nox.Types.CultureCode cultureCode)
    {
        {{- for attribute in entity.Attributes }}
            {{- if !IsNoxTypeUpdatable attribute.Type -}}
                {{ continue; }}
            {{- end}}
        {{ if attribute.IsLocalized }}if(IsDefaultCultureCode(cultureCode)) {{ end }}
        {{- if attribute.IsRequired -}}
        entity.{{attribute.Name}} = {{codeGeneratorState.DomainNameSpace}}.{{entity.Name}}Metadata.Create{{attribute.Name}}(updateDto.{{attribute.Name}}
            {{- if IsNoxTypeSimpleType attribute.Type -}}.NonNullValue<{{SinglePrimitiveTypeForKey attribute}}>()
            {{- else -}}.NonNullValue<{{attribute.Type}}Dto>()
            {{- end}});
        {{- else -}}
        if(updateDto.{{attribute.Name}} is null)
        {
             entity.{{attribute.Name}} = null;
        }
        else
        {
            entity.{{attribute.Name}} = {{codeGeneratorState.DomainNameSpace}}.{{entity.Name}}Metadata.Create{{attribute.Name}}(updateDto.{{attribute.Name}}
            {{- if IsNoxTypeSimpleType attribute.Type -}}.ToValueFromNonNull<{{SinglePrimitiveTypeForKey attribute}}>()
            {{- else -}}.ToValueFromNonNull<{{attribute.Type}}Dto>()
            {{- end}});
        }      
        {{- end }}
        {{- end }}

        {{- for key in entity.Keys ~}}
		    {{- if key.Type == "Nuid" }}
		entity.Ensure{{key.Name}}();
		    {{- end }}
		{{- end }}

        {{- if (entity.OwnedRelationships | array.size) > 0 }}
	    UpdateOwnedEntities(entity, updateDto, cultureCode);    
		{{- end }}
    }

    private void PartialUpdateEntityInternal({{entity.Name}}Entity entity, Dictionary<string, dynamic> updatedProperties, Nox.Types.CultureCode cultureCode)
    {
        {{- for attribute in entity.Attributes }}
            {{- if !IsNoxTypeUpdatable attribute.Type -}}
                {{ continue; }}
            {{- end}}

        if ({{- if attribute.IsLocalized }}IsDefaultCultureCode(cultureCode) && {{ end -}} updatedProperties.TryGetValue("{{attribute.Name}}", out var {{attribute.Name}}UpdateValue))
        {
            {{- if attribute.IsRequired }}
            if ({{attribute.Name}}UpdateValue == null)
            {
                throw new ArgumentException("Attribute '{{attribute.Name}}' can't be null");
            }
            {{- else }}
            if ({{attribute.Name}}UpdateValue == null) { entity.{{attribute.Name}} = null; }
            else
            {{- end }}
            {
                entity.{{attribute.Name}} = {{codeGeneratorState.DomainNameSpace}}.{{entity.Name}}Metadata.Create{{attribute.Name}}({{attribute.Name}}UpdateValue);
            }
        }

        {{- end }}

        {{- for key in entity.Keys ~}}
		    {{- if key.Type == "Nuid" }}
		entity.Ensure{{key.Name}}();
		    {{- end }}
		{{- end }}
    }

    private static bool IsDefaultCultureCode(Nox.Types.CultureCode cultureCode)
        => cultureCode == _defaultCultureCode;

    {{- if (entity.OwnedRelationships | array.size) > 0 }}

	private void UpdateOwnedEntities({{entity.Name}}Entity entity, {{entityUpdateDto}} updateDto, Nox.Types.CultureCode cultureCode)
	{
		{{- for ownedRelationship in entity.OwnedRelationships }}
			{{- navigationName = GetNavigationPropertyName entity ownedRelationship }}
			{{- key = ownedRelationship.Related.Entity.Keys | array.first }}
        
        {{- if ownedRelationship.WithSingleEntity }}
		if(updateDto.{{navigationName}} is null)
        {
            if(entity.{{navigationName}} is not null) 
                _repository.DeleteOwned(entity.{{navigationName}});
        {{- else }}
        if(!updateDto.{{navigationName}}.Any())
        { 
            _repository.DeleteOwnedRange(entity.{{navigationName}});
        {{- end }}
			entity.DeleteAllRefTo{{navigationName}}();
        }
		else
		{
			{{- if ownedRelationship.WithSingleEntity }}
            if(entity.{{navigationName}} is not null)
                {{ownedRelationship.Entity}}Factory.UpdateEntity(entity.{{navigationName}}, updateDto.{{navigationName}}, cultureCode);
            else
			    entity.CreateRefTo{{navigationName}}({{ownedRelationship.Entity}}Factory.CreateEntity(updateDto.{{navigationName}}));
			{{- else # WithMultiEntity }}
			var updated{{navigationName}} = new List<{{codeGeneratorState.DomainNameSpace}}.{{ownedRelationship.Entity}}>();
			foreach(var ownedUpsertDto in updateDto.{{navigationName}})
			{
				if(ownedUpsertDto.{{key.Name}} is null)
					updated{{navigationName}}.Add({{ownedRelationship.Entity}}Factory.CreateEntity(ownedUpsertDto));
				else
				{
					var key = {{codeGeneratorState.DomainNameSpace}}.{{ownedRelationship.Entity}}Metadata.Create{{key.Name}}(ownedUpsertDto.{{key.Name}}.NonNullValue<{{keyType key}}>());
					var ownedEntity = entity.{{navigationName}}.FirstOrDefault(x => x.{{key.Name}} == key);
					if(ownedEntity is null)
						{{- if !IsNoxTypeCreatable key.Type }}
						throw new RelatedEntityNotFoundException("{{navigationName}}.{{key.Name}}", key.ToString());
                        {{- else }}
						updated{{navigationName}}.Add({{ownedRelationship.Entity}}Factory.CreateEntity(ownedUpsertDto));
						{{- end }}
					else
					{
						{{ownedRelationship.Entity}}Factory.UpdateEntity(ownedEntity, ownedUpsertDto, cultureCode);
						updated{{navigationName}}.Add(ownedEntity);
					}
				}
			}
            _repository.DeleteOwnedRange<{{codeGeneratorState.DomainNameSpace}}.{{ownedRelationship.Entity}}>(
                entity.{{navigationName}}.Where(x => !updated{{navigationName}}.Any(upd => upd.{{key.Name}} == x.{{key.Name}})).ToList());
			entity.UpdateRefTo{{navigationName}}(updated{{navigationName}});
			{{- end }}
		}
		{{- end }}
	}
	{{- end }}
}

internal partial class {{className}} : {{className}}Base
{
    public {{className}}
    (
        {{- for ownedEntity in ownedEntities #Factories Properties for owned entitites}}
        IEntityFactory<{{codeGeneratorState.DomainNameSpace}}.{{ownedEntity}}, {{ownedEntity}}UpsertDto, {{ownedEntity}}UpsertDto> {{fieldFactoryName ownedEntity}},
        {{- end }}
        IRepository repository
    ) : base({{ ownedEntities | array.each @fieldFactoryName | array.join "," }}{{if ownedEntities | array.size > 0 }},{{end}} repository)
    {}
}