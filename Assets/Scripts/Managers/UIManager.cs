using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image _hpBar;
    [SerializeField] private Text _currencyText, _interactText, _timer, _portalText;
    [SerializeField] private GameObject _pauseMenu, _gameOverMenu, _winMenu, _playerUI, _powerUpsMenu, _npcMenu;
    public static UIManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        HidePauseMenu();
        HideWinMenu();
        HideGameoverMenu();
        HideInteractText();
        HidePowerupMenu();
        HideNPCMenu();
    }
    public void UpdateCurrency(int amount)
    {
        _currencyText.text = amount.ToString();
    }

    public void UpdateHealthBar(float hp, float maxHp)
    {
        _hpBar.fillAmount = hp / maxHp;
    }
    public void UpdateTimer(int time)
    {
        _timer.text = $"Time Left: {time}";
    }
    public void UpdatePortals(int portalsLeft, int totalPortals)
    {
        _portalText.text = "Portals left    "+ portalsLeft.ToString() + " / " + totalPortals.ToString();
    }
    public void ShowPowerupMenu()
    {
        _powerUpsMenu.SetActive(true);
    }
    public void HidePowerupMenu()
    {
        _powerUpsMenu.SetActive(false);
    }
    public void ShowNPCMenu()
    {
        _npcMenu.SetActive(true);
    }
    public void HideNPCMenu()
    {
        _npcMenu.SetActive(false);
    }
    public void ShowInteractText()
    {
        _interactText.gameObject.SetActive(true);
    }
    public void HideInteractText()
    {
        _interactText.gameObject.SetActive(false);
    }
    public void HideUI()
    {
        _playerUI.SetActive(false);
    }
    public void ShowUi()
    {
        _playerUI.SetActive(true);
    }
    public void ShowPauseMenu()
    {
        _pauseMenu.SetActive(true);
        HideUI();
    }
    public void HidePauseMenu()
    {
        _pauseMenu.SetActive(false);
        ShowUi();
    }
    public void ShowGameOverMenu()
    {
        _gameOverMenu.SetActive(true);
        HideUI();
    }
    public void HideGameoverMenu()
    {
        _gameOverMenu.SetActive(false);
        ShowUi();
    }
    public void ShowWinMenu()
    {
        _winMenu.SetActive(true);
        HideUI();
    }
    public void HideWinMenu()
    {
        _winMenu.SetActive(false);
        ShowUi();
    }
}

