using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using Object = UnityEngine.Object;
using UnityEditor.PackageManager.Requests;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Knot.OpenAI
{
    public static class KnotOpenAI
    {
        internal const string CoreName = "KNOT OpenAI";

        internal static KnotOpenAIProjectSettings ProjectSettings =>
            _projectSettings ?? (_projectSettings = LoadProjectSettings());
        private static KnotOpenAIProjectSettings _projectSettings;

        public static string ApiKey
        {
            get
            {
                if (Manager == null || string.IsNullOrEmpty(Manager.OverrideApiKey))
                    return ProjectSettings.ApiKey;

                return Manager.OverrideApiKey;
            }
            set
            {
                if (Manager != null)
                    Manager.OverrideApiKey = value;
            }
        }

        internal static KnotManager Manager => _manager;
        private static KnotManager _manager;


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        static void Init()
        {
            if (ProjectSettings == null)
                return;

            _manager = GetManager();
        }

        static KnotManager GetManager()
        {
            var manager = new GameObject($"{CoreName} Manager").AddComponent<KnotManager>();
            Object.DontDestroyOnLoad(manager);

            return manager;
        }

        static KnotOpenAIProjectSettings LoadProjectSettings()
        {
            KnotOpenAIProjectSettings settings;

#if UNITY_EDITOR
            var allSettings =
                AssetDatabase.FindAssets($"t:{nameof(KnotOpenAIProjectSettings)}").
                    Select(AssetDatabase.GUIDToAssetPath).
                    Select(AssetDatabase.LoadAssetAtPath<KnotOpenAIProjectSettings>).ToArray();

            if (allSettings.Length == 0)
            {
                string path = $"Assets/{nameof(KnotOpenAIProjectSettings)}.asset";
                settings = AssetDatabase.LoadAssetAtPath<KnotOpenAIProjectSettings>(path);

                if (settings == null)
                    settings = PlayerSettings.GetPreloadedAssets().OfType<KnotOpenAIProjectSettings>().FirstOrDefault();

                if (settings == null)
                {
                    var instance = KnotOpenAIProjectSettings.CreateDefault();
                    AssetDatabase.CreateAsset(instance, path);
                    AssetDatabase.SaveAssets();
                    settings = instance;

                    var preloadedAssets = PlayerSettings.GetPreloadedAssets();
                    PlayerSettings.SetPreloadedAssets(preloadedAssets.Append(settings).ToArray());
                }
            }
            else
            {
                settings = allSettings.FirstOrDefault(p => p.name.Equals(nameof(KnotOpenAIProjectSettings)));
                if (settings == null)
                    settings = allSettings.First();
            }
#else
            settings = Resources.FindObjectsOfTypeAll<KnotOpenAIProjectSettings>().FirstOrDefault();
#endif

            if (settings == null)
            {
                settings = KnotOpenAIProjectSettings.Empty;
                Log("Unable to load or create Project Settings. Empty Project Settings will be assigned.", LogType.Warning);
            }
            return settings;
        }

        internal static void Log(object message, LogType type, Object context = null)
        {
            message = $"{CoreName}: {message}";
            switch (type)
            {
                default:
                    Debug.Log(message, context);
                    break;
                case LogType.Error:
                    Debug.LogError(message, context);
                    break;
                case LogType.Warning:
                    Debug.LogWarning(message, context);
                    break;
            }
        }



        internal class KnotManager : MonoBehaviour
        {
            public string OverrideApiKey { get; set; }
        }
    }
}
