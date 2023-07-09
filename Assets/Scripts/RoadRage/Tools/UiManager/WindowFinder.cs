using System;
using System.Collections.Generic;
using Models.Controllers;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Tools.UiManager
{
    public class WindowFinder : IWindowFinder
    {
        private readonly List<Window> _stackWindows;
        private readonly Transform _stock;
        private readonly UiFabric _uiFabric;

        public WindowFinder(Transform stock, UiFabric uiFabric)
        {
            _uiFabric = uiFabric;
            _stock = stock;
            _stackWindows = new List<Window>();
        }

        public T FindWindow<T>() where T : Window
        {
            T window = (T)_stackWindows.Find(m => m.GetType() == typeof(T));
            if (window != null)
                return window;

            return null;
        }

        public T GetWindow<T>() where T : Window
        {
            Type windowType = typeof(T);

            T window = (T)_stackWindows.Find(m => m.GetType() == windowType);
            if (window != null)
                return window;

            window = LoadWindow<T>(_stock);

            _stackWindows.Add(window);
            return window;
        }

        public T LoadWindow<T>(Transform parent) where T : Window
        {
            GameObject windowGo = null;

            string windowName = typeof(T).Name;

            GameObject go = Resources.Load<GameObject>("Windows/" + windowName);
            var lastActiveState = go.activeSelf;
            var lastHideFlags = go.hideFlags;

            go.hideFlags = HideFlags.DontSave;
            go.SetActive(false);

            try
            {
#if UNITY_EDITOR
                windowGo = (GameObject)UnityEditor.PrefabUtility.InstantiatePrefab(go, parent);
#else
                windowGo = Object.Instantiate(go, parent, false);
                windowGo.name = go.name;
#endif
            }
            finally
            {
                go.SetActive(lastActiveState);
                MakeHierarchyHidden(go, lastHideFlags);
            }

            _uiFabric.InjectGameObject(windowGo);
            return windowGo.GetComponent<T>();
        }

        private void MakeHierarchyHidden(GameObject go, HideFlags lastHideFlags)
        {
            go.hideFlags = lastHideFlags;
            foreach (Transform child in go.transform)
            {
                MakeHierarchyHidden(child.gameObject, lastHideFlags);
            }
        }

        public void UnloadWindow<T>(T window) where T : Window
        {
            _stackWindows.Remove(window);
            Object.Destroy(window.gameObject);
        }
    }
}