using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu ("Extended/UI/Menu/Settings Screen")]
public class SettingsScreenController : MonoBehaviour
{
    [Tooltip ("The label responsible for displaying the current quality level.")]
    [SerializeField] private Text _QualityLevelLabel = null;
    [Tooltip ("The label responsible for displaying the current AA level.")]
    [SerializeField] private Text _AAQualityLabel = null;

    public void MuteMusic (bool mute)
    {
        //TODO: Interface with global audio system.
    }

    public void MuteSFX (bool mute)
    {
        //TODO: Interface with global audio system.
    }

    public void IncreaseQualityLevel ()
    {
        QualitySettings.IncreaseLevel ();

        var qualityLevel = QualitySettings.GetQualityLevel ();
        _QualityLevelLabel.text = QualitySettings.names[qualityLevel];
    }

    public void DecreaseQualityLevel ()
    {
        QualitySettings.DecreaseLevel ();

        var qualityLevel = QualitySettings.GetQualityLevel ();
        _QualityLevelLabel.text = QualitySettings.names[qualityLevel];
    }

    public void IncreaseAALevel ()
    {
        QualitySettings.antiAliasing++;

        _AAQualityLabel.text = "AA: " + QualitySettings.antiAliasing;
    }

    public void DecreaseAALevel ()
    {
        QualitySettings.antiAliasing--;

        _AAQualityLabel.text = "AA: " + QualitySettings.antiAliasing;
    }

    public void ChangeAnisotropicFiltering (bool active)
    {
        if (active)
            QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
        else
            QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
    }

    public void SoftParticles (bool soften)
    {
        if (soften)
            QualitySettings.softParticles = true;
        else
            QualitySettings.softParticles = false;
    }

    public void Menu ()
    {
        EventManager.MenuStateChanged (MenuStates.Menu);
    }
}
