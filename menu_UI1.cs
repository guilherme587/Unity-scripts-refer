using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Volume
    public Slider volumeSlider;
    private float volumeLevel;

    // Resolução
    public Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    // Vsync
    public Toggle vsyncToggle;
    private bool vsyncEnabled;

    // Modo de Tela
    public Toggle fullscreenToggle;
    private bool fullscreenEnabled;

    // Seleção de Fase
    public Button[] levelButtons;
    private int currentLevel;

    // Seleção de Itens
    public Button[] itemButtons;
    private int currentItem;

    void Start()
    {
        // Configurações de Volume
        volumeLevel = PlayerPrefs.GetFloat("Volume", 1f);
        volumeSlider.value = volumeLevel;

        // Configurações de Resolução
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        int currentResolutionIndex = 0;
        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        // Configurações de Vsync
        vsyncEnabled = PlayerPrefs.GetInt("Vsync", 0) == 1;
        vsyncToggle.isOn = vsyncEnabled;

        // Configurações de Modo de Tela
        fullscreenEnabled = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
        fullscreenToggle.isOn = fullscreenEnabled;

        // Configurações de Seleção de Fase
        currentLevel = PlayerPrefs.GetInt("Level", 0);
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelIndex = i;
            levelButtons[i].onClick.AddListener(() => SelectLevel(levelIndex));
        }

        // Configurações de Seleção de Itens
        currentItem = PlayerPrefs.GetInt("Item", 0);
        for (int i = 0; i < itemButtons.Length; i++)
        {
            int itemIndex = i;
            itemButtons[i].onClick.AddListener(() => SelectItem(itemIndex));
        }
    }

    public void SetVolume(float volume)
    {
        volumeLevel = volume;
        PlayerPrefs.SetFloat("Volume", volumeLevel);
        AudioListener.volume = volumeLevel;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, fullscreenEnabled);
    }

    public void SetVsync(bool vsync)
    {
        vsyncEnabled = vsync;
        PlayerPrefs.SetInt("Vsync", vsyncEnabled ? 1 : 0);
        QualitySettings.vSyncCount = vsyncEnabled ? 1 : 0;
    }

    public void SetFullscreen(bool fullscreen)
    {
        fullscreenEnabled = fullscreen;
        PlayerPrefs.SetInt("Fullscreen", fullscreenEnabled ? 1 : 0);
        Screen.fullScreen = fullscreenEnabled;
    }

    public void SelectLevel(int levelIndex)
    {
        currentLevel = levelIndex;
        PlayerPrefs.SetInt("Level", currentLevel);
    }

    public void SelectItem(int itemIndex)
    {
        currentItem =
