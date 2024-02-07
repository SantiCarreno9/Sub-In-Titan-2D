using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Submarine
{
    public class ActionMenuPresenter : MonoBehaviour
    {
        [SerializeField] private ActionMenuView _view;
        [SerializeField] private ActionMenuModule _model;

        private void OnEnable()
        {
            _model.OnOpen += Open;
            _model.OnClose += Close;
        }

        private void OnDisable()
        {
            _model.OnOpen -= Open;
            _model.OnClose -= Close;
        }

        public void Open()
        {
            _view.Open();
        }

        public void Close()
        {
            _view.Close();
        }

        public void StartRepairing()
        {
            if (_model.AnyProcessRunning())
                return;

            _model.StartRepairing();
            _view.ShowCancelButton();
        }

        public void CancelProcess()
        {
            if (_model.AnyProcessRunning())
            {
                if (_model.IsRepairing())
                {
                    _model.CancelRepairing();
                    _view.CancelRepairing();
                    return;
                }

                if (_model.IsReloading())
                {
                    _model.CancelReloading();
                    _view.CancelReloading();
                }
            }
        }

        public void StartReloading()
        {
            if (_model.AnyProcessRunning())
                return;

            _model.StartRepairing();
            _view.ShowCancelButton();
        }

        private void Update()
        {
            if (_model.AnyProcessRunning())
            {
                if (_model.IsRepairing())
                {
                    _view.UpdateRepairProgress(_model.GetRepairingProcess());
                    return;
                }

                if (_model.IsReloading())
                {
                    _view.UpdateReloadProgress(_model.GetReloadingProcess());
                }
            }
        }
    }
}