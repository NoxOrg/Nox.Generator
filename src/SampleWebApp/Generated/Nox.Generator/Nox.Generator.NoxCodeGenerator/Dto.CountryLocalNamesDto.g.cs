﻿// Generated

#nullable enable

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper;
using MediatR;
using Nox.Types;
using Nox.Domain;
using SampleWebApp.Application.DataTransferObjects;
using SampleWebApp.Domain;

namespace SampleWebApp.Application.Dto;

/// <summary>
/// The name of a country in other languages.
/// </summary>
[AutoMap(typeof(CountryLocalNamesCreateDto))]
[PrimaryKey(nameof(Id))]
public partial class CountryLocalNamesDto : AuditableEntityBase
{

    /// <summary>
    ///  (Required).
    /// </summary>
    [Key, Column(Order=1)]
    public System.String Id { get; set; } = default!;
}