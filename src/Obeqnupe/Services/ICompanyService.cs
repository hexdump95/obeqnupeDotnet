using Obeqnupe.Models;

namespace Obeqnupe.Services;

public interface ICompanyService
{
    Task<FilterDataViewModel> GetFilterData();
}
