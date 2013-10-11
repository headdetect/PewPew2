using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace PewPew2.Entity
{
    public class Sprite : Entity
    {
        private static readonly Dictionary<string, Texture2D> Cache = new Dictionary<string, Texture2D>();

        private readonly string _textureLocation;
        public Texture2D Texture { get; set; }

        public Sprite(PewPew2Game game, string texture) : base(game)
        {
            _textureLocation = texture;
        }

        public override void Initialize()
        {
            if (Cache.ContainsKey(_textureLocation))
                Texture = Cache[_textureLocation];
            else
            {
                Texture = PewPew2Game.Content.Load<Texture2D>(_textureLocation);
                Cache.Add(_textureLocation, Texture);
            }
            base.Initialize();
        }
    }
}
