using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewManager : Singleton<ViewManager>
{
    [SerializeField] private View startView;
    [SerializeField] private View[] views;
    private View currentView;
    public string registType;

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

    public static void Show(View view)
    {
        if (Instance.currentView != null)
        {
            Instance.currentView.Hide();
        }
        view.Show();
        Instance.currentView = view;
    }

    private void Start()
    {
        for (int i = 0; i < views.Length; i++)
        {
            views[i].Initialized();
            views[i].Hide();
        }
        if (startView != null)
        {
            Show(startView);
        }
    }


}
