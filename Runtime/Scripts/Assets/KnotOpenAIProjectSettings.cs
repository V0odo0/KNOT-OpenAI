using System;
using UnityEngine;

namespace Knot.OpenAI
{
    public class KnotOpenAIProjectSettings : ScriptableObject
    {
        internal static KnotOpenAIProjectSettings Empty => _empty ?? (_empty = CreateDefault());
        private static KnotOpenAIProjectSettings _empty;

        public string ApiKey
        {
#if UNITY_EDITOR
            get => _editorOnlyApiKey;
#else
            get => _runtimeApiKey;
#endif
        }
        [SerializeField] private string _runtimeApiKey;
#if UNITY_EDITOR
        [SerializeField] private string _editorOnlyApiKey;
#endif

        public EndpointPreset Endpoints => _endpoints;
        [SerializeField] private EndpointPreset _endpoints;

        public static KnotOpenAIProjectSettings CreateDefault()
        {
            var instance = CreateInstance<KnotOpenAIProjectSettings>();

            return instance;
        }


        [Serializable]
        public class EndpointPreset
        {
            public KnotEndpoint CreateCompletion => _createCompletion;
            [SerializeField] private KnotEndpoint _createCompletion = new KnotEndpoint("https://api.openai.com/v1/chat/completions");

            public KnotEndpoint CreateImage => _createImage;
            [SerializeField] private KnotEndpoint _createImage = new KnotEndpoint("https://api.openai.com/v1/images/generations");
        }
    }
}
