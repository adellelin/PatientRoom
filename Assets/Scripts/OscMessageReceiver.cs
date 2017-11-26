/* Copyright (c) 2017 ExT (V.Sigalkin) */

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using DigitalRuby.AnimatedLineRenderer;

namespace extOSC.Examples
{
    public class OscMessageReceiver : MonoBehaviour
    {
        #region Public Vars
        public movieStart moviePlayer;
        public Animator pulseAnimator;
        public VideoPlayer videoCross;
        public GameObject videoCrossParent;
        public VideoPlayer videoLine;
        public GameObject videoLineParent;
        public GameObject spherePos;
        public Text osc;
        public Slider slider;
        public LineDraw lineDrawClass;

        #endregion

        #region Private Vars
        bool drawCheck1 = false;
        bool drawCheck2 = false;
        public Text painScaleText;
        #endregion

        #region OSC Addresses
        public string Address = "/1/easy/1";
        private const string _toggleAddress = "/2/toggle1";
        private const string _toggleAddress2 = "/2/toggle2";
        private const string _toggleAddress3 = "/2/toggle3";
        private const string _toggleAddressA = "/1/toggle1";
        private const string _toggleAddressA2 = "/1/toggle2";
        private const string _toggleAddressA3 = "/1/toggle3";
        private const string _buttonAddress1 = "/2/push1";
        private const string _faderAddress = "/1/fader5";
        private const string _faderCheckAddress1 = "/1/fader1";
        private const string _faderCheckAddress2 = "/1/fader2";

        [Header("OSC Settings")]
        public OSCReceiver Receiver;

        #endregion

        #region Unity Methods



        protected virtual void Start()
        {
            Receiver.Bind(Address, ReceivedMessage);
            Receiver.Bind(_toggleAddress, toggleCrossDraw);
            Receiver.Bind(_toggleAddress2, togglePulse);
            Receiver.Bind(_toggleAddress3, toggleTDLine);
            Receiver.Bind(_faderAddress, faderMethod);
            Receiver.Bind(_toggleAddressA, toggleDrawOnLine1);
            Receiver.Bind(_toggleAddressA2, toggleDrawnOnLine2);
            Receiver.Bind(_toggleAddressA3, toggleDrawnOnLine3);
            Receiver.Bind(_faderCheckAddress1, toggleDrawnOnCheck1);
            Receiver.Bind(_faderCheckAddress2, toggleDrawnOnCheck2);

            painScaleText.text = "0";
        }

        #endregion

        #region Private Methods

        private void ReceivedMessage(OSCMessage message)
        {
            Debug.LogWarning("Received: " + message);
            //osc.text = "bear: " + message.ToString();
         
     
        }

        private void toggleCrossDraw(OSCMessage message)
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

        private void togglePulse(OSCMessage message)
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

        private void toggleTDLine(OSCMessage message)
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
            float painScale;
            if (message.ToFloat(out value))
            {

            
                slider.value = value;
                
                painScale = value * 7;
                painScaleText.text = Mathf.Ceil(painScale).ToString();
                //Debug.LogWarning(painScaleText + " pain");
            }
        }

        private void toggleDrawOnLine1(OSCMessage message)
        {
            float value;
            if (message.ToFloat(out value))
            {
                if (value == 1)
                {
                    lineDrawClass.DrawLine1();
                }
                else
                {
                    lineDrawClass.ResetLine();
                }
            }

        }

        private void toggleDrawnOnLine2(OSCMessage message)
        {
            float value;
            if (message.ToFloat(out value))
            {
                if (value == 1)
                {
                    lineDrawClass.DrawLine2();
                }
                else
                {
                    lineDrawClass.ResetLine();
                }
            }

        }

        private void toggleDrawnOnLine3(OSCMessage message)
        {
            float value;
            if (message.ToFloat(out value))
            {
                if (value == 1)
                {
                    lineDrawClass.DrawLine3();
                }
                else
                {
                    lineDrawClass.ResetLine();
                }
            }

        }


        private void toggleDrawnOnCheck1(OSCMessage message)
        {
            float value;
            if (message.ToFloat(out value))
            {
                if (value > 0.2 && !drawCheck1)
                {
                    lineDrawClass.DrawCheckLine1();
                    drawCheck1 = true;
                }
                else if (value < 0.2 && drawCheck1)
                {
                    lineDrawClass.ResetCheckLine();
                    drawCheck1 = false;
                }
            }

        }

        private void toggleDrawnOnCheck2(OSCMessage message)
        {
            float value;
            if (message.ToFloat(out value))
            {
                if (value > 0.2 && !drawCheck2)
                {
                    lineDrawClass.DrawCheckLine2();
                    drawCheck2 = true;
                }
                else if (value < 0.2 && drawCheck2)
                {
                    lineDrawClass.ResetCheckLine2();
                    drawCheck2 = false;
                }
            }

        }
        #endregion
    }

}