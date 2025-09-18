using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Products.Commands;
using ProductService.Application.Products.Queries;
using ProductService.Application.Products.DTOs;
using ProductService.Application.Queries;
using ErrorOr;
using ProductService.Api.Common.Errors;

namespace ProductService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // get all products
    // Match comes from ErrorOr library
    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var query = new GetAllProductsQuery();
        var result = await _mediator.Send(query);

        return result.Match(
            products => Ok(products),
            errors => Problem(
                detail: string.Join(", ", errors.Select(e => e.Description)),
                statusCode: ErrorMapping.ToStatusCode(errors.First().Type)
            )
        );
    }

    // GET: api/products/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetById(Guid id)
    {
        var query = new GetProductByIdQuery(id);
        var result = await _mediator.Send(query);

        return result.Match(
            product => Ok(product),
            errors => Problem(
                detail: string.Join(", ", errors.Select(e => e.Description)),
                statusCode: ErrorMapping.ToStatusCode(errors.First().Type)
            )
        );
    }

    // POST: api/products
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
    {
        var result = await _mediator.Send(command);

        return result.Match(
            id => CreatedAtAction(nameof(GetById), new { id }, id),
            errors => Problem(
                detail: string.Join(", ", errors.Select(e => e.Description)),
                 statusCode: ErrorMapping.ToStatusCode(errors.First().Type)
            )
        );
    }

    // update
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("Route id and body id must match");
        }

        var result = await _mediator.Send(command);

        return result.Match<IActionResult>(
        _ => NoContent(),
        errors => Problem(
            detail: string.Join(", ", errors.Select(e => e.Description)),
            statusCode: ErrorMapping.ToStatusCode(errors.First().Type)
        )
    );
    }

    [HttpDelete("{id}")]
public async Task<IActionResult> Delete(Guid id)
{
    var command = new DeleteProductCommand(id);
    var result = await _mediator.Send(command);

    return result.Match(
        deletedId => Ok(new { message = $"Product {deletedId} deleted successfully." }),
        errors => Problem(
            detail: string.Join(", ", errors.Select(e => e.Description)),
            statusCode: ErrorMapping.ToStatusCode(errors.First().Type)
        )
    );
}



    [HttpGet("boom")]
    public IActionResult Boom()
    {
        throw new Exception("Boom! Something went wrong 🚨");
    }

}
