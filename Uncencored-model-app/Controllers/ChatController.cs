using Microsoft.AspNetCore.Mvc;
using Uncencored_model_app.Models;
using Uncencored_model_app.Services;

namespace Uncencored_model_app.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController(ChatService chatService) : ControllerBase
{
    private readonly ChatService _chatService = chatService;

    [HttpPost("chatles")]
    public async Task<IActionResult> Chatles([FromBody] ChatRequest chatRequest)
    {
        if (string.IsNullOrEmpty(chatRequest.Message))
        {
            return BadRequest("Message cannot be empty!");
        }

        var response = await _chatService.SendChatMessage(chatRequest.Message);

        return Ok(new { Response = response });
    }
}
