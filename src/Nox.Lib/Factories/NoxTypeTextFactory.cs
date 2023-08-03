﻿using Nox.Solution;
using Nox.Types;

namespace Nox.Factories
{
    public class NoxTypeTextFactory : NoxTypeFactoryBase<Text>
    {
        public NoxTypeTextFactory(NoxSolution solution) : base(solution)
        {
        }

        public override Text CreateNoxType(Entity entityDefinition, string propertyName, dynamic value)
        {
            var attributeDefinition = entityDefinition.Attributes!.Single(attribute => attribute.Name == propertyName);

            return Text.From(value, attributeDefinition.TextTypeOptions ?? new TextTypeOptions());
        }
    }
}
