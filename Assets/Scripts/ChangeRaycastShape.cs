using JMRSDK.InputModule;
using JMRSDK.UX;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JMRSDKExampleSnippets
{
    /// <summary>
    /// Control and change the raycast shape - length and width
    /// </summary>
    public class ChangeRaycastShape : MonoBehaviour, ITouchHandler
    {
        /// <summary>
        /// Text showing current length of pointer
        /// </summary>
        public TextMeshProUGUI pointerLengthText;
        /// <summary>
        /// Text showing current width of pointer
        /// </summary>
        public TextMeshProUGUI pointerWidthText;
        /// <summary>
        /// Slider to manipulate current length of pointer
        /// </summary>
        public Slider lengthSlider;
        /// <summary>
        /// Slider to manipulate current width of pointer
        /// </summary>
        public Slider widthSlider;

        /// <summary>
        /// Caching PointerLaserUnity
        /// </summary>
        PointerLaserUnity _pointerLaserUnity;
        /// <summary>
        /// Caching JMRPointerManager
        /// </summary>
        JMRPointerManager _jmrPointerManager;

        /// <summary>
        /// Add a global listener
        /// </summary>
        public void Start()
        {
            JMRInputManager.Instance.AddGlobalListener(gameObject);
        }

        /// <summary>
        /// Checks if PointerLaserUnity or JMRPointerManager is null or not.
        /// Try to find and cache the objects if null.
        /// </summary>
        public void NullCheck()
        {
            if (_pointerLaserUnity == null)
            {
                _pointerLaserUnity = FindObjectOfType<PointerLaserUnity>();
                if (_pointerLaserUnity == null)
                {
                    Debug.LogError("Pointer Laser Missing!");
                    return;
                }
            }

            if (_jmrPointerManager == null)
            {
                _jmrPointerManager = FindObjectOfType<JMRPointerManager>();
                if (_jmrPointerManager == null)
                {
                    Debug.LogError("Pointer Laser Missing!");
                    return;
                }
            }
        }

        /// <summary>
        /// Change length of raycast.
        /// </summary>
        /// <param name="value">Value of raycast length to change to.</param>
        public void ChangeLength(float value)
        {
            if (!Application.isPlaying) return;

            NullCheck();
            _jmrPointerManager.MaxPointerCollisionDistance = value;
            pointerLengthText.text = value.ToString("f3");
        }

        /// <summary>
        /// Change width of raycast.
        /// </summary>
        /// <param name="value">Value of raycast width to change to.</param>
        public void ChangeWidth(float value)
        {
            if (!Application.isPlaying) return;

            NullCheck();
            _pointerLaserUnity.WidthMultiplier = value;
            pointerWidthText.text = value.ToString("f3");
        }

        /// <summary>
        /// Update sliders UI to reflect the current values.
        /// </summary>
        public void updateSliders()
        {
            widthSlider.value = _pointerLaserUnity.WidthMultiplier;
            lengthSlider.value = _jmrPointerManager.MaxPointerCollisionDistance;
        }

        /// <summary>
        /// Function called when touch started.
        /// </summary>
        /// <remarks>
        /// Not implemented as not required.
        /// </remarks>
        /// <param name="eventData">Touch related data</param>
        /// <param name="TouchData">Position of touch</param>
        public void OnTouchStart(TouchEventData eventData, Vector2 TouchData)
        { }

        /// <summary>
        /// Function called when touch stops
        /// </summary>
        /// <remarks>
        /// Not implemented as not required.
        /// </remarks>
        /// <param name="eventData">Touch related data</param>
        /// <param name="TouchData">Position of touch</param>
        public void OnTouchStop(TouchEventData eventData, Vector2 TouchData)
        { }

        /// <summary>
        /// Function called when touch updates.
        /// 
        /// Update the shape of raycast based on touch position update.
        /// 
        /// x axis is mapped to change width, y axis is mapped to change length.
        /// </summary>
        /// <param name="eventData">Touch related data</param>
        /// <param name="TouchData">Position of touch</param>
        public void OnTouchUpdated(TouchEventData eventData, Vector2 TouchData)
        {
            NullCheck();
            if (TouchData.x > 0.8f || TouchData.x < 0.2f)
            {
                float newWidth = Mathf.Clamp(_pointerLaserUnity.WidthMultiplier + (TouchData.x - 0.5f) * Time.deltaTime,
                    widthSlider.minValue, widthSlider.maxValue);
                ChangeWidth(newWidth);
            }
            if (TouchData.y > 0.8f || TouchData.y < 0.2f)
            {
                float newLength = Mathf.Clamp(_jmrPointerManager.MaxPointerCollisionDistance - (TouchData.y - 0.5f) * Time.deltaTime, lengthSlider.minValue, lengthSlider.maxValue);
                ChangeLength(newLength);
            }
            updateSliders();
        }
    }
}