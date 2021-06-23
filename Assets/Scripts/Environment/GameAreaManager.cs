using Global;
using UnityEngine;

namespace Environment
{
    /// <summary>
    /// Class which manages game area
    /// </summary>
    public class GameAreaManager : MonoBehaviour
    {
        /// <summary>
        /// Unity event for checking if any collider leaves this object.
        /// Destroys all objects which go out of the game area.
        /// </summary>
        /// <param name="other">Object with collider component which left current object.</param>
        private void OnTriggerExit(Collider other)
        {
            var spaceObject = GameController.TryGetParentSpaceObject(other.transform);
            Destroy(spaceObject != null ? spaceObject.gameObject : other.gameObject);
        }
    }
}
