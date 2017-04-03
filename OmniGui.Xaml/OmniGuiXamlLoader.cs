namespace OmniGui.Xaml
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using OmniXaml;
    using OmniXaml.Rework;
    using OmniXaml.ReworkPhases;
    using OmniXaml.Services;
    using Rework;
    using Templates;
    using IObjectBuilder = OmniXaml.Services.IObjectBuilder;
    using ObjectBuilder = OmniXaml.Services.ObjectBuilder;

    public class OmniGuiXamlLoader : XamlLoader
    {
        private readonly Func<ICollection<ControlTemplate>> getControlTemplates;
        private readonly ITypeLocator locator;

        public OmniGuiXamlLoader(IList<Assembly> assemblies, Func<ICollection<ControlTemplate>> getControlTemplates, ITypeLocator locator) : base(assemblies)
        {
            this.getControlTemplates = getControlTemplates;
            this.locator = locator;
        }

        protected override IObjectBuilder GetObjectBuilder(ISmartInstanceCreator instanceCreator, IStringSourceValueConverter converter, IMemberAssigmentApplier memberAssigmentApplier)
        {
            return new ObjectBuilder(instanceCreator, converter, memberAssigmentApplier);
        }

        protected override ISmartInstanceCreator GetInstanceCreator(IStringSourceValueConverter converter)
        {
            return new Rework.OmniGuiInstanceCreator(converter, locator);
        }

        protected override IMemberAssigmentApplier GetMemberAssignmentApplier(IStringSourceValueConverter converter)
        {
            return new OmniGuiMemberAssignmentApplier(converter, new OmniGuiValuePipeline(new MarkupExtensionValuePipeline(new NoActionValuePipeline())));
        }
    }

    public abstract class ValuePipeline : IValuePipeline
    {
        private readonly IValuePipeline pipeline;

        protected ValuePipeline(IValuePipeline pipeline)
        {
            this.pipeline = pipeline;
        }

        public void Handle(object parent, Member member, MutablePipelineUnit mutable)
        {
            if (!mutable.Handled)
            {
                pipeline.Handle(parent, member, mutable);
                HandleCore(parent, member, mutable);
            }
        }

        protected abstract void HandleCore(object parent, Member member, MutablePipelineUnit mutable);
    }

    public class OmniGuiValuePipeline : ValuePipeline
    {
        public OmniGuiValuePipeline(IValuePipeline inner) : base(inner)
        {
        }

        protected override void HandleCore(object parent, Member member, MutablePipelineUnit mutable)
        {
            var bindDefinition = mutable.Value as BindDefinition;
            if (bindDefinition != null)
            {
                mutable.Handled = true;
            }
        }
    }

    public class MarkupExtensionValuePipeline : ValuePipeline
    {
        public MarkupExtensionValuePipeline(IValuePipeline inner) : base(inner)
        {
        }

        protected override void HandleCore(object parent, Member member, MutablePipelineUnit mutable)
        {
            var extension = mutable.Value as IMarkupExtension;
            if (extension != null)
            {
                var keyedInstance = new KeyedInstance(parent);
                var assignment = new Assignment(keyedInstance, member, mutable.Value);
                var finalValue = extension.GetValue(new ExtensionValueContext(assignment, null, null, null));
                mutable.Value = finalValue;
            }
        }
    }

    public class OmniGuiMemberAssignmentApplier : MemberAssigmentApplier
    {
        public OmniGuiMemberAssignmentApplier(IStringSourceValueConverter converter, IValuePipeline pipeline) : base(converter, pipeline)
        {
        }
    }
}