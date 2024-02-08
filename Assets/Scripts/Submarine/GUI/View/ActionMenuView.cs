using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Submarine
{
    public class ActionMenuView : MonoBehaviour
    {
        [SerializeField] private ActionMenuPresenter _presenter;

        [SerializeField] private GameObject _menu;
        [Header("Repair")]
        [SerializeField] private Button _repairButton;
        [SerializeField] private Image _repairProgressBar;

        [Header("Reload")]
        [SerializeField] private Button _reloadButton;
        [SerializeField] private Image _reloadProgressBar;

        [Header("Cancel")]
        [SerializeField] private GameObject _cancelButton;

        public void Open()
        {
            _menu.SetActive(true);
        }

        public void Close()
        {
            _menu.SetActive(false);
        }

        public void EnableProgressButtons()
        {
            EnableReloadButton();
            EnableRepairButton();
        }

        public void DisableProgressButtons()
        {
            DisableReloadButton();
            DisableRepairButton();
        }

        public void ShowCancelButton()
        {
            _cancelButton.SetActive(true);
        }

        public void HideCancelButton()
        {
            _cancelButton.SetActive(false);
        }

        public void CancelProcess()
        {
            _presenter.CancelProcess();
        }

        #region REPAIR

        public void StartRepairing()
        {
            _presenter.StartRepairing();
        }        

        public void EnableRepairButton()
        {
            _repairButton.interactable = true;
        }

        public void DisableRepairButton()
        {
            _repairButton.interactable = false;
        }

        public void ShowRepairProgressBar()
        {
            _repairProgressBar.gameObject.SetActive(true);
        }

        public void HideRepairProgressBar()
        {
            _repairProgressBar.gameObject.SetActive(false);
        }

        public void UpdateRepairProgress(float percentage)
        {
            _repairProgressBar.fillAmount = percentage;
        }

        #endregion

        #region RELOAD

        public void StartReloading()
        {
            _presenter.StartReloading();            
        }        

        public void ShowReloadProgressBar()
        {
            _reloadProgressBar.gameObject.SetActive(true);
        }

        public void HideReloadProgressBar()
        {
            _reloadProgressBar.gameObject.SetActive(false);
        }

        public void UpdateReloadProgress(float percentage)
        {
            _reloadProgressBar.fillAmount = percentage;
        }

        public void EnableReloadButton()
        {
            _reloadButton.interactable = true;
        }

        public void DisableReloadButton()
        {
            _reloadButton.interactable = false;
        }

        #endregion
    }
}