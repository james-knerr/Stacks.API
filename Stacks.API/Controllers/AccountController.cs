using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stacks.API.Helpers;
using Stacks.API.Models;
using Stacks.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Stacks.API.Controllers
{
    [Route("accounts")]
    public class AccountController: Controller
    {
        private IStacksRepository _repository;
        private ILogger<AccountController> _logger;
        public AccountController(IStacksRepository repository, ILogger<AccountController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpPost("sign-in")]
        public async Task<JsonResult> Login([FromBody] LoginViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    LoginResponseViewModel responseVM = await ProviderLogin(vm);
                    Response.StatusCode = (int)HttpStatusCode.OK;
                    return Json(responseVM);
                }

                else
                {
                    _logger.LogError("Invalid login model.");
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new Message(ModelState));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to retrieve user.", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new Message(ex));
            }
        }

        private async Task<LoginResponseViewModel> ProviderLogin(LoginViewModel vm)
        {
            LoginResponseViewModel resp = new LoginResponseViewModel();
            ICollection<Stack> stacks = new List<Stack>();
            stacks = _repository.GetStacks();
            ICollection<StackListItemViewModel> vms = new List<StackListItemViewModel>(stacks.Count);
            foreach (var stack in stacks)
            {
                vms.Add(stack.ToListItemViewModel());
            }
            resp.SessionInfo = new SessionInfoViewModel();
            resp.SessionInfo.IdToken = GuidMappings.Map(Guid.NewGuid());
            resp.SessionInfo.Username = vm.Username;
            resp.Stacks = vms;
            return resp;
        }
       }
}
