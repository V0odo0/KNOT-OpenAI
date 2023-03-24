using System;
using UnityEngine;
using UnityEngine.Networking;

namespace Knot.OpenAI
{
    public abstract class KnotRequestBase : IDisposable
    {
        public UnityWebRequest WebRequest { get; protected set; }

        protected virtual UnityWebRequest BuildWebRequest(KnotEndpoint endPoint)
        {
            WebRequest?.Dispose();

            WebRequest = UnityWebRequest.Put(endPoint.Uri, JsonUtility.ToJson(this));
            WebRequest.timeout = endPoint.Timeout;
            WebRequest.method = "POST";
            WebRequest.SetRequestHeader("Content-Type", "application/json");
            WebRequest.SetRequestHeader("Authorization", $"Bearer {KnotOpenAI.ApiKey}");

            return WebRequest;
        }

        public abstract UnityWebRequest GetWebRequest();

        public virtual void Dispose()
        {
            WebRequest?.Dispose();
        }
    }
}
