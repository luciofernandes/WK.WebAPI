using WK.Catalog.Application.UseCases.Category.Common;
using DomainEntity = WK.Catalog.Domain.Entity;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;
using System.Net;
using WK.Catalog.Application.UseCases.Category.CreateCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WK.Catalog.Api.ApiModels.Response;

namespace WK.Catalog.EndToEndTests.Api.Category.CreateCategory;

[Collection(nameof(CreateCategoryApiTestFixture))]
public class CreateCategoryApiTest
    : IDisposable
{
    private readonly CreateCategoryApiTestFixture _fixture;

    public CreateCategoryApiTest(CreateCategoryApiTestFixture fixture)
        => _fixture = fixture;

    [Fact(DisplayName = nameof(CreateCategory))]
    [Trait("EndToEnd/API", "Category/Create - Endpoints")]
    public async Task CreateCategory()
    {
        var input = _fixture.getExampleInput();

        var (response, output) = await _fixture.
            ApiClient.Post<ApiResponse<CategoryModelOutput>>(
                "/categories",
                input
            );

        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(HttpStatusCode.Created);
        output.Should().NotBeNull();
        output!.Data.Should().NotBeNull();
        output.Data.Name.Should().Be(input.Name);
        output.Data.Id.Should().NotBeEmpty();
        output.Data.CreatedAt.Should()
            .NotBeSameDateAs(default);
        var dbCategory = await _fixture
            .Persistence.GetById(output.Data.Id);
        dbCategory.Should().NotBeNull();
        dbCategory!.Name.Should().Be(input.Name);
        dbCategory.Id.Should().NotBeEmpty();
        dbCategory.CreatedAt.Should()
            .NotBeSameDateAs(default);
    }

   
    public void Dispose()
        => _fixture.CleanPersistence();
}
