#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using IAssetBundle;
#endif
public class AssetBundleWindow : EditorWindow
{
    #region static

    [MenuItem("Allen/Tool/window")]
    static void AddWindow()
    {
        //AssetBundleWindow windon = GetWindow<AssetBundleWindow>("Asset Bundle", true);
        Rect rect = new Rect(0, 0, 600, 300);
        AssetBundleWindow window = (AssetBundleWindow)EditorWindow.GetWindowWithRect(typeof(AssetBundleWindow), rect, true, "widow name");
        window.Show();
    }

    #endregion

    #region 内部

    public bool _is_test = false;
    void OnGUI()
    {
        //try
        {
            _init();
            GUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Add", GUILayout.ExpandWidth(false)))
                {
                    config.filters.Add(new AssetBundleWindowFilter());
                }
                if (GUILayout.Button("Save", GUILayout.ExpandWidth(false)))
                {
                    _save();
                }
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("BuildAssetBundleOptions:", GUILayout.Width(200));
                config.bundle_options = (BuildAssetBundleOptions)EditorGUILayout.EnumPopup(config.bundle_options, GUILayout.ExpandWidth(false));
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("BuildTarget:", GUILayout.Width(200));
                config.bundle_platform = (BuildTarget)EditorGUILayout.EnumPopup(config.bundle_platform, GUILayout.ExpandWidth(false));
            }
            GUILayout.EndHorizontal();

            GUILayout.Space(15);

            GUILayout.BeginVertical();
            {
                int length = config.filters.Count;
                for (int i = 0; i < length; i++)
                {
                    AssetBundleWindowFilter filter = config.filters[i];
                    GUILayout.BeginHorizontal();
                    {
                        filter.valid = GUILayout.Toggle(filter.valid, "", GUILayout.ExpandWidth(false));
                        filter.path = GUILayout.TextField(filter.path, GUILayout.Width(250));
                        if (GUILayout.Button("Select", GUILayout.ExpandWidth(false)))
                        {
                            string data_path = Application.dataPath;
                            string select_data_path = EditorUtility.OpenFolderPanel("Path", data_path, "");
                            if (!string.IsNullOrEmpty(select_data_path) && select_data_path.StartsWith(data_path))
                                filter.path = "Assets/" + select_data_path.Substring(data_path.Length + 1);
                            else
                                ShowNotification(new GUIContent("select file is must be in Asset "));
                        }
                        filter.filter = GUILayout.TextField(filter.filter, GUILayout.Width(150));

                        if (GUILayout.Button("remove", GUILayout.ExpandWidth(false)))
                        {
                            config.filters.RemoveAt(i);
                            i--;
                        }
                    }
                    GUILayout.EndHorizontal();
                }
            }
            GUILayout.EndVertical();

            GUILayout.Space(15);
            bool bundle = false;

            GUILayout.BeginVertical();
            {
                if (GUILayout.Button("Build AssetBundle", GUILayout.ExpandWidth(false)))
                    bundle = true;
            }
            GUILayout.EndVertical();

            if (GUI.changed) EditorUtility.SetDirty(config);
            if (bundle) _build();
        }
//         catch (System.Exception e)
//         {
//             Close();
//             Debug.LogError(e.Message);
//             EditorUtility.DisplayDialog("Error", e.Message, "OK");
//         }
    }

    void OnFocus()
    {
        //Debug.Log("当窗口获得焦点时调用一次");
    }

    void OnLostFocus()
    {
        //Debug.Log("当窗口丢失焦点时调用一次");
    }

    void OnHierarchyChange()
    {
        //Debug.Log("当Hierarchy视图中的任何对象发生改变时调用一次");
    }

    void OnProjectChange()
    {
        //Debug.Log("当Project视图中的资源发生改变时调用一次");
    }

    void OnInspectorUpdate()
    {
        //this.Repaint();
    }

    void OnSelectionChange()
    {
        foreach (Transform t in Selection.transforms)
        {
            Debug.Log("OnSelectionChange" + t.name);
        }
    }

    void OnDestroy()
    {
        //Debug.Log("当窗口关闭时调用");
    }

    #endregion

    void _init()
    {
        if (config == null)
        {
            config = ABhelper.LoadAssetAtPath<AssetBundleWindowConfig>(savePath);
            if (config == null)config = new AssetBundleWindowConfig();
        }
    }

    void _save()
    {
        if (config == null) return;
        config.removeInvalidFilter();
        if (ABhelper.LoadAssetAtPath<AssetBundleWindowConfig>(savePath) == null)
            ABhelper.SaveAssetAtPath(savePath,config);
        else
            EditorUtility.SetDirty(config);
    }

    void _build()
    {
        _save();
        BuildAssetBundle.BuildAssetBundles(config);
    }


    public AssetBundleWindowConfig config;
    const string savePath = "Assets/config/config.asset";
}


