using System;
using System.Reflection;
using UnityEditor;
using UnityEngine.UIElements;

namespace Dungeons.Editor
{
    [InitializeOnLoad]
    public static class CustomEditorToolbar
    {
        public static event Action Initialized;
        
        private static VisualElement _toolbarRoot;
        private static VisualElement _toolbarLeft;
        private static VisualElement _toolbarPlayButtons;
        private static VisualElement _toolbarRight;
        private static VisualElement _customToolbarLeft;
        private static VisualElement _customToolbarRight;
        
        static CustomEditorToolbar()
        {
            EditorApplication.update += OnUpdate;
        }
        
        public static void AddLeft(VisualElement element)
        {
            _customToolbarLeft.Add(element);
        }
        
        public static void AddMiddle(VisualElement element)
        {
            _toolbarPlayButtons.Add(element);
        }
        
        public static void AddRight(VisualElement element)
        {
            _customToolbarRight.Add(element);
        }

        private static void TryInitialize()
        {
            if (_toolbarRoot != null)
            {
                return;
            }

            InitToolbarRoot();
            _toolbarLeft = _toolbarRoot.Q("ToolbarZoneLeftAlign");
            _toolbarPlayButtons = _toolbarRoot.Q(className: "unity-editor-toolbar__button-strip");
            _toolbarRight = _toolbarRoot.Q("ToolbarZoneRightAlign");

            _customToolbarLeft = new VisualElement
            {
                name = "custom-toolbar-left",
                style =
                {
                    flexGrow = 1,
                    flexDirection = FlexDirection.RowReverse,
                    overflow = Overflow.Hidden,
                },
            };
            _toolbarLeft.Add(_customToolbarLeft);

            _customToolbarRight = new VisualElement
            {
                name = "custom-toolbar-right",
                style =
                {
                    flexGrow = 1,
                    flexDirection = FlexDirection.Row,
                    overflow = Overflow.Hidden,
                },
            };
            _toolbarRight.Add(_customToolbarRight);
            
            Initialized?.Invoke();
        }

        private static void OnUpdate()
        {
            TryInitialize();
        }

        private static void InitToolbarRoot()
        {
            var toolbarType = typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.Toolbar");
            var toolbarObj = toolbarType.GetField("get").GetValue(null);
            _toolbarRoot = (VisualElement)toolbarType.GetField("m_Root",
                BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(toolbarObj);
        }
    }
}