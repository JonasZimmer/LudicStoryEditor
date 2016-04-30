using UnityEngine;
using System.Collections;
using LSE.VISUALIZATION;

namespace LSE.INTERACTION
{
    [HideInInspector]
    public class I010_CameraController : MonoBehaviour
    {
        private Vector3 _startPos, _lastPos;
        // Rahmen der Kamerabewegung
        private float minX, minY = 0;
        private float maxX, maxY = 1;

        //Verstecke das Script
       /* [ExecuteInEditMode]
        protected I010_CameraController()
        {
            gameObject.GetComponent<I010_CameraController>().hideFlags = 
                HideFlags.HideInInspector;
        }*/

        public void LateUpdate()
        {
            CheckBorders();
            if (transform.position != _lastPos)
            {
                _lastPos = transform.position;
                SendDeltaX();
            }
        }

        private void CheckBorders()
        {
            Vector3 p = transform.position;
            if (p.x > maxX) p = new Vector3(maxX, p.y, p.z);
            if (p.x < minX) p = new Vector3(minX, p.y, p.z);
            if (p.y > maxY) p = new Vector3(p.x, maxY, p.z);
            if (p.y < minY) p = new Vector3(p.x, minY, p.z);
            transform.position = p;
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
                for (int i = 0; i < param.Length; i++) if (!float.TryParse(param[i], out values[i])) values[i] = 0;
                _startPos = new Vector3(values[0], values[1], values[2]);
                transform.position = _startPos;
                _lastPos = _startPos;
                minX = values[3];
                maxX = values[4];
                minY = values[5];
                maxY = values[6];
                I501_ControllableObject i501 = gameObject.GetComponent<I501_ControllableObject>();
                if (i501 == null)
                    i501 = gameObject.AddComponent<I501_ControllableObject>();
                i501.CType = (I501_ControllableObject.ControllerType) I501_ControllableObject.ControllerType.Parse(typeof(I501_ControllableObject.ControllerType), param[7]);
                if (i501.GetComponent<I513_FollowControl>() != null && param[8] != "0")
                    i501.GetComponent<I513_FollowControl>().Target =
                        ObjectIDToVisual((int) values[8]);
            }
        }

        private V000_Visual ObjectIDToVisual(int _instanceID)
        {
            V000_Visual v = null;
            V000_Visual[] visuals = FindObjectsOfType(typeof(V000_Visual)) as V000_Visual[];
            foreach (V000_Visual _v in visuals)
                if (_v.GetInstanceID() == _instanceID) { return _v; }
            return v;
        }
    }
}