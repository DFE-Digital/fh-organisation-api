using AutoMapper;
using FamilyHubs.Organisation.Core;
using FamilyHubs.Organisation.Core.Dto;
using FamilyHubs.Organisation.Infrastructure.Persistence.Repository;
using FamilyHubs.OrganisationApi.Api.Commands.CreateOpenReferralOrganisation;
using FamilyHubs.OrganisationApi.Api.Commands.UpdateOpenReferralOrganisation;
using FamilyHubs.OrganisationApi.Api.Queries.GetOpenReferralOrganisationById;
using FamilyHubs.OrganisationApi.Api.Queries.ListOrganisation;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace FamilyHubs.OrganisationApi.UnitTests.Organisation;

public class WhenUsingOrganisationCommands : BaseCreateDbUnitTest
{
    [Fact]
    public async Task ThenCreateOpenReferralOrganisation()
    {
        //Arange
        var myProfile = new AutoMappingProfiles();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
        IMapper mapper = new Mapper(configuration);
        var logger = new Mock<ILogger<CreateOpenReferralOrganisationCommandHandler>>();
        var mockApplicationDbContext = GetApplicationDbContext();
        var dbInit = new ApplicationDbContextInitialiser(new Mock<ILogger<ApplicationDbContextInitialiser>>().Object, mockApplicationDbContext);
        await dbInit.SeedAsync();
        var testOrganisation = GetTestCountyCouncilRecord();
        CreateOpenReferralOrganisationCommand command = new(testOrganisation);
        CreateOpenReferralOrganisationCommandHandler handler = new(mockApplicationDbContext, mapper, logger.Object);

        //Act
        var result = await handler.Handle(command, new System.Threading.CancellationToken());

        //Assert
        result.Should().NotBeNull();
        result.Should().Be(testOrganisation.Id);
    }

    [Fact]
    public async Task ThenUpdateOpenReferralOrganisation()
    {
        //Arange
        var myProfile = new AutoMappingProfiles();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
        IMapper mapper = new Mapper(configuration);
        var logger = new Mock<ILogger<CreateOpenReferralOrganisationCommandHandler>>();
        var mockApplicationDbContext = GetApplicationDbContext();
        var dbInit = new ApplicationDbContextInitialiser(new Mock<ILogger<ApplicationDbContextInitialiser>>().Object, mockApplicationDbContext);
        await dbInit.SeedAsync();
        var testOrganisation = GetTestCountyCouncilRecord();
        var updatelogger = new Mock<ILogger<UpdateOpenReferralOrganisationCommandHandler>>();
        CreateOpenReferralOrganisationCommand command = new(testOrganisation);
        CreateOpenReferralOrganisationCommandHandler handler = new(mockApplicationDbContext, mapper, logger.Object);
        var id = await handler.Handle(command, new System.Threading.CancellationToken());


        OpenReferralOrganisationExDto updateTestOrganisation = new OpenReferralOrganisationExDto(
            "f16ab625-51e8-4383-a680-f8648cff0620",
            "1",
            "Unit Test A County Council",
            "Unit Test A County Council",
            "logo",
            new Uri("https://www.test.gov.uk/").ToString(),
            "https://www.test.gov.uk/",
            "someone@aol.com",
            "Contact Name",
            "Contact Phone");
        
        UpdateOpenReferralOrganisationCommand updatecommand = new(updateTestOrganisation.Id, updateTestOrganisation);
        UpdateOpenReferralOrganisationCommandHandler updatehandler = new(mockApplicationDbContext, updatelogger.Object);

        //Act
        var result = await updatehandler.Handle(updatecommand, new System.Threading.CancellationToken());

        //Assert
        result.Should().NotBeNull();
        result.Should().Be(testOrganisation.Id);
    }

    [Fact]
    public async Task ThenGetOpenReferralOrganisationById()
    {
        //Arange
        var myProfile = new AutoMappingProfiles();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
        IMapper mapper = new Mapper(configuration);
        var logger = new Mock<ILogger<CreateOpenReferralOrganisationCommandHandler>>();
        var mockApplicationDbContext = GetApplicationDbContext();
        var dbInit = new ApplicationDbContextInitialiser(new Mock<ILogger<ApplicationDbContextInitialiser>>().Object, mockApplicationDbContext);
        await dbInit.SeedAsync();
        var testOrganisation = GetTestCountyCouncilRecord();
        CreateOpenReferralOrganisationCommand command = new(testOrganisation);
        CreateOpenReferralOrganisationCommandHandler handler = new(mockApplicationDbContext, mapper, logger.Object);
        var id = await handler.Handle(command, new System.Threading.CancellationToken());

        GetOpenReferralOrganisationByIdCommand getcommand = new() { Id = testOrganisation.Id };
        GetOpenReferralOrganisationByIdHandler gethandler = new(mockApplicationDbContext, mapper);

        //Act
        var result = await gethandler.Handle(getcommand, new System.Threading.CancellationToken());

        //Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(testOrganisation);
    }

    [Fact]
    public async Task ThenListOpenReferralOrganisations()
    {
        //Arange
        var myProfile = new AutoMappingProfiles();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
        IMapper mapper = new Mapper(configuration);
        var logger = new Mock<ILogger<CreateOpenReferralOrganisationCommandHandler>>();
        var mockApplicationDbContext = GetApplicationDbContext();
        var dbInit = new ApplicationDbContextInitialiser(new Mock<ILogger<ApplicationDbContextInitialiser>>().Object, mockApplicationDbContext);
        await dbInit.SeedAsync();
        var testOrganisation = GetTestCountyCouncilRecord();
        CreateOpenReferralOrganisationCommand command = new(testOrganisation);
        CreateOpenReferralOrganisationCommandHandler handler = new(mockApplicationDbContext, mapper, logger.Object);
        var id = await handler.Handle(command, new System.Threading.CancellationToken());

        ListOpenReferralOrganisationCommand getcommand = new();
        ListOpenReferralOrganisationCommandHandler gethandler = new(mockApplicationDbContext);
        

        //Act
        var result = await gethandler.Handle(getcommand, new System.Threading.CancellationToken());
        var test = result.FirstOrDefault(x => x.Id == testOrganisation.Id);

        //Assert
        result.Should().NotBeNull();
        test.Should().BeEquivalentTo(testOrganisation);
    }
}
