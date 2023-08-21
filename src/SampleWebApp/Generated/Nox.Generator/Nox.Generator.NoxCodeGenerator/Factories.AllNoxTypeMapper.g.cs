﻿// Generated

#nullable enable

using MediatR;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Nox.Abstractions;
using Nox.Solution;
using Nox.Domain;
using Nox.Factories;
using Nox.Types;
using Nox.Application;
using Nox.Extensions;
using Nox.Exceptions;
using SampleWebApp.Application.Dto;
using SampleWebApp.Domain;


namespace SampleWebApp.Application;

public class AllNoxTypeMapper: EntityMapperBase<AllNoxType>
{
    public  AllNoxTypeMapper(NoxSolution noxSolution, IServiceProvider serviceProvider): base(noxSolution, serviceProvider) { }

    public override void MapToEntity(AllNoxType entity, Entity entityDefinition, dynamic dto)
    {
    #pragma warning disable CS0168 // Variable is declared but never used        
        dynamic? noxTypeValue;
    #pragma warning restore CS0168 // Variable is declared but never used
            
        noxTypeValue = CreateNoxType<Nox.Types.Text>(entityDefinition, "TextId", dto.TextId);        
        if(noxTypeValue != null)
        {        
            entity.TextId = noxTypeValue;
        }

        // TODO map NuidField Nuid remaining types and remove if else
        noxTypeValue = CreateNoxType<Nox.Types.Boolean>(entityDefinition,"BooleanField",dto.BooleanField);
        if(noxTypeValue != null)
        {        
            entity.BooleanField = noxTypeValue;
        }
        noxTypeValue = CreateNoxType<Nox.Types.CountryCode2>(entityDefinition,"CountryCode2Field",dto.CountryCode2Field);
        if(noxTypeValue != null)
        {        
            entity.CountryCode2Field = noxTypeValue;
        }
        noxTypeValue = CreateNoxType<Nox.Types.CountryCode3>(entityDefinition,"CountryCode3Field",dto.CountryCode3Field);
        if(noxTypeValue != null)
        {        
            entity.CountryCode3Field = noxTypeValue;
        }
        noxTypeValue = CreateNoxType<Nox.Types.CountryNumber>(entityDefinition,"CountryNumberField",dto.CountryNumberField);
        if(noxTypeValue != null)
        {        
            entity.CountryNumberField = noxTypeValue;
        }
        noxTypeValue = CreateNoxType<Nox.Types.CultureCode>(entityDefinition,"CultureCodeField",dto.CultureCodeField);
        if(noxTypeValue != null)
        {        
            entity.CultureCodeField = noxTypeValue;
        }
        noxTypeValue = CreateNoxType<Nox.Types.CurrencyCode3>(entityDefinition,"CurrencyCode3Field",dto.CurrencyCode3Field);
        if(noxTypeValue != null)
        {        
            entity.CurrencyCode3Field = noxTypeValue;
        }
        noxTypeValue = CreateNoxType<Nox.Types.DateTime>(entityDefinition,"DateTimeField",dto.DateTimeField);
        if(noxTypeValue != null)
        {        
            entity.DateTimeField = noxTypeValue;
        }

        // TODO map FormulaField Formula remaining types and remove if else
        noxTypeValue = CreateNoxType<Nox.Types.Html>(entityDefinition,"HtmlField",dto.HtmlField);
        if(noxTypeValue != null)
        {        
            entity.HtmlField = noxTypeValue;
        }

        // TODO map LanguageCodeField LanguageCode remaining types and remove if else
        noxTypeValue = CreateNoxType<Nox.Types.Length>(entityDefinition,"LengthField",dto.LengthField);
        if(noxTypeValue != null)
        {        
            entity.LengthField = noxTypeValue;
        }
        noxTypeValue = CreateNoxType<Nox.Types.MacAddress>(entityDefinition,"MacAddressField",dto.MacAddressField);
        if(noxTypeValue != null)
        {        
            entity.MacAddressField = noxTypeValue;
        }
        noxTypeValue = CreateNoxType<Nox.Types.Markdown>(entityDefinition,"MarkdownField",dto.MarkdownField);
        if(noxTypeValue != null)
        {        
            entity.MarkdownField = noxTypeValue;
        }
        noxTypeValue = CreateNoxType<Nox.Types.PhoneNumber>(entityDefinition,"PhoneNumberField",dto.PhoneNumberField);
        if(noxTypeValue != null)
        {        
            entity.PhoneNumberField = noxTypeValue;
        }
        noxTypeValue = CreateNoxType<Nox.Types.Temperature>(entityDefinition,"TemperatureField",dto.TemperatureField);
        if(noxTypeValue != null)
        {        
            entity.TemperatureField = noxTypeValue;
        }
        noxTypeValue = CreateNoxType<Nox.Types.Yaml>(entityDefinition,"YamlField",dto.YamlField);
        if(noxTypeValue != null)
        {        
            entity.YamlField = noxTypeValue;
        }
        noxTypeValue = CreateNoxType<Nox.Types.Year>(entityDefinition,"YearField",dto.YearField);
        if(noxTypeValue != null)
        {        
            entity.YearField = noxTypeValue;
        }
        noxTypeValue = CreateNoxType<Nox.Types.Weight>(entityDefinition,"WeightField",dto.WeightField);
        if(noxTypeValue != null)
        {        
            entity.WeightField = noxTypeValue;
        }
        noxTypeValue = CreateNoxType<Nox.Types.Volume>(entityDefinition,"VolumeField",dto.VolumeField);
        if(noxTypeValue != null)
        {        
            entity.VolumeField = noxTypeValue;
        }
        noxTypeValue = CreateNoxType<Nox.Types.Url>(entityDefinition,"UrlField",dto.UrlField);
        if(noxTypeValue != null)
        {        
            entity.UrlField = noxTypeValue;
        }
        noxTypeValue = CreateNoxType<Nox.Types.Uri>(entityDefinition,"UriField",dto.UriField);
        if(noxTypeValue != null)
        {        
            entity.UriField = noxTypeValue;
        }
        noxTypeValue = CreateNoxType<Nox.Types.TimeZoneCode>(entityDefinition,"TimeZoneCodeField",dto.TimeZoneCodeField);
        if(noxTypeValue != null)
        {        
            entity.TimeZoneCodeField = noxTypeValue;
        }
        noxTypeValue = CreateNoxType<Nox.Types.Percentage>(entityDefinition,"PercentageField",dto.PercentageField);
        if(noxTypeValue != null)
        {        
            entity.PercentageField = noxTypeValue;
        }

        // TODO map TimeField Time remaining types and remove if else
        noxTypeValue = CreateNoxType<Nox.Types.Number>(entityDefinition,"NumberField",dto.NumberField);
        if(noxTypeValue != null)
        {        
            entity.NumberField = noxTypeValue;
        }
        noxTypeValue = CreateNoxType<Nox.Types.Text>(entityDefinition,"TextField",dto.TextField);
        if(noxTypeValue != null)
        {        
            entity.TextField = noxTypeValue;
        }
        noxTypeValue = CreateNoxType<Nox.Types.StreetAddress>(entityDefinition,"StreetAddressField",dto.StreetAddressField);
        if(noxTypeValue != null)
        {        
            entity.StreetAddressField = noxTypeValue;
        }
        noxTypeValue = CreateNoxType<Nox.Types.File>(entityDefinition,"FileField",dto.FileField);
        if(noxTypeValue != null)
        {        
            entity.FileField = noxTypeValue;
        }
        noxTypeValue = CreateNoxType<Nox.Types.TranslatedText>(entityDefinition,"TranslatedTextField",dto.TranslatedTextField);
        if(noxTypeValue != null)
        {        
            entity.TranslatedTextField = noxTypeValue;
        }
        noxTypeValue = CreateNoxType<Nox.Types.VatNumber>(entityDefinition,"VatNumberField",dto.VatNumberField);
        if(noxTypeValue != null)
        {        
            entity.VatNumberField = noxTypeValue;
        }

        // TODO map PasswordField Password remaining types and remove if else
        noxTypeValue = CreateNoxType<Nox.Types.Money>(entityDefinition,"MoneyField",dto.MoneyField);
        if(noxTypeValue != null)
        {        
            entity.MoneyField = noxTypeValue;
        }

        // TODO map HashedTexField HashedText remaining types and remove if else
        noxTypeValue = CreateNoxType<Nox.Types.LatLong>(entityDefinition,"LatLongField",dto.LatLongField);
        if(noxTypeValue != null)
        {        
            entity.LatLongField = noxTypeValue;
        }

        // TODO map EncryptedTextField EncryptedText remaining types and remove if else
    }

    public override void PartialMapToEntity(AllNoxType entity, Entity entityDefinition, Dictionary<string, dynamic> updatedProperties)
    {
        { 
            if (updatedProperties.TryGetValue("NuidField", out dynamic? value))
            {
                var noxTypeValue = CreateNoxType<Nox.Types.Nuid>(entityDefinition,"NuidField",value);
                if(noxTypeValue == null)
                {
                    entity.NuidField = null;
                }
                else
                {
                    entity.NuidField = noxTypeValue;
                }
            }
        }
        { 
            if (updatedProperties.TryGetValue("BooleanField", out dynamic? value))
            {
                var noxTypeValue = CreateNoxType<Nox.Types.Boolean>(entityDefinition,"BooleanField",value);
                if(noxTypeValue == null)
                {
                    entity.BooleanField = null;
                }
                else
                {
                    entity.BooleanField = noxTypeValue;
                }
            }
        }
        { 
            if (updatedProperties.TryGetValue("CountryCode2Field", out dynamic? value))
            {
                var noxTypeValue = CreateNoxType<Nox.Types.CountryCode2>(entityDefinition,"CountryCode2Field",value);
                if(noxTypeValue == null)
                {
                    throw new EntityAttributeIsNotNullableException("AllNoxType", "CountryCode2Field");
                }
                else
                {
                    entity.CountryCode2Field = noxTypeValue;
                }
            }
        }
        { 
            if (updatedProperties.TryGetValue("CountryCode3Field", out dynamic? value))
            {
                var noxTypeValue = CreateNoxType<Nox.Types.CountryCode3>(entityDefinition,"CountryCode3Field",value);
                if(noxTypeValue == null)
                {
                    entity.CountryCode3Field = null;
                }
                else
                {
                    entity.CountryCode3Field = noxTypeValue;
                }
            }
        }
        { 
            if (updatedProperties.TryGetValue("CountryNumberField", out dynamic? value))
            {
                var noxTypeValue = CreateNoxType<Nox.Types.CountryNumber>(entityDefinition,"CountryNumberField",value);
                if(noxTypeValue == null)
                {
                    entity.CountryNumberField = null;
                }
                else
                {
                    entity.CountryNumberField = noxTypeValue;
                }
            }
        }
        { 
            if (updatedProperties.TryGetValue("CultureCodeField", out dynamic? value))
            {
                var noxTypeValue = CreateNoxType<Nox.Types.CultureCode>(entityDefinition,"CultureCodeField",value);
                if(noxTypeValue == null)
                {
                    entity.CultureCodeField = null;
                }
                else
                {
                    entity.CultureCodeField = noxTypeValue;
                }
            }
        }
        { 
            if (updatedProperties.TryGetValue("CurrencyCode3Field", out dynamic? value))
            {
                var noxTypeValue = CreateNoxType<Nox.Types.CurrencyCode3>(entityDefinition,"CurrencyCode3Field",value);
                if(noxTypeValue == null)
                {
                    entity.CurrencyCode3Field = null;
                }
                else
                {
                    entity.CurrencyCode3Field = noxTypeValue;
                }
            }
        }
        { 
            if (updatedProperties.TryGetValue("DateTimeField", out dynamic? value))
            {
                var noxTypeValue = CreateNoxType<Nox.Types.DateTime>(entityDefinition,"DateTimeField",value);
                if(noxTypeValue == null)
                {
                    entity.DateTimeField = null;
                }
                else
                {
                    entity.DateTimeField = noxTypeValue;
                }
            }
        }
        { 
            if (updatedProperties.TryGetValue("HtmlField", out dynamic? value))
            {
                var noxTypeValue = CreateNoxType<Nox.Types.Html>(entityDefinition,"HtmlField",value);
                if(noxTypeValue == null)
                {
                    entity.HtmlField = null;
                }
                else
                {
                    entity.HtmlField = noxTypeValue;
                }
            }
        }
        { 
            if (updatedProperties.TryGetValue("LanguageCodeField", out dynamic? value))
            {
                var noxTypeValue = CreateNoxType<Nox.Types.LanguageCode>(entityDefinition,"LanguageCodeField",value);
                if(noxTypeValue == null)
                {
                    throw new EntityAttributeIsNotNullableException("AllNoxType", "LanguageCodeField");
                }
                else
                {
                    entity.LanguageCodeField = noxTypeValue;
                }
            }
        }
        { 
            if (updatedProperties.TryGetValue("LengthField", out dynamic? value))
            {
                var noxTypeValue = CreateNoxType<Nox.Types.Length>(entityDefinition,"LengthField",value);
                if(noxTypeValue == null)
                {
                    throw new EntityAttributeIsNotNullableException("AllNoxType", "LengthField");
                }
                else
                {
                    entity.LengthField = noxTypeValue;
                }
            }
        }
        { 
            if (updatedProperties.TryGetValue("MacAddressField", out dynamic? value))
            {
                var noxTypeValue = CreateNoxType<Nox.Types.MacAddress>(entityDefinition,"MacAddressField",value);
                if(noxTypeValue == null)
                {
                    throw new EntityAttributeIsNotNullableException("AllNoxType", "MacAddressField");
                }
                else
                {
                    entity.MacAddressField = noxTypeValue;
                }
            }
        }
        { 
            if (updatedProperties.TryGetValue("MarkdownField", out dynamic? value))
            {
                var noxTypeValue = CreateNoxType<Nox.Types.Markdown>(entityDefinition,"MarkdownField",value);
                if(noxTypeValue == null)
                {
                    throw new EntityAttributeIsNotNullableException("AllNoxType", "MarkdownField");
                }
                else
                {
                    entity.MarkdownField = noxTypeValue;
                }
            }
        }
        { 
            if (updatedProperties.TryGetValue("PhoneNumberField", out dynamic? value))
            {
                var noxTypeValue = CreateNoxType<Nox.Types.PhoneNumber>(entityDefinition,"PhoneNumberField",value);
                if(noxTypeValue == null)
                {
                    throw new EntityAttributeIsNotNullableException("AllNoxType", "PhoneNumberField");
                }
                else
                {
                    entity.PhoneNumberField = noxTypeValue;
                }
            }
        }
        { 
            if (updatedProperties.TryGetValue("TemperatureField", out dynamic? value))
            {
                var noxTypeValue = CreateNoxType<Nox.Types.Temperature>(entityDefinition,"TemperatureField",value);
                if(noxTypeValue == null)
                {
                    throw new EntityAttributeIsNotNullableException("AllNoxType", "TemperatureField");
                }
                else
                {
                    entity.TemperatureField = noxTypeValue;
                }
            }
        }
        { 
            if (updatedProperties.TryGetValue("YamlField", out dynamic? value))
            {
                var noxTypeValue = CreateNoxType<Nox.Types.Yaml>(entityDefinition,"YamlField",value);
                if(noxTypeValue == null)
                {
                    entity.YamlField = null;
                }
                else
                {
                    entity.YamlField = noxTypeValue;
                }
            }
        }
        { 
            if (updatedProperties.TryGetValue("YearField", out dynamic? value))
            {
                var noxTypeValue = CreateNoxType<Nox.Types.Year>(entityDefinition,"YearField",value);
                if(noxTypeValue == null)
                {
                    entity.YearField = null;
                }
                else
                {
                    entity.YearField = noxTypeValue;
                }
            }
        }
        { 
            if (updatedProperties.TryGetValue("WeightField", out dynamic? value))
            {
                var noxTypeValue = CreateNoxType<Nox.Types.Weight>(entityDefinition,"WeightField",value);
                if(noxTypeValue == null)
                {
                    entity.WeightField = null;
                }
                else
                {
                    entity.WeightField = noxTypeValue;
                }
            }
        }
        { 
            if (updatedProperties.TryGetValue("VolumeField", out dynamic? value))
            {
                var noxTypeValue = CreateNoxType<Nox.Types.Volume>(entityDefinition,"VolumeField",value);
                if(noxTypeValue == null)
                {
                    entity.VolumeField = null;
                }
                else
                {
                    entity.VolumeField = noxTypeValue;
                }
            }
        }
        { 
            if (updatedProperties.TryGetValue("UrlField", out dynamic? value))
            {
                var noxTypeValue = CreateNoxType<Nox.Types.Url>(entityDefinition,"UrlField",value);
                if(noxTypeValue == null)
                {
                    entity.UrlField = null;
                }
                else
                {
                    entity.UrlField = noxTypeValue;
                }
            }
        }
        { 
            if (updatedProperties.TryGetValue("UriField", out dynamic? value))
            {
                var noxTypeValue = CreateNoxType<Nox.Types.Uri>(entityDefinition,"UriField",value);
                if(noxTypeValue == null)
                {
                    entity.UriField = null;
                }
                else
                {
                    entity.UriField = noxTypeValue;
                }
            }
        }
        { 
            if (updatedProperties.TryGetValue("TimeZoneCodeField", out dynamic? value))
            {
                var noxTypeValue = CreateNoxType<Nox.Types.TimeZoneCode>(entityDefinition,"TimeZoneCodeField",value);
                if(noxTypeValue == null)
                {
                    entity.TimeZoneCodeField = null;
                }
                else
                {
                    entity.TimeZoneCodeField = noxTypeValue;
                }
            }
        }
        { 
            if (updatedProperties.TryGetValue("PercentageField", out dynamic? value))
            {
                var noxTypeValue = CreateNoxType<Nox.Types.Percentage>(entityDefinition,"PercentageField",value);
                if(noxTypeValue == null)
                {
                    entity.PercentageField = null;
                }
                else
                {
                    entity.PercentageField = noxTypeValue;
                }
            }
        }
        { 
            if (updatedProperties.TryGetValue("TimeField", out dynamic? value))
            {
                var noxTypeValue = CreateNoxType<Nox.Types.Time>(entityDefinition,"TimeField",value);
                if(noxTypeValue == null)
                {
                    entity.TimeField = null;
                }
                else
                {
                    entity.TimeField = noxTypeValue;
                }
            }
        }
        { 
            if (updatedProperties.TryGetValue("NumberField", out dynamic? value))
            {
                var noxTypeValue = CreateNoxType<Nox.Types.Number>(entityDefinition,"NumberField",value);
                if(noxTypeValue == null)
                {
                    entity.NumberField = null;
                }
                else
                {
                    entity.NumberField = noxTypeValue;
                }
            }
        }
        { 
            if (updatedProperties.TryGetValue("TextField", out dynamic? value))
            {
                var noxTypeValue = CreateNoxType<Nox.Types.Text>(entityDefinition,"TextField",value);
                if(noxTypeValue == null)
                {
                    throw new EntityAttributeIsNotNullableException("AllNoxType", "TextField");
                }
                else
                {
                    entity.TextField = noxTypeValue;
                }
            }
        }
        { 
            if (updatedProperties.TryGetValue("StreetAddressField", out dynamic? value))
            {
                var noxTypeValue = CreateNoxType<Nox.Types.StreetAddress>(entityDefinition,"StreetAddressField",value);
                if(noxTypeValue == null)
                {
                    entity.StreetAddressField = null;
                }
                else
                {
                    entity.StreetAddressField = noxTypeValue;
                }
            }
        }
        { 
            if (updatedProperties.TryGetValue("FileField", out dynamic? value))
            {
                var noxTypeValue = CreateNoxType<Nox.Types.File>(entityDefinition,"FileField",value);
                if(noxTypeValue == null)
                {
                    entity.FileField = null;
                }
                else
                {
                    entity.FileField = noxTypeValue;
                }
            }
        }
        { 
            if (updatedProperties.TryGetValue("TranslatedTextField", out dynamic? value))
            {
                var noxTypeValue = CreateNoxType<Nox.Types.TranslatedText>(entityDefinition,"TranslatedTextField",value);
                if(noxTypeValue == null)
                {
                    entity.TranslatedTextField = null;
                }
                else
                {
                    entity.TranslatedTextField = noxTypeValue;
                }
            }
        }
        { 
            if (updatedProperties.TryGetValue("VatNumberField", out dynamic? value))
            {
                var noxTypeValue = CreateNoxType<Nox.Types.VatNumber>(entityDefinition,"VatNumberField",value);
                if(noxTypeValue == null)
                {
                    entity.VatNumberField = null;
                }
                else
                {
                    entity.VatNumberField = noxTypeValue;
                }
            }
        }
        { 
            if (updatedProperties.TryGetValue("MoneyField", out dynamic? value))
            {
                var noxTypeValue = CreateNoxType<Nox.Types.Money>(entityDefinition,"MoneyField",value);
                if(noxTypeValue == null)
                {
                    entity.MoneyField = null;
                }
                else
                {
                    entity.MoneyField = noxTypeValue;
                }
            }
        }
        { 
            if (updatedProperties.TryGetValue("LatLongField", out dynamic? value))
            {
                var noxTypeValue = CreateNoxType<Nox.Types.LatLong>(entityDefinition,"LatLongField",value);
                if(noxTypeValue == null)
                {
                    entity.LatLongField = null;
                }
                else
                {
                    entity.LatLongField = noxTypeValue;
                }
            }
        }
    }  
}