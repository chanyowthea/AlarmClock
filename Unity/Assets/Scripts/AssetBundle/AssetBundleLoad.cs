using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AssetBundleLoad : MonoSingleton<AssetBundleLoad>
{
    //void OnGUI()
    //{
    //    if (GUILayout.Button("Load", GUILayout.Width(300), GUILayout.Height(200)))
    //    {
    //        LoadManifest();
    //        var go = InstanceAsset("bot");
    //        go.transform.localEulerAngles = new Vector3(-10, -50, 17); 
    //    }
    //}
    
    private static AssetBundleManifest manifest = null;
    private static Dictionary<string, AssetBundle> assetBundleDic = new Dictionary<string, AssetBundle>();

    public void LoadManifest()
    {
        // 加载StreamingAssets的AssetBundle
        var path = HotFix.Context.PlatformIdentifier + "/" + HotFix.Context.AssetBundlePrefix + HotFix.Context._assetBundleSuffix;
        AssetBundle manifestAssetBundle = AssetBundle.LoadFromFile(path);
        // 加载AssetBundleManifest
        manifest = (AssetBundleManifest)manifestAssetBundle.LoadAsset("AssetBundleManifest");
    }

    public AssetBundle LoadAssetBundle(string Url)
    {
        // 如果这个字典里有，那么加载字典中的AssetBundle
        if (assetBundleDic.ContainsKey(Url))
            return assetBundleDic[Url];

        if (manifest != null)
        {
            //获取当前加载AssetBundle的所有依赖项的路径
            string[] objectDependUrl = manifest.GetAllDependencies(Url);
            foreach (string tmpUrl in objectDependUrl)
            {
                //通过递归调用加载所有依赖项
                LoadAssetBundle(tmpUrl);
            }

            var path = HotFix.Context._assetBundlePath + HotFix.Context._assetBundleSuffix;
            Debug.Log("LoadAssetBundle " + path);
            assetBundleDic[Url] = AssetBundle.LoadFromFile(path);
            return assetBundleDic[Url];
        }
        return null;
    }

    public GameObject InstanceAsset(string assetBundleName)
    {
        string assetBundlePath = assetBundleName;
        int index = assetBundleName.LastIndexOf('/');
        string realName = assetBundleName.Substring(index + 1, assetBundleName.Length - index - 1);
        var bundle = LoadAssetBundle(assetBundlePath);
        if (bundle != null)
        {
            Object tmpObj = bundle.LoadAsset(realName);
            var go = GameObject.Instantiate(tmpObj);
            bundle.Unload(false);
            return (GameObject)go; 
        }
        return null; 
    }
}
