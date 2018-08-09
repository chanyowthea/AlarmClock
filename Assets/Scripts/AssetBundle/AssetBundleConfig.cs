using UnityEngine;
using System.Collections;
namespace HotFix
{
    public class Context
    {
        static public string AssetBundlePrefix = "AssetBundles";

        static public string LocalAddr = Application.streamingAssetsPath + "/" + PlatformIdentifier;

        static public string PlatformIdentifier
        {
            get
            {
#if UNITY_STANDALONE_WIN
                return "win";
#elif UNITY_IPHONE
                return "ios";
#elif UNITY_ANDROID
                return "android";
#else
                return "unsupported";
#endif
            }
        }

        static public string AssetBundleManifestPath
        {
            get
            {
                return Context.AssetBundlePrefix;
            }
        }

        public static string _assetBundlePath = LocalAddr + "/" + AssetBundlePrefix + "/";
        public static string _assetBundleSuffix = ".unity3d";
    }
}

public class AssetBundleConfig : MonoBehaviour
{
    private void Start()
    {

    }

}