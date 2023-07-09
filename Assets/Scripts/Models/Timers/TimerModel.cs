using System;
using UniRx;

namespace Models.Timers
{
    public class TimerModel
    {
        private int _currentSec = 0;
        private TimerTypeEnum _timerTypeEnum = TimerTypeEnum.Default;
        private readonly TimerService _timerService;
        private event Action<int> OnTimeModelTick;
        private event Action OnTimeModelEnd;

        private bool _ignoreTimeScale = true;
        private CompositeDisposable _disposable = new CompositeDisposable();

        public TimerModel(TimerService timerService, int currentSec, TimerTypeEnum timerTypeEnum,
            Action<int> timeModelTick, Action timeModelEnd, bool ignoreTimeScale)
        {
            _timerService = timerService;
            Init(currentSec, timerTypeEnum, timeModelTick, timeModelEnd, ignoreTimeScale);
        }

        public void Init(int currentSec, TimerTypeEnum timerTypeEnum, Action<int> timeModelTick, Action timeModelEnd,
            bool ignoreTimeScale)
        {
            OnTimeModelTick = timeModelTick;
            _currentSec = currentSec;
            OnTimeModelEnd = timeModelEnd;
            _timerTypeEnum = timerTypeEnum;
            _ignoreTimeScale = ignoreTimeScale;
        }

        public void StartTick()
        {
            _disposable.Clear();
            if (_ignoreTimeScale)
            {
                Observable.Timer(System.TimeSpan.FromSeconds(1), Scheduler.MainThreadIgnoreTimeScale)
                    .Repeat()
                    .Subscribe(_ => { TimerSet(); }).AddTo(_disposable);
            }
            else
            {
                Observable.Timer(System.TimeSpan.FromSeconds(1))
                    .Repeat()
                    .Subscribe(_ => { TimerSet(); }).AddTo(_disposable);
            }
        }

        private void TimerSet()
        {
            _currentSec--;
            if (_currentSec < 0)
            {
                _currentSec = 0;
                OnTimeModelTick?.Invoke(_currentSec);
                OnTimeModelEnd?.Invoke();
                _timerService.RemoveTimer(_timerTypeEnum, this);
                _disposable.Clear();
            }
            else
            {
                OnTimeModelTick?.Invoke(_currentSec);
            }
        }

        public void StopTick()
        {
            _disposable.Clear();
        }

        public void RestartTick()
        {
            _disposable.Clear();
            _currentSec = 0;
            OnTimeModelTick?.Invoke(0);
            OnTimeModelEnd?.Invoke();
        }
    }
}