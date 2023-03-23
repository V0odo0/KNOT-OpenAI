using System;
using UnityEngine;
using UnityEngine.Networking;

namespace Knot.OpenAI
{
    public class KnotCreateCompletion : KnotRequest<KnotCreateCompletion.Response>
    {
        public string Model
        {
            get => model;
            set => model = value;
        }
        [SerializeField] private string model = "gpt-3.5-turbo";

        public MessageRequest[] Messages
        {
            get => messages;
            set => messages = value;
        }
        [SerializeField] private MessageRequest[] messages;


        public KnotCreateCompletion()
        {

        }

        public KnotCreateCompletion(string model, params MessageRequest[] messages)
        {
            this.model = model;
            this.messages = messages;
        }

        public KnotCreateCompletion(params MessageRequest[] messages)
        {
            this.messages = messages;
        }


        public override UnityWebRequest GetWebRequest()
        {
            return BuildWebRequest(KnotOpenAI.ProjectSettings.Endpoints.CreateCompletion);
        }
        

        public static KnotCreateCompletion FromMessage(string message)
        {
            return new KnotCreateCompletion(new MessageRequest(message));
        }
        

        [Serializable]
        public class MessageRequest
        {
            public string Role
            {
                get => role;
                set => role = value;
            }
            [SerializeField] private string role = "user";

            public string Message
            {
                get => content;
                set => content = value;
            }
            [SerializeField] private string content;


            public MessageRequest() { }
            
            public MessageRequest(string role, string message)
            {
                this.role = message;
                this.content = message;
            }

            public MessageRequest(string message)
            {
                this.content = message;
            }
        }

        [Serializable]
        public class Response : KnotResponseBase
        {
            public ResponseChoice[] Choices => choices;
            [SerializeField] private ResponseChoice[] choices;
        }

        [Serializable]
        public struct ResponseChoice
        {
            public string Message => message.Message;
            [SerializeField] private ResponseChoiceMessage message;
        }

        [Serializable]
        public struct ResponseChoiceMessage
        {
            public string Message => content;
            [SerializeField] private string content;
        }
    }
}
