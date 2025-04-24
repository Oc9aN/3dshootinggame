using UnityEngine;
using UnityEngine.UI;

public class MinimapView : MonoBehaviour
{
    [SerializeField]
    private Button _minimapZoomInButton;
    public Button MinimapZoomInButton => _minimapZoomInButton;
    
    [SerializeField]
    private Button _minimapZoomOutButton;
    public Button MinimapZoomOutButton => _minimapZoomOutButton;
}
