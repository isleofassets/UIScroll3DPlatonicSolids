using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace UIScroll3DPlatonicSolids
{
    [HelpURL("https://assetstore.unity.com/packages/slug/225664")]
    public class Scroll : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [SerializeField]
        private Transform content;

        [SerializeField]
        private Renderer[] models;

        [SerializeField]
        private SpriteRenderer[] shapes;

        [SerializeField]
        private Image[] colors;

        [SerializeField]
        private float speed, minSizeX, maxSizeX, distance;

        private float nearestValue, lastClickPosX, lastContentPosX, sensitivity;
        private bool isClick;

        /// <summary>
        /// Clicking on the scroll
        /// </summary>
        /// <param name="data"></param>
        public void OnPointerDown(PointerEventData data)
        {
            isClick = true;
            lastClickPosX = data.position.x;
            lastContentPosX = content.position.x;
        }

        /// <summary>
        /// Dragging the scroll
        /// </summary>
        /// <param name="data"></param>
        public void OnDrag(PointerEventData data)
        {
            content.position = Vector3.right * Mathf.Clamp(lastContentPosX + (data.position.x - lastClickPosX) * sensitivity, distance * 4 + distance * 0.5f, distance * -0.5f);
            for (int i = 0; i < shapes.Length; i++)
                if (content.position.x <= distance * (-0.5f + i) && content.position.x > distance * (0.5f + i))
                {
                    if (nearestValue != distance * i)
                    {
                        nearestValue = distance * i;
                        SelectDice();
                    }
                    else
                        nearestValue = distance * i;
                }
        }

        /// <summary>
        /// Releasing the scroll
        /// </summary>
        /// <param name="data"></param>
        public void OnPointerUp(PointerEventData data)
        {
            isClick = false;
        }

        /// <summary>
        /// Setting the sprite color that the user clicked on
        /// </summary>
        /// <param name="index"></param>
        public void SetColor(int index)
        {
            for (int i = 0; i < shapes.Length; i++)
                shapes[i].color = new Color(colors[index].color.r, colors[index].color.g, colors[index].color.b, nearestValue == i * distance ? 0.9f : 0.6f);
            for (int i = 0; i < colors.Length; i++)
                colors[i].transform.GetChild(0).gameObject.SetActive(i == index);
            PlayerPrefs.SetInt("Color", index);
        }

        /// <summary>
        /// It is called by pressing the button and sets the color of 3d models
        /// </summary>
        public void SelectColor()
        {
            for (int i = 0; i < models.Length; i++)
                models[i].material.color = shapes[i].color;
        }

        /// <summary>
        /// Select active dice
        /// </summary>
        private void SelectDice()
        {
            for (int i = 0; i < shapes.Length; i++)
            {
                shapes[i].color = new Color(shapes[i].color.r, shapes[i].color.g, shapes[i].color.b, nearestValue == i * distance ? 0.9f : 0.6f);
                shapes[i].transform.localScale = nearestValue == i * distance ? new Vector3(maxSizeX, maxSizeX, 1) : new Vector3(minSizeX, minSizeX, 1);
            }
        }

        /// <summary>
        /// Assigning default parameters
        /// </summary>
        private void Start()
        {
            sensitivity = 20f / Screen.height;
            SelectDice();
            SetColor(PlayerPrefs.GetInt("Color"));
            SelectColor();
        }

        /// <summary>
        /// The movement of the scroll to the nearest object when releasing the finger
        /// </summary>
        private void Update()
        {
            if (isClick)
                return;
            content.position = Vector3.Lerp(content.position, Vector3.right * nearestValue, speed * Time.deltaTime);
        }
    }
}