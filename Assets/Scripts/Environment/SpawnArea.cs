using UnityEngine;

namespace Environment
{
    /// <summary>
    /// Class which represents spawn area.
    /// </summary>
    public class SpawnArea : MonoBehaviour
    {
        [SerializeField] private float width;

        /// <summary>
        /// Gets random spawn position inside spawn area.
        /// </summary>
        /// <returns>Random spawn position inside spawn area.</returns>
        public Vector3 GetSpawnPosition()
        {
            return new Vector3(Random.Range(-width, width), transform.position.y, transform.position.z);
        }
    }
}
