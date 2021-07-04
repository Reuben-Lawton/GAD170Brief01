using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This handles all the battle logic of who is determined to be the winner of a fight between
/// two characters.
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
    #region SimulateBattle
    public float SimulateBattle(Stats MyStats, Stats Opponent)
    {
        float myPoints = MyStats.ReturnDancePowerLevel(); // our current powerlevel
        float opponentPoints = Opponent.ReturnDancePowerLevel(); // our opponents current power level
        float newNormalisedValue = 0.00f;

        if (myPoints <= 0 || opponentPoints <= 0) // if no points, mean no battle working.. 
        {
            Debug.LogWarning(" Simulate battle called; but Player or NPC battle points is 0, most likely the logic has not be setup for this yet");
        }
        else if (myPoints == opponentPoints) // if points are the same then do ummm perhaps your versing your clone 
        {
            Debug.Log("Somehow our points are the same,....hhmmm draw ?");
        }
        else if (myPoints < opponentPoints)  // if opponent got more points then use this normalised value 
        {
            newNormalisedValue = (myPoints / opponentPoints);
            Debug.Log("Opponent has higher points than player. Opponents points currently at : " + opponentPoints + ". Player only has : " + myPoints + " and a normalised value of : " + (float)newNormalisedValue);
        }
        else if (myPoints > opponentPoints) // if player got more points then use this normalised value
        {
            newNormalisedValue = (opponentPoints / myPoints);
            Debug.Log("Player has higher points than opponent. Player points currently at : " + myPoints + ". Opponent only has: " + opponentPoints + " and a normalised value of : " + (float)newNormalisedValue);
        }
       
          return newNormalisedValue;
    }

    #endregion
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
        #region xp generated from battle;   
        if (playerPowerLevel == npcPowerLevel)
    {               
            SetWinningEffects(player, npc, 0); // setting the winning effects to a draw.
            player.AddXP(0); // player gets no xp
            npc.AddXP(0); // npc gets no xp either
        {
            Debug.Log("Draw yo, need to git gud");
        }
    }
    else if (playerPowerLevel > npcPowerLevel)
    {
            npc.AddXP(0); // npc gets no xp
        
            if ((playerPowerLevel - npcPowerLevel) <= 5) 
            {
                player.AddXP(5); // if player power remaining is 5 or less then award 5 xp
                SetWinningEffects(player, npc, 1); // setting the winning effects to player greater than NPC.

                Debug.Log("Player power level is 5 or less, more then opponent, awarding 5XP");
            }
            else if ((playerPowerLevel - npcPowerLevel) > 5 && (playerPowerLevel - npcPowerLevel) <= 10)
            {
                player.AddXP(10); // if player power remaining is 6 to 10 then award 10 xp
                SetWinningEffects(player, npc, 1); // setting the winning effects to player greater than NPC.

                Debug.Log("Player power level is greater then 5 and less then or equal to 10, more then opponent, awarding 10 XP.");
            }
            else if ((playerPowerLevel - npcPowerLevel) > 10 && (playerPowerLevel - npcPowerLevel) <= 20)
            {
                player.AddXP(15); // if player power remaining is 11 to 20 then award 15 xp
                SetWinningEffects(player, npc, 1); // setting the winning effects to player greater than NPC.

                Debug.Log("Player power level is greater then 10 and less then or equal to 20, more then opponent, awarding 15 XP.");
            }
            else if ((playerPowerLevel - npcPowerLevel) > 20 && (playerPowerLevel - npcPowerLevel) <= 84)
            {
                player.AddXP(20); // if player power remaining is over 20 then award a max 20 xp
                SetWinningEffects(player, npc, 1); // setting the winning effects to player greater than NPC.

                Debug.Log("Player power level is greater then 20 and less then or equal to 84, more then opponent, awarding 20 XP.");
            }
    }
    else if (playerPowerLevel < npcPowerLevel)
    {
            player.AddXP(0); // npc gets no xp

            if ((npcPowerLevel - playerPowerLevel) <= 5)
            {
                npc.AddXP(5); // if npc power remaining is 5 or less then award 5 xp
                SetWinningEffects(player, npc, -1); // setting the winning effects to player greater than NPC.
                Debug.Log("Player power level is 5 or less, more then opponent, awarding 5XP");
            }
            else if ((npcPowerLevel - playerPowerLevel) > 5 && (npcPowerLevel - playerPowerLevel) <= 10)
            {
                npc.AddXP(10); // if npc power remaining is 6 to 10 then award 10 xp
                SetWinningEffects(player, npc, -1); // setting the winning effects to player greater than NPC.
                Debug.Log("Player power level is greater then 5 and less then or equal to 10, more then opponent, awarding 10 XP.");
            }
            else if ((npcPowerLevel - playerPowerLevel) > 10 && (npcPowerLevel - playerPowerLevel) <= 20)
            {
                npc.AddXP(15); // if npc power remaining is 11 to 20 then award 15 xp
                SetWinningEffects(player, npc, -1); // setting the winning effects to player greater than NPC.
                Debug.Log("Player power level is greater then 10 and less then or equal to 20, more then opponent, awarding 15 XP.");
            }
            else if ((npcPowerLevel - playerPowerLevel) > 20 && (npcPowerLevel - playerPowerLevel) <= 84)
            {
                player.AddXP(20); // if npc power remaining is over 20 then award a max 20 xp
                SetWinningEffects(player, npc, -1); // setting the winning effects to player greater than NPC.
                Debug.Log("Player power level is greater then 20 and less then or equal to 84, more then opponent, awarding 20 XP.");
            }
    }

        #endregion

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
