using UnityEngine;

namespace UIScroll3DPlatonicSolids
{
    [HelpURL("https://assetstore.unity.com/packages/slug/225664")]
    public class Bubble : MonoBehaviour
    {
        [SerializeField]
        private float minSpeed, maxSpeed;

        private float speed;

        /// <summary>
        /// Bubble bursting
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter2D(Collision2D collision)
        {
            Animation anim = GetComponent<Animation>();
            anim.Play();
            Destroy(gameObject, anim.clip.length);
        }

        /// <summary>
        /// Spawn bubble
        /// </summary>
        private void Start()
        {
            speed = Random.Range(minSpeed, maxSpeed);
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), -6f, 15f));
        }

        /// <summary>
        /// Bubble Flight
        /// </summary>
        private void Update()
        {
            transform.position += Vector3.up * (speed * Time.deltaTime);
            if (transform.position.y > 6f)
                Destroy(gameObject);
        }
    }
}