using UnityEngine;

namespace Environment
{
    public class SpawnArea : MonoBehaviour
    {
        [SerializeField] private float width;

        public Vector3 GetSpawnPosition()
        {
            return new Vector3(Random.Range(-width, width), transform.position.y, transform.position.z);
        }
    }
}
