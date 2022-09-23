using FamilyHubs.Organisation.Core.Dto;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FamilyHubs.OrganisationApi.FunctionalTests;

[Collection("Sequential")]
public class WhenUsingOpenReferralOrganisationApiUnitTests : BaseWhenUsingOpenReferralApiUnitTests
{
    [Fact]
    public async Task ThenTheOpenReferralOrganisationIsCreated()
    {
        var command = GetTestCountyCouncilRecord();

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(_client.BaseAddress + "api/organizations"),
            Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json"),
        };

        using var response = await _client.SendAsync(request);

        response.EnsureSuccessStatusCode();

        var stringResult = await response.Content.ReadAsStringAsync();

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        stringResult.ToString().Should().Be(command.Id);
    }

    [Fact]
    public async Task ThenTheOpenReferralOrganisationIsRetrieved()
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(_client.BaseAddress + "api/organizations/72e653e8-1d05-4821-84e9-9177571a6013"),

        };

        using var response = await _client.SendAsync(request);

        response.EnsureSuccessStatusCode();


        var retVal = await JsonSerializer.DeserializeAsync<OpenReferralOrganisationExDto>(await response.Content.ReadAsStreamAsync(), options: new JsonSerializerOptions { PropertyNameCaseInsensitive = true });


        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        retVal.Should().NotBeNull();
        ArgumentNullException.ThrowIfNull(retVal, nameof(retVal));
        retVal.Id.Should().Be("72e653e8-1d05-4821-84e9-9177571a6013");
    }

    [Fact]
    public async Task ThenListOpenReferralOrganisationsIsRetrieved()
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(_client.BaseAddress + "api/organizations"),
        };

        using var response = await _client.SendAsync(request);

        response.EnsureSuccessStatusCode();

        var retVal = await JsonSerializer.DeserializeAsync<List<OpenReferralOrganisationExDto>>(await response.Content.ReadAsStreamAsync(), options: new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        retVal.Should().NotBeNull();
        ArgumentNullException.ThrowIfNull(retVal, nameof(retVal));
        retVal.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task ThenTheOpenReferralOrganisationIsUpdated()
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(_client.BaseAddress + "api/organizations/72e653e8-1d05-4821-84e9-9177571a6013"),

        };

        using var response = await _client.SendAsync(request);

        response.EnsureSuccessStatusCode();


        var retVal = await JsonSerializer.DeserializeAsync<OpenReferralOrganisationExDto>(await response.Content.ReadAsStreamAsync(), options: new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        ArgumentNullException.ThrowIfNull(retVal, nameof(retVal));

        var update = new OpenReferralOrganisationExDto(
            retVal.Id,
            retVal.OrganisationTypeId.Trim(),
            retVal.Name ?? string.Empty,
            retVal.Description + " Update Test",
            retVal.Logo,
            retVal.Uri,
            retVal.Url,
            retVal.Email,
            retVal.ContactName,
            retVal.ContactPhone
            );

        var updaterequest = new HttpRequestMessage
        {
            Method = HttpMethod.Put,
            RequestUri = new Uri(_client.BaseAddress + "api/organizations/72e653e8-1d05-4821-84e9-9177571a6013"),
            Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(update), Encoding.UTF8, "application/json"),
        };

        using var updateresponse = await _client.SendAsync(updaterequest);

        updateresponse.EnsureSuccessStatusCode();

        var stringResult = await updateresponse.Content.ReadAsStringAsync();

        updateresponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        stringResult.ToString().Should().Be("72e653e8-1d05-4821-84e9-9177571a6013");
    }

    private OpenReferralOrganisationExDto GetTestCountyCouncilRecord()
    {
        return new OpenReferralOrganisationExDto(
            "ce21cc59-3af9-430a-9c49-fdc75c56a040",
            "1",
            "Test County Council",
            "Test County Council",
            "logo",
            new Uri("https://www.test.gov.uk/").ToString(),
            "https://www.test.gov.uk/",
            "someone@aol.com",
            "Contact Name",
            "Contact Phone");
    }
}
