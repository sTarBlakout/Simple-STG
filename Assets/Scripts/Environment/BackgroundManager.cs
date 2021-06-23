using UnityEngine;

namespace Environment
{
    /// <summary>
    /// Class processes background moving.
    /// </summary>
    public class BackgroundManager : MonoBehaviour
    {
        private static readonly int MainTex = Shader.PropertyToID("_MainTex");
        
        [SerializeField] private float scrollSpeed = 0.5f;
        private Renderer _renderer;

        /// <summary>
        /// Unity event inherited from Unity MonoBehavior class.
        /// Is called first once when Monobehavior object is created.
        /// Gets renderer component.
        /// </summary>
        private void Awake()
        {
            _renderer = GetComponent<Renderer> ();
        }

        /// <summary>
        /// Unity event inherited from Unity MonoBehavior class
        /// Is called every frame after "Start".
        /// Scrolling background image through renderer component.
        /// </summary>
        private void Update()
        {
            var offset = Time.time * scrollSpeed;
            _renderer.material.SetTextureOffset(MainTex, new Vector2(0, offset));
        }
    }
}
