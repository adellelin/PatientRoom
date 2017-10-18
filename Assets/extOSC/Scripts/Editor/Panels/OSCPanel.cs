/* Copyright (c) 2017 ExT (V.Sigalkin) */

using UnityEngine;

using extOSC.Editor.Windows;

namespace extOSC.Editor.Panels
{
    public class OSCPanel
    {
        #region Public Vars

        public Rect ContentRect
        {
            get { return _contentRect; }
        }

        public OSCWindow Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        public string UniqueName
        {
            get { return _uniqueName; }
            set { _uniqueName = value; }
        }

        #endregion

        #region Protected Vars

        protected OSCWindow _parent;

        protected string _uniqueName;

        #endregion

        #region Private Vars

        private Rect _contentRect;

        #endregion

        #region Public Methods

        public OSCPanel(OSCWindow parent, string uniqueName)
        {
            _uniqueName = uniqueName;
            _parent = parent;
        }

        public virtual void SetContentRect(Rect rect)
        {
            _contentRect = rect;
        }

        public virtual void Update()
        { }

        public virtual void Draw()
        {
            GUILayout.BeginArea(_contentRect);
            // PRE DRAW
            PreDrawContent();

            _contentRect.x = _contentRect.y = 0;

            DrawContent(_contentRect);

            // POST DRAW
            PostDrawContent();

            GUILayout.EndArea();
        }

        #endregion

        #region Protected Methods

        protected virtual void PreDrawContent()
        { }

        protected virtual void DrawContent(Rect contentRect)
        { }

        protected virtual void PostDrawContent()
        { }

        #endregion
    }
}