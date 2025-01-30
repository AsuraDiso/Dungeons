using System;
using UnityEditor;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.UIElements;

namespace Dungeons.Editor
{
    [UxmlElement]
    public partial class PlayFromBootstrapButton : VisualElement
    {
        public event Action Click;
        public PlayFromBootstrapButton()
        {
            style.marginRight = 25;
            
            var smallIconTexture = (Texture2D)EditorGUIUtility.IconContent("SceneLoadIn").image;
            var iconTexture = (Texture2D)EditorGUIUtility.IconContent("PlayButton").image;
            var toggle = new EditorToolbarToggle(iconTexture);
            Add(toggle);
            
            var smallIconRoot = new VisualElement();
            smallIconRoot.style.position = Position.Absolute;
            smallIconRoot.style.flexGrow = 1;
            smallIconRoot.style.width = new Length(50, LengthUnit.Percent);
            smallIconRoot.style.height = new Length(50, LengthUnit.Percent);
            smallIconRoot.style.left = new Length(40, LengthUnit.Percent);
            smallIconRoot.style.top = new Length(55, LengthUnit.Percent);
            toggle.Add(smallIconRoot);
            
            var smallIcon = new Image { image = smallIconTexture };
            smallIcon.style.flexGrow = 1;
            smallIconRoot.Add(smallIcon);

            toggle.RegisterCallback<ClickEvent>(OnClick);
        }

        private void OnClick(ClickEvent evt)
        {
            Click?.Invoke();
        }
    }
}