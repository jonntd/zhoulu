
using SummerEditor;
using UnityEditor;
using UnityEngine;


namespace Summer.Editor
{
    public class EditorMapLevel : EditorWindow
    {
        static float t_width = 1280;
        static float t_height = 720;
        public EComponent _container;

        public static void ShowWindown()
        {
            EditorMapLevel win = EditorWindow.GetWindow<EditorMapLevel>();
            win.minSize = new Vector2(t_width, t_height);
            win.maxSize = new Vector2(t_width + 40, t_height + 40);
            win.Show();
        }

        public EditorMapLevel()
        {
            _container = new EComponent(t_width, t_height);
            _container.show_bg = true;
            _container.ResetPosition(t_width / 2, t_height / 2);
            _container.SetBg(0, 0, 0, 1);
        }
    }
}

