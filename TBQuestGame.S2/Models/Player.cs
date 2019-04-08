﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBQuestGame.PresentationLayer;

namespace TBQuestGame.Models
{
    public class Player : Character
    {
        public enum AttackType {
            BasicAttack, SkillOneAttack, SkillTwoAttack, SkillThreeAttack, ThirdEyeAttack
        }
        public enum ClassType
        {
            Warrior, Archer, Mage
        }
        public enum PlayerState
        {
            Fighting, Neutral
        }
        #region FIELDS 
        //private double _basicAttack = 3.2;
        private double _basicAttack = 50.0;
        private double _skillOneAttack;
        private double _skillTwoAttack;
        private double _skillThreeAttack;
        private double _thirdEyeAttack;
        private int _playerLevel;
        private double _playerXP;
        private int _gold;
        private AttackType attackType;
        private ClassType classType;
        private PlayerState _playerState;
        private Enemy _currentlyAttacking;
        private double _maxLevelXPRange = 250.5;
        private double _minLevelXPRange;
        // May remove quest points in the future
        private int _questPoints;

        #endregion

        #region PROPERTIES
        public double MaxLevelXPRange
        {
            get { return Level * _maxLevelXPRange; } 
        }
        public double MinLevelXPRange
        {
            get { return _minLevelXPRange; }
            set { _minLevelXPRange = value; }
        }
         
        public PlayerState PlayersCurrentState
        {
            get { return _playerState;  }
            set { _playerState = value; }
        }

        public AttackType AttackTypeProp
        {
            get { return attackType; }
            set { attackType = value;  }
        }
        public ClassType ClassTypeProp
        {
            get { return classType; }
            set { classType = value; }
        }
        private string _classToString;
        public string ClassToString
        {
            get { return _classToString; }
            set { _classToString = ClassTypeProp.ToString(); }
        }

        public int Level
        {
            get { return _playerLevel; }
            set { _playerLevel = value; }
        }

        public int QuestPoints
        {
            get { return _questPoints; }
            set { _questPoints = value; }
        }
        public int Gold
        {
            get { return _gold; }
            set { _gold = value; }
        }
        public double XP
        {
            get { return _playerXP; }
            set { _playerXP = value; }
        }
         
        public double BasicAttack
        {
            get { return _basicAttack; }
            set { _basicAttack = value; }
        }
        public double SkillOneAttack
        {
            get { return _skillOneAttack; }
            set { _skillOneAttack = value; }
        }
        public double SkillTwoAttack
        {
            get { return _skillTwoAttack; }
            set { _skillTwoAttack = value; }
        }
        public double SkillThreeAttack
        {
            get { return _skillThreeAttack; }
            set { _skillThreeAttack = value; }
        }
        public double ThirdEyeAttack
        {
            get { return _thirdEyeAttack; }
            set { _thirdEyeAttack = value; }
        }
        public Enemy currentlyAttacking
        {
            get{ return _currentlyAttacking; }
            set { _currentlyAttacking = value; }
        }

        

        #endregion

        #region METHODS 
        public override string GetName()
            {
                return base.GetName();
            }
        
            public override bool Alive()
            {
                return IsAlive ? true : false;
            }
        public void setClassTypeSkillDamage()
        {
            switch (classType)
            {
                case ClassType.Warrior:
                    //SkillOneAttack = 
                    //SkillTwoAttack =
                    //SkillThreeAttack = 
                    break;
                case ClassType.Archer:
                    //SkillOneAttack = 
                    //SkillTwoAttack =
                    //SkillThreeAttack = 
                    break;
                case ClassType.Mage:
                    //SkillOneAttack = 
                    //SkillTwoAttack =
                    //SkillThreeAttack = 
                    break;
                default:
                    break;
            }
        }
        public void playerLevelUp(GameSessionViewModel gsm, GameSessionView gsv)
        {
            gsm.PlayerLevel += 1; 
            gsm.MinPlayerXP = gsm.Player.XP;
            gsv.playerStatsWindow.PlayerXPBar.Value = gsm.PlayerXP;
            gsv.playerStatsWindow.PlayerXPBar.Minimum = gsm.PlayerXP;
            
            gsv.playerStatsWindow.PlayerXPBar.Maximum = gsm.MaxPlayerXP;
            
        }
        #endregion

        #region CONSTRUCTORS 

        public void AttackEnemy(GameSessionViewModel gsm, GameSessionView GSV, AttackType typeOfAttack)
        {
            // setting fightingEnemy to the enemy with position in currentEnemies of 
            // Send id of currentfightingenemy and set fightingenemy to currentenemieswiththat position
            // What if current fighting id is 15 and the list is only 4 big, then it would be out of bounds error
            // Need to look for enemy with a specific listPlacement 
            Enemy fightingEnemy = currentlyAttacking;
            attackType = typeOfAttack;
            //
            // ADD IN, IF NOT SELECTED THEN AUTOMATICALLY ATTACK FIRST ENEMY IN LIST
            //
            if (gsm.CurrentEnemies.Count > 0) {
                //If current enemy is alive/has more than 0 health
                if (PlayersCurrentState == PlayerState.Fighting) {
                    bool anEnemyHasSelection = false;
                    foreach (Enemy enemy in gsm.CurrentEnemies)
                    { 
                        if (enemy.SelectedToFight == true)
                        {
                            anEnemyHasSelection = true; break;
                        }
                        else if (enemy.SelectedToFight == false)
                        {
                            anEnemyHasSelection = false;
                        } 
                    }
                    if (anEnemyHasSelection == false)
                    {
                        gsm.Player.currentlyAttacking = gsm.CurrentEnemies[0];
                        gsm.CurrentEnemies[0].SelectedToFight = true;
                        gsm.CurrentEnemies[0].AttackingPlayer = true; 
                        gsm.CurrentEnemies[0].startAttackingPlayer();
                        gsm.CurrentEnemyID = gsm.CurrentEnemies[0].ID;
                        gsm.CurrentFightingEnemyListPlacement = gsm.CurrentEnemies[0].listPlacement;
                        fightingEnemy = gsm.CurrentEnemies[0];
                        GSV.EnemyHealthDisplay.Visibility = System.Windows.Visibility.Visible;
                        gsm.EnemyDamage = fightingEnemy.BaseAttack;
                        gsm.EnemyHealth = fightingEnemy.Health;
                        gsm.EnemyLevel = fightingEnemy.Level;
                        gsm.EnemyName = fightingEnemy.Name;
                    }
                    if (fightingEnemy.IsAlive == true)
            {
                switch (attackType)
                {
                    case AttackType.BasicAttack:
                        GSV.EnemyHealthDisplay.Visibility = System.Windows.Visibility.Visible;
                        fightingEnemy.Health -= BasicAttack;
                        GSV.EnemyHealthDisplay.Value = fightingEnemy.Health;
                        GSV.DialogueBox.Text = fightingEnemy.Health.ToString();
                        gsm.EnemyDamage = fightingEnemy.BaseAttack;
                        gsm.EnemyHealth = fightingEnemy.Health;
                        gsm.EnemyLevel = fightingEnemy.Level;
                        gsm.EnemyName = fightingEnemy.Name;
                    if (fightingEnemy.Health <= 0)
                        {
                            fightingEnemy.Health = 0;
                            fightingEnemy.Alive(GSV, gsm, fightingEnemy);
                            fightingEnemy.stopAttackingPlayer();
                            GSV.DialogueBox.Text = fightingEnemy.Health.ToString();
                            fightingEnemy.onDeathRewardPlayer(gsm, fightingEnemy);
                            GSV.EnemyHealthDisplay.Visibility = System.Windows.Visibility.Hidden;
                            
                            if (gsm.PlayerXP >= gsm.MaxPlayerXP)
                            {
                                playerLevelUp(gsm,GSV);
                            }
                            if (gsm.CurrentEnemies.Count == 0)
                            {
                              Location.enableControls(GSV);
                                        gsm.EnemyDamage = 0;
                                        gsm.EnemyHealth = 0;
                                        gsm.EnemyName = "Currently Not Fighting";
                                        gsm.EnemyLevel = 0;
                            }
                        }
                        break;
                    case AttackType.SkillOneAttack:
                        fightingEnemy.Health -= SkillOneAttack;
                        if (fightingEnemy.Health <= 0)
                        {
                            fightingEnemy.Health = 0;
                            fightingEnemy.Alive(GSV, gsm, fightingEnemy);
                        }
                        break;
                    case AttackType.SkillTwoAttack:
                        fightingEnemy.Health -= SkillTwoAttack;
                        if (fightingEnemy.Health <= 0)
                        {
                            fightingEnemy.Health = 0;
                            fightingEnemy.Alive(GSV, gsm, fightingEnemy);
                        }
                        break;
                    case AttackType.SkillThreeAttack:
                        fightingEnemy.Health -= SkillThreeAttack;
                        if (fightingEnemy.Health <= 0)
                        {
                            fightingEnemy.Health = 0;
                            fightingEnemy.Alive(GSV, gsm, fightingEnemy);
                        }
                        break;
                    case AttackType.ThirdEyeAttack:


                        if (fightingEnemy.IsAlive == true)
                        {
                            fightingEnemy.Health -= ThirdEyeAttack;
                        }
                        if (fightingEnemy.Health <= 0)
                        {
                            fightingEnemy.Health = 0;
                            fightingEnemy.Alive(GSV, gsm, fightingEnemy);
                        }
                        break;
                    default:
                        break;
                }

            }
        }
            
        }
        #endregion

        
    }
}
    }