using UnityEngine;
using System.Collections;

namespace UIScroll3DPlatonicSolids
{
    [HelpURL("https://assetstore.unity.com/packages/slug/225664")]
    public class SpawnBubbles : MonoBehaviour
    {
        [SerializeField]
        private GameObject prefab;

        [SerializeField]
        private float spawnDelay;

        private const float SIZE = 0.25f;

        /// <summary>
        /// Spawn bubbles
        /// </summary>
        private IEnumerator Start()
        {
            while (true)
            {
                Instantiate(prefab).transform.localScale = new Vector3(SIZE, SIZE, 1f);
                yield return new WaitForSeconds(spawnDelay);
            }
        }
    }
}