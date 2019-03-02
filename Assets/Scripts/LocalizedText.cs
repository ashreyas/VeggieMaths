using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LocalizedText : MonoBehaviour
{

    public string key;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(UpdateText());
    }

    public IEnumerator UpdateText()
    {
        while (!LocalizationManager.instance.isReady)
            yield return new WaitForSeconds(0.2f);
        Text text = GetComponent<Text>();
        text.text = LocalizationManager.instance.GetLocalizedValue(key);
    }

}