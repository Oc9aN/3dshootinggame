using UnityEngine;

public class CameraView : MonoBehaviour
{
    [SerializeField]
    private Transform _firstViewTarget;

    [SerializeField]
    private Transform _player;

    private Transform _target;

    private ICameraViewStrategy _cameraViewStrategy;

    private CameraFirstView _cameraFirstView;
    private CameraThirdView _cameraThirdView;
    private CameraQuarterView _cameraQuarterView;

    private void Start()
    {
        _cameraFirstView = new CameraFirstView(transform, _firstViewTarget, _player);
        _cameraThirdView = new CameraThirdView(transform, _player);
        _cameraQuarterView = new CameraQuarterView(transform, _player);

        ViewManager.Instance.OnViewChanged += OnViewChanged;
        ViewManager.Instance.ViewType = EViewType.FirstPerson;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            ViewManager.Instance.ViewType = EViewType.FirstPerson;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            ViewManager.Instance.ViewType = EViewType.ThirdPerson;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            ViewManager.Instance.ViewType = EViewType.QuarterView;
        }

        _cameraViewStrategy.View();
    }

    private void OnViewChanged(EViewType viewType)
    {
        if (viewType == EViewType.FirstPerson)
        {
            _cameraViewStrategy = _cameraFirstView;
            _cameraViewStrategy.Initialize();
        }
        else if (viewType == EViewType.ThirdPerson)
        {
            _cameraViewStrategy = _cameraThirdView;
            _cameraViewStrategy.Initialize();
        }
        else if (viewType == EViewType.QuarterView)
        {
            _cameraViewStrategy = _cameraQuarterView;
            _cameraViewStrategy.Initialize();
        }
    }
}