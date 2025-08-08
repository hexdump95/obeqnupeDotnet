using Obeqnupe.Models;
using Obeqnupe.Repositories;

namespace Obeqnupe.Services;

public class CompanyService : ICompanyService
{
    private readonly IBenefitRepository _benefitRepository;
    private readonly ICompanyTypeRepository _companyTypeRepository;
    private readonly ILocationRepository _locationRepository;
    private readonly ISkillRepository _skillRepository;

    public CompanyService(IBenefitRepository benefitRepository, ICompanyTypeRepository companyTypeRepository,
        ILocationRepository locationRepository, ISkillRepository skillRepository)
    {
        _benefitRepository = benefitRepository;
        _companyTypeRepository = companyTypeRepository;
        _locationRepository = locationRepository;
        _skillRepository = skillRepository;
    }

    public async Task<FilterDataViewModel> GetFilterData()
    {
        return new FilterDataViewModel
        {
            Benefits = (await _benefitRepository.FindAllAsync()).ToList(),
            CompanyTypes = (await _companyTypeRepository.FindAllAsync()).ToList(),
            Locations = (await _locationRepository.FindAllAsync()).ToList(),
            Skills = (await _skillRepository.FindAllAsync()).ToList()
        };
    }
}
