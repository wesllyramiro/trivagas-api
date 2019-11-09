﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TriVagas.Application.Controllers;
using TriVagas.Services.Interfaces;
using TriVagas.Services.Notify;
using TriVagas.Services.Requests;

namespace TriVagas.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PageController : ApiController
    {
        private readonly IPageServer _pageService;
        private readonly ICompanyService _companyService;
        private readonly IUserService _userService;
        public PageController(
            IPageServer pageService,
            ICompanyService companyService, 
            IUserService userService,
            INotify _notify):base(_notify) 
        {
            _pageService = pageService;
            _companyService = companyService;
            _userService = userService;
        }
        /// <summary>
        /// Create new Page
        /// </summary>
        /// <param name="request"></param>
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public IActionResult CreatePage(CreatePageRequest request)
        {
            var user = _userService.GetById(request.UserId);

            var newCompany = _companyService.CreateCompany(request, user);

            if (newCompany != null) 
                _pageService.CreatePage(newCompany, user);

            return Response(code:201);
        }
    }
}