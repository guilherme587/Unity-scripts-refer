using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Variáveis para os controles de UI
    public Slider volumeSlider;
    public Dropdown resolutionDropdown;
    public Toggle vsyncToggle;
    public Dropdown screenModeDropdown;
    public Button startButton;
    public Button quitButton;
    public GameObject itemSelectionPanel;
    public GameObject levelSelectionPanel;

    // Variáveis para a resolução do jogo
    private Resolution[] resolutions;
    private int currentResolutionIndex = 0;

    void Start()
    {
        // Inicializa os controles de UI
        InitializeUI();

        // Carrega as resoluções disponíveis
        resolutions = Screen.resolutions;

        // Adiciona as resoluções disponíveis ao dropdown de resolução
        resolutionDropdown.ClearOptions();
        List<string> resolutionOptions = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            resolutionOptions.Add(option);

            // Seleciona a resolução atual
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(resolutionOptions);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    void InitializeUI()
    {
        // Configura o valor inicial do slider de volume
        volumeSlider.value = AudioListener.volume;

        // Configura o valor inicial do toggle de VSync
        vsyncToggle.isOn = QualitySettings.vSyncCount != 0;

        // Configura o valor inicial do dropdown de modo de tela
        screenModeDropdown.value = (int)Screen.fullScreenMode;

        // Configura as ações dos botões
        startButton.onClick.AddListener(OnStartButtonClick);
        quitButton.onClick.AddListener(OnQuitButtonClick);
    }

    void OnStartButtonClick()
    {
        // Mostra o painel de seleção de fase
        levelSelectionPanel.SetActive(true);
    }

    void OnQuitButtonClick()
    {
        // Sai do jogo
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        // Define o volume do áudio
        AudioListener.volume = volume;
    }

    public void SetResolution(int resolutionIndex)
    {
        // Define a resolução
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVSync(bool vsync)
    {
        // Define o valor de VSync
        QualitySettings.vSyncCount = vsync ? 1 : 0;
    }

    public void SetScreenMode(int modeIndex)
    {
        // Define o modo de tela
        Screen.fullScreenMode = (FullScreenMode)modeIndex;
    }

    public void ShowItemSelectionPanel()
    {
        // Mostra o painel de seleção de itens
        itemSelectionPanel.SetActive(true);
    }

    public void HideItemSelectionPanel()
    {
        // Esconde o painel de seleção de itens
        itemSelectionPanel.SetActive(false);
    }

    public void SelectLevel(string levelName)
    {
        // Carrega o nível selecionado
        SceneManager.LoadScene(levelName);
    }
}

// using UnityEngine;
// using UnityEngine.UI;

// public class UIManager : MonoBehaviour
// {
//     public GameObject mainMenu;
//     public GameObject optionsMenu;
//     public GameObject resolutionMenu;
//     public GameObject audioMenu;
//     public GameObject graphicsMenu;
//     public GameObject levelSelectMenu;
//     public GameObject inventoryMenu;

//     // Resolução
//     public Dropdown resolutionDropdown;

//     // Áudio
//     public Slider masterVolumeSlider;
//     public Slider musicVolumeSlider;
//     public Slider sfxVolumeSlider;

//     // Gráficos
//     public Toggle vsyncToggle;
//     public Dropdown qualityDropdown;
//     public Dropdown screenModeDropdown;

//     void Start()
//     {
//         // Configuração da resolução
//         resolutionDropdown.ClearOptions();
//         foreach (Resolution res in Screen.resolutions)
//         {
//             resolutionDropdown.options.Add(new Dropdown.OptionData(res.ToString()));
//         }
//         resolutionDropdown.value = Screen.resolutions.Length - 1;
//         resolutionDropdown.RefreshShownValue();

//         // Configuração dos valores dos sliders de volume
//         masterVolumeSlider.value = AudioManager.instance.GetMasterVolume();
//         musicVolumeSlider.value = AudioManager.instance.GetMusicVolume();
//         sfxVolumeSlider.value = AudioManager.instance.GetSFXVolume();

//         // Configuração dos valores das opções de gráficos
//         vsyncToggle.isOn = QualitySettings.vSyncCount != 0;
//         qualityDropdown.value = QualitySettings.GetQualityLevel();
//         screenModeDropdown.value = (int)Screen.fullScreenMode;
//     }

//     void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.Escape))
//         {
//             CloseAllMenus();
//             OpenMainMenu();
//         }
//     }

//     // Funções de abertura de menus
//     public void OpenMainMenu()
//     {
//         mainMenu.SetActive(true);
//     }

//     public void OpenOptionsMenu()
//     {
//         optionsMenu.SetActive(true);
//     }

//     public void OpenResolutionMenu()
//     {
//         resolutionMenu.SetActive(true);
//     }

//     public void OpenAudioMenu()
//     {
//         audioMenu.SetActive(true);
//     }

//     public void OpenGraphicsMenu()
//     {
//         graphicsMenu.SetActive(true);
//     }

//     public void OpenLevelSelectMenu()
//     {
//         levelSelectMenu.SetActive(true);
//     }

//     public void OpenInventoryMenu()
//     {
//         inventoryMenu.SetActive(true);
//     }

//     // Funções de fechamento de menus
//     public void CloseAllMenus()
//     {
//         mainMenu.SetActive(false);
//         optionsMenu.SetActive(false);
//         resolutionMenu.SetActive(false);
//         audioMenu.SetActive(false);
//         graphicsMenu.SetActive(false);
//         levelSelectMenu.SetActive(false);
//         inventoryMenu.SetActive(false);
//     }

//     public void CloseCurrentMenu(GameObject currentMenu)
//     {
//         currentMenu.SetActive(false);
//     }

//     // Funções de controle da resolução
//     public void SetResolution()
//     {
//         Resolution res = Screen.resolutions[resolutionDropdown.value];
//         Screen.SetResolution(res.width, res.height, Screen.fullScreenMode);
//     }

//     // Funções de controle do áudio
//     public void SetMasterVolume(float volume)
//     {
//         AudioManager.instance.SetMasterVolume(volume);
//     }

//     public void SetMusicVolume(float volume)
//     {
//         AudioManager.instance.SetMusicVolume(volume);
//     }

//     public void SetSFXVolume(float volume)
//     {
//         AudioManager.instance.SetSFXVolume(volume);
//     }

//     // Funções de controle dos gráficos
//     public void SetVSync(bool vsync)
//     {
//         QualitySettings.vSyncCount = vsync ? 1 : 0;
//     }

//     public void SetQualityLevel(int level)
//     {
//         QualitySettings.SetQualityLevel(level);