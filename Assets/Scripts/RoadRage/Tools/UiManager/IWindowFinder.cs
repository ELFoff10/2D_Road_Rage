using UnityEngine;

namespace Tools.UiManager
{
    public interface IWindowFinder
    {
        T FindWindow<T>() where T : Window;
        T GetWindow<T>() where T : Window;
        T LoadWindow<T>(Transform parent) where T : Window;
        void UnloadWindow<T>(T window) where T : Window;
    }
}