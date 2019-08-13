using AutoMapper;
using BotDetect.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeduShop.Model.Model;
using TeduShop.Service;
using TeduShop.Web.InFrastructure.Extension;
using TeduShop.Web.Models;

namespace TeduShop.Web.Controllers
{
    public class ContactController : Controller
    {
        IContactDetailService _contactDetailService;
        IFeedbackService _feedbackService;
        public ContactController( IContactDetailService contactDetailService, IFeedbackService feedbackService)
        {
            this._contactDetailService = contactDetailService;
            this._feedbackService = feedbackService;
        }
        // GET: Contact
        public ActionResult Index()
        {
            
            FeedbackViewModel viewModel = new FeedbackViewModel();
            viewModel.ContactDetail = GetDetail(); 
            return View(viewModel);
        }
        [HttpPost]
        [CaptchaValidation("CaptchaCode", "contactCaptcha", "Mã xác nhận không đúng")]
        public ActionResult SendFeedback(FeedbackViewModel feedbackViewModel)

        {
            if(ModelState.IsValid)
            {
                Feedback newFeedback = new Feedback();
                newFeedback.UpdateFeedback(feedbackViewModel);
                _feedbackService.Creat(newFeedback);
                _feedbackService.Save();
                ViewData["SuccessMsg"] = "gửi phản hồi thành công";
                feedbackViewModel.Name = "";
                feedbackViewModel.Email = "";
                feedbackViewModel.Message = "";
            }
            feedbackViewModel.ContactDetail = GetDetail();
            return View("Index", feedbackViewModel);
        }
        private ContactDetailViewModel GetDetail()
        {
            var model = _contactDetailService.GetDefaultContact();
            var contactViewModel = Mapper.Map<ContactDetail, ContactDetailViewModel>(model);
            return contactViewModel;
        }

    }
}