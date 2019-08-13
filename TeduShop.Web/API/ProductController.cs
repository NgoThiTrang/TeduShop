using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using TeduShop.Model.Model;
using TeduShop.Service;
using TeduShop.Web.InFrastructure.Core;
using TeduShop.Web.InFrastructure.Extension;
using TeduShop.Web.Models;

namespace TeduShop.Web.API
{
    
        [RoutePrefix("api/product")]
        [Authorize]
        public class ProductController : ApiControllerBase
        {
            private IProductService _productService;

            public ProductController(IErrorService errorService, IProductService productService) :
                base(errorService)
            {
                this._productService = productService;
            }

            [Route("getall")]
            [HttpGet]
            public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
            {
                return CreateHttpResponse(request, () =>
                {
                    int totalRow = 0;

                    var model = _productService.GetAll(keyword);
                    totalRow = model.Count();
                    var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);
                    var responseData = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(query);
                    var paginationSet = new PaginationSet<ProductViewModel>() // vif hàm không thể trả về nhièu biến nên phải gom vào pagination để trả về
                    {
                        Items = responseData,

                        Page = page,
                        TotalCount = totalRow,
                        TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)

                    };
                    var response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                    return response;
                });
            }//e thấy cái response cuosoi cùng nó cũng bằng cái response data truyền vào như băng chỗ nào
             // response là cả cái cục to
             //responseData chỉ là cái data trog cục to đấy
             //bằng đau mà  băngf e hiểu rồi
             //thấy cai chỗ data kia chưa
             //  chính là cái responseData e gán vào đâý
            [Route("create")]
            [HttpPost]
            [AllowAnonymous]
            public HttpResponseMessage Create(HttpRequestMessage request, ProductViewModel productVm)
            {
                return CreateHttpResponse(request, () =>
                {
                    HttpResponseMessage response = null;
                    if (!ModelState.IsValid)
                    {
                        response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                    }
                    else
                    {
                        var newProduct = new Product();
                        newProduct.UpdateProduct(productVm);
                        newProduct.CreatedDate = DateTime.Now;
                        newProduct.CreatedBy = User.Identity.Name;
                        _productService.Add(newProduct);
                        _productService.Save();
                        var responseData = Mapper.Map<Product, ProductViewModel>(newProduct);
                        response = request.CreateResponse(HttpStatusCode.Created, responseData);
                    }
                    return response;
                });
            }
            [Route("getallparents")]
            [HttpGet]
            public HttpResponseMessage GetAll(HttpRequestMessage request)
            {

                return CreateHttpResponse(request, () =>
                {

                    var model = _productService.GetAll();
                    var responseData = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(model);

                    var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                    return response;
                });
            }
            [Route("getbyid/{id:int}")]
            [HttpGet]
            public HttpResponseMessage GetByID(HttpRequestMessage request, int id)
            {
                return CreateHttpResponse(request, () =>
                {
                    var model = _productService.GetById(id);
                    var responseData = Mapper.Map<Product, ProductViewModel>(model);
                    var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                    return response;

                }
                );
            }
            [Route("update")]
            [HttpPut]
            [AllowAnonymous]
            public HttpResponseMessage Update(HttpRequestMessage request, ProductViewModel productVm)
            {
                return CreateHttpResponse(request, () =>
                {
                    HttpResponseMessage response = null;
                    if (!ModelState.IsValid)
                    {
                        response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                    }
                    else
                    {
                        var dbProduct= _productService.GetById(productVm.ID);
                        dbProduct.UpdateProduct(productVm);
                        dbProduct.CreatedDate = DateTime.Now;
                        dbProduct.UpdatedBy = User.Identity.Name;
                        _productService.Update(dbProduct);
                        _productService.Save();
                        var responseData = Mapper.Map<Product, ProductViewModel>(dbProduct);
                        response = request.CreateResponse(HttpStatusCode.Created, responseData);
                    }
                    return response;
                });
            }
            [Route("delete")]
            [HttpDelete]
            [AllowAnonymous]
            public HttpResponseMessage Delete(HttpRequestMessage request, int id)
            {
                return CreateHttpResponse(request, () =>
                {
                    HttpResponseMessage response = null;
                    if (!ModelState.IsValid)
                    {
                        response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                    }
                    else
                    {
                        var oldProduct = _productService.Delete(id);
                        _productService.Save();

                        var responseData = Mapper.Map<Product, ProductViewModel>(oldProduct);
                        response = request.CreateResponse(HttpStatusCode.Created, responseData);
                    }

                    return response;
                });

            }
            [Route("deleteMultiple")]
            [HttpDelete]
            [AllowAnonymous]
            public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkProducts)
            {
                return CreateHttpResponse(request, () =>
                {
                    HttpResponseMessage response = null;
                    if (!ModelState.IsValid)
                    {
                        response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                    }
                    else
                    {
                        var listProduct = new JavaScriptSerializer().Deserialize<List<int>>(checkProducts);
                        foreach (var item in listProduct)
                        {
                            _productService.Delete(item);
                        }
                        _productService.Save();


                        response = request.CreateResponse(HttpStatusCode.OK, listProduct.Count);
                    }

                    return response;
                });

            }
        }
    }
