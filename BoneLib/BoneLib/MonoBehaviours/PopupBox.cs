using System;
using Il2CppTMPro;
using UnityEngine;

namespace BoneLib.MonoBehaviours
{
    internal class PopupBox : MonoBehaviour
    {
        public PopupBox(IntPtr intPtr) : base(intPtr) { }

        private TextMeshPro tmpro;

        private float timeToLerp = 5f;
        private float timeForNextColor = 0;
        private int curColorIndex = 0;
        private int nextColorIndex = 1;
        private Color[] colors = new Color[]
        {
            Color.red,
            Color.yellow,
            Color.green,
            Color.blue
        };


        private void Start()
        {
            tmpro = gameObject.GetComponentInChildren<TextMeshPro>();
            timeForNextColor = Time.time + timeToLerp;
        }

        private void Update()
        {
            if (Time.time >= timeForNextColor)
            {
                curColorIndex = nextColorIndex;
                if (++nextColorIndex == colors.Length)
                    nextColorIndex = 0;

                timeForNextColor = Time.time + timeToLerp;
            }

            tmpro.color = Color.Lerp(colors[curColorIndex], colors[nextColorIndex], Mathf.InverseLerp(timeForNextColor - timeToLerp, timeForNextColor, Time.time)); // Random colors go brrrr
        }
    }
}
