using Microsoft.AspNetCore.Mvc;

using Obeqnupe.Dtos;
using Obeqnupe.Services;

namespace Obeqnupe.Controllers;

[Route("api/v1/companies")]
[ApiController]
public class CompanyController : ControllerBase
{
    private readonly ICompanyService _companyService;

    public CompanyController(ICompanyService companyService)
    {
        _companyService = companyService;
    }

    [HttpGet]
    public async Task<ActionResult<PageDto<CompanyResponse>>> GetAll(
        [FromQuery] FilterRequest filter,
        [FromQuery] int page = 1
    )
    {
        return Ok(await _companyService.FindAll(filter, page - 1));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CompanyDetailResponse>> GetOne(Guid id)
    {
        var company = await _companyService.FindOne(id);
        return Ok(company);
    }

}
