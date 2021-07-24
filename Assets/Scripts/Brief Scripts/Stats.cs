using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class handles all the data related to a characters stats.
/// 
/// TODO:
///  Generate some Physical stats for our character.    DONE
///  Calculate our Dancing stats based on our physical stats.    DONE
///  SetPercentageValue based on the decimal value coming in turn this into a %.    DONE
///  ReturnDancePowerLevel return a power level based on our dancing stats.    DONE
///  AddXP based on the xp coming in, add some xp.    DONE
///  LevelUp increase our level as well as increase our threshold for levelling up, finally increase our physical stats.    DONE
///  DistributePhysicalStatsOnLevelUp increase each of our physical stats by a value, and recalculate our dancing stats.    DONE
/// 
/// </summary>
/// 


public class Stats : MonoBehaviour
{
    #region the variables
    /// <summary>
    /// Our current level.
    /// </summary>
    public int level = -1;

    /// <summary>
    /// The current amount of xp we have accumulated.
    /// </summary>
    public int currentXp;

    /// <summary>
    /// The amount of xp required to level up.
    /// </summary>
    public int xpThreshold = 10;

    /// <summary>
    /// Amount of XP to distribute to stats
    /// </summary>
    public int xpToDistribute = 0;
    
    /// <summary>
    /// Our variables used to determine our fighting power.
    /// </summary>
    public int style;
    public int luck; 
    public int rhythm;

    /// <summary>
    /// Our physical stats that determine our dancing stats.
    /// </summary>
    public int agility = 2;
    public int intelligence;
    public int strength;

    /// <summary>
    /// Used to determine the conversion of 1 physical stat, to 1 dancing stat.
    /// </summary>
    public float agilityMultiplier = 0.5f;
    public int intelligenceMultiplier = 2;
    public int strengthMultiplier = 1;

    /// <summary>
    /// A float used to display what the chance of winning the current fight is.
    /// </summary>
    public int perecentageChanceToWin = 0;

    #endregion
    #region character references, no mods required
    [HideInInspector]
    public AnimationController animController; // reference to our animation controller on our character
    [HideInInspector]
    public SFXHandler sfxHandler; // reference to our sfx Handler in our scene
    [HideInInspector]
    public ParticleHandler particleHandler; // a refernce to our particle system that is played when we level up.  
    public UIManager uIManager; // a reference to the UI Manager in our scene.
    public StatsUI statsUI; // a referecence to our stats ui for this character.
    #endregion

    /// <summary>
    /// Called on the very first frame of the game
    /// </summary>
    private void Start()
    {
        SetUpReferences();//sets up the references to other scripts we need for functionality.
        GeneratePhysicalStatsStats(); // we want to generate some physical stats.
        CalculateDancingStats();// using those physical stats we want to generate some dancing stats.      
        SetPercentageValue(); // sets the percentage chance to win at the start of the game based on stats.
        ReturnDancePowerLevel(); // generates a power level to use in battle
    }

    #region physical and dance stats 
    /// <summary>
    /// This function should set our starting stats of Agility, Strength and Intelligence
    /// to some default RANDOM values.
    /// </summary>


    public void GeneratePhysicalStatsStats()
    {
        int Min = 1; // int minimum
        int agilityMin = 2; // agility minimum set at 2 so that when we use the agility multiplier we dont get a value of 0.5
        int Max = 10; // int max
        // int statpool = 25; decided to not implement statpool

        agility = Random.Range(agilityMin, Max);
        // statpool -= agility;

        intelligence = Random.Range(Min, Max);
        // statpool -= intelligence;

        strength = Random.Range(Min, Max);
        // statpool = 0;

        {
            Debug.Log("Physical stats have been randomly generated. " + " Agility: " + agility + " Intelligience: " + intelligence + " Strength: " + strength);
        }       

        UpdateStatsUI(); // update our current UI for our character
    }

    /// <summary>
    /// This function should set our style, luck and ryhtmn to values
    /// based on our currrent agility,intelligence and strength.
    /// </summary>
    public void CalculateDancingStats()
    {
        style = (int)((float)(agility) * (float)agilityMultiplier);  // style set by multiply agility by the muliplier, both as a float then returned as an int
        luck = ((strength) * strengthMultiplier); // luck set by multiply strength by the multiplier
        rhythm = ((intelligence) * intelligenceMultiplier); // rhythm set by multiply intelligence by the multiplier
        {
            Debug.Log("Dance stats have been set, Style: " + style + " Luck: " + luck + " Rhythm: " + rhythm);
        }

        UpdateStatsUI(); // update our current UI for our character
    }

    /// <summary>
    /// This is takes in a normalised value i.e. 0.0f - 1.0f, and is used to display our % chance to win.
    /// </summary>
    /// <param name="normalisedValue"></param>
    /// 
    int maxStyle = 5; // max style that can be generated
    int maxLuck = 10; // max luck that can be generated 
    int maxRhythm = 20; // max rhythm that can be generated
    #endregion

    #region chance to win percentage and random power level for battle
    public void SetPercentageValue(float normalisedValue = 0.0f)
    {
        float maxLevel = (float)(maxStyle + maxLuck + maxRhythm); // used to calculate a max level to compare to player level
        float playerLevel = (float)(style + luck + rhythm); // players current level that we'll use 
        // float opponentLevel = (opponentStyle + opponentLuck + opponentRhythm);
        
        normalisedValue = (playerLevel / maxLevel); // should give us a normalised value, if it doesn't then I need to rethink this
        {
            Debug.Log("Player normalised value between 0.0 and 1.0 is :" + normalisedValue);
        }

        perecentageChanceToWin = (int)(normalisedValue * 100); // multiply the normalised value by 100 to give us a percent chance to win
        {
            Debug.Log("Max Level: " + maxLevel + "Player is currently at: " + playerLevel + ". Their percent chance to win is:  " + " % " + perecentageChanceToWin);
        }              
              
           UpdateStatsUI(); // update our current UI for our character

    }

    /// <summary>
    /// Used to generate a number of battle points that is used in combat.
    /// </summary>
    /// <returns></returns>
    public int ReturnDancePowerLevel()
    {
        // We want to design some algorithm that will generate a number of points based off of our luck,style and rythm, we probably want to add some randomness in our calculation too
        // to ensure that there is not always a draw, by default it just returns 0. 
        // If you right click this function and find all references you can see where it is called.
        // Let's also throw in a little randomness in here, so it's not a garunteed win

        int currentRandomStyle = (style * (Random.Range(1, 3))), currentRandomLuck = (luck * (Random.Range(1, 3))), currentRandomRhythm = (rhythm * (Random.Range(1, 2)));
        int MaxStyleMultiplier = 3, MaxLuckMultiplier = 3, MaxRhythmMultiplier = 2; // 

        int maxRandomStyle = (maxStyle * MaxStyleMultiplier), maxRandomLuck = (maxLuck * MaxLuckMultiplier), maxRandomRhythm = (maxRhythm * MaxRhythmMultiplier);
        float maxRandomPower = (maxRandomStyle + maxRandomLuck + maxRandomRhythm);

        float danceRandomPower = (currentRandomStyle + currentRandomLuck + currentRandomRhythm);

        int returnRandomDancingPower = (int)((danceRandomPower / maxRandomPower) * 100);

        string myDebugMessage = "Generating a random power level of : " + danceRandomPower + ". Compare to Max Power: " + maxRandomPower + ". Generates a power level of: " + (returnRandomDancingPower);

        Debug.Log(myDebugMessage);

        Debug.LogWarning("ReturnDancePowerLevel has been called, generated a random power level to use for battle points based on our stats");

        return (returnRandomDancingPower);

    }

    /// <summary>
    /// A function called when the battle is completed and some xp is to be awarded.
    /// The amount of xp gained is coming into this function
    /// </summary>

    // public BattleHandler Battle;
    #endregion

    #region after battle add Xp, level up and distribute stats
    public void AddXP(int xpGained)

    {
        int minXp = 1;
        int maxXp = 85;
        if (xpGained == 0) // if no XP gained go to next if statement
        {
            Debug.Log("No XP achieved, try harder!");
        }
        else if (xpGained >= minXp && xpGained <= maxXp) // if Xp is gained between minimum of 1 and maximum of 85 then add xp to current xp, then check to see if we can level up
        {            
            currentXp += (xpGained);
            LevelUp();
            Debug.Log("XP gained is : " + xpGained + " so your current XP is  : " + currentXp);           
        }

        xpToDistribute = xpGained; // using xp to distribute taken from the xp gained in battle
        uIManager.ShowPlayerXPUI(xpGained);
    }
    

    /// <summary>
    /// A function used to handle actions associated with levelling up.
    /// </summary>
    
    public void LevelUp()
    {
        int ThresholdIncrement = 12; // amount the Threshold increases after each levelling up
        
        if (currentXp < xpThreshold) // if Current Xp is less then Threshold do nothing and check the next if statement
        {
            Debug.Log("Current Xp is not enough to level up, current XP is : " + currentXp + " , Xp increases at the next threshold: " + xpThreshold);
        }
        else if (currentXp >= xpThreshold && currentXp <= (xpThreshold) + ThresholdIncrement) // if current Xp is greater then Threshold and less then Threshold plus Increment then increase level by 1
        {
            level = (level + 1);
            Debug.Log("Current Xp is :" + currentXp + " You have Leveled up !, your current Level is : " + level + " , Congratulations!");
            xpThreshold = (xpThreshold + ThresholdIncrement);
            ShowLevelUpEffects(); // displays some fancy particle effects.
        }
        else if (currentXp > ((xpThreshold) + ThresholdIncrement) && currentXp <= ((xpThreshold + ThresholdIncrement) + ThresholdIncrement)) // if current xp is greater then xp threshold + 1 increment and less or then threshold + 2 increments then level up by 2
        {
            level = (level + 2); // level up 2 times
            Debug.Log("Current Xp is :" + currentXp + " You have Leveled up !, your current Level is : " + level + " , Congratulations!");
            xpThreshold = (xpThreshold + ThresholdIncrement + ThresholdIncrement);
            ShowLevelUpEffects(); // displays some fancy particle effects.
        }
        else if (currentXp > ((xpThreshold + ThresholdIncrement) + ThresholdIncrement) && currentXp <= (((xpThreshold + ThresholdIncrement) + ThresholdIncrement) + ThresholdIncrement)) // if current xp is more then threshold + 2 increments and less then threshold + 3 increment then level up by 3
        {
            level = (level + 3); // level up 3 times
            Debug.Log("Current Xp is :" + currentXp + " You have Leveled up !, your current Level is : " + level + " , Congratulations!");
            xpThreshold = (xpThreshold + ThresholdIncrement + ThresholdIncrement + ThresholdIncrement);
            ShowLevelUpEffects(); // displays some fancy particle effects.
        }
                
        else if (currentXp >= xpThreshold + (ThresholdIncrement * 20)) // if player has leveled up 20 times then  ???
        {
            level = (9000); // set beyond reasonable doubt
            Debug.Log("Current Xp is :" + currentXp + " You have Leveled up !, your at Max Level, : " + level + " , Congratulations! Go find another hobby");
            xpThreshold *= 9000;
        }
        DistributePhysicalStatsOnLevelUp(xpToDistribute); // after levelling up we distribute the xp points to our physical stats

        UpdateStatsUI(); // update our current UI for our character
    }

    /// <summary>
    /// A function used to assign a random amount of points ot each of our skills.
    /// </summary>
    public void DistributePhysicalStatsOnLevelUp(int PointsPool) 

    {
        PointsPool = xpToDistribute; // points pool taken from the xp to distribute that was the xp gained in battle

        if (PointsPool == 0)
        {
            Debug.Log("Points Pool has got no points... hmm what now ?"); // a message that should never display
        }
        else if (PointsPool >= 1) // Points pool should be 5, 10 , 15 or 20
        {
            agility += (Random.Range(1, PointsPool)); // agility gets agility plus a random range from 1 to the max amount in pointspool
            PointsPool -= agility; // points pool decreases, after using some points for agility
            intelligence += (Random.Range(1, PointsPool)); // points pool gives intelligence a random amount from 1 to whatever is left in pointspool
            PointsPool -= intelligence; // points pool decreases after giving points to intelligence
            strength += (Random.Range(1, PointsPool)); // strength gets some points from the points pool
            // there is the chance that points only go to agility or agility & intelligence but not strenght, I could implement further conditions to prevent this if I had thought of it sooner
            PointsPool = 0; // points pool returns to 0
            {
                Debug.Log("Xp gained from battle has been distributed to player physical stats");
            }
            if (PointsPool == 0) // when points pool is at 0 then calculate dance stats, 
            {
                CalculateDancingStats(); // calculates dancing stats again
                ReturnDancePowerLevel(); // generates a power level to use in battle
                UpdateStatsUI(); // update our current UI for our character

                {
                    Debug.Log("Dance stats have been updated set, Style: " + style + " Luck: " + luck + " Rhythm: " + rhythm);
                }
            }
        }
        
        UpdateStatsUI(); // update our current UI for our character
    }
    #endregion

    #region No Mods Required
    /// <summary>
    /// Get's all the script references required for this charactert
    /// </summary>
    private void SetUpReferences()
    {
        animController = GetComponent<AnimationController>(); // just getting a reference to our animation component on our dancer...this is behind the scenes for the dancing to occur.
        sfxHandler = FindObjectOfType<SFXHandler>(); // Finds a reference to our sfxHandler script that is in our scene.
        particleHandler = GetComponentInChildren<ParticleHandler>(); // searching through the child objects of this object to find the particle system.
    }

    /// <summary>
    /// If our statsUI field is not null, then we pass in a reference to ourself and update the stats.
    /// </summary>
    public void UpdateStatsUI()
    {
        // this just updates our UI for our character to show new stats.
        if (statsUI != null)
        {
            statsUI.UpdateStatsUI(this); // pass in a reference to our own stat script.
        }
    }

    /// <summary>
    /// Shows the level up effects whenever the character has levelled up
    /// </summary>
    private void ShowLevelUpEffects()
    {
        // plays the level up sound effect.
        sfxHandler.LevelUp();
        // emits a particle effect to show we have levelled up
        particleHandler.Emit();
        // Displays a UI Message to the player we have levelled up
        uIManager.ShowLevelUI();
    }
    #endregion 
}
