/* Copyright (c) 2017 ExT (V.Sigalkin) */

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace extOSC.Examples
{
    public class OscMessageReceiver : MonoBehaviour
    {
        #region Public Vars

        public string Address = "/1/easy/1";
        private const string _toggleAddress = "/1/toggle1";
        private const string _toggleAddress2 = "/1/toggle2";
        public movieStart moviePlayer;
        public Animator pulseAnimator;
        public VideoPlayer video;
        public GameObject videoParent;
        public GameObject spherePos;
        public Text osc;

        [Header("OSC Settings")]
        public OSCReceiver Receiver;

        #endregion

        #region Unity Methods



        protected virtual void Start()
        {
            Receiver.Bind(Address, ReceivedMessage);
            Receiver.Bind(_toggleAddress, toggleMethod);
            Receiver.Bind(_toggleAddress2, toggleMethod2);
         
        }

        #endregion

        #region Private Methods

        private void ReceivedMessage(OSCMessage message)
        {
            Debug.LogWarning("Received: " + message);
            //osc.text = "bear: " + message.ToString();
         
     
        }

        private void toggleMethod(OSCMessage message)
        {
            Debug.LogWarning("received toggle");
            float value;
            if (message.ToFloat(out value))
            {
                Debug.Log(value);
                int Intvalue = (int)value;
                if (Intvalue == 1)
                {
                    // moviePlayer.PlayMovie();
                    video.Play();
                    videoParent.SetActive(true);

                } else
                {
                    video.Stop();
                    videoParent.SetActive(false);
                }
            }
        }

        private void toggleMethod2(OSCMessage message)
        {
            float value;
            
            if (message.ToFloat(out value))
            {
                Debug.Log(value);
                if (value > 0.5f)
                {
                    spherePos.SetActive(true);
                    pulseAnimator.SetTrigger("startPulse");
                } else
                {
                    pulseAnimator.SetTrigger("stopPulse");
                    spherePos.SetActive(false);
                }
           
            }
          
        
        }

        #endregion
    }
}