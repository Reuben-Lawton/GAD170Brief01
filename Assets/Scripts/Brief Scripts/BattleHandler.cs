using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This handles all the battle logic of who is determined to be the winner of a fight between
/// two characters.
/// 
/// TODO:
///     - SimulateBattle needs to calculate a normalised (decimal) value to display the % chance of winning a fight.
///     - Battle needs to handle the logic for who wins a fight and telling the winner they get some XP for their trouble.
/// </summary>
public class BattleHandler:MonoBehaviour
{
    public float MyStats = 0.0f;
    public float Opponent = 0.0f;
    public SFXHandler sfxHandler; // reference to our sfx Handler to play sound effects.
    
    /// <summary>
    /// Returns a float of the percentage chance to win the fight based on your characters current stats.
    /// </summary>
    /// <param name="MyStats"></param>
    /// <param name="Opponent"></param>
    /// <returns></returns>
    public float SimulateBattle(Stats MyStats, Stats Opponent)
    {
        int myPoints = MyStats.ReturnDancePowerLevel(); // our current powerlevel
        int opponentPoints = Opponent.ReturnDancePowerLevel(); // our opponents current power level


        if (myPoints <= 0 || opponentPoints <= 0)
        {
            Debug.LogWarning(" Simulate battle called; but Player or NPC battle points is 0, most likely the logic has not be setup for this yet");
        }
        else if (myPoints == opponentPoints)
        {
            Debug.Log("Somehow our points are the same,....hhmmm draw ?");
        }
        else if (myPoints < opponentPoints)
        {

            Debug.Log("Opponent has higher points than player. Opponents points currently at : " + opponentPoints + ". Player only has : " + myPoints);
        }
        else if (myPoints > opponentPoints)
        {

            Debug.Log("Player has higher points than opponent. Player points currently at : " + myPoints + ". Opponent only has: " + opponentPoints);
        }

        // we probably want to compare our powerlevels...hope they aren't over 9000.
        // we need to return a normalised (decimal) value....how much do you remember about percentages?
        // don't forget that we are returning a float...but diving 2 ints...what happens?

        //Debug.LogWarning("Simulate battle called, but the logic hasn't been set up yet, so defaulting to 0");
        return 0;
    }


    /// <summary>
    /// Is called when the player presses space bar.
    /// This function should take a player and npc 
    /// then determine who has won and give some xp and show some sweet winning effects.
    /// </summary>
    /// <param name="player"></param>
    /// <param name="npc"></param>
    public void Battle(Stats player, Stats npc)
    {     
        int playerPowerLevel = player.ReturnDancePowerLevel(); // player powerlevel
        int npcPowerLevel = npc.ReturnDancePowerLevel(); // npc powerlevel

        /**if (playerPowerLevel <= 0 || npcPowerLevel <= 0)
        {
            Debug.LogWarning("Player or NPC battle points is 0, most likely the logic has not be setup for this yet");
        }*/
         
    if (playerPowerLevel == npcPowerLevel)
        
            SetWinningEffects(player, npc, 0); // setting the winning effects to a draw.
            player.AddXP(0); // player gets no xp
            npc.AddXP(0); // npc gets no xp either
            {
                Debug.Log("Draw yo, need to git gud");
            }
        if (playerPowerLevel > npcPowerLevel)
        {
            SetWinningEffects(player, npc, 1); // setting the winning effects to player greater than NPC.
            npc.AddXP(0); // npc gets no xp

            if ((playerPowerLevel - npcPowerLevel) <= 5)
            {
                player.AddXP(5);
                Debug.Log("Player power level is 5 or less, more then opponent, awarding 5XP");
            }
            else if ((playerPowerLevel - npcPowerLevel) > 5 && (playerPowerLevel - npcPowerLevel) <= 10)
            {
                player.AddXP(10);
                Debug.Log("Player power level is greater then 5 and less then or equal to 10, more then opponent, awarding 10 XP.");
            }
            else if ((playerPowerLevel - npcPowerLevel) > 10 && (playerPowerLevel - npcPowerLevel) <= 20)
            {
                player.AddXP(15);
                Debug.Log("Player power level is greater then 10 and less then or equal to 20, more then opponent, awarding 15 XP.");
            }
            else if ((playerPowerLevel - npcPowerLevel) > 20 && (playerPowerLevel - npcPowerLevel) <= 84)
            {
                player.AddXP(20);
                Debug.Log("Player power level is greater then 20 and less then or equal to 84, more then opponent, awarding 20 XP.");
            }
        }
        

    if (playerPowerLevel < npcPowerLevel)
        {
            SetWinningEffects(player, npc, -1); // setting the winning effects to player greater than NPC.
            player.AddXP(0); // npc gets no xp

            if ((npcPowerLevel - playerPowerLevel) <= 5)
            {
                npc.AddXP(5);
                Debug.Log("Player power level is 5 or less, more then opponent, awarding 5XP");
            }
            else if ((npcPowerLevel - playerPowerLevel) > 5 && (npcPowerLevel - playerPowerLevel) <= 10)
            {
                npc.AddXP(10);
                Debug.Log("Player power level is greater then 5 and less then or equal to 10, more then opponent, awarding 10 XP.");
            }
            else if ((npcPowerLevel - playerPowerLevel) > 10 && (npcPowerLevel - playerPowerLevel) <= 20)
            {
                npc.AddXP(15);
                Debug.Log("Player power level is greater then 10 and less then or equal to 20, more then opponent, awarding 15 XP.");

            }
            else if ((npcPowerLevel - playerPowerLevel) > 20 && (npcPowerLevel - playerPowerLevel) <= 84)
            {
                player.AddXP(20);
                Debug.Log("Player power level is greater then 20 and less then or equal to 84, more then opponent, awarding 20 XP.");
            }
        }

        /*
    }
    else if (playerPowerLevel > npcPowerLevel)
    {
        SetWinningEffects(player, npc, 1); // setting the winning effects to player greater than NPC.
        player.AddXP(0); // player gets 1 XP
        npc.AddXP(0); // npc gets no xp
        Debug.Log("Player 1 power level is greater than the oponenet NPC");
     }
    else if (playerPowerLevel < npcPowerLevel)
    {
        SetWinningEffects(player, npc, -1); // setting the winning effects to Npc greater than player.
        player.AddXP(0); // player gets 0 XP
        npc.AddXP(0); // npc gets 1 xp
        Debug.Log("NPC power level is greater than the player");
    } */

        // we probably want to compare our powerlevels...hope they aren't over 9000.
        // if someone wins...we probs wanna give some xp...but how much?
        // also if we want to show some winning effects 0 = draw, -1 = npc, 1 = player

    }

    #region No Modifications Required Section
    /// <summary>
    /// Is called at the begining of a fight, and sets the two characters to their dancing states.
    /// </summary>
    /// <param name="player"></param>
    /// <param name="npc"></param>
    public void BeginBattlePhase(Stats player, Stats npc)
    {
        player.animController.Dance();
        npc.animController.Dance();
    }

    /// <summary>
    /// Takes in the player and npc stat scripts called at the end of the fight and sets the dancers states to either win, or lose state.
    /// 1 = player wins
    /// 0 = draw
    /// -1 = npc has won
    /// </summary>
    /// <param name="Player"></param>
    /// <param name="NPC"></param>
    /// <param name="outcome"></param>
    public void SetWinningEffects(Stats Player, Stats NPC, float BattleResult)
    {
        Player.animController.BattleResult(BattleResult);
        // give the npc the opposite of what ever the result is.
        NPC.animController.BattleResult(BattleResult * -1);
        // Play the appropriate sfx depending who won.
        sfxHandler.BattleResult(BattleResult);
    }
    #endregion
}
