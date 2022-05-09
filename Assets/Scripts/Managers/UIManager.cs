using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Canvas playerInfoCanvas;
    public Canvas settingsCanvas;
    private Button newGameBtn;
    private Button continueBtn;
    private Button settingsBtn;
    private Button aboutBtn;
    private Button quitBtn;

    private Animator newGameAnim;
    private Animator continueAnim;
    private Animator settingsAnim;
    private Animator settingsPanelAnim;
    private Animator aboutAnim;
    private Animator aboutPanelAnim;

    public Camera mainCamera;
    public GameObject cm;
    private void Awake()
    {
        playerInfoCanvas.enabled = false;

        newGameBtn = settingsCanvas.transform.GetChild(1).GetComponent<Button>();
        continueBtn = settingsCanvas.transform.GetChild(2).GetComponent<Button>();
        settingsBtn = settingsCanvas.transform.GetChild(3).GetComponent<Button>();
        aboutBtn = settingsCanvas.transform.GetChild(4).GetComponent<Button>();
        quitBtn = settingsCanvas.transform.GetChild(5).GetComponent<Button>();

        newGameAnim = settingsCanvas.transform.GetChild(1).GetComponent<Animator>();
        continueAnim = settingsCanvas.transform.GetChild(2).GetComponent<Animator>();
        settingsAnim = settingsCanvas.transform.GetChild(3).GetComponent<Animator>();
        aboutAnim = settingsCanvas.transform.GetChild(4).GetComponent<Animator>();
        settingsPanelAnim = settingsCanvas.transform.GetChild(5).GetComponent<Animator>();
        aboutPanelAnim = settingsCanvas.transform.GetChild(6).GetComponent<Animator>();

        newGameBtn.onClick.AddListener(NewGame);
        continueBtn.onClick.AddListener(Continue);
        settingsBtn.onClick.AddListener(Settings);
        aboutBtn.onClick.AddListener(About);
        quitBtn.onClick.AddListener(QuitGame);
    }

    void NewGame()
    {

    }

    void Continue()
    {

    }

    void Settings()
    {

    }

    void About()
    {

    }

    void QuitGame()
    {
        Application.Quit();
    }
}
