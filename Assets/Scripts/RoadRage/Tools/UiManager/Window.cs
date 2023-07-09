using System;
using RoadRage.Tools.UiManager;
using UniRx;
using UnityEngine;

namespace Tools.UiManager
{
    public abstract class Window : UIBehaviour
    {
#pragma warning disable 0649
        [SerializeField]
        private bool _isUndestroyable;

        [SerializeField]
        private bool _isTransparent = true;
#pragma warning restore 0649

        [NonSerialized]
        protected WindowPriority _windowPriority = WindowPriority.Default;

        public virtual WindowPriority Priority
        {
            get => _windowPriority;
            set => _windowPriority = value;
        }

        public bool IsShowing => this.gameObject.activeSelf;

        [NonSerialized]
        public ReactiveProperty<bool> IsShowingReactive = new ReactiveProperty<bool>(false);

        public bool IsUndestroyable => _isUndestroyable;
        public bool IsTransparent => _isTransparent;
        public string Name => name;
        protected CompositeDisposable ActivateDisposables { get; private set; }

        public void Setup(IWindowManager manager)
        {
            _manager = manager;
        }

        public Transform Parent
        {
            set => this.transform.SetParent(value, false);
        }

        protected IWindowManager _manager;

        public void Hide()
        {
            this.gameObject.SetActive(false);
            OnDeactivate();
        }

        public void Show()
        {
            this.gameObject.SetActive(true);
            OnActivate();
        }

        public int GetSiblingIndex()
        {
            return this.transform.GetSiblingIndex();
        }

        public void SetSiblingIndex(int index)
        {
            this.transform.SetSiblingIndex(index);
        }

        public void SetAsLastSibling()
        {
            this.transform.SetAsLastSibling();
        }

        public void SetAsFirstSibling()
        {
            this.transform.SetAsFirstSibling();
        }

        protected sealed override void OnEnable()
        {
            IsShowingReactive.Value = true;
        }

        protected sealed override void OnDisable()
        {
            IsShowingReactive.Value = false;
        }

        protected virtual void OnActivate()
        {
            if (ActivateDisposables == null)
                ActivateDisposables = new CompositeDisposable().AddTo(this);
            else
                ActivateDisposables.Clear();
        }

        protected virtual void OnDeactivate()
        {
            ActivateDisposables?.Clear();
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();

            if (Application.isPlaying)
                return;
        }
#endif
    }
}