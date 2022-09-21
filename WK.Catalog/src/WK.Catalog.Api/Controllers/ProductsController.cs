using WK.Catalog.Api.ApiModels.Product;
using WK.Catalog.Api.ApiModels.Response;
using WK.Catalog.Application.UseCases.Product.Common;
using WK.Catalog.Application.UseCases.Product.CreateProduct;
using WK.Catalog.Application.UseCases.Product.DeleteProduct;
using WK.Catalog.Application.UseCases.Product.GetProduct;
using WK.Catalog.Application.UseCases.Product.ListProducts;
using WK.Catalog.Application.UseCases.Product.UpdateProduct;
using WK.Catalog.Domain.SeedWork.SearchableRepository;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WK.Catalog.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
        => _mediator = mediator;

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<ProductModelOutput>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Create(
        [FromBody] CreateProductInput input,
        CancellationToken cancellationToken
    )
    {
        var output = await _mediator.Send(input, cancellationToken);
        return CreatedAtAction(
            nameof(Create),
            new { output.Id },
            new ApiResponse<ProductModelOutput>(output)
        );
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<ProductModelOutput>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Update(
        [FromBody] UpdateProductApiInput apiInput,
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        var input = new UpdateProductInput(
            id,
            apiInput.Name,
            apiInput.Description,
            apiInput.CategoryId
        );
        var output = await _mediator.Send(input, cancellationToken);
        return Ok(new ApiResponse<ProductModelOutput>(output));
    }
  
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<ProductModelOutput>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        var output = await _mediator.Send(new GetProductInput(id), cancellationToken);
        return Ok(new ApiResponse<ProductModelOutput>(output));
    }
    
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        await _mediator.Send(new DeleteProductInput(id), cancellationToken);
        return NoContent();
    }

    [HttpGet]
    [ProducesResponseType(typeof(ListProductsOutput), StatusCodes.Status200OK)]
    public async Task<IActionResult> List(
        CancellationToken cancellationToken,        
        [FromQuery] int? page = null,
        [FromQuery(Name = "per_page")] int? perPage = null,
        [FromQuery] string? search = null,
        [FromQuery] string? sort = null,
        [FromQuery] SearchOrder? dir = null
    )
    {
        var input = new ListProductsInput();
        if (page is not null) input.Page = page.Value;
        if (perPage is not null) input.PerPage = perPage.Value;
        if (!String.IsNullOrWhiteSpace(search)) input.Search = search;
        if (!String.IsNullOrWhiteSpace(sort)) input.Sort = sort;
        if (dir is not null) input.Dir = dir.Value;
        
        var output = await _mediator.Send(input, cancellationToken);
        return Ok(
            new ApiResponseList<ProductModelOutput>(output)
        );
    }
}
