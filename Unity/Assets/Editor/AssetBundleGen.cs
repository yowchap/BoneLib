#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class AssetBundleGen : EditorWindow
{
    private enum TargetPlatform
    {
        PCVR,
        Quest
    }

    private TargetPlatform m_targetPlatform;
    [SerializeField] private List<Object> m_resources;
    private string m_packName;
    private string m_exportLocation;
    private bool m_openFolderAfterExport;

    private const string m_extension = ".pack";
    private const string m_androidExtension = ".android" + m_extension;

    [MenuItem("BoneLib/Build Assets", false, 10)]
    public static void ShowWindow()
    {
        EditorWindow window = GetWindow(typeof(AssetBundleGen));
        window.titleContent = new GUIContent("Asset Builder");
    }

    private void OnEnable()
    {
        m_resources = new List<Object>();
    }

    private void OnGUI()
    {
        m_targetPlatform = (TargetPlatform)EditorGUILayout.EnumPopup("Platform:", m_targetPlatform);

        m_packName = EditorGUILayout.TextField(new GUIContent("Bundle Name"), m_packName);

        var target = this;
        SerializedObject so = new SerializedObject(target);
        SerializedProperty property = so.FindProperty("m_resources");

        EditorGUILayout.PropertyField(property, true);
        so.ApplyModifiedProperties();

        if (m_resources.Count == 0)
        {
            return;
        }

        if (GUILayout.Button("Build"))
        {
            string exportedPath = GetExportPath();

            AssetBundleBuild bundleBuild = CreateBundleBuild();

            Directory.CreateDirectory(exportedPath);

            GenerateBundles(exportedPath, bundleBuild);

            EditorUtility.DisplayDialog("Export completed!", $"Export completed at location:\n{exportedPath}", "OK");
        }
    }

    private string GetExportPath()
    {
        string editorExportLocation = Path.Combine(Application.dataPath, "AssetBundles");
        return Path.Combine(editorExportLocation, m_targetPlatform == TargetPlatform.PCVR ? "PCVR" : "Quest");
    }

    private AssetBundleBuild CreateBundleBuild()
    {
        List<string> assetNames = new List<string>();
        AssetBundleBuild build = new AssetBundleBuild();

        if (m_targetPlatform == TargetPlatform.PCVR)
        {
            build.assetBundleName += m_packName;
            build.assetBundleName += m_extension;
        }
        else
        {
            build.assetBundleName += m_packName;
            build.assetBundleName += m_androidExtension;
        }
        
        foreach (var resource in m_resources)
            assetNames.Add(AssetDatabase.GetAssetPath(resource));

        build.assetNames = assetNames.ToArray();

        return build;
    }

    private void GenerateBundles(string exportPath, AssetBundleBuild build)
    {
        BuildTarget buildTarget = m_targetPlatform == TargetPlatform.PCVR
            ? BuildTarget.StandaloneWindows64
            : BuildTarget.Android;

        BuildPipeline.BuildAssetBundles(exportPath, new AssetBundleBuild[1] { build }, BuildAssetBundleOptions.ChunkBasedCompression, buildTarget);
    }
}
#endif