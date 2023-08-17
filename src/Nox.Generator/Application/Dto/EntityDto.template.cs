﻿// Generated

#nullable enable

using AutoMapper;
using MediatR;

using Microsoft.AspNetCore.Http;

using System.ComponentModel.DataAnnotations.Schema;

using Nox.Types;
using Nox.Domain;
using {{codeGeneratorState.DataTransferObjectsNameSpace}};
using {{codeGeneratorState.DomainNameSpace}};

namespace {{codeGeneratorState.ApplicationNameSpace}}.Dto;

/// <summary>
/// {{entity.Description}}.
/// </summary>
[AutoMap(typeof({{entity.Name}}CreateDto))]
public partial class {{className}} 
{
{{- for key in entity.Keys }}

    /// <summary>
    /// {{key.Description}} (Required).
    /// </summary>    
    {{ if key.Type == "Entity" -}}
    public {{SingleKeyPrimitiveTypeForEntity key.EntityTypeOptions.Entity}} {{key.Name}} { get; set; } = default!; 
    {{- else -}}
    public {{entity.KeysFlattenComponentsType[key.Name]}} {{key.Name}} { get; set; } = default!; 
    {{- end}}
{{- end }}
{{- for attribute in entity.Attributes }}

    /// <summary>
    /// {{attribute.Description}} ({{if attribute.IsRequired}}Required{{else}}Optional{{end}}).
    /// </summary>
    {{ if componentsInfo[attribute.Name].IsSimpleType -}}
        {{- if attribute.Type == "Formula" -}}
    [NotMapped]
        {{- end -}}
    public {{componentsInfo[attribute.Name].ComponentType}}{{ if !attribute.IsRequired}}?{{end}} {{attribute.Name}} { get; set; } {{if attribute.IsRequired}}= default!;{{end}}
    {{- else -}}
    public {{attribute.Type}}Dto{{ if !attribute.IsRequired}}?{{end}} {{attribute.Name}} { get; set; } {{if attribute.IsRequired}}= default!;{{end}}
    {{- end}}
{{- end }}
{{- ######################################### Relationships###################################################### -}}
{{- for relationship in entity.Relationships }}

    /// <summary>
    /// {{entity.Name}} {{relationship.Description}} {{relationship.Relationship}} {{relationship.EntityPlural}}
    /// </summary>
    {{- if relationship.Relationship == "ZeroOrMany" || relationship.Relationship == "OneOrMany"}}
    public virtual List<{{relationship.Entity}}Dto> {{relationship.EntityPlural}} { get; set; } = new();
    {{- else}}
        {{- if relationship.ShouldGenerateForeignOnThisSide}}  
    //EF maps ForeignKey Automatically
    public virtual string {{if relationship.Relationship == "ZeroOrOne"}}?{{end}}{{relationship.Entity}}Id { get; set; } = null!;
        {{- end}}
    public virtual {{relationship.Entity}}Dto {{if relationship.Relationship == "ZeroOrOne"}}?{{end}}{{relationship.Entity}} { get; set; } = null!;
    {{-end}}
{{- end }}
{{- for relationship in entity.OwnedRelationships #TODO how to reuse as partial template?}}

    /// <summary>
    /// {{entity.Name}} {{relationship.Description}} {{relationship.Relationship}} {{relationship.EntityPlural}}
    /// </summary>
	[AutoExpand]
    {{- if relationship.Relationship == "ZeroOrMany" || relationship.Relationship == "OneOrMany"}}
    public virtual List<{{relationship.Entity}}> {{relationship.EntityPlural}} { get; set; } = new();
    {{- if (relationship.EntityPlural) != relationship.Name}}
    
    public List<{{relationship.Entity}}Dto> {{relationship.Name}} => {{relationship.EntityPlural}};
    {{- end}}
    {{- else}}
    public virtual {{relationship.Entity}}Dto {{if relationship.Relationship == "ZeroOrOne"}}?{{end}} {{relationship.EntityPlural}} { get; set; } = null!;
    {{-end}}
{{- end }}

{{- if isAudited #TODO do not expose Deleted on end points??}}

    public System.DateTime? DeletedAtUtc { get; set }

    public System.Boolean? IsDeleted => DeletedAtUtc is not null;
{{- end}}
}