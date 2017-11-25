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
        private const string _toggleAddress3 = "/1/toggle3";
        private const string _faderAddress = "/1/fader5";
        public movieStart moviePlayer;
        public Animator pulseAnimator;
        public VideoPlayer videoCross;
        public GameObject videoCrossParent;
        public VideoPlayer videoLine;
        public GameObject videoLineParent;
        public GameObject spherePos;
        public Text osc;
        public Slider slider;

        [Header("OSC Settings")]
        public OSCReceiver Receiver;

        #endregion

        #region Unity Methods



        protected virtual void Start()
        {
            Receiver.Bind(Address, ReceivedMessage);
            Receiver.Bind(_toggleAddress, toggleMethod);
            Receiver.Bind(_toggleAddress2, toggleMethod2);
            Receiver.Bind(_toggleAddress3, toggleMethod3);
            Receiver.Bind(_faderAddress, faderMethod);

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
                    videoCross.Play();
                    videoCrossParent.SetActive(true);

                } else
                {
                    videoCross.Stop();
                    videoCrossParent.SetActive(false);
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
                }
                else
                {
                    pulseAnimator.SetTrigger("stopPulse");
                    spherePos.SetActive(false);
                }

            }
        }

        private void toggleMethod3(OSCMessage message)
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
                    videoLine.Play();
                    videoLineParent.SetActive(true);

                }
                else
                {
                    videoLine.Stop();
                    videoLineParent.SetActive(false);
                }
            }
        }

        private void faderMethod(OSCMessage message)
        {
            float value;
            if (message.ToFloat(out value))
            {

                Debug.Log("value " + value);
                slider.value = value;
            }
        }

        

        #endregion
    }
}