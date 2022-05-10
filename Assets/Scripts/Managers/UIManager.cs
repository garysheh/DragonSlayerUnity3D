using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Canvas")]
    public Canvas playerInfoCanvas;
    public Canvas settingsCanvas;
    public Canvas selectCanvas;

    private Button newGameBtn;
    private Button continueBtn;
    private Button settingsBtn;
    private Button aboutBtn;
    private Button quitBtn;
    private Button saveGameBtn;

    private Button wizardBtn;
    private Button heroBtn;

    private Animator titleAnim;
    private Animator newGameAnim;
    private Animator continueAnim;
    private Animator settingsAnim;
    private Animator settingsPanelAnim;
    private Animator aboutAnim;
    private Animator aboutPanelAnim;
    private Animator quitAnim;
    private Animator saveGameAnim;

    [Header("Camera")]
    public Camera mainCamera;
    public Transform lookAtPoint;

    private bool canRotateCam;
    private bool isGameOn;

    private void Awake()
    {
        playerInfoCanvas.enabled = false;
        selectCanvas.enabled = false;
        isGameOn = false;

        newGameBtn = settingsCanvas.transform.GetChild(1).GetComponent<Button>();
        continueBtn = settingsCanvas.transform.GetChild(2).GetComponent<Button>();
        settingsBtn = settingsCanvas.transform.GetChild(3).GetComponent<Button>();
        aboutBtn = settingsCanvas.transform.GetChild(4).GetComponent<Button>();
        quitBtn = settingsCanvas.transform.GetChild(5).GetComponent<Button>();
        saveGameBtn = settingsCanvas.transform.GetChild(8).GetComponent<Button>();
        wizardBtn = selectCanvas.transform.GetChild(0).GetComponent<Button>();
        heroBtn = selectCanvas.transform.GetChild(1).GetComponent<Button>();

        titleAnim = settingsCanvas.transform.GetChild(0).GetComponent<Animator>();
        newGameAnim = settingsCanvas.transform.GetChild(1).GetComponent<Animator>();
        continueAnim = settingsCanvas.transform.GetChild(2).GetComponent<Animator>();
        settingsAnim = settingsCanvas.transform.GetChild(3).GetComponent<Animator>();
        aboutAnim = settingsCanvas.transform.GetChild(4).GetComponent<Animator>();
        quitAnim = settingsCanvas.transform.GetChild(5).GetComponent<Animator>();
        settingsPanelAnim = settingsCanvas.transform.GetChild(6).GetComponent<Animator>();
        aboutPanelAnim = settingsCanvas.transform.GetChild(7).GetComponent<Animator>();
        saveGameAnim = settingsCanvas.transform.GetChild(8).GetComponent<Animator>();

        newGameBtn.onClick.AddListener(NewGame);
        continueBtn.onClick.AddListener(Continue);
        settingsBtn.onClick.AddListener(Settings);
        aboutBtn.onClick.AddListener(About);
        quitBtn.onClick.AddListener(QuitGame);
        wizardBtn.onClick.AddListener(SelectWizard);
        heroBtn.onClick.AddListener(SelectHero);
        saveGameBtn.onClick.AddListener(SaveGame);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToMainMenu();
        }

        RotateCam();
        if (IsRotationDone())
        {
            selectCanvas.enabled = true;
            canRotateCam = false;
        }
    }

    void ReturnToMainMenu()
    {
        if (SettingsPanelOnScreen())
        {
            settingsPanelAnim.SetTrigger("BounceOut");
        }

        if (AboutPanelOnScreen())
        {
            aboutPanelAnim.SetTrigger("BounceOut");
        }
        titleAnim.SetTrigger("BounceIn");
        if (isGameOn)
            saveGameAnim.SetTrigger("BounceIn");
        else
            newGameAnim.SetTrigger("BounceIn");
        continueAnim.SetTrigger("BounceIn");
        settingsAnim.SetTrigger("BounceIn");
        aboutAnim.SetTrigger("BounceIn");
        quitAnim.SetTrigger("BounceIn");
    }

    void RotateCam()
    {
        if (canRotateCam)
        {
            mainCamera.transform.rotation = Quaternion.RotateTowards(mainCamera.transform.rotation, lookAtPoint.rotation, 0.5f);
        }
    }

    bool IsRotationDone()
    {
        return Quaternion.Angle(mainCamera.transform.rotation, lookAtPoint.rotation) < 0.1f;
    }

    #region Menu
    void NewGame()
    {
        titleAnim.ResetTrigger("BounceIn");
        if (isGameOn)
            saveGameAnim.ResetTrigger("BounceIn");
        else
            newGameAnim.ResetTrigger("BounceIn");
        continueAnim.ResetTrigger("BounceIn");
        settingsAnim.ResetTrigger("BounceIn");
        aboutAnim.ResetTrigger("BounceIn");
        quitAnim.ResetTrigger("BounceIn");

        PlayerPrefs.DeleteAll();
        canRotateCam = true;
        titleAnim.SetTrigger("BounceOut");
        if (isGameOn)
            saveGameAnim.SetTrigger("BounceOut");
        else
            newGameAnim.SetTrigger("BounceOut");
        continueAnim.SetTrigger("BounceOut");
        settingsAnim.SetTrigger("BounceOut");
        aboutAnim.SetTrigger("BounceOut");
        quitAnim.SetTrigger("BounceOut");
    }

    void Continue()
    {

    }

    void Settings()
    {
        titleAnim.ResetTrigger("BounceIn");
        if (isGameOn)
            saveGameAnim.ResetTrigger("BounceIn");
        else
            newGameAnim.ResetTrigger("BounceIn");
        continueAnim.ResetTrigger("BounceIn");
        settingsAnim.ResetTrigger("BounceIn");
        aboutAnim.ResetTrigger("BounceIn");
        quitAnim.ResetTrigger("BounceIn");

        if (isGameOn)
            saveGameAnim.SetTrigger("BounceOut");
        else
            newGameAnim.SetTrigger("BounceOut");
        continueAnim.SetTrigger("BounceOut");
        settingsAnim.SetTrigger("BounceOut");
        aboutAnim.SetTrigger("BounceOut");
        quitAnim.SetTrigger("BounceOut");
        settingsPanelAnim.SetTrigger("BounceIn");
    }

    bool SettingsPanelOnScreen()
    {
        return settingsPanelAnim.transform.position.y >= -800;
    }

    void About()
    {
        titleAnim.ResetTrigger("BounceIn");
        if (isGameOn)
            saveGameAnim.ResetTrigger("BounceIn");
        else
            newGameAnim.ResetTrigger("BounceIn");
        continueAnim.ResetTrigger("BounceIn");
        settingsAnim.ResetTrigger("BounceIn");
        aboutAnim.ResetTrigger("BounceIn");
        quitAnim.ResetTrigger("BounceIn");

        if (isGameOn)
            saveGameAnim.SetTrigger("BounceOut");
        else
            newGameAnim.SetTrigger("BounceOut");
        continueAnim.SetTrigger("BounceOut");
        settingsAnim.SetTrigger("BounceOut");
        aboutAnim.SetTrigger("BounceOut");
        quitAnim.SetTrigger("BounceOut");
        aboutPanelAnim.SetTrigger("BounceIn");
    }

    bool AboutPanelOnScreen()
    {
        return aboutPanelAnim.transform.position.y >= -800;
    }

    void QuitGame()
    {
        Application.Quit();
    }

    void SaveGame()
    {

    }
    #endregion

    #region SelectCHaracter
    void SelectWizard()
    {
        GameManager.Instance.SelectWizard();
        StartGame();
    }

    void SelectHero()
    {
        GameManager.Instance.SelectHero();
        StartGame();
    }

    void StartGame()
    {
        isGameOn = true;
        saveGameAnim.SetBool("IsGameOn", isGameOn);
        selectCanvas.gameObject.SetActive(false);

        playerInfoCanvas.enabled = true;
    }
    #endregion

}
