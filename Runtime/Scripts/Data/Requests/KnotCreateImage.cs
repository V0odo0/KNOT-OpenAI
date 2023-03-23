using System;
using Unity.Plastic.Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace Knot.OpenAI
{
    [Serializable]
    public class KnotCreateImage : KnotRequest<KnotCreateImage.Response>
    {
        public string Prompt
        {
            get => prompt;
            set => prompt = value;
        }
        [SerializeField] private string prompt;

        public int ImagesCount
        {
            get => n;
            set => n = value;
        }
        [SerializeField] private int n = 1;

        public ImageSize Size
        {
            get
            {
                switch (size)
                {
                    default:
                        return ImageSize.x256;
                    case "512x512":
                        return ImageSize.x512;
                    case "1024x1024":
                        return ImageSize.x1024;
                }
            }
            set
            {
                switch (value)
                {
                    case ImageSize.x256:
                        size = "256x256";
                        break;
                    case ImageSize.x512:
                        size = "512x512";
                        break;
                    case ImageSize.x1024:
                        size = "1024x1024";
                        break;
                }
            }
        }
        [SerializeField] private string size = "256x256";


        public KnotCreateImage() { }

        public KnotCreateImage(string prompt, int imagesCount, ImageSize size)
        {
            Prompt = prompt;
            ImagesCount = imagesCount;
            Size = size;
        }


        public override UnityWebRequest GetWebRequest()
        {
            return BuildWebRequest(KnotOpenAI.ProjectSettings.Endpoints.CreateImage);
        }

        public static KnotCreateImage FromPrompt(string prompt, ImageSize imageSize = ImageSize.x256)
        {
            return new KnotCreateImage(prompt, 1, imageSize);
        }


        [Serializable]
        public class Response : KnotResponseBase
        {
            public ResponseData[] Data => data;
            [SerializeField] private ResponseData[] data;
        }

        [Serializable]
        public class ResponseData
        {
            public string ImageUrl => url;
            [SerializeField] private string url;
        }

        public enum ImageSize
        {
            x256, x512, x1024 
        }
    }
}
