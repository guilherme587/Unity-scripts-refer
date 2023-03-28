using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // UI elements
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject resolutionDropdown;
    public GameObject vsyncToggle;
    public GameObject fullscreenToggle;
    public GameObject volumeSlider;
    public GameObject levelSelectMenu;
    public GameObject itemSelectMenu;

    // Audio settings
    private AudioSource audioSource;
    private float defaultVolume;

    // Resolution settings
    private Resolution[] resolutions;
    private Dropdown resolutionDropdownComponent;
    private int currentResolutionIndex;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        defaultVolume = audioSource.volume;

        // Get available resolutions
        resolutions = Screen.resolutions;

        // Fill resolution dropdown with available resolutions
        resolutionDropdownComponent = resolutionDropdown.GetComponent<Dropdown>();
        resolutionDropdownComponent.ClearOptions();

        List<string> resolutionOptions = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            resolutionOptions.Add(resolutions[i].ToString());
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdownComponent.AddOptions(resolutionOptions);
        resolutionDropdownComponent.value = currentResolutionIndex;
        resolutionDropdownComponent.RefreshShownValue();
    }

    // Volume settings
    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public void ResetVolume()
    {
        audioSource.volume = defaultVolume;
    }

    // Resolution settings
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    // VSync settings
    public void SetVSync(bool isOn)
    {
        QualitySettings.vSyncCount = isOn ? 1 : 0;
    }

    // Fullscreen settings
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    // Level select
    public void OpenLevelSelect()
    {
        mainMenu.SetActive(false);
        levelSelectMenu.SetActive(true);
    }

    public void SelectLevel(string levelName)
    {
        // Load level
    }

    // Item select
    public void OpenItemSelect()
    {
        mainMenu.SetActive(false);
        itemSelectMenu.SetActive(true);
    }

    public void SelectItem(string itemName)
    {
        // Use item
    }

    // Back buttons
    public void BackToMainMenu()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        levelSelectMenu.SetActive(false);
        itemSelectMenu.SetActive(false);
    }

    public void BackToSettingsMenu()
    {
        settingsMenu.SetActive(true);
        resolutionDropdownComponent.value = currentResolutionIndex;
        resolutionDropdownComponent.RefreshShownValue();
    }
}
