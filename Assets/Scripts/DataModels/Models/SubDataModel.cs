using DataModels.Data;
using UniRx;

namespace DataModels.Models
{
    public interface ISubDataModel
    {
        IReadOnlyReactiveProperty<bool> FirstLaunchTest { get; }
        void SetFirstLaunchTest(bool value);
    }

    public class SubDataModel : ISubDataModel
    {
        #region Fields

        private ReactiveProperty<bool> _firstLaunchTest = new ReactiveProperty<bool>(false);

        public IReadOnlyReactiveProperty<bool> FirstLaunchTest => _firstLaunchTest;

        #endregion

        public void SetFirstLaunchTest(bool value)
        {
            _firstLaunchTest.Value = value;
        }

        #region Storage

        public SubData GetSubData() => new()
        {
            FirstLaunchTest = _firstLaunchTest.Value
        };

        public void SetSubData(SubData subData)
        {
            _firstLaunchTest.Value = subData.FirstLaunchTest;
        }

        public void SetAndInitEmptySubData(SubData subData)
        {
            SetSubData(subData);
        }

        #endregion
    }
}