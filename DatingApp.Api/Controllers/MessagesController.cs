using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.Api.Data;
using DatingApp.Api.Dtos;
using DatingApp.Api.Helpers;
using DatingApp.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.Api.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Authorize]
    [Route("api/users/{userID}/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IDatingRepository repo;
        private readonly IMapper mapper;
        public MessagesController(IDatingRepository _repo, IMapper _mapper)
        {
            repo = _repo;
            mapper = _mapper;
        }

        [HttpGet("{id}", Name = "GetMessage")]
        public async Task<IActionResult> GetMessage(int userID, int id)
        {
            if (userID != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();  

            var messageFromRepo = await repo.GetMessage(id);

            if (messageFromRepo == null)
            {
                return NotFound();
            }

            return Ok(messageFromRepo);
        }

        [HttpGet]
        public async Task<IActionResult> GetMessagesForUser(int userID, [FromQuery]MessageParams messageParams)
        {
            if (userID != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();  

            messageParams.UserID = userID;

            var messagesFromRepo = await repo.GetMessagesForUser(messageParams);

            var messages = mapper.Map<IEnumerable<MessageToReturnDto>>(messagesFromRepo);

            Response.AddPagination(messagesFromRepo.CurrentPage, messagesFromRepo.PageSize, 
                messagesFromRepo.TotalCount, messagesFromRepo.TotalPages);

            return Ok(messages);
        }

        [HttpGet("thread/{recipientID}")]
        public async Task<IActionResult> GetMessageThread(int userID, int recipientID)
        {
            if (userID != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized(); 

            var messagesFromRepo = await repo.GetMessageThread(userID, recipientID);

            var messageThread = mapper.Map<IEnumerable<MessageToReturnDto>>(messagesFromRepo);

            return Ok(messageThread);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(int userID, MessageForCreationDto messageForCreationDto)
        {
            var sender = await repo.GetUser(userID);

            if (sender.ID != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
               return Unauthorized();  
            
            messageForCreationDto.SenderID = userID;

            var recipient = await repo.GetUser(messageForCreationDto.RecipientID);

            if (recipient == null)
            {
                return BadRequest("چنین کاربری وجود ندارد");
            }

            var message = mapper.Map<Message>(messageForCreationDto);

            repo.Add(message);

            if (await repo.SaveAll())
            {
                var messageToReturn = mapper.Map<MessageToReturnDto>(message);

                return CreatedAtRoute("GetMessage", new {id = message.ID}, messageToReturn);
            }

            throw new Exception("خطا در ارسال پیام");
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> DeleteMessage(int id, int userID)
        {
            if (userID != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized(); 

            var messageFromRepo = await repo.GetMessage(id);

            if (messageFromRepo.SenderID == userID)
                messageFromRepo.SenderDeleted = true;

            if (messageFromRepo.RecipientID == userID)
                messageFromRepo.RecipientDeleted = true;
            
            if (messageFromRepo.SenderDeleted && messageFromRepo.RecipientDeleted)
                repo.Delete(messageFromRepo);

            if (await repo.SaveAll())
                return NoContent();

            throw new Exception("خطا در هنگام حذف پیام");
        }

        [HttpPost("{id}/read")]
        public async Task<IActionResult> MarkMessageAsRead(int userID, int id)
        {
            if (userID != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized(); 

            var message = await repo.GetMessage(id);

            if (message.RecipientID != userID)
                return Unauthorized();

            message.IsRead = true;
            message.DateRead = DateTime.Now;

            await repo.SaveAll();

            return NoContent();
        }
    }
}