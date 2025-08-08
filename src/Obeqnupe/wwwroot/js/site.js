let currentPage = 1;
let skillIds = [];
let excludedSkillIds = [];
let benefitIds = [];
let excludedBenefitIds = [];

$(document).ready(() => {
    getCompanies();
});

function addSkill(element, id) {
    const isInSkillsIds = skillIds.indexOf(id) !== -1;
    const isInExcludedSkillIds = excludedSkillIds.indexOf(id) !== -1;
    if (!isInSkillsIds && !isInExcludedSkillIds) {
        skillIds.push(id);
        element.classList.remove('text-bg-dark');
        element.classList.add('text-bg-secondary');
    }
    if (isInSkillsIds) {
        skillIds = skillIds.filter(x => x !== id);
        excludedSkillIds.push(id);
        element.classList.remove('text-bg-secondary');
        element.classList.add('text-bg-danger');
    }
    if (isInExcludedSkillIds) {
        excludedSkillIds = excludedSkillIds.filter(x => x !== id);
        element.classList.remove('text-bg-danger');
        element.classList.add('text-bg-dark');
    }
}

function addBenefit(element, id) {
    const isInBenefitIds = benefitIds.indexOf(id) !== -1;
    const isInExcludedBenefitIds = excludedBenefitIds.indexOf(id) !== -1;
    if (!isInBenefitIds && !isInExcludedBenefitIds) {
        benefitIds.push(id);
        element.classList.remove('text-bg-dark');
        element.classList.add('text-bg-secondary');
    }
    if (isInBenefitIds) {
        benefitIds = benefitIds.filter(x => x !== id);
        excludedBenefitIds.push(id);
        element.classList.remove('text-bg-secondary');
        element.classList.add('text-bg-danger');
    }
    if (isInExcludedBenefitIds) {
        excludedBenefitIds = excludedBenefitIds.filter(x => x !== id);
        element.classList.remove('text-bg-danger');
        element.classList.add('text-bg-dark');
    }
}

function getQueryData() {
    let data = {
        page: currentPage
    }
    const locationId = $('#location').val();
    const companyTypeId = $('#company-type').val();
    const query = $('#query').val();
    if (skillIds.length > 0)
        data.skillIds = skillIds.join(',');
    if (excludedSkillIds.length > 0)
        data.excludedSkillIds = excludedSkillIds.join(',');
    if (benefitIds.length > 0)
        data.benefitIds = benefitIds.join(',');
    if (excludedBenefitIds.length > 0)
        data.excludedBenefitIds = excludedBenefitIds.join(',');
    if (locationId)
        data.locationId = locationId;
    if (companyTypeId)
        data.companyTypeId = companyTypeId;
    if (query)
        data.query = query;
    return data;
}

function getCompanies() {
    $('#spinner').removeClass('d-none').addClass('d-flex');
    $.ajax({
        type: 'GET',
        url: '/api/v1/companies',
        dataType: 'json',
        data: getQueryData(),
        success: (res) => {
            const columnWidth = $('#table').width() / 4;
            let html = '';
            const paginationId = $('#pagination');
            if (res.items.length > 0) {
                $.each(res.items, (_, company) => {
                    html += '<tr>';
                    html += '<td class="overflow-auto" style="width: ' + columnWidth + 'px">' + company.name + '</td>';
                    html += '<td class="overflow-auto" style="width: ' + columnWidth + 'px">' + company.companyTypeName + '</td>';
                    html += '<td class="overflow-auto" style="width: ' + columnWidth + 'px">' + company.locationName + '</td>';
                    html += '<td class="overflow-auto" style="width: ' + columnWidth + 'px">';
                    html += '<button type="button" class="btn btn-default" data-bs-toggle="modal" title="View info" data-bs-target="#my-modal" onclick="getCompany(\'' + company.id + '\');"><i class="bi bi-eye"></i></button>';
                    html += '<a href="' + company.page + '" class="btn btn-link" title="Visit site"><i class="bi bi-link-45deg"></i></a>';
                    html += '</td>';
                    html += '</tr>';
                });
                $('#page-info').html(`Page ${currentPage} of ${res.totalPages}`);
                $('#current-page').html(currentPage);

                $('#prev-page').toggleClass('disabled', !res.hasPreviousPage);

                $('#next-page').toggleClass('disabled', !res.hasNextPage);
                if (paginationId.is(':hidden')) {
                    paginationId.show();
                }
            } else {
                html += '<tr><td colspan="4">There are no items.</td></tr>';
                paginationId.hide();
            }
            $("#companies").html(html);
            $('#spinner').removeClass('d-flex').addClass('d-none');
        }
    });
}

$('#filter-form').submit(function (event) {
    event.preventDefault();
    currentPage = 1;
    getCompanies();
});

function previousPage() {
    if (currentPage > 0) {
        currentPage--;
        getCompanies();
    }
}

function nextPage() {
    currentPage++;
    getCompanies();
}

function getCompany(id) {
    $('#company-detail').empty();
    $('#spinner').removeClass('d-none').addClass('d-flex');
    $.ajax({
        type: 'GET',
        url: '/api/v1/companies/' + id,
        dataType: 'json',
        success: function (res) {
            $("#company-name").html(res.name);

            let html = '';
            html += '<a href="' + res.page + '">Visit site</a>';
            html += '<p>Location: ' + res.locationName + '</p>';
            if (res.benefits.length > 0)
                html += '<p>Benefits: ' + res.benefits.join(', ') + '.' + '</p>';
            if (res.skills.length > 0)
                html += '<p>Skills: ' + res.skills.join(', ') + '.' + '</p>';
            $('#company-detail').html(html);

            $('#spinner').removeClass('d-flex').addClass('d-none');
        }
    });
}

function search(text) {
    if (text.length < 3)
        return;
    $.ajax({
        type: 'GET',
        url: '/api/v1/search?query=' + text,
        dataType: 'json',
        success: function (res) {
            console.log(res);
            let html = '<ul>';
            $.each(res, (_, x) => {
                html += '<li onclick="completeText(\'' + x + '\');">' + x + '</li>';
            });
            html += '</ul>';
            $('#result-search').html(html);
        }
    });
}

function completeText(text) {
    $('#query').val(text);
    $('#result-search').empty();
}
