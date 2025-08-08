using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;

using Obeqnupe.Models;
using Obeqnupe.Repositories;
using Obeqnupe.Services;

namespace Obeqnupe.Controllers;

public class HomeController : Controller
{
    private readonly ICompanyService _companyService;

    public HomeController(ICompanyService companyService)
    {
        _companyService = companyService;
    }

    public async Task<IActionResult> Index()
    {
        var data = await _companyService.GetFilterData();
        var filterData = new FilterDataViewModel
        {
            Benefits = data.Benefits.ToList(),
            CompanyTypes = data.CompanyTypes.ToList(),
            Locations = data.Locations.ToList(),
            Skills = data.Skills.ToList()
        };
        return View(filterData);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
