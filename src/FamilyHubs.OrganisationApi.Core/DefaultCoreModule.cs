using Autofac;
using FamilyHubs.Organisation.Core.Entities;
using FamilyHubs.Organisation.Core.Interfaces.Entities;

namespace FamilyHubs.Organisation.Core;

public class DefaultCoreModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterType<OpenReferralOrganisation>()
            .As<IOpenReferralOrganisation>().InstancePerLifetimeScope();
    }
}
