using UnityEngine;
using System.Collections;

namespace LSE.INTERACTION
{
    public class I010_CameraController : MonoBehaviour
    {
        private Vector3 _startPos;
        private Vector3 _lastPos;
        // Rahmen der Kamerabewegung
        private float minX = 0;
        private float maxX = 1;
        // Bei falschen oder fehlerhaften Rahmen ist die Kamera unbeweglich
        private bool isStatic;

        public void Update()
        {
            if (!isStatic)
            {
                float xAxisValue = Input.GetAxis("Horizontal")/3.0f;
                if (Camera.current != null)
                {
                    transform.Translate(new Vector3(xAxisValue, 0, 0));
                    CheckBorders();
                }
            }

            if (transform.position != _lastPos)
            {
                _lastPos = transform.position;
                SendDeltaX();
            }
        }

        private void CheckBorders()
        {
            Vector3 p = transform.position;
            if (p.x > maxX)
                transform.position = new Vector3(maxX, p.y, p.z);
            else if (p.x < minX)
                transform.position = new Vector3(minX, p.y, p.z);
        }

        private void SendDeltaX()
        {
            SendDeltaX(_startPos.x - _lastPos.x);
        }

        private void SendDeltaX(float deltaX)
        {
            LSE.EVENT.E000_EventManager.Instance.Event("CAMERA", "Move", deltaX.ToString());
        }

        private void Awake()
        {
            LSE.EVENT.E000_EventManager.Instance.AddEventListener("CAMERA", CameraEventListener);
        }

        private void CameraEventListener(string _command, string _param = "")
        {
            if (_command == "Init")
            {
                string[] param = _param.Split(';');
                float[] values = new float[param.Length];
                for (int i = 0; i < param.Length; i++) values[i] = float.Parse(param[i]);
                _startPos = new Vector3(values[0], values[1], values[2]);
                transform.position = _startPos;
                _lastPos = _startPos;
                minX = values[3];
                maxX = values[4];
                if (maxX <= minX) isStatic = true;
                else isStatic = false;
            }
        }
    }
}