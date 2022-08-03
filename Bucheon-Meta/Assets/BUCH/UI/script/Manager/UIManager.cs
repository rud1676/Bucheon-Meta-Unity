using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    //View Object들은 켜넣고 있어야지 초기화가 제대로 이루어집니다.

    [SerializeField] private View[] views;

    [SerializeField] private View startView;
    public View currentView;
	public MapArea _staticMapArea;
	public ShowLocation _staticShowLocation;

    public static T GetView<T>() where T : View
    {
        for (int i = 0; i < Instance.views.Length; i++)
        {
            if (Instance.views[i] is T tView)
            {
                return tView;
            }
        }
        return null;
    }
    public static void Show(View view)
    {
        if (Instance.currentView != null)
        {
            Instance.currentView.Hide();
        }
        view.Show();
        Instance.currentView = view;
    }

    public static void Show<T>() where T : View
    {
        for (int i = 0; i < Instance.views.Length; i++)
        {
            if (Instance.views[i] is T)
            {
                if (Instance.currentView != null)
                {
                    Instance.currentView.Hide();
                }

                Instance.views[i].Show();
                Instance.currentView = Instance.views[i];
            }
        }
    }
    private void Start()
    {
        for (int i = 0; i < views.Length; i++)
        {
            views[i].Initialized();
            views[i].Show();
            views[i].Hide();
        }
        if (startView != null)
        {
            Show(startView);
        }
    }

}
