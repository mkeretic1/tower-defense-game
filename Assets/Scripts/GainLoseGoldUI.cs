using UnityEngine;
using UnityEngine.UI;

public class GainLoseGoldUI :MonoBehaviour
{
    public GameObject gainLoseGoldUI;
    private Text GainLoseGoldText;

    public void goldFadeawayCanvas(Vector3 position, Quaternion rotation, string amount)
    {
        GainLoseGoldText = gainLoseGoldUI.GetComponentInChildren<Text>();
        GainLoseGoldText.text = amount.ToString();

        GameObject goldEffectUI = (GameObject) Instantiate(gainLoseGoldUI, position, rotation);
        Destroy(goldEffectUI, 5f);
    }

}
