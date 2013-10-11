using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using PewPew2.Entity.SpriteTypes;

namespace PewPew2.Entity.Sprites
{
    public class BlueAlienBaseSprite : MotionSprite
    {
        public BlueAlienBaseSprite(PewPew2Game game) 
            : base(game)
        {

        }


        public override void Initialize()
        {
            Size = new Vector2(7, 13);

            Animator = new Animator(this, 5);

            PhysicsBody = BodyFactory.CreateCapsule(Game.PhysicsWorld, Size.Y, 3f, 1f);

            
        }
    }
}
