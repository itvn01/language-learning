using UnityEngine;
using System.Collections;

namespace M1PetGame
{
    public class CameraShake : MonoBehaviour
    {
        // Amplitude of the shake. A larger value shakes the camera harder.
        public float shakeAmount = 0.7f;

        float duration;
        float t;
        Vector3 originalPos;

        float _lastVibration;
        const float _vibrateFreq = 0.7f;

        void Awake()
        {
            originalPos = transform.localPosition;
            t = -1;
        }

        [ContextMenu("Test Shake")]
        void TestShake()
        {
            Shake(2);
        }

        public void Shake(float duration)
        {
            this.duration = duration;
            this.t = 0;
        }

        void Update()
        {
            if (t >= 0 && t <= duration)
            {
                if (Time.time - _lastVibration > _vibrateFreq)
                {
                    // Vibration.VibratePeek();
                    _lastVibration = Time.time;
                }
                transform.localPosition = originalPos + Random.insideUnitSphere * (shakeAmount * (1 - (t / duration)));

                t += Time.deltaTime;
            }
            else
            {
                t = -1;
                transform.localPosition = originalPos;
            }
        }
    }
}