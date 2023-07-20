using UniRx;
using UnityEngine;

namespace Tools.UiManager
{
    public interface IWindowManager
    {
        T FindWindow<T>() where T : Window;
        T GetWindow<T>() where T : Window;
        void Show(Window window);
        void Show(Window window, WindowPriority priority);
        void Hide(Window window);

        void First(Window window);
        void First(Window window, WindowPriority priority);

        T Show<T>(WindowPriority? priority = null) where T : Window;
        T Hide<T>() where T : Window;

        T First<T>(WindowPriority? priority = null) where T : Window;

        ReadOnlyReactiveProperty<Window> LastWindow { get; }
        Canvas MenuCanvas { get; }

        void ClearStack();
    }
}