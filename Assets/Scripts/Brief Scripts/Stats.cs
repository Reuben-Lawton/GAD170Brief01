using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class handles all the data related to a characters stats.
/// 
/// TODO:
///  Generate some Physical stats for our character.
///  Calculate our Dancing stats based on our physical stats.
///  SetPercentageValue based on the decimal value coming in turn this into a %.
///  ReturnDancePowerLevel return a power level based on our dancing stats.
///  AddXP based on the xp coming in, add some xp.
///  LevelUp increase our level as well as increase our threshold for levelling up, finally increase our physical stats.
///  DistributePhysicalStatsOnLevelUp increase each of our physical stats by a value, and recalculate our dancing stats.
/// 
/// </summary>
/// 


public class Stats : MonoBehaviour
{
    /// <summary>
    /// Our current level.
    /// </summary>
    public int level;

    /// <summary>
    /// The current amount of xp we have accumulated.
    /// </summary>
    public int currentXp;

    /// <summary>
    /// The amount of xp required to level up.
    /// </summary>
    public int xpThreshold = 10;
    
    /// <summary>
    /// Our variables used to determine our fighting power.
    /// </summary>
    public int style;
    public int luck; 
    public int rhythm;

    /// <summary>
    /// Our physical stats that determine our dancing stats.
    /// </summary>
    public int agility;
    public int intelligence;
    public int strength;

    /// <summary>
    /// Used to determine the conversion of 1 physical stat, to 1 dancing stat.
    /// </summary>
    public float agilityMultiplier = 0.5f;
    public int strengthMultiplier = 1;
    public int intelligenceMultiplier = 2;

    /// <summary>
    /// A float used to display what the chance of winning the current fight is.
    /// </summary>
    public int perecentageChanceToWin;

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
    }

    /// <summary>
    /// This function should set our starting stats of Agility, Strength and Intelligence
    /// to some default RANDOM values.
    /// </summary>
    public void GeneratePhysicalStatsStats()
    {
        int Min = 1;
        int agilityMin = 2;
        int Max = 10;
        // int statpool = 25;
        Debug.LogWarning("Generate Physical Stats has been called");


        agility = Random.Range(agilityMin, Max);
        // statpool -= agility;

        intelligence = Random.Range(Min, Max);
        // statpool -= intelligence;

        strength = Random.Range(Min, Max);
        // statpool = 0;

        {
            Debug.Log("Physical stats have been randomly generated. " + " Agility: " + agility + " Intelligience: " + intelligence + " Strength: " + strength);
        }

        {
            Debug.LogWarning("Player stats have been generated. " + "Agility: " + agility + "Intelligience: " + intelligence + "Strength: " + strength);
        }


        UpdateStatsUI(); // update our current UI for our character
    }

    /// <summary>
    /// This function should set our style, luck and ryhtmn to values
    /// based on our currrent agility,intelligence and strength.
    /// </summary>
    public void CalculateDancingStats()
    {
        Debug.LogWarning("Generate Calculate Dancing Stats has been called");

        style = (int)((float)(agility) * (float)agilityMultiplier);
        luck = ((strength) * strengthMultiplier);
        rhythm = ((intelligence) * intelligenceMultiplier);
        {
            Debug.Log("Dance stats have been set, Style: " + style + " Luck: " + luck + " Rhythm: " + rhythm);
        }
        /* float currentAgility = agility;
         float currentStrength = strength;
         float currentIntelligience = intelligence;
         float currentStyle = style;
         float currentLuck = luck;
         float currentRhythm = rhythm;
        */
        // style = agility * agilityMultiplier;
        //luck = strength * strengthMultiplier;
        // rhythm = intelligience * intelligenceMultiplier;

    
/*
        {
            Debug.Log("Style has been set using: " + " agility of " + currentAgility + "multiplied by " + agilityMultiplier + " . Giving a Style value of " + (currentAgility * agilityMultiplier));
        }

        {
            Debug.Log("Luck has been set using: " + " strength of " + currentStrength + "multiplied by " + strengthMultiplier + " . Giving a Luck value of " + (currentStrength * strengthMultiplier));
        }

        {
            Debug.Log("Rhythm has been set using: " + " intelligience of " + currentIntelligience + "multiplied by " + intelligenceMultiplier + " . Giving a Rhythm value of " + (agility * agilityMultiplier));
        }*/



        // what we want I want is for you to take our physical stats and translate them into our dancing stats,
        // based on the multiplier of that stat as follows:
        // our Style should be based on our Agility.
        // our Rhythm should be based on our Strength.
        // our Luck should be based on our intelligence.
        // hint...your going to need to convert our ints into floats, then back to ints.

        UpdateStatsUI(); // update our current UI for our character
    }

    /// <summary>
    /// This is takes in a normalised value i.e. 0.0f - 1.0f, and is used to display our % chance to win.
    /// </summary>
    /// <param name="normalisedValue"></param>
    /// 
    int maxStyle = 5;
    int maxLuck = 10;
    int maxRhythm = 20;

    public void SetPercentageValue(float normalisedValue)
    {
        float maxLevel = (maxStyle + maxLuck + maxRhythm);
        float playerLevel = (style + luck + rhythm);
        // float opponentLevel = (opponentStyle + opponentLuck + opponentRhythm);

        normalisedValue = (float)(playerLevel / maxLevel);
        {
            Debug.Log("Player normalised value between 0.0 and 1.0 is :" + normalisedValue);
        }

        perecentageChanceToWin = (int)(normalisedValue * 100);
        {
            Debug.Log("Max Level: " + maxLevel + "Player is currently at: " + playerLevel + ". Their percent chance to win is:  " + " % " + perecentageChanceToWin);
        }              

      
        // Essentially we want to set our percentage to win, to be a percentage using our normalised value (decimal value of a fraction)
        // How can we convert out normalised value into a whole number?

        Debug.LogWarning("SetPercentageValue has been called we probably want to convert our normalised value to a percentage");
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
        int MaxStyleMultiplier = 3, MaxLuckMultiplier = 3, MaxRhythmMultiplier = 2;

        int maxRandomStyle = (maxStyle * MaxStyleMultiplier), maxRandomLuck = (maxLuck * MaxLuckMultiplier), maxRandomRhythm = (maxRhythm * MaxRhythmMultiplier);
        float maxRandomPower = (maxRandomStyle + maxRandomLuck + maxRandomRhythm);

        float danceRandomPower = (currentRandomStyle + currentRandomLuck + currentRandomRhythm);

        int returnRandomDancingPower = (int)((danceRandomPower / maxRandomPower) * 100);

        string myDebugMessage = "Generating a random power level of : " + danceRandomPower + "comparing to Max Power: " + maxRandomPower + ". Generates a power level of: " + (returnRandomDancingPower);

        Debug.Log(myDebugMessage);

        Debug.LogWarning("ReturnDancePowerLevel has been called, generated a random power level to use for battle points based on our stats");

        return (returnRandomDancingPower);

    }

    /// <summary>
    /// A function called when the battle is completed and some xp is to be awarded.
    /// The amount of xp gained is coming into this function
    /// </summary>

    public BattleHandler Battle;
    public void AddXP(int xpGained)
        
    {
        if(xpGained == 0)
        {
            Debug.Log("No XP achieved, try harder!");
        }
        else if (xpGained >= 1 && xpGained <= 85)
        {
            currentXp = (currentXp + xpGained);
            Debug.Log("XP gained is : " + xpGained + " so your current XP is  : " + currentXp);
        }
        //Debug.LogWarning("This character needs some xp to be given, the xpGained from the fight was: " + xpGained);

        // we probably want to do something with the xpGained.


        //We probably want to display the xp we just gained, by default it is 0
        uIManager.ShowPlayerXPUI(xpGained);

        // We probably also want to check to see if the player can level up and if so do something....what should we be checking?
    }
    

    /// <summary>
    /// A function used to handle actions associated with levelling up.
    /// </summary>
    
    private void LevelUp()
    {
        if (currentXp < xpThreshold)
        {
            Debug.Log("Current Xp is :" + currentXp);
        }
        else if (currentXp >= xpThreshold && currentXp <= (xpThreshold + 10) + 2)
        {
            level = (level + 1);
            Debug.Log("Current Xp is :" + currentXp + " You have Leveled up !, your current Level is : " + level + " , Congratulations!");
            xpThreshold = (xpThreshold + 10) + 2;
        }
        else if (currentXp >= xpThreshold && currentXp <= (xpThreshold + 10) + 2)
        {
            level = (level + 1);
            Debug.Log("Current Xp is :" + currentXp + " You have Leveled up !, your current Level is : " + level + " , Congratulations!");            xpThreshold = (xpThreshold * 2) + 2;
            xpThreshold = (xpThreshold + 10) + 2;
        }
        else if (currentXp >= xpThreshold && currentXp <= (xpThreshold + 10) + 2)
        {
            level = (level + 1);
            Debug.Log("Current Xp is :" + currentXp + " You have Leveled up !, your current Level is : " + level + " , Congratulations!");
            xpThreshold = (xpThreshold + 10) + 2;
        }
        else if (currentXp >= xpThreshold && currentXp <= (xpThreshold + 10) + 2)
        {
            level = (level + 1);
            Debug.Log("Current Xp is :" + currentXp + " You have Leveled up !, your current Level is : " + level + " , Congratulations!");
            xpThreshold = (xpThreshold + 10) + 2;
        }
        else if (currentXp >= xpThreshold && currentXp <= (xpThreshold + 10) + 2)
        {
            level = (level + 1);
            Debug.Log("Current Xp is :" + currentXp + " You have Leveled up !, your current Level is : " + level + " , Congratulations!");
            xpThreshold = (xpThreshold + 10) + 2;
        }
        else if (currentXp >= xpThreshold && currentXp <= (xpThreshold + 10) + 2)
        {
            level = (level + 1);
            Debug.Log("Current Xp is :" + currentXp + " You have Leveled up !, your current Level is : " + level + " , Congratulations!");
            xpThreshold = (xpThreshold + 10) + 2;
        }
        else if (currentXp >= xpThreshold && currentXp <= (xpThreshold + 10) + 2)
        {
            level = (level + 1);
            Debug.Log("Current Xp is :" + currentXp + " You have Leveled up !, your current Level is : " + level + " , Congratulations!");
            xpThreshold = (xpThreshold + 10) + 2;
        }
        else if (currentXp >= xpThreshold && currentXp <= (xpThreshold + 10) + 2)
        {
            level = (level + 1);
            Debug.Log("Current Xp is :" + currentXp + " You have Leveled up !, your current Level is : " + level + " , Congratulations!");
            xpThreshold = (xpThreshold + 10) + 2;
        }
        else if (currentXp >= xpThreshold && currentXp <= (xpThreshold + 10) + 2)
        {
            level = (level + 1);
            Debug.Log("Current Xp is :" + currentXp + " You have Leveled up !, your current Level is : " + level + " , Congratulations!");
            xpThreshold = (xpThreshold + 10) + 2;
        }
        else if (currentXp >= xpThreshold && currentXp <= (xpThreshold + 10) + 2)
        {
            level = (level + 1);
            Debug.Log("Current Xp is :" + currentXp + " You have Leveled up !, your current Level is : " + level + " , Congratulations!");
            xpThreshold = (xpThreshold + 10) + 2;
        }
        else if (currentXp >= xpThreshold && currentXp <= (xpThreshold + 10) + 2)
        {
            level = (level + 1);
            Debug.Log("Current Xp is :" + currentXp + " You have Leveled up !, your current Level is : " + level + " , Congratulations!");
            xpThreshold = (xpThreshold + 10) + 2;
        }
        else if (currentXp >= xpThreshold && currentXp <= (xpThreshold + 10) + 2)



            Debug.LogWarning("Level up has been called");
        // we probs want to increase our level....
        // As well as probably want to increase our threshold for when we should level up...based on our current new level
        // Last thing we probably want to do is increase our physical stats...if only we had a function to do that for us.       
      
        ShowLevelUpEffects(); // displays some fancy particle effects.
    }
    
    /// <summary>
    /// A function used to assign a random amount of points ot each of our skills.
    /// </summary>
    public void DistributePhysicalStatsOnLevelUp(int PointsPool)
    {
        Debug.LogWarning("DistributePhysicalStatsOnLevelUp has been called " + PointsPool);
        // let's share these points somewhat evenly or based on some forumal to increase our current physical stats
        // then let's recalculate our dancing stats again to process and update the new values.
    }

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
