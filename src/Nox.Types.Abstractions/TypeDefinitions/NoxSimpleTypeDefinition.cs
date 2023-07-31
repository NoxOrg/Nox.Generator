﻿using Nox.TypeOptions;
using Nox.Types.Schema;

namespace Nox.Types;

[GenerateJsonSchema("type")]
[Title("Definition namespace for Nox simple types.")]
[Description("Nox simple types definition used throughout Nox.Solution project.")]
[AdditionalProperties(false)]
public class NoxSimpleTypeDefinition
{
    [Required]
    [Title("The name of the attribute. Contains no spaces.")]
    [Description("Assign a descriptive name to the attribute. Should be a singular noun and be unique within a collection of attributes. PascalCase recommended.")]
    [Pattern(@"^[^\s]*$")]
    public string Name { get; internal set; } = null!;

    [Title("The description of the attribute.")]
    [Description("A descriptive phrase that explains the nature and function of this attribute within a collection.")]
    public string? Description { get; internal set; }

    [Required]
    [Title("The Nox type of this attribute.")]
    [Description("Select the Nox value object type that best represents this attribute and its functional requirements within the domain.")]
    public NoxType Type { get; internal set; } = NoxType.Object;

    #region TypeOptions

    [IfEquals("Type", NoxType.Text)]
    public TextTypeOptions? TextTypeOptions { get; set; }

    [IfEquals("Type", NoxType.Number)]
    public NumberTypeOptions? NumberTypeOptions { get; set; }

    [IfEquals("Type", NoxType.Money)]
    public MoneyTypeOptions? MoneyTypeOptions { get; set; }

    [IfEquals("Type", NoxType.Entity)]
    public EntityTypeOptions? EntityTypeOptions { get; set; }

    [IfEquals("Type", NoxType.Date)]
    public DateTypeOptions? DateTypeOptions { get; set; }

    [IfEquals("Type", NoxType.Percentage)]
    public PercentageTypeOptions? PercentageTypeOptions { get; set; }

    [IfEquals("Type", NoxType.TranslatedText)]
    public TranslatedTextTypeOptions? TranslatedTextTypeOptions { get; set; }

    [IfEquals("Type", NoxType.Nuid)]
    public NuidTypeOptions? NuidTypeOptions { get; set; }

    [IfEquals("Type", NoxType.Year)]
    public YearTypeOptions? YearTypeOptions { get; set; }

    [IfEquals("Type", NoxType.DateTimeDuration)]
    public DateTimeDurationTypeOptions? DateTimeDurationTypeOptions { get; set; }

    [IfEquals("Type", NoxType.HashedText)]
    public HashedTextTypeOptions? HashedTextTypeOptions { get; set; }

    [IfEquals("Type", NoxType.DateTime)]
    public DateTimeTypeOptions? DateTimeTypeOptions { get; set; }
    
    [IfEquals("Type", NoxType.Image)]
    public ImageTypeOptions? ImageTypeOptions { get; set; }

    [IfEquals("Type", NoxType.Time)]
    public TimeTypeOptions? TimeTypeOptions { get; set; }

    [IfEquals("Type", NoxType.File)]
    public FileTypeOptions? FileTypeOptions { get; set; }

	[IfEquals("Type", NoxType.Area)]
    public AreaTypeOptions? AreaTypeOptions { get; set; }

    [IfEquals("Type", NoxType.EncryptedText)]
    public EncryptedTextTypeOptions? EncryptedTextTypeOptions { get; set; }

    [IfEquals("Type", NoxType.User)]
    public UserTypeOptions? UserTypeOptions { get; set; }

    [IfEquals("Type", NoxType.Password)]
    public PasswordTypeOptions? PasswordTypeOptions { get; set; }

    [IfEquals("Type", NoxType.Formula)]
    public FormulaTypeOptions? FormulaTypeOptions { get; set; }

    [IfEquals("Type", NoxType.Json)]
    public JsonTypeOptions? JsonTypeOptions { get; set; }

    [IfEquals("Type", NoxType.Temperature)]
    public TemperatureTypeOptions? TemperatureTypeOptions { get; set; }

    [IfEquals("Type", NoxType.Length)]
    public LengthTypeOptions? LengthTypeOptions { get; set; }

    #endregion TypeOptions

    [Title("Is the attribute required? Boolean value.")]
    [Description("Indicates whether this attribute is required within the collection. Defaults to false.")]
    public bool IsRequired { get; internal set; } = false;

    public TypeUserInterface? UserInterface { get; internal set; }

    [Title("Is this attribute readonly? Boolean value.")]
    [Description("Indicates whether this attribute is readonly. Defaults to false.")]
    public bool IsReadonly { get; internal set; } = false;

    public NoxSimpleTypeDefinition ShallowCopy()
    {
        return (NoxSimpleTypeDefinition)MemberwiseClone();
    }
}