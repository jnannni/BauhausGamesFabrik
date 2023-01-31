using System.Collections.Generic;
using UnityEditor;
#if UNITY_2020_2_OR_NEWER
using UnityEditor.AssetImporters;
#else
using UnityEditor.Experimental.AssetImporters;
#endif
using UnityEngine;
using System.Linq;
using Yarn.Compiler;
using System.IO;
using UnityEditorInternal;
using System.Collections;

#if USE_UNITY_LOCALIZATION
using UnityEditor.Localization;
using UnityEngine.Localization.Tables;
using System;

public class AddAssetsToAssetTableCollectionWizard : EditorWindow {
    private AssetTableCollection assetTableCollection;

    private Type assetType = typeof(UnityEngine.AudioClip);

    private Dictionary<string, DefaultAsset> localesToFolders = new Dictionary<string, DefaultAsset>();
    private List<Type> allTypes;

    [MenuItem("Window/Yarn Spinner/Add Assets to Table Collection")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        var window = CreateWindow<AddAssetsToAssetTableCollectionWizard>();
        window.ShowPopup();
        window.titleContent = new GUIContent("Add Assets to Table Collection");

        if (Selection.activeObject is AssetTableCollection collection) {
            window.assetTableCollection = collection;
        }
    }

    void OnEnable() {
        allTypes = TypeCache.GetTypesDerivedFrom<UnityEngine.Object>().OrderBy(t => t.Name).ToList();
    }

    void OnGUI()
    {

        bool readyToAddAssets = true;

        using (new GUILayout.VerticalScope())
        {

            EditorGUILayout.BeginVertical();

            assetTableCollection = EditorGUILayout.ObjectField("Asset Table Collection", assetTableCollection, typeof(AssetTableCollection), allowSceneObjects: false) as AssetTableCollection;

            readyToAddAssets &= assetTableCollection != null;

            
            var selectedIndex = allTypes.IndexOf(assetType);

            using (var change = new EditorGUI.ChangeCheckScope()) {
                var newSelection = EditorGUILayout.Popup(new GUIContent("Asset Type"), selectedIndex, allTypes.Select(t => new GUIContent(
                    t.Name,
                    EditorGUIUtility.ObjectContent(null, t).image
                )).ToArray());

                if (change.changed) {
                    assetType = allTypes[newSelection];
                }
            }

            foreach (var locale in LocalizationEditorSettings.GetLocales())
            {
                LocalizationTable localizationTable = null;

                if (assetTableCollection != null)
                {
                    localizationTable = assetTableCollection.GetTable(locale.Identifier);
                }

                using (new EditorGUI.DisabledGroupScope(localizationTable == null))
                {
                    localesToFolders.TryGetValue(locale.Identifier.Code, out var currentFolder);

                    localesToFolders[locale.Identifier.Code] = EditorGUILayout.ObjectField(
                        locale.LocaleName,
                        currentFolder,
                        typeof(DefaultAsset),
                        allowSceneObjects: false) as DefaultAsset;
                }
            }

            // We can only add assets if at least one folder has been provided.
            readyToAddAssets &= localesToFolders.Where(kv => kv.Value != null).Count() > 0;


            GUILayout.FlexibleSpace();

            using (new GUILayout.HorizontalScope())
            {
                GUILayout.FlexibleSpace();

                using (new EditorGUI.DisabledGroupScope(!readyToAddAssets))
                {
                    if (GUILayout.Button("Add Assets")) {
                        AddAssets();
                        // this.Close();
                    }
                }
            }

            EditorGUILayout.EndVertical();
        }
    }

    private void AddAssets()
    {
        int totalCount = 0;
        foreach (var locale in LocalizationEditorSettings.GetLocales())
        {
            int perLocaleCount = 0;
            if (localesToFolders.TryGetValue(locale.Identifier.Code, out var folder) == false
                || folder == null)
            {
                // No folder given for this locale. Skip it!
                Debug.Log($"Skipping {locale.LocaleName} because no folder was provided");
                continue;
            }

            if (assetTableCollection.ContainsTable(locale.Identifier) == false)
            {
                // No table in this collection for this locale! Skip it!
                Debug.Log($"Skipping {locale.LocaleName} because no table exists");
                continue;
            }

            var path = AssetDatabase.GetAssetPath(folder);

            var allFiles = Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories).Where(path => !path.EndsWith(".meta"));

            foreach (var file in allFiles) {
                var asset = AssetDatabase.LoadAssetAtPath(file, assetType);

                if (asset == null) {
                    // Not the type of asset we're looking for
                    continue;
                }

                var keyName = Path.GetFileNameWithoutExtension(file);

                assetTableCollection.AddAssetToTable(locale.Identifier, keyName, asset);

                perLocaleCount += 1;
                totalCount += 1;
            }
            Debug.Log($"Added {perLocaleCount} assets to {locale.LocaleName}");
        }
        Debug.Log($"Added {totalCount} assets to asset table collection");
                
    }
}
#endif
