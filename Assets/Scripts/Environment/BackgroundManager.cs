using UnityEngine;

namespace Environment
{
    public class BackgroundManager : MonoBehaviour
    {
        private static readonly int MainTex = Shader.PropertyToID("_MainTex");
        
        [SerializeField] private float scrollSpeed = 0.5f;
        private Renderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<Renderer> ();
        }

        private void Update()
        {
            var offset = Time.time * scrollSpeed;
            _renderer.material.SetTextureOffset(MainTex, new Vector2(0, offset));
        }
    }
}
