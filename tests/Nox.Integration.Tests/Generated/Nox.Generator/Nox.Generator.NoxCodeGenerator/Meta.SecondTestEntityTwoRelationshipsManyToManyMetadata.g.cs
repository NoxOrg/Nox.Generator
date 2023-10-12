﻿// Generated

#nullable enable

using Nox.Types;
using Nox.Domain;
using Nox.Solution;
using System;
using System.Collections.Generic;

namespace TestWebApp.Domain;

/// <summary>
/// Static methods for the SecondTestEntityTwoRelationshipsManyToMany class.
/// </summary>
public partial class SecondTestEntityTwoRelationshipsManyToManyMetadata
{
    
        /// <summary>
        /// Type options for property 'Id'
        /// </summary>
        public static Nox.Types.TextTypeOptions IdTypeOptions {get; private set;} = new ()
        {
            MinLength = 2,
            MaxLength = 2,
            IsUnicode = false,
            IsLocalized = true,
            Casing = Nox.Types.TextTypeCasing.Normal,
        };
    
    
        /// <summary>
        /// Factory for property 'Id'
        /// </summary>
        public static Nox.Types.Text CreateId(System.String value)
            => Nox.Types.Text.From(value, IdTypeOptions);
        
    
        /// <summary>
        /// Type options for property 'TextTestField2'
        /// </summary>
        public static Nox.Types.TextTypeOptions TextTestField2TypeOptions {get; private set;} = new ()
        {
            MinLength = 4,
            MaxLength = 63,
            IsUnicode = true,
            IsLocalized = true,
            Casing = Nox.Types.TextTypeCasing.Normal,
        };
    
    
        /// <summary>
        /// Factory for property 'TextTestField2'
        /// </summary>
        public static Nox.Types.Text CreateTextTestField2(System.String value)
            => Nox.Types.Text.From(value, TextTestField2TypeOptions);
        
    
        /// <summary>
        /// Type options for property 'TestEntityTwoRelationshipsManyToManyId'
        /// </summary>
        public static Nox.Types.TextTypeOptions TestEntityTwoRelationshipsManyToManyIdTypeOptions {get; private set;} = new ()
        {
            MinLength = 2,
            MaxLength = 2,
            IsUnicode = false,
            IsLocalized = true,
            Casing = Nox.Types.TextTypeCasing.Normal,
        };
    
    
        /// <summary>
        /// Factory for property 'TestEntityTwoRelationshipsManyToManyId'
        /// </summary>
        public static Nox.Types.Text CreateTestEntityTwoRelationshipsManyToManyId(System.String value)
            => Nox.Types.Text.From(value, TestEntityTwoRelationshipsManyToManyIdTypeOptions);
        

        /// <summary>
        /// User Interface for property 'TextTestField2'
        /// </summary>
        public static TypeUserInterface? TextTestField2UiOptions(NoxSolution solution) 
            => solution.Domain!
                .GetEntityByName("SecondTestEntityTwoRelationshipsManyToMany")
                .GetAttributeByName("TextTestField2")?
                .UserInterface;
}