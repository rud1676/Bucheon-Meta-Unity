using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AndroidKeystore : ScriptableObject
{

    public const string CustomSettingsPath = "Assets/Editor/Android Keystore Password.asset";

    [SerializeField] private string keystorePassword;
    [SerializeField] private string keyaliasPassword;

    public void UpdatePlayerSettings()
    {
        var i = GetOrCreateSettings();
        PlayerSettings.Android.keystorePass = i.keystorePassword;
        PlayerSettings.Android.keyaliasPass = i.keyaliasPassword;
    }

    internal static AndroidKeystore GetOrCreateSettings()
    {
        var settings = AssetDatabase.LoadAssetAtPath<AndroidKeystore>(CustomSettingsPath);
        if (settings == null)
        {
            settings = ScriptableObject.CreateInstance<AndroidKeystore>();
            AssetDatabase.CreateAsset(settings, CustomSettingsPath);
            AssetDatabase.SaveAssets();
        }
        return settings;
    }

    internal static SerializedObject GetSerializedSettings()
    {
        return new SerializedObject(GetOrCreateSettings());
    }

    [SettingsProvider]
    public static SettingsProvider CreateSettingsProvider()
    {
        // First parameter is the path in the Settings window.
        // Second parameter is the scope of this setting: it only appears in the Project Settings window.
        var provider = new SettingsProvider("Project/Waker/Android Keystore Password", SettingsScope.Project)
        {
            // By default the last token of the path is used as display name if no label is provided.
            label = "Android Keystore Password",
            // Create the SettingsProvider and initialize its drawing (IMGUI) function in place:
            guiHandler = (searchContext) =>
            {
                var settings = AndroidKeystore.GetSerializedSettings();
                settings.Update();
                EditorGUILayout.PropertyField(settings.FindProperty("keystorePassword"), new GUIContent("keystorePassword"));
                EditorGUILayout.PropertyField(settings.FindProperty("keyaliasPassword"), new GUIContent("keyaliasPassword"));
                settings.ApplyModifiedProperties();
                if (GUILayout.Button("Update"))
                {
                    AndroidKeystore.GetOrCreateSettings().UpdatePlayerSettings();
                }
            },
            // Populate the search keywords to enable smart search filtering and label highlighting:
            // keywords = new HashSet<string>(new[] { "Number", "Some String" })

        };
        return provider;
    }
}
