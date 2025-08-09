using Microsoft.EntityFrameworkCore;

using Obeqnupe.Dtos;
using Obeqnupe.Models;
using Obeqnupe.Repositories;

namespace Obeqnupe.Services;

public class CompanyService : ICompanyService
{
    private readonly IBenefitRepository _benefitRepository;
    private readonly ICompanyTypeRepository _companyTypeRepository;
    private readonly ILocationRepository _locationRepository;
    private readonly ISkillRepository _skillRepository;
    private readonly ICompanyRepository _companyRepository;
    private const int PageSize = 12;

    public CompanyService(IBenefitRepository benefitRepository, ICompanyTypeRepository companyTypeRepository,
        ILocationRepository locationRepository, ISkillRepository skillRepository, ICompanyRepository companyRepository)
    {
        _benefitRepository = benefitRepository;
        _companyTypeRepository = companyTypeRepository;
        _locationRepository = locationRepository;
        _skillRepository = skillRepository;
        _companyRepository = companyRepository;
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

    public async Task<PageDto<CompanyResponse>> FindAll(FilterRequest filter, int page)
    {
        var companies = _companyRepository.GetQueryable();

        if (filter.LocationId == null && filter.CompanyTypeId == null
                                      && String.IsNullOrEmpty(filter.SkillIds) &&
                                      String.IsNullOrEmpty(filter.ExcludedSkillIds)
                                      && String.IsNullOrEmpty(filter.BenefitIds) &&
                                      String.IsNullOrEmpty(filter.ExcludedBenefitIds)
                                      && String.IsNullOrEmpty(filter.Query))
        {
            companies = companies.Where(c => !c.Benefits.Any() && !c.Skills.Any());
        }

        else
        {
            if (filter.LocationId != null)
            {
                companies = companies.Where(c => c.Location!.Id == filter.LocationId);
            }

            if (filter.CompanyTypeId != null)
            {
                companies = companies.Where(c => c.CompanyType!.Id == filter.CompanyTypeId);
            }

            var skillIds = filter.SkillIds?.Split(',').Select(long.Parse).ToList();
            if (skillIds != null && skillIds.Count > 0)
            {
                companies = companies.Where(c =>
                    skillIds.All(skillId => c.Skills.Any(s => s.Id == skillId))
                );
            }

            var excludedSkillIds = filter.ExcludedSkillIds?.Split(',').Select(long.Parse).ToList();
            if (excludedSkillIds != null && excludedSkillIds.Count > 0)
            {
                companies = companies.Where(c =>
                    !excludedSkillIds.Any(excludedSkillId =>
                        c.Skills.Any(s => s.Id == excludedSkillId))
                );
            }

            var benefitIds = filter.BenefitIds?.Split(',').Select(long.Parse).ToList();
            if (benefitIds != null && benefitIds.Count > 0)
            {
                companies = companies.Where(c =>
                    benefitIds.All(benefitId => c.Benefits.Any(b => b.Id == benefitId))
                );
            }

            var excludedBenefitIds = filter.ExcludedBenefitIds?.Split(',').Select(long.Parse).ToList();
            if (excludedBenefitIds != null && excludedBenefitIds.Any())
            {
                companies = companies.Where(c =>
                    !excludedBenefitIds.Any(excludedBenefitId =>
                        c.Benefits.Any(b => b.Id == excludedBenefitId))
                );
            }

            if (!string.IsNullOrWhiteSpace(filter.Query))
            {
                companies = companies.Where(c =>
                    c.Name!.ToUpper().Contains(filter.Query.ToUpper())
                );
            }
        }

        var totalCount = await companies.CountAsync();
        var totalPages = (int)Math.Ceiling((double)totalCount / PageSize);

        var pagedCompanies = await companies
            .OrderBy(c => c.Name)
            .Skip(page * PageSize)
            .Take(PageSize)
            .ToListAsync();

        var companyResponses = pagedCompanies.Select(x =>
            new CompanyResponse
            {
                Id = x.Id,
                Name = x.Name,
                CompanyTypeName = x.CompanyType?.Name,
                LocationName = x.Location?.Name,
                Page = x.Page
            }
        );

        var response = new PageDto<CompanyResponse>
        {
            PageIndex = page + 1,
            TotalPages = totalPages,
            HasPreviousPage = page + 1 > 1,
            HasNextPage = page + 1 < totalPages,
            Items = companyResponses,
        };
        return response;
    }

    public async Task<CompanyDetailResponse> FindOne(Guid id)
    {
        var company = await _companyRepository.FindByIdAsync(id);
        if (company == null)
        {
            throw new Exception("Company not found"); // TODO: change this!
        }

        return new()
        {
            Name = company.Name,
            Page = company.Page,
            LocationName = company.Location!.Name,
            Skills = company.Skills.Select(s => s.Name).ToList(),
            Benefits = company.Benefits.Select(s => s.Name).ToList(),
        };
    }
}
