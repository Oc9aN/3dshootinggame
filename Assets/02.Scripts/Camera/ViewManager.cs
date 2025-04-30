using System;

public class ViewManager : Singleton<ViewManager>
{
    // 현재 뷰에대한 정보를 제공
    public event Action<EViewType> OnViewChanged;
    
    private EViewType _viewType;

    public EViewType ViewType
    {
        get
        {
            return _viewType;
        }
        set
        {
            _viewType = value;
            OnViewChanged?.Invoke(_viewType);
        }
    }
}