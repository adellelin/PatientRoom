/* Copyright (c) 2017 ExT (V.Sigalkin) */

using UnityEngine.Events;

using System;

namespace extOSC.Core.Events
{
    [Serializable]
    public class OSCEvent : UnityEvent
    {
        #region Protected Vars

        protected int _listenersCount;

        #endregion

        #region Public Methods

        public int GetRuntimeEventCount()
        {
            return _listenersCount;
        }

        new public void AddListener(UnityAction callback)
        {
            base.AddListener(callback);

            _listenersCount++;
        }

        new public void RemoveListener(UnityAction callback)
        {
            base.RemoveListener(callback);

            _listenersCount--;
        }

        new public void RemoveAllListeners()
        {
            base.RemoveAllListeners();

            _listenersCount = 0;
        }

        #endregion
    }

    [Serializable]
    public class OSCEvent<T> : UnityEvent<T>
    {
        #region Protected Vars

        protected int _listenersCount;

        #endregion

        #region Public Methods

        public int GetRuntimeEventCount()
        {
            return _listenersCount;
        }

        new public void AddListener(UnityAction<T> callback)
        {
            base.AddListener(callback);

            _listenersCount++;
        }

        new public void RemoveListener(UnityAction<T> callback)
        {
            base.RemoveListener(callback);

            _listenersCount--;
        }

        new public void RemoveAllListeners()
        {
            base.RemoveAllListeners();

            _listenersCount = 0;
        }

        #endregion
    }
}