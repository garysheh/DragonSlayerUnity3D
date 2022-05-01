using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public event Action<int, int> healthBarwithAttack;
    public CharacterData_SO characterStats;
    private CharacterData_SO characterStats_instance;
    public AttackData_SO attackData;
    public SkillData_SO skillData;

    [HideInInspector]
    public bool isCrit = false;

    private void Awake()
    {
        if (characterStats != null)
        {
            characterStats_instance = Instantiate(characterStats);
        }
    }
    #region from Stats_SO
    public int MaxHealth {
        get
        {
            if (characterStats_instance != null)
            {
                return characterStats_instance.maxHealth;
            }
            else
            {
                return 0;
            }
        }
        set
        {
            characterStats_instance.maxHealth = value;
        }
    }

    public int CurrentHealth
    {
        get
        {
            if (characterStats_instance != null)
            {
                return characterStats_instance.currentHealth;
            }
            else
            {
                return 0;
            }
        }
        set
        {
            characterStats_instance.currentHealth = value;
        }
    }

    public int MaxMana
    {
        get
        {
            if (characterStats_instance != null)
            {
                return characterStats_instance.maxMana;
            }
            else
            {
                return 0;
            }
        }
        set
        {
            characterStats_instance.maxMana = value;
        }
    }

    public int CurrentMana
    {
        get
        {
            if (characterStats_instance != null)
            {
                return characterStats_instance.currentMana;
            }
            else
            {
                return 0;
            }
        }
        set
        {
            characterStats_instance.currentMana = value;
        }
    }

    public int baseDefence
    {
        get
        {
            if (characterStats_instance != null)
            {
                return characterStats_instance.baseDefence;
            }
            else
            {
                return 0;
            }
        }
        set
        {
            characterStats_instance.maxHealth = value;
        }
    }

    public int CurrentDefence
    {
        get
        {
            if (characterStats_instance != null)
            {
                return characterStats_instance.currentDefence;
            }
            else
            {
                return 0;
            }
        }
        set
        {
            characterStats_instance.currentDefence = value;
        }
    }
    #endregion

    #region from Attack_SO
    public float AttackRange
    {
        get
        {
            if (attackData != null)
            {
                return attackData.attackRange;
            }
            else
            {
                return 0;
            }
        }
        set
        {
            attackData.attackRange = value;
        }
    }

    public float AttackCD
    {
        get
        {
            if (attackData != null)
            {
                return attackData.attackCD;
            }
            else
            {
                return 0;
            }
        }
        set
        {
            attackData.attackCD = value;
        }
    }

    public float SkillRange
    {
        get
        {
            if (attackData != null)
            {
                return attackData.skillRange;
            }
            else
            {
                return 0;
            }
        }
        set
        {
            attackData.skillRange = value;
        }
    }

    public float SkillCD
    {
        get
        {
            if (attackData != null)
            {
                return attackData.skillCD;
            }
            else
            {
                return 0;
            }
        }
        set
        {
            attackData.skillCD = value;
        }
    }

    public int MinDamage
    {
        get
        {
            if (attackData != null)
            {
                return attackData.minDamage;
            }
            else
            {
                return 0;
            }
        }
        set
        {
            attackData.minDamage = value;
        }
    }

    public int MaxDamage
    {
        get
        {
            if (attackData != null)
            {
                return attackData.maxDamage;
            }
            else
            {
                return 0;
            }
        }
        set
        {
            attackData.maxDamage = value;
        }
    }

    public float CritMultiplier
    {
        get
        {
            if (attackData != null)
            {
                return attackData.critMultiplier;
            }
            else
            {
                return 0;
            }
        }
        set
        {
            attackData.critMultiplier = value;
        }
    }

    public float CriticalChance
    {
        get
        {
            if (attackData != null)
            {
                return attackData.critChance;
            }
            else
            {
                return 0;
            }
        }
        set
        {
            attackData.critChance = value;
        }
    }
    #endregion


    #region combat related
    public void takeDamage(CharacterStats attacker, CharacterStats defender)
    {
        //  default damge is 1
        int damage = Mathf.Max((attacker.damageCalc() - defender.CurrentDefence), 1);
        CurrentHealth = Mathf.Max((CurrentHealth - damage), 0);

        if (isCrit)
        {
            defender.GetComponent<Animator>().SetTrigger("GetHit");
        }
        //  TODO: UI update
        healthBarwithAttack?.Invoke(CurrentHealth, MaxHealth);
        //  TODO: leveling
    }

    public int damageCalc()
    {
        float damage = UnityEngine.Random.Range(attackData.minDamage, attackData.maxDamage);
        return isCrit ? (int)(damage * attackData.critMultiplier) : (int)damage;
    }
    #endregion
}
