using UnityEditor;
using UnityEngine;

namespace Knot.OpenAI.Editor
{
    [CustomEditor(typeof(KnotOpenAIProjectSettings))]
    public class KnotOpenAIProjectSettingsEditor : UnityEditor.Editor
    {
        internal static string SettingsPath = $"Project/KNOT/OpenAI";
        internal static string[] DefaultKeyWords = 
        {
            "knot",
            "ai",
            "openai",
            "gpt",
            "dalle"
        };


        public override void OnInspectorGUI()
        {
            if (target == null)
                return;

            serializedObject.Update();
            SerializedProperty property = serializedObject.GetIterator();
            if (property.NextVisible(true))
            {
                do
                {
                    if (property.name == "m_Script")
                        continue;

                    EditorGUILayout.PropertyField(serializedObject.FindProperty(property.name), true);
                }
                while (property.NextVisible(false));
            }
            serializedObject.ApplyModifiedProperties();
        }


        [SettingsProvider]
        static SettingsProvider GetSettingsProvider()
        {
            var provider = new SettingsProvider(SettingsPath, SettingsScope.Project, DefaultKeyWords);
            var editor = CreateEditor(KnotOpenAI.ProjectSettings);
            
            provider.guiHandler += s =>
            {
                editor.OnInspectorGUI();
            };

            return provider;
        }


        public static void Open()
        {
            SettingsService.OpenProjectSettings(SettingsPath);
        }
    }
}