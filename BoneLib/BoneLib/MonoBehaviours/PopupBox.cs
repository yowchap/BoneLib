﻿using System;
using Il2CppTMPro;
using UnityEngine;
using MelonLoader;

namespace BoneLib.MonoBehaviours
{
    [RegisterTypeInIl2Cpp]
    internal class PopupBox : MonoBehaviour
    {
        public PopupBox(IntPtr intPtr) : base(intPtr)
        {
        }

        private TextMeshPro Il2CppTMPro;

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
            Il2CppTMPro = gameObject.GetComponentInChildren<TextMeshPro>();
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

            Il2CppTMPro.color = Color.Lerp(colors[curColorIndex], colors[nextColorIndex], Mathf.InverseLerp(timeForNextColor - timeToLerp, timeForNextColor, Time.time)); // Random colors go brrrr
        }
    }
}