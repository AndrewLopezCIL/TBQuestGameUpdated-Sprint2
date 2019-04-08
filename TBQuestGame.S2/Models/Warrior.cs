using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBQuestGame.PresentationLayer;
namespace TBQuestGame.Models
{
    public class Warrior : Enemy
    { 
        private int _level = 35;
        private string _imageString;
        private int health = 125;
        
        public string Image
        {
            get { return _imageString; }
            set { _imageString = value; }
        }
       
        public Warrior()
        {

        }
        public Warrior(bool isBoss, GameSessionViewModel _gameSessionViewModel, GameSessionView GSV) : base(_gameSessionViewModel, GSV)
        {
            
            this.Health = health;
            this.Level = _level;
            this.IsAlive = true;
            this.MaxHealth = 125;
            Random ran = new Random();
            this.GoldDrop = ran.Next(10,19);
            this._imageString = "warrior-icon.png";
            Random ranXPDrop = new Random();

            this.XPDrop = ranXPDrop.Next(20,35);
            this.Name = "Warrior";
            this.BaseAttack = this.BaseAttack += (this.Level / 100) + .75; 
            _gameSessionViewModel.CurrentEnemyID += 1;
            this.ID = _gameSessionViewModel.CurrentEnemyID;
            //
            // if passed isBoss bool value is true, then set the property to true, otherwise set the property to false
            //
            isBoss = true ? IsBoss = isBoss : IsBoss = isBoss;
        }
    }
}
