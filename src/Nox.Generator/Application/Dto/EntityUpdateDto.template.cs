﻿// Generated

#nullable enable

using Nox.Abstractions;
using Nox.Types;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace {{codeGeneratorState.ApplicationNameSpace }}.Dto;

/// <summary>
/// {{entity.Description}}.
/// </summary>
public partial class {{className}}
{    
{{- for attribute in entity.Attributes }}
    {{- if componentsInfo[attribute.Name].IsUpdatable == false -}}
    {{ continue; }}
    {{- end}}
    /// <summary>
    /// {{attribute.Description}} ({{if attribute.IsRequired}}Required{{else}}Optional{{end}}).
    /// </summary>
    {{- if attribute.IsRequired}}
    [Required(ErrorMessage = "{{attribute.Name}} is required")]
    {{ end}}
    {{ if componentsInfo[attribute.Name].IsSimpleType -}}
    public {{componentsInfo[attribute.Name].ComponentType}}{{ if !attribute.IsRequired}}?{{end}} {{attribute.Name}} { get; set; }{{if attribute.IsRequired}} = default!;{{end}}
    {{- else -}}
    public {{attribute.Type}}Dto{{- if !attribute.IsRequired}}?{{end}} {{attribute.Name}} { get; set; }{{if attribute.IsRequired}} = default!;{{end}}
    {{- end}}
{{- end }}
{{- for relationship in entity.Relationships}}
    {{- if relationship.WithSingleEntity && relationship.ShouldGenerateForeignOnThisSide}}

    /// <summary>
    /// {{entity.Name}} {{relationship.Description}} {{relationship.Relationship}} {{relationship.EntityPlural}}
    /// </summary>
    {{ if relationship.Relationship == "ExactlyOne" }}[Required(ErrorMessage = "{{relationship.Name}} is required")]{{-end}}
    public System.{{relationship.ForeignKeyPrimitiveType}}{{if relationship.Relationship == "ZeroOrOne"}}?{{end}} {{relationship.Name}}Id { get; set; } = default!;
    {{-end}}
{{- end }}
}