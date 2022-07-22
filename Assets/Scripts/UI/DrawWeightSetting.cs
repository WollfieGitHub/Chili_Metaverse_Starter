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
        _dropdown.onValueChanged.AddListener(delegate {
            HandleDrawWeightChoice(_dropdown);
        });
    }

    private void HandleDrawWeightChoice(Dropdown dropdown)
    {
        ConfigManager.Instance.SetBowType((BowType)dropdown.value);
    }
}
