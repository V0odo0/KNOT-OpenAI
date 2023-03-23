using System;
using UnityEngine;
using UnityEngine.Networking;

namespace Knot.OpenAI
{
    public abstract class KnotRequestBase : IDisposable
    {
        protected UnityWebRequest _webRequest;

        protected virtual UnityWebRequest BuildWebRequest(KnotEndpoint endPoint)
        {
            _webRequest?.Dispose();

            _webRequest = UnityWebRequest.Put(endPoint.Uri, JsonUtility.ToJson(this));
            _webRequest.method = "POST";
            _webRequest.SetRequestHeader("Content-Type", "application/json");
            _webRequest.SetRequestHeader("Authorization", $"Bearer {KnotOpenAI.ApiKey}");

            return _webRequest;
        }


        public abstract UnityWebRequest GetWebRequest();

        public virtual void Dispose()
        {
            _webRequest?.Dispose();
        }
    }
}
