using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILevelInfo : MonoBehaviour
{
    #region Fields

    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Image _progress;

    #endregion

    #region Properties

    public string Text
    {
        set => _text.text = value;
    }

    public float Progress
    {
        set => _progress.fillAmount = value;
    }

    #endregion
}
