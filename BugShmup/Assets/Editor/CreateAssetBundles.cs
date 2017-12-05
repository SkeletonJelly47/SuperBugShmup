using System.IO;
using UnityEditor;

public class CreateAssetBundles
{
    [MenuItem ("Assets/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        if (!Directory.Exists("AssetBundles"))
        {
            Directory.CreateDirectory("AssetBundles");
        }

        BuildPipeline.BuildAssetBundles("AssetBundles", BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
    }

}

