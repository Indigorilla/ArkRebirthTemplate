using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using OpenAI;
using UnityEngine;
using VContainer;

public class AiChatService
{
    [Inject] private Func<Transform, AiChatBubble> aiChatBubbleFactory;
    [Inject] private AiChatSetting aiChatSetting;
    
    private Dictionary<int, AiChatAgent> activeAiChatAgents = new();

    private OpenAIApi openAIApi;

    public void Initialize()
    {
        openAIApi = new OpenAIApi(aiChatSetting.ApiKey);
    }
    
    public void SetAiChatAgent(AiChatAgent agent)
    {
        if (activeAiChatAgents.ContainsKey(agent.Id))
        {
            return;
        }
        
        var aiChatBubble = aiChatBubbleFactory.Invoke(agent.transform);
        agent.Initialize(aiChatBubble);
        
        activeAiChatAgents.Add(agent.Id, agent);
    }

    public async UniTask<string> SendChat(string requestChat)
    {
        if (activeAiChatAgents.Count == 0)
        {
            return string.Empty;
        }
        
        var req = new CreateChatCompletionRequest
        {
            Model = "gpt-5-nano",
            Messages = new List<ChatMessage>
            {
                new ChatMessage
                {
                    Role = "user",
                    Content = requestChat
                }
            }
        };
        
        var res = await openAIApi.CreateChatCompletion(req);
        if(res.Error != null)
        {
            return string.Empty;
        }
        
        var firstAgent = activeAiChatAgents.FirstOrDefault();
        if (firstAgent.Value == null)
        {
            return string.Empty;
        }
        
        var resultString = string.Join("", res.Choices.Select(x => x.Message.Content));
        firstAgent.Value.ShowChat(resultString);
        
        return resultString;
    }
}
