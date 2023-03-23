using System;
using UnityEngine;
using UnityEngine.Networking;

namespace Knot.OpenAI
{
    public abstract class KnotRequest<TResponse> : KnotRequestBase where TResponse : KnotResponseBase
    {
        public virtual TResponse GetResponse()
        {
            if (_webRequest == null)
                return default;

            var response = JsonUtility.FromJson<TResponse>(_webRequest.downloadHandler.text);
            response.WebRequest = _webRequest;
            return response;
        }
    }

    [Serializable]
    public class KnotResponseBase : IDisposable
    {
        public UnityWebRequest WebRequest { get; set; }

        public virtual void Dispose()
        {
            WebRequest?.Dispose();
        }
    }
}
