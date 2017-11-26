using UnityEngine;
using System.Collections;

namespace DigitalRuby.AnimatedLineRenderer
{
    public class LineDraw : MonoBehaviour
    {
        public AnimatedLineRenderer AnimatedLine;
        public AnimatedLineRenderer AnimatedCheckLine;
        public AnimatedLineRenderer AnimatedCheckLine2;
        private Vector3[] linePositions;
        private Vector3[] linePositions2;
        private int linePoints;
        private int linePoints2;

        private int line1Points = 5;
        private int line2Points = 4;
        private int line3Points = 3;

        private Vector3 gameObjectPosition; 

        private void Start()
        {
            //DrawLine1();
        }

        private void Update()
        {
            if (AnimatedLine == null)
            {
                return;
            }

            else if (Input.GetMouseButton(0))
            {
         
            }
            else if (Input.GetKey(KeyCode.R))
            {
                AnimatedLine.ResetAfterSeconds(0.5f, null);
            }
    
        }

        public void ResetLine()
        {
            AnimatedLine.ResetAfterSeconds(0.5f, null);
        }

        public void ResetCheckLine()
        {
            AnimatedCheckLine.ResetAfterSeconds(0.5f, null);
        }

        public void ResetCheckLine2()
        {
            AnimatedCheckLine2.ResetAfterSeconds(0.5f, null);
        }



        public void DrawLine1()
        {
            // gameObjectPosition = transform.localPosition;
            AnimatedLine.StartWidth = 0.05f;
            linePositions = new Vector3[line1Points];
            linePositions[0] = new Vector3(0.18f, -.02f, 0);
            linePositions[1] = new Vector3(0.2f, -.06f, 0f);
            linePositions[2] = new Vector3(0.3f, -.08f, 0f);
            linePositions[3] = new Vector3(0.4f, -.09f, 0f);
            linePositions[4] = new Vector3(0.5f, -.1f, 0f);
            for (int i = 0; i < line1Points; i++)
            {
                Vector3 pos = linePositions[i];

                //pos = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, AnimatedLine.transform.position.z));

                AnimatedLine.Enqueue(pos, 1);
            }
        }

        public void DrawLine2()
        {
            AnimatedLine.StartWidth = 0.03f;
            AnimatedLine.EndWidth = 0.005f;
            // gameObjectPosition = transform.localPosition;
            linePositions = new Vector3[line2Points];
            linePositions[0] = new Vector3(0.18f, -.02f, 0);
            linePositions[1] = new Vector3(0.2f, -.06f, 0f);
            linePositions[2] = new Vector3(0.3f, -.08f, 0f);
            linePositions[3] = new Vector3(0.4f, -.09f, 0f);

            for (int i = 0; i < line2Points; i++)
            {
                Vector3 pos = linePositions[i];

                //pos = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, AnimatedLine.transform.position.z));

                AnimatedLine.Enqueue(pos, 1);
            }
        }

        public void DrawLine3()
        {
            AnimatedLine.StartWidth = 0.015f;
            AnimatedLine.EndWidth = 0.005f;
            // gameObjectPosition = transform.localPosition;
            linePositions = new Vector3[line3Points];
            linePositions[0] = new Vector3(0.18f, -.04f, 0);
            linePositions[1] = new Vector3(0.2f, -.06f, 0f);
            linePositions[2] = new Vector3(0.3f, -.08f, 0f);

            for (int i = 0; i < line3Points; i++)
            {
                Vector3 pos = linePositions[i];
                AnimatedLine.Enqueue(pos, 1);
            }
        }

        public void DrawCheckLine1()
        {
            AnimatedCheckLine.StartWidth = 0.012f;
            AnimatedCheckLine.EndWidth = 0.005f;
            linePoints = 4;
            // gameObjectPosition = transform.localPosition;
            linePositions = new Vector3[linePoints];
            linePositions[0] = new Vector3(0.16f, -.02f, 0);
            linePositions[1] = new Vector3(0.22f, -.06f, 0f);
            linePositions[2] = new Vector3(0.22f, -.02f, 0f);
            linePositions[3] = new Vector3(0.16f, -.06f, 0f);

            for (int i = 0; i < linePoints; i++)
            {
                Vector3 pos = linePositions[i];
                AnimatedCheckLine.Enqueue(pos, 0.5f);
            }
        }

        public void DrawCheckLine2()
        {
            AnimatedCheckLine2.StartWidth = 0.005f;
            AnimatedCheckLine2.EndWidth = 0.010f;
            linePoints2 = 4;
            // gameObjectPosition = transform.localPosition;
            linePositions2 = new Vector3[linePoints2];
            linePositions2[0] = new Vector3(0.16f, -.02f, 0);
            linePositions2[1] = new Vector3(0.22f, -.06f, 0f);
            linePositions2[2] = new Vector3(0.22f, -.02f, 0f);
            linePositions2[3] = new Vector3(0.16f, -.06f, 0f);

            for (int i = 0; i < linePoints; i++)
            {
                Vector3 pos = linePositions2[i];
                AnimatedCheckLine2.Enqueue(pos, 0.5f);
            }
        }
    }
}