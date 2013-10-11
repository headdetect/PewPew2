using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PewPew2.Entity
{
    public abstract class Entity : DrawableGameComponent
    {
        
        internal PewPew2Game PewPew2Game { get; set; }

        public Vector2 Position { get; set; }

        public Vector2 Size { get; set; }

        public float Scale { get; set; }

        public float Rotation { get; set; }

        public Color Color { get; set; }

        public SpriteEffects Effects { get; set; }


        private PewPew2Game _gameInstance;

        public new PewPew2Game Game
        {
            get
            {
                if (_gameInstance == null)
                    _gameInstance = base.Game as PewPew2Game;

                if(_gameInstance == null) throw new NullReferenceException("Game is null");

                return _gameInstance;
            }

            set
            {
                if(value == null) throw new ArgumentNullException("value", "value cannot be null");
                _gameInstance = value;
            }
        }

        protected Entity(PewPew2Game game)
            : base(game)
        {
            
            PewPew2Game = game;

            // Defaults //
            Position = Vector2.Zero;
            Size = Vector2.Zero;
            Scale = 1f;
            Rotation = 1f;
            Color = Color.White;
            Effects = SpriteEffects.None;
        }

    }
}
