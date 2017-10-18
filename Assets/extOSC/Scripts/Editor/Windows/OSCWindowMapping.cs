/* Copyright (c) 2017 ExT (V.Sigalkin) */

using UnityEditor;
using UnityEngine;

using extOSC.Editor.Panels;
using extOSC.Mapping;

namespace extOSC.Editor.Windows
{
    public class OSCWindowMapping : OSCWindow<OSCWindowMapping>
    {
        #region Static Public Methods

        [MenuItem("Window/extOSC/Mapping Window", false, 0)]
        public static void ShowWindow()
        {
            Instance.titleContent = new GUIContent("OSC Mapping", OSCEditorTextures.IronWall);
            Instance.minSize = new Vector2(550, 200);
            Instance.Show();
        }

        public static void OpenBundle(OSCMapBundle bundle)
        {
            ShowWindow();

            Instance.Focus();
            Instance.mappingPanel.CurrentMapBundle = bundle;
        }

        #endregion

        #region Protected Vars

        protected OSCPanelMapping mappingPanel;

        #endregion

        #region Private Vars

        private readonly string _lastFileSettings = OSCEditorSettings.Mapping + "lastfile";

        #endregion

        #region Unity Methods

        protected override void OnEnable()
        {
            mappingPanel = new OSCPanelMapping(this, "mappingWindowPanel");

            SetRoot(mappingPanel);

            base.OnEnable();
        }

        protected override void OnDestroy()
        {
            SaveWindowSettings();

            base.OnDestroy();
        }

        #endregion

        #region Protected Methods

        protected override void LoadWindowSettings()
        {
            var assetPath = OSCEditorSettings.GetString(_lastFileSettings, "");

            if (!string.IsNullOrEmpty(assetPath))
            {
                mappingPanel.CurrentMapBundle = AssetDatabase.LoadAssetAtPath<OSCMapBundle>(assetPath);
            }
        }

        protected override void SaveWindowSettings()
        {
            if (mappingPanel == null) return;

            mappingPanel.SaveCurrentMapBundle();

            if (mappingPanel.CurrentMapBundle != null)
            {
                OSCEditorSettings.SetString(_lastFileSettings, AssetDatabase.GetAssetPath(mappingPanel.CurrentMapBundle));
            }
            else
            {
                OSCEditorSettings.SetString(_lastFileSettings, "");
            }
        }

        #endregion
    }
}