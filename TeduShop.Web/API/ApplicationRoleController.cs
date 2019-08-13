using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using TeduShop.Common.Exceptions;
using TeduShop.Model.Model;
using TeduShop.Service;
using TeduShop.Web.InFrastructure.Core;
using TeduShop.Web.InFrastructure.Extension;
using TeduShop.Web.Models;

namespace TeduShop.Web.API
{

    [RoutePrefix("api/applicationRole")]
    [Authorize]
    public class ApplicationRoleController : ApiControllerBase
    {
        private IApplicationRoleService _appRoleService;
        public ApplicationRoleController(IErrorService errorService, IApplicationRoleService appRoleService) : base(errorService)
        {
            _appRoleService = appRoleService;
        }
        [Route("getlistpaging")]
        [HttpGet]
        public HttpResponseMessage GetListPaging(HttpRequestMessage request, int page, int pageSize, string filter = null)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int totalRow = 0;
                var model = _appRoleService.GetAll(page, pageSize, out totalRow, filter);
                IEnumerable<ApplicationRoleViewModel> modelVm = Mapper.Map<IEnumerable<ApplicationRole>, IEnumerable<ApplicationRoleViewModel>>(model);

                PaginationSet<ApplicationRoleViewModel> pagedSet = new PaginationSet<ApplicationRoleViewModel>()
                {
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize),
                    Items = modelVm
                };

                response = request.CreateResponse(HttpStatusCode.OK, pagedSet);

                return response;
            });
        }
        [Route("getlistall")]
        [HttpGet]
        public HttpResponseMessage GetListAll(HttpRequestMessage request)
        {
            HttpResponseMessage response = null;
            var model = _appRoleService.GetAll();
            var modelVm = Mapper.Map<IEnumerable<ApplicationRole>, IEnumerable<ApplicationRoleViewModel>>(model);
            response = request.CreateResponse(HttpStatusCode.OK, modelVm);

            return response;

        }
        [Route("detail/id")]
        [HttpGet]
        public HttpResponseMessage Details(HttpRequestMessage request, string id)
        {

            if (id == null)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest, nameof(id) + " not fuond");
            }
            ApplicationRole appRole = _appRoleService.GetDetail(id);

            if (appRole == null)
            {
                return request.CreateResponse(HttpStatusCode.NoContent, "not found");

            }
            return request.CreateResponse(HttpStatusCode.OK, appRole);
        }
        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, ApplicationRoleViewModel appRoleViewModel)
        {
            if (ModelState.IsValid)
            {
                var appRole = _appRoleService.GetDetail(appRoleViewModel.Id);
                try
                {
                    appRole.UpdateApplicationRole(appRoleViewModel, "update");
                    _appRoleService.Update(appRole);
                    _appRoleService.Save();
                    return request.CreateResponse(HttpStatusCode.OK, appRole);

                }
                catch (NameDuplicatedException dex)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, dex.Message);
                }
            }
            else
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }
        [Route("add")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, ApplicationRoleViewModel appRoleViewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationRole newAppRole = new ApplicationRole();
                newAppRole.UpdateApplicationRole(appRoleViewModel);
                try
                {
                    _appRoleService.Add(newAppRole);
                    _appRoleService.Save();
                    return request.CreateResponse(HttpStatusCode.OK, appRoleViewModel);
                }
                catch (NameDuplicatedException dex)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, dex.Message);
                }
            }
            else
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }
        [Route("delete")]
        [HttpDelete]
        public HttpResponseMessage Delete(HttpRequestMessage request, string id)
        {
            _appRoleService.Delete(id);
            _appRoleService.Save();
            return request.CreateResponse(HttpStatusCode.OK, id);
        }
        [Route("deletemulti")]
        [HttpDelete]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedList)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    return request.CreateResponse(HttpStatusCode.BadRequest, "khong hop le");
                }
                else
                {
                    var listItem = new JavaScriptSerializer().Deserialize<List<string>>(checkedList);
                    foreach (var item in listItem)
                    {
                        _appRoleService.Delete(item);
                    }
                    _appRoleService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, listItem.Count);
                }

                return response;
            });
        }
    }
}

