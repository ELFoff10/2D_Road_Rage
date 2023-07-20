using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Tools.UiManager
{
    public class WindowStack
    {
        public List<Window> VisibleDestroyableWindows => _visibleWindows.Where(w => !w.IsUndestroyable).ToList();
        public List<Window> VisibleWindows => _visibleWindows;

        private ReactiveProperty<Window> _lastWindow = new ReactiveProperty<Window>();

        public readonly ReadOnlyReactiveProperty<Window> LastWindow;

        private readonly List<Window> _visibleWindows = new List<Window>();
        private readonly Transform _canvas;
        private readonly Transform _invisibleWindowsRoot;
        private readonly Transform _stash;

        public WindowStack(Transform canvas, Transform stash)
        {
            _canvas = canvas;
            _invisibleWindowsRoot = _CreateInvisibleWindowsRoot(_canvas);
            _stash = stash;
            _lastWindow.Value = null;

            LastWindow = new ReadOnlyReactiveProperty<Window>(_lastWindow);
        }

        public void First(Window first)
        {
            Clear();
            Add(first);
        }

        public void Clear()
        {
            for (int i = 0; i < _visibleWindows.Count;)
            {
                if (!_visibleWindows[i].IsUndestroyable)
                    Remove(_visibleWindows[i]);
                else
                    ++i;
            }

            _lastWindow.Value = _visibleWindows.LastOrDefault(w =>
                w.Priority != WindowPriority.TopPanel && w.Priority != WindowPriority.Debug);

            var parent = _canvas;
            for (int i = _visibleWindows.Count - 1; i >= 0; i--)
            {
                var w = _visibleWindows[i];
                w.Parent = parent;
                if (!w.IsTransparent)
                    parent = _invisibleWindowsRoot;
                w.SetAsFirstSibling();
            }
        }

        public void Add(Window window)
        {
            string windowName = window.Name;

            Window first = _visibleWindows.FirstOrDefault(w => w.Priority > window.Priority);
            if (first != null)
            {
                _visibleWindows.Insert(_visibleWindows.IndexOf(first), window);
            }
            else
            {
                _visibleWindows.Add(window);
            }

            _lastWindow.Value = _visibleWindows.LastOrDefault(w =>
                w.Priority != WindowPriority.TopPanel && w.Priority != WindowPriority.Debug);

            var parent = _canvas;
            for (int i = _visibleWindows.Count - 1; i >= 0; i--)
            {
                var w = _visibleWindows[i];
                w.Parent = parent;
                if (!w.IsTransparent)
                    parent = _invisibleWindowsRoot;
                w.SetAsFirstSibling();
            }
        }

        public void Remove(Window window)
        {
            if (window != null)
            {
                string windowName = window.Name;
                window.Parent = _stash;
            }

            if (!ReferenceEquals(window, null))
                _visibleWindows.Remove(window);

            _lastWindow.Value = _visibleWindows.LastOrDefault(w =>
                w.Priority != WindowPriority.TopPanel && w.Priority != WindowPriority.Debug);
        }

        private static Transform _CreateInvisibleWindowsRoot(Transform parent)
        {
            var go = new GameObject("InvisibleWindowsRoot");
            var canvas = go.AddComponent<Canvas>();
            canvas.enabled = false;
            go.transform.SetParent(parent, false);
            return go.transform;
        }
    }
}