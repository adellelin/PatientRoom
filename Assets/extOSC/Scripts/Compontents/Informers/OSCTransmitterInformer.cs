/* Copyright (c) 2017 ExT (V.Sigalkin) */

using UnityEngine;

using System;

using extOSC.Core.Internals;

namespace extOSC.Components.Informers
{
    public abstract class OSCTransmitterInformer : OSCTransmitterComponent
    {
        #region Public Vars

        public abstract Type InformerType { get; }

        public bool InformOnChanged
        {
            get { return informOnChanged; }
            set { informOnChanged = value; }
        }

        public float InformInterval
        {
            get { return informInterval; }
            set { informInterval = Mathf.Max(value, 0); }
        }

        public OSCReflectionMember ReflectionTarget
        {
            get { return reflectionMember; }
            set
            {
                if (reflectionMember == value)
                    return;

                reflectionMember = value;

                UpdateCachedReferences();
            }
        }

        #endregion

        #region Protected Vars

        [SerializeField]
        protected OSCReflectionMember reflectionMember;

        [SerializeField]
        protected bool informOnChanged = true;

        [SerializeField]
        protected float informInterval = 0;

        protected OSCReflectionProperty cachedProperty
        {
            get
            {
                return _property;
            }
        }

        #endregion

        #region Private Vars

        private OSCReflectionProperty _property;

        #endregion

        #region Unity Methods

        protected void OnEnable()
        {
            informInterval = Mathf.Max(informInterval, 0);

            UpdateCachedReferences();
        }

#if UNITY_EDITOR
        protected void OnValidate()
        {
            UpdateCachedReferences();
        }
#endif

        #endregion

        #region Public Methods

        [Obsolete("\"SetProperty(OSCReflectionMember)\" is deprecated, please use \"ReflectionTarget\" instead.")]
        public void SetProperty(OSCReflectionMember property)
        {
            reflectionMember = property;
        }

        #endregion

        #region Private Methods

        private void UpdateCachedReferences()
        {
            if (reflectionMember != null && reflectionMember.IsValid())
            {
                _property = reflectionMember.GetProperty();
            }
            else
            {
                _property = null;
            }
        }

        #endregion
    }

    public abstract class OSCTransmitterInformer<T> : OSCTransmitterInformer
    {
        #region Public Vars

        public override Type InformerType
        {
            get { return typeof(T); }
        }

        #endregion

        #region Private Vars

        private T _previousValue;

        private float _sendTimer;

        #endregion

        #region Unity Methods

        protected virtual void Awake()
        {
            if (cachedProperty != null)
                _previousValue = (T)cachedProperty.GetValue();
        }

        protected virtual void Update()
        {
            if (cachedProperty == null) return;

            if (informOnChanged)
            {
                var currentValue = (T)cachedProperty.GetValue();

                if (!currentValue.Equals(_previousValue))
                {
                    Send();

                    _previousValue = currentValue;
                }
            }
            else
            {
                if (informInterval < float.Epsilon)
                {
                    Send();
                }
                else
                {
                    _sendTimer += Time.deltaTime;

                    if (informInterval < _sendTimer)
                    {
                        Send();

                        _sendTimer = 0;
                    }
                }
            }
        }

        #endregion

        #region Protected Methods

        protected override bool FillMessage(OSCMessage message)
        {
            if (cachedProperty != null)
            {
                FillMessage(message, (T)cachedProperty.GetValue());
                return true;
            }

            return false;
        }

        protected abstract void FillMessage(OSCMessage message, T value);

        #endregion
    }
}