/* Copyright (c) 2017 ExT (V.Sigalkin) */

using UnityEditor;
using UnityEngine;

using extOSC.Editor.Panels;

namespace extOSC.Editor.Windows
{
    public class OSCWindow : EditorWindow
    {
        #region Protected Vars

        protected OSCPanel rootPanel;

        #endregion

        #region Unity Methods

        protected virtual void Awake()
        { }

        protected virtual void Update()
        {
            if (rootPanel != null)
                rootPanel.Update();
        }

        protected virtual void OnEnable()
        {
            LoadWindowSettings();
        }

        protected virtual void OnDisable()
        {
            SaveWindowSettings();
        }

        protected virtual void OnDestroy()
        { }

        protected virtual void OnGUI()
        {
            DrawRootPanel(new Rect(0, 0, position.width, position.height));
        }

        #endregion

        #region Public Methods

        public void SetRoot(OSCPanel panel)
        {
            if (rootPanel != null)
            {
                Debug.LogErrorFormat("[{0}] Already has root panel!", GetType());
                return;
            }

            rootPanel = panel;
        }

        #endregion

        #region Protected Methods

        protected void DrawRootPanel(Rect contentRect)
        {
            if (rootPanel == null) return;

            rootPanel.Parent = this;
            rootPanel.SetContentRect(contentRect);
            rootPanel.Draw();
        }

        protected virtual void LoadWindowSettings()
        { }

        protected virtual void SaveWindowSettings()
        { }

        #endregion
    }

    public class OSCWindow<T> : OSCWindow where T : OSCWindow
    {
        #region Public Vars

        public static T Instance
        {
            get { return GetWindow<T>(false, "", false); }
        }

        #endregion
    }
}