using System;
using UnityEngine;

namespace Knot.OpenAI
{
    [Serializable]
    public class KnotEndpoint
    {
        public string Uri
        {
            get => _uri;
            set => _uri = value;
        }
        [SerializeField] private string _uri;


        public KnotEndpoint() { }

        public KnotEndpoint(string uri)
        {
            _uri = uri;
        }
    }
}
