using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public CharacterData_SO characterStats;
    public AttackData_SO attackData;

    #region from Stats_SO
    public int MaxHealth {
        get
        {
            if (characterStats != null)
            {
                return characterStats.maxHealth;
            }
            else
            {
                return 0;
            }
        }
        set
        {
            characterStats.maxHealth = value;
        }
    }

    public int CurrentHealth
    {
        get
        {
            if (characterStats != null)
            {
                return characterStats.currentHealth;
            }
            else
            {
                return 0;
            }
        }
        set
        {
            characterStats.currentHealth = value;
        }
    }

    public int baseDefence
    {
        get
        {
            if (characterStats != null)
            {
                return characterStats.baseDefence;
            }
            else
            {
                return 0;
            }
        }
        set
        {
            characterStats.maxHealth = value;
        }
    }

    public int CurrentDefence
    {
        get
        {
            if (characterStats != null)
            {
                return characterStats.currentDefence;
            }
            else
            {
                return 0;
            }
        }
        set
        {
            characterStats.currentDefence = value;
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

}
