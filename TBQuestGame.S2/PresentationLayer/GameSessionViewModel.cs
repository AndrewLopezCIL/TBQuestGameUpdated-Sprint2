﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBQuestGame.Models;
using TBQuestGame.PresentationLayer;
using TBQuestGame.DataLayer;
namespace TBQuestGame.PresentationLayer
{
    public class GameSessionViewModel : ObservableObject
    {
        #region FIELDS
        // player value for Player property
        private Player _player;

        // player health value for PlayerHealth property
        private double _playerHealth;

        // player shield value for PlayerShield property
        private double _playerShield;

        // locationWarningImages value for LocationWarningImage property
        private string _locationWarningImages;

        // gameMap value for GameMap property
        private Map _gameMap;

        // current location value for CurrentLocation property
        private Location _currentLocation;

        // multiAttackLocation value for MultiAttackLocation property
        private bool _multiAttackLocation;

        // GameData object being instantiated
        GameData _gameData = new GameData();

        // currentEnemyID value for CurrentEnemyID property
        private int currentEnemyID;

        // currentLocationName value for CurrentLocationName property
        private string _currentLocationName;

        // accessibleLocations value for AccessibleLocations property
        public ObservableCollection<Location> _accessibleLocations;

        // currentEnemies list for CurrentEnemies property
        public ObservableCollection<Enemy> _currentEnemies;

        // Current enemy the player's fighting's ID
        private int _currentFightingEnemyID;
        // Current enemy fighting's listplacement ID
        private int _currentFightingEnemyListPlacement;


        // To Display Player's Name
        private string _playerName;
        // To Display Player's Level
        private int _playerLevel;
        // To Display Player's Gold
        private int _playerGold;
        // To Display Player's XP
        private double _playerXP;
        // To Display Player's Base Attack Damage
        private double _playerBaseAttack;
        // To Display Player's Skill One Damage
        private double _playerSkillOne;
        // To Display Player's Skill Two Damage
        private double _playerSkillTwo;
        // To Display Player's Skill Three Damage
        private double _playerSkillThree;
        // To Display Player's Third Eye Attack Damage
        private double _playerThirdEye;
        private string _playerClassToString;
        public string PlayerClassString
        {
            get { return _playerClassToString; }
            set { Player.ClassToString = value; OnPropertyChanged(nameof(PlayerClassString)); }
        }

        
        public int PlayerLevel
        {
            get { return Player.Level; }
            set { Player.Level = value; OnPropertyChanged(nameof(PlayerLevel)); }
        }
        public int PlayerGold
        {
            get { return Player.Gold; }
            set { Player.Gold = value; OnPropertyChanged(nameof(PlayerGold)); }
        }
        public double PlayerXP
        {
            get { return Player.XP; }
            set { Player.XP = value; OnPropertyChanged(nameof(PlayerXP)); }
        }
        public double PlayerBaseAttack
        {
            get { return _playerBaseAttack; }
            set { _playerBaseAttack = value; OnPropertyChanged(nameof(PlayerBaseAttack)); }
        }
        public double PlayerSkillOne
        {
            get { return _playerSkillOne; }
            set { _playerSkillOne = value; OnPropertyChanged(nameof(PlayerSkillOne)); }
        }
        public double PlayerSkillTwo
        {
            get { return _playerSkillTwo; }
            set { _playerSkillTwo = value; OnPropertyChanged(nameof(PlayerXP)); }
        }
        public double PlayerSkillThree
        {
            get { return _playerSkillThree; }
            set { _playerSkillThree = value; OnPropertyChanged(nameof(PlayerXP)); }
        }
        public double PlayerThirdEye
        {
            get { return _playerThirdEye; }
            set { _playerThirdEye = value; OnPropertyChanged(nameof(PlayerXP)); }
        }
        public string Name
        {
            get { return Player.Name; }
            set { Player.Name = value; OnPropertyChanged(nameof(Name)); }
        }
        public double MaxPlayerXP
        {
            get { return Player.MaxLevelXPRange; }
            set { _maxPlayerXP = value; OnPropertyChanged(nameof(MaxPlayerXP)); }
        }
        public double MinPlayerXP
        {
            get { return Player.MinLevelXPRange; }
            set { _minPlayerXP = value; OnPropertyChanged(nameof(MinPlayerXP)); }
        }
        

        //
        // CurrentFightingEnemyID property
        // used for getting the current selected enemy's ID
        // set when player selects enemies from the list
        //
        public int CurrentFightingEnemyID
        {
            get { return _currentFightingEnemyID; }
            set { _currentFightingEnemyID = value; OnPropertyChanged(nameof(CurrentFightingEnemyID)); }
        }
        public int CurrentFightingEnemyListPlacement
        {
            get { return _currentFightingEnemyListPlacement; }
            set { _currentFightingEnemyListPlacement = value; OnPropertyChanged(nameof(CurrentFightingEnemyListPlacement)); }
        }
        //
        // used to stop a reoccuring boss battle when user goes back to location
        //
        public List<Location> bossesDefeated = new List<Location>();
        private int _missionLength = 0;
        //
        // List of current enemies battling, this is binded and used to display the list of enemies in the
        // ActiveEnemies ListBox controls
        //
        public ObservableCollection<Enemy> CurrentEnemies
        {
            get { return _currentEnemies; }
            set { _currentEnemies = value;  OnPropertyChanged(nameof(CurrentEnemies)); }
        }
        private Enemy _selectingEnemy;
        public Enemy SelectingEnemy
        {
            get { return _selectingEnemy; }
            set { _selectingEnemy = value; OnPropertyChanged(nameof(SelectingEnemy)); }
        }
        //
        // Going to be removed in the future
        //
        public int MissionLength
        {
            get { return _missionLength; }
            set { _missionLength = value; }
        }
        //
        // Current Enemy Stats
        //
        private string _enemyName;
        public string EnemyName
        {
            get { return _enemyName; }
            set { Player.currentlyAttacking.Name = value; _enemyName = value;  OnPropertyChanged(nameof(EnemyName)); }
        }
        private double _enemyHealth;
        public double EnemyHealth
        {
            get { return _enemyHealth; }
            set{ Player.currentlyAttacking.Health = value; _enemyHealth = value; OnPropertyChanged(nameof(EnemyHealth)); }
        }
        private double _enemyDamage;
        public double EnemyDamage
        {
            get { return _enemyDamage; }
            set { Player.currentlyAttacking.BaseAttack = value; _enemyDamage = value;  OnPropertyChanged(nameof(EnemyDamage)); }
        }
        private int _enemyLevel;
        public int EnemyLevel
        {
            get { return _enemyLevel; }
            set { Player.currentlyAttacking.Level = value; _enemyLevel = value; OnPropertyChanged(nameof(EnemyLevel)); }
        }

        //
        // Gets the current enemy's ID 
        // This is used to track which enemies are which, increments everytime an enemy is instantiated
        // Going to be used to track which enemy in the list gets hurt when they're selected in ActiveEnemies control
        //
        public int CurrentEnemyID
        {
            get { return currentEnemyID; }
            set { currentEnemyID = value; OnPropertyChanged(nameof(CurrentEnemyID)); }
        }
        //
        // AccessibleLocations is used to store all of the locations that the player can access
        //
        public ObservableCollection<Location> AccessibleLocations
        {
            get { return _accessibleLocations; }
            set { _accessibleLocations = value; OnPropertyChanged(nameof(AccessibleLocations)); }
        }
        //
        // Returns the current Location's name
        //
        public string CurrentLocationName
        {
            get { return _currentLocationName; }
            set { _currentLocationName = value; OnPropertyChanged(nameof(CurrentLocationName)); }
        }
        //
        // Returns the player's current Location
        //
        public Location CurrentLocation
        {
            get { return _currentLocation; }
            set { _currentLocation = value; OnPropertyChanged(nameof(CurrentLocation)); }
        }
        //
        // Returns messages for the current Location
        //
        private List<string> _messages;
        #endregion

        #region PROPERTIES
        //
        // Gets / Sets the current GameMap
        //
        public Map GameMap
        {
            get { return _gameMap; }
            set { _gameMap = value; }
        }
        //
        // Gets / Set the current player
        //
        public Player Player
        {
            get { return _player; }
            set { _player = value; }
        } 
        //
        // Used to determine which enemy is currently selected on the ListBox ActiveEnemies
        //
        private bool _enemySelected;
        private double _maxPlayerXP;
        private double _minPlayerXP;

        public bool EnemySelected
        {
            get { return _enemySelected; }
            set { _enemySelected = value; OnPropertyChanged(nameof(EnemySelected)); }
        }

        //
        // Return the list of strings converted to a single string
        // with new lines between each message
        //
        public string Messages
        {
            get { return string.Join("\n\n",_messages); }
        }
        //
        // Gets / Sets the player's health
        //
        public double PlayerHealth
        {
            get { return _playerHealth; }
            set { _playerHealth = value; OnPropertyChanged(nameof(PlayerHealth)); }
        }
        //
        // Gets / Sets the player's shield
        //
        public double PlayerShield
        {
            get { return _playerShield; }
            set { _playerShield = value; OnPropertyChanged(nameof(PlayerShield)); }
        }

        #endregion

        #region METHODS 
        public void SelectedEnemySetter(int selected, GameSessionView gsv)
        {
            EnemySelected = true;  
            foreach (Enemy E in CurrentEnemies)
            {

                E.refreshAllEnemiesPositions();
                if (E.listPlacement == selected)
                { 
                        CurrentFightingEnemyID = E.ID;
                        CurrentFightingEnemyListPlacement = E.listPlacement;
                        Player.currentlyAttacking = E;
                        SelectingEnemy = E;
                        if (E.SelectedToFight == false)
                        {
                            E.AttackingPlayer = true;
                            E.startAttackingPlayer();
                            gsv.EnemyPicture.Source = E.PictureSource;
                            E.SelectedToFight = true;
                        EnemyDamage = E.BaseAttack;
                        EnemyName = E.Name;
                        EnemyHealth = E.Health;
                        EnemyLevel = E.Level;
                        gsv.EnemyHealthDisplay.Visibility = System.Windows.Visibility.Visible;
                        gsv.EnemyHealthDisplay.Maximum = E.MaxHealth;
                        gsv.EnemyHealthDisplay.Value = E.Health;
                        gsv.EnemyHealthDisplay.Minimum = 0;
                        }
                        else
                        {
                            E.SelectedToFight = true;
                        }
                 }
                else if (E.listPlacement != selected)
                {
                    if (E.listPlacement != selected && E.SelectedToFight == true)
                    {
                    E.SelectedToFight = false;
                        E.AttackingPlayer = false;
                        E.stopAttackingPlayer();
                    }
                    else
                    {
                        E.SelectedToFight = false;
                    } 
                }
            } 
        }
        #endregion

        #region CONSTRUCTORS
        public GameSessionViewModel()
        {

        }
        //
        // Constructor used for setting the current properties values
        //
        public GameSessionViewModel(Player player, List<string>initialMessages, Map gameMap, GameMapCoordinates currentLocationCoordinates, ObservableCollection<Enemy> currentEnemies)
        {
            // sets the player's shield
            _playerShield = player.Shield;
             
            // sets the player's health
            _playerHealth = player.Health;
             
            // Player's XP
            _playerXP = player.XP;

            // Max Player XP
            _maxPlayerXP = player.MaxLevelXPRange;
          
            // Player's Level
            _playerLevel = player.Level;

            // Player's Gold
            _playerGold = player.Gold;

            // Player's Base Attack
            _playerBaseAttack = player.BasicAttack;

            // Player's Skill One
            _playerSkillOne = player.SkillOneAttack;

            // Player's Skill Two
            _playerSkillTwo = player.SkillTwoAttack;

            // Player's Skill Three
            _playerSkillThree = player.SkillThreeAttack;

            // Player's Third Eye
            _playerThirdEye = player.ThirdEyeAttack;

            // Setting player class string
            //_playerClassToString =  Player.ClassTypeProp.ToString();

            if (player.currentlyAttacking != null) {
                // EnemyHealth
                _enemyHealth = player.currentlyAttacking.Health;
                // EnemyName
                _enemyName = player.currentlyAttacking.Name;
                // EnemyDamage
                _enemyDamage = player.currentlyAttacking.BaseAttack;
                // EnemyLevel
                _enemyLevel = player.currentlyAttacking.Level;
            }
            else
            {
                // EnemyHealth
                _enemyHealth = 0;
                // EnemyName
                _enemyName = "Currently Not Fighting";
                // EnemyDamage
                _enemyDamage = 0;
                // EnemyLevel
                _enemyLevel = 0;
            }
            // gets currentEnemyID from gameData, and sets the currentEnemyID variable in the view model to equal that
            currentEnemyID = _gameData.currentEnemyID;
            // gets the current enemies list that is passed and sets _currentEnemies in the view model to equal that
            _currentEnemies = currentEnemies;
            // sets the player
            _player = player;
            // sets the messages property
            _messages = initialMessages;
            // sets the current gameMap
            _gameMap = gameMap;
            // sets the player's currentLocationCoordinates in the view model
            _gameMap.CurrentLocationCoordinates = currentLocationCoordinates;
            // sets the currentLocation to equal the gameMap's passed currentLocation
            _currentLocation = _gameMap.CurrentLocation;
            // sets the current locationWarningImage to the gameMap's currentLocation's warning Image
            _locationWarningImages = _gameMap.CurrentLocation.LocationWarningImage;
        }
        
        #endregion

    }
}
