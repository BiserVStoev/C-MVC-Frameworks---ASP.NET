namespace TechJunk.Web.Controllers
{
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using PagedList;
    using TechJunk.Models.BindingModels.Messages;
    using TechJunk.Models.ViewModels.Messages;
    using TechJunk.Services.Interfaces;

    [RoutePrefix("messages")]
    [Authorize(Roles = "JunkLover")]
    public class MessagesController : Controller
    {
        private IMessagesService service;

        public MessagesController(IMessagesService service)
        {
            this.service = service;
        }

        [Route]
        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        [Route("Inbox")]
        public ActionResult Inbox(int? pageNumber)
        {
            var userId = this.User.Identity.GetUserId();

            var vms = this.service.GetInboxMessages(userId).ToPagedList(pageNumber ?? 1, 10);
            
            this.ViewBag.ActionToRender = "recieved";
            this.ViewBag.InboxActive = "active";

            return this.View("Index", vms);
        }

        [HttpGet]
        [Route("Sent")]
        public ActionResult Sent(int? pageNumber)
        {
            var userId = this.User.Identity.GetUserId();

            var vms = this.service.GetSentMessages(userId).ToPagedList(pageNumber ?? 1, 10);
            
            this.ViewBag.ActionToRender = "sent";
            this.ViewBag.SentActive = "active";

            return this.View("Index", vms);
        }

        [HttpGet]
        [Route("Message/{userId}")]
        public ActionResult Message(string userId)
        {
            if (userId == null)
            {
                return this.HttpNotFound();
            }

            if (!this.service.UserExists(userId))
            {
                return this.HttpNotFound("User does not exist");
            }

            AddMessageVm vm = new AddMessageVm();
            vm.RecieverId = userId;

            return this.View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Message/{userId}")]
        public ActionResult MessagePost(AddMessageBm bm)
        {
            if (this.ModelState.IsValid)
            {
                var currentUserId = this.User.Identity.GetUserId();

                this.service.AddMessage(bm, currentUserId);

                return this.RedirectToAction("Sent");
            }

            AddMessageVm vm = new AddMessageVm();
            vm.RecieverId = bm.RecieverId;

            return this.View("Message", vm);
        }

        [HttpGet]
        public ActionResult ReMessage(string firstId, string secondId)
        {
            var currentUserId = this.User.Identity.GetUserId();
            string recieverId = string.Empty;
            if (firstId == currentUserId)
            {
                recieverId = secondId;
            }
            else
            {
                recieverId = firstId;
            }

            return this.RedirectToAction("Message", new { userId = recieverId});
        }

        [HttpGet]
        [Route("Details/{messageId}")]
        [OutputCache(Duration = 86400)]
        public ActionResult Details(int messageId)
        {
            if (!this.service.MessageExists(messageId))
            {
                return this.HttpNotFound("Message not found.");
            }

            if (!this.service.MessageExistsInUserData(messageId))
            {
                return this.HttpNotFound("Message not found.");
            }

            var userId = this.User.Identity.GetUserId();
            if (!this.service.IsUserAuthenticatedToViewMessage(messageId, userId))
            {
                return this.HttpNotFound("Unnauthorized.");
            }

            DetailedMessageVm vm = this.service.GetMessageDetails(messageId);

            return this.View(vm);
        }

        [HttpGet]
        [Route("delete/{messageId}")]
        public ActionResult Delete(int messageId)
        {
            if (!this.service.MessageExists(messageId))
            {
                return this.HttpNotFound("Message not found.");
            }

            string userId = this.User.Identity.GetUserId();

            bool isValid = this.service.IsUserAuthenticatedToViewMessage(messageId, userId);

            if (isValid)
            {
                DeleteMessageVm vm = this.service.GetDeleteVm(messageId, userId);

                return this.View(vm);
            }

            return this.RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("delete")]
        public ActionResult DeletePost(DeleteMessageBm bm)
        {
            this.service.DeleteMessage(bm.Id, bm.UserId);

            return this.RedirectToAction("Index");
        }
    }
}