using System;
using UnityEngine;
using UnityEngine.UI;

namespace UnityStandardAssets.CrossPlatformInput
{
    public class ButtonHandler : MonoBehaviour
    {
        private Image image;

        public string Name;
        public Color normalColor;
        public Color pressedColor;


        

        void OnEnable()
        {
            image = GetComponent<Image>();
        }

        public void ChangeButtonColor(Color color)
        {
            image.color = color;
        }

        public void SetDownState()
        {
            CrossPlatformInputManager.SetButtonDown(Name);
            ChangeButtonColor(pressedColor);
        }


        public void SetUpState()
        {
            CrossPlatformInputManager.SetButtonUp(Name);
            ChangeButtonColor(normalColor);
        }


        public void SetAxisPositiveState()
        {
            CrossPlatformInputManager.SetAxisPositive(Name);
        }


        public void SetAxisNeutralState()
        {
            CrossPlatformInputManager.SetAxisZero(Name);
        }


        public void SetAxisNegativeState()
        {
            CrossPlatformInputManager.SetAxisNegative(Name);
        }

        public void Update()
        {

        }
    }
}
