﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnitStats;
using Utilities;
using TMPro;

public class CharacterInfoHandler : MonoBehaviour
{
    public Image hpBar;
    public CharacterStatsSystem unitStats;
    public TextMeshProUGUI curHealth;
    public TextMeshProUGUI maxHealth;
    public TextMeshProUGUI curMana;
    public TextMeshProUGUI maxMana;
    public TextMeshProUGUI weightCounter;
    public TextMeshProUGUI nickname;
    public bool isUpdating = false;

    public UserInterfaceResizeFitter fitter;
    public void Start()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.UPDATE_UNIT_HEALTH, UpdateHealthBar);
    }
    public void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveActionAtObserver(EventNames.UPDATE_UNIT_HEALTH, UpdateHealthBar);
    }
    public void Initialize(CharacterStatsSystem stats)
    {
        unitStats = stats;
        nickname.text = stats.name;
        curHealth.text = stats.health_C.ToString();
        maxHealth.text = stats.health_M.ToString();
        UpdateHealthBar();
    }

    public void UpdateHealthBar(Parameters p = null)
    {
        hpBar.fillAmount = GetHpFill();
        if(unitStats != null)
        {
            if(curHealth != null)   curHealth.text = unitStats.health_C.ToString();
            if(maxHealth != null) maxHealth.text = unitStats.health_M.ToString();
        }
    }

    public float GetHpFill()
    {
        float fill = 0;

        if (unitStats == null)
        {
            return 0;
        }
        float  tmp = unitStats.health_C / unitStats.health_M;
        fill = tmp;
        
        return fill;
    }

    public void ClearHandler()
    {
        hpBar.fillAmount = 1;
        unitStats = null;
    }

    public void SetAsManualUnit()
    {
        if (fitter == null)
            return;

        fitter.SizeIncrease();
    }
    public void SetAsAutoUnit()
    {
        if (fitter == null)
            return;

        fitter.SizeDecrease();
    }
}
