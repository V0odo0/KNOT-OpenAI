## Installation

Install via Package Manager

![Screenshot_3](https://user-images.githubusercontent.com/10213769/162617479-51c3d2d5-8573-44a2-bc56-8c68d09183f1.png)

```
https://github.com/V0odo0/KNOT-OpenAI.git
```

*or*

Add dependency to your /YourProjectName/Packages/manifest.json

```
"com.knot.openai": "https://github.com/V0odo0/KNOT-OpenAI.git",
```

Refer to Project Settings/KNOT/OpenAI to setup your [API key](https://platform.openai.com/account/api-keys)

## Usage example

```C#
 IEnumerator Start()
        {
            //Get chat completion
            var completionRequest = KnotCreateCompletion.FromMessage("Give me one fun fact");
            yield return completionRequest.GetWebRequest().SendWebRequest();
            var completionResponse = completionRequest.GetResponse();

            if (completionResponse.WebRequest.result == UnityWebRequest.Result.Success)
                Debug.Log(completionResponse.Choices.First().Message);

            completionResponse.Dispose();

            //Get DALL-E image URL
            var imageRequest = KnotCreateImage.FromPrompt("Draw me a cow");
            yield return imageRequest.GetWebRequest().SendWebRequest();
            var imageResponse = imageRequest.GetResponse();

            if (imageResponse.WebRequest.result == UnityWebRequest.Result.Success)
                Debug.Log(imageResponse.Data.First().ImageUrl);

            imageResponse.Dispose();
        }
```