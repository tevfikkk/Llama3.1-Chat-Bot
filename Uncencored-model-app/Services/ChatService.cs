using OllamaSharp;
using System.Text;

namespace Uncencored_model_app.Services;

public class ChatService(OllamaApiClient ollamaApiClient)
{
    private readonly Chat _chat = new(ollamaApiClient);

    public async Task<string> SendChatMessage(string message)
    {
        var response = new StringBuilder();

        await foreach (var answerToken in _chat.Send(message))
        {
            response.Append(answerToken);
        }

        return response.ToString();
    }

}
