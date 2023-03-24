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

        public int Timeout
        {
            get => _timeout;
            set => _timeout = value;
        }
        [SerializeField] private int _timeout = 10;


        public KnotEndpoint() { }

        public KnotEndpoint(string uri, int timeout = 10)
        {
            _uri = uri;
            _timeout = 10;
        }
    }
}
