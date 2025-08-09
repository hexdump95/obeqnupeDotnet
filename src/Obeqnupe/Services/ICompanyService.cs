using Obeqnupe.Dtos;
using Obeqnupe.Models;

namespace Obeqnupe.Services;

public interface ICompanyService
{
    Task<FilterDataViewModel> GetFilterData();
    Task<PageDto<CompanyResponse>> FindAll(FilterRequest filter, int page);
}
