namespace OmniGui.Xaml.Rework
{
    using OmniXaml;
    using OmniXaml.ReworkPhases;
    using OmniXaml.Services;
    using IObjectBuilder = OmniXaml.Services.IObjectBuilder;

    public class OmniGuiObjectBuilder : IObjectBuilder
    {
        private readonly ISmartInstanceCreator instanceCreator;
        private readonly IStringSourceValueConverter sourceValueConverter;
        private readonly IMemberAssigmentApplier memberAssigmentApplier;

        public OmniGuiObjectBuilder(ISmartInstanceCreator instanceCreator, IStringSourceValueConverter sourceValueConverter, IMemberAssigmentApplier memberAssigmentApplier)
        {
            this.instanceCreator = instanceCreator;
            this.sourceValueConverter = sourceValueConverter;
            this.memberAssigmentApplier = memberAssigmentApplier;
        }

        public object Inflate(ConstructionNode ctNode)
        {
            var mainBuilder = new Phase1Builder(instanceCreator, sourceValueConverter, memberAssigmentApplier);
            var unresolvedFixer = new Phase2Builder(sourceValueConverter);

            var inflatedNode = mainBuilder.Inflate(ctNode);
            return unresolvedFixer.Fix(inflatedNode);
        }
    }
}