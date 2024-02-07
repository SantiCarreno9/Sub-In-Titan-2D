using UnityEngine;
using UnityEngine.Events;

namespace Submarine
{
    public abstract class SubmarineProcess : MonoBehaviour
    {
        [SerializeField] protected float _defaultProcessDuration = 5f;
        protected float _processTime = 0;

        private float _processDuration;
        public bool IsPerformingProcess { get; protected set; } = false;

        public UnityAction OnProcessStarted;
        public UnityAction OnProcessCanceled;
        public UnityAction OnProcessFinished;

        private void Start()
        {
            ResetProcessDuration();
        }

        protected void SetProcessDuration(float newDuration)
        {
            _processDuration = newDuration;
        }
        protected void ResetProcessDuration() => SetProcessDuration(_defaultProcessDuration);

        public virtual void StartProcess()
        {
            if (IsPerformingProcess)
                return;

            OnProcessStarted?.Invoke();
            IsPerformingProcess = true;
        }

        public virtual void CancelProcess()
        {
            if (!IsPerformingProcess)
                return;

            OnProcessCanceled?.Invoke();
            IsPerformingProcess = false;
        }

        protected virtual void FinishProcess()
        {
            Debug.Log("Process Finished");
            IsPerformingProcess = false;
            OnProcessFinished?.Invoke();
            _processTime = 0;
        }


        public float GetProgress()
        {
            return _processTime / _processDuration;
        }

        private void Update()
        {
            if (IsPerformingProcess)
            {
                _processTime += Time.deltaTime;
                Debug.Log(_processTime);
                if (_processTime >= _processDuration)
                    FinishProcess();
            }
        }
    }
}