using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float _timeLimit;
    private float _timePassed;
    private int  _portalsTotal, _portalsLeft;
    private bool _isPaused, _powerUpMenuActive;
    private Player _player;
    private UIManager _uiManager;
    private PowerUpManager _powerUpManager;
    public static GameManager instance;

    public bool IsPaused { get => _isPaused; set => _isPaused = value; }
    public Player Player { get => _player; }
    public int PortalsLeft { get => _portalsLeft; set => _portalsLeft = value; }
    public bool PowerUpMenuActive { get => _powerUpMenuActive; set => _powerUpMenuActive = value; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
        }
        Time.timeScale = 1f;    
    }
    private void Start()
    {
        _uiManager = UIManager.instance;
        _powerUpManager = PowerUpManager.instance;
        _timePassed = _timeLimit;
        StartCoroutine(CheckPortals()); 
    }
    private void Update()
    {
        _timePassed -= Time.deltaTime;
        _uiManager.UpdateTimer(((int)_timePassed));
        if (_timePassed <= 0)
        {
            _uiManager.ShowGameOverMenu();
            Time.timeScale = 0;
        }
        if (PowerUpMenuActive)
        {
            Time.timeScale = 0f;
        }
    }
    IEnumerator CheckPortals()
    {
        yield return new WaitForSeconds(0.1f);
        _portalsTotal = PortalsLeft;
        _uiManager.UpdatePortals(PortalsLeft, _portalsTotal);
    }
    public void PortalDestoyed()
    {
        PortalsLeft --;
        _uiManager.UpdatePortals(PortalsLeft, _portalsTotal);
        if( PortalsLeft <= 0)
        {
            _uiManager.ShowWinMenu();
        }
        else
        {
            PowerUpMenu();
        }
    }
    public void PowerUpMenu()
    {
        Time.timeScale = 0f;
        IsPaused = true;
        PowerUpMenuActive = true;
        _powerUpManager.SpawnCards();
        _uiManager.ShowPowerupMenu();
    }
    public void DisablePowerUpMenu()
    {
        Time.timeScale = 1f;
        IsPaused = false;
        PowerUpMenuActive = false;
        _powerUpManager.DestroyCards();
        _uiManager.HidePowerupMenu();
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        IsPaused = true;
        _uiManager.ShowPauseMenu();
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        IsPaused = false;
        _uiManager.HidePauseMenu();
    }
    public void WinGame()
    {
        Time.timeScale = 0;
        _player.GetComponent<Player>().enabled = false;
        _uiManager.ShowWinMenu();
    }
    public void DeathScreen()
    {
        Time.timeScale = 0;
        _player.GetComponent<Player>().enabled = false;
        _uiManager.ShowGameOverMenu();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
