using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stacks.API.Models;
using Stacks.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Stacks.API.Controllers
{
    [Route("app")]
//    [Authorize]
    public class AppController: Controller
    {
        private IStacksRepository _repository;
        private ILogger<AppController> _logger;
        public AppController(IStacksRepository repository, ILogger<AppController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("")]
        public JsonResult GetStacks()
        {

            try
            {
                ICollection<Stack> stacks = new List<Stack>();
                stacks = _repository.GetStacks();
                ICollection<StackListItemViewModel> vms = new List<StackListItemViewModel>(stacks.Count);
                foreach (var stack in stacks)
                {
                    vms.Add(stack.ToListItemViewModel());
                }
                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json(vms);

            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to retrieve stacks.", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new Message(ex));
            }
        }

        [HttpGet("{stackId}")]
        public JsonResult GetStackById(Guid stackId)
        {

            try
            {
                Stack stack = _repository.GetStackById(stackId);
                StackViewModel vm = stack.ToViewModel() ;
                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json(vm);

            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to retrieve stack.", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new Message(ex));
            }
        }
        [HttpPost("")]
        public JsonResult AddStack([FromBody] StackListItemViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Stack newStack = vm.ToModel();
                    _repository.AddStack(newStack);
                    StackListItemViewModel livm = newStack.ToListItemViewModel();
                    if (_repository.SaveAll(User))
                    {
                        Response.StatusCode = (int)HttpStatusCode.OK;
                        return Json(livm);
                    }
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new Message(MessageType.Error, "Unable to save new stack"));
                }
                else
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new Message(ModelState));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to add stack.", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new Message(ex));
            }
        }

        [HttpPost("{stackId}")]
        public JsonResult AddRecord(Guid stackId, [FromBody] RecordViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Record newRecord = vm.ToModel();
                    _repository.AddRecord(newRecord, stackId);
                    RecordViewModel livm = newRecord.ToViewModel();
                    if (_repository.SaveAll(User))
                    {
                        Response.StatusCode = (int)HttpStatusCode.OK;
                        return Json(livm);
                    }
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new Message(MessageType.Error, "Unable to save new record"));
                }
                else
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new Message(ModelState));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to add record.", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new Message(ex));
            }
        }
    }
}
