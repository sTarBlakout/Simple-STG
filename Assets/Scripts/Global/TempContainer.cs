using UnityEngine;

/// <summary>
/// Temporary container for all fast paced objects.
/// </summary>
public class TempContainer : MonoBehaviour
{
    private static TempContainer _instance;
    public static TempContainer Instance => _instance;

    /// <summary>
    /// Unity event inherited from Unity MonoBehavior class.
    /// Is called first once when Monobehavior object is created.
    /// Initializes Singleton of this class
    /// </summary>
    private void Awake()
    {
        _instance = this;
    }

    /// <summary>
    /// Moves passed GameObject as child of the container.
    /// </summary>
    public void MoveToContainer(GameObject smth)
    {
        smth.transform.parent = transform;
    }
}
