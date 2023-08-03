﻿// Generated

#nullable enable

using MediatR;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Nox.Solution;
using Nox.Domain;
using Nox.Factories;
using Nox.Types;
using Nox.Application;
using Nox.Extensions;
using {{codeGeneratorState.ODataNameSpace}};
using {{codeGeneratorState.DomainNameSpace}};


namespace {{codeGeneratorState.ApplicationNameSpace}};

public class {{className}}: EntityFactoryBase<{{entity.Name}}Dto, {{entity.Name}}>
{
    public  {{className}}(NoxSolution noxSolution, IServiceProvider serviceProvider): base(noxSolution, serviceProvider) { }

    protected override void MapEntity({{entity.Name}} entity, Entity entityDefinition, {{entity.Name}}Dto dto)
    {
    #pragma warning disable CS0168 // Variable is declared but never used        
        dynamic? noxTypeValue;
    #pragma warning restore CS0168 // Variable is declared but never used
    {{- for attribute in entity.Attributes }}            
    {{ if attribute.Type == "Text" || attribute.Type == "Number" }}
        noxTypeValue =  CreateNoxType<{{attribute.Type}}>(entityDefinition,"{{attribute.Name}}",dto.{{attribute.Name}});
        if(noxTypeValue != null)
        {        
            entity.{{attribute.Name}} = noxTypeValue;
        }
    {{- else -}}
        // TODO map {{attribute.Name}} {{attribute.Type}} remaining types and remove if else
    {{- end}}        
    {{- end }}
    }
}