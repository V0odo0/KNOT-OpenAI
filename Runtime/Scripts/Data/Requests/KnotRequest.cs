using System;
using UnityEngine;
using UnityEngine.Networking;

namespace Knot.OpenAI
{
    public abstract class KnotRequest<TResponse> : KnotRequestBase where TResponse : KnotResponseBase
    {
        public virtual TResponse GetResponse()
        {
            if (WebRequest == null)
                return default;

            var response = JsonUtility.FromJson<TResponse>(WebRequest.downloadHandler.text);
            response.WebRequest = WebRequest;
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
