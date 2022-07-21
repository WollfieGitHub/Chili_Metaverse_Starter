using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawWeightSetting : MonoBehaviour
{
    private Dropdown _dropdown;

    private void Start()
    {
        _dropdown = GetComponentInChildren<Dropdown>();
        _dropdown.onValueChanged.AddListener(HandleDrawWeightChoice);
    }

    private void HandleDrawWeightChoice(int index)
    {
        ConfigManager.Instance.SetBowType((BowType)index);
    }
}
