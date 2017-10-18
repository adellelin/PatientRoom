/* Copyright (c) 2017 ExT (V.Sigalkin) */

using UnityEngine;
using UnityEngine.UI;

namespace extOSC.Examples
{
    public class OscMessageReceiver : MonoBehaviour
    {
        #region Public Vars

        public string Address = "/1/easy/1";

        public Text osc;

        [Header("OSC Settings")]
        public OSCReceiver Receiver;

        #endregion

        #region Unity Methods

        protected virtual void Start()
        {
            Receiver.Bind(Address, ReceivedMessage);
        }

        #endregion

        #region Private Methods

        private void ReceivedMessage(OSCMessage message)
        {
            Debug.LogWarning("Received: " + message);
            //osc.text = "bear: " + message.ToString();
        }

        #endregion
    }
}