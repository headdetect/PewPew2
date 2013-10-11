using Microsoft.Xna.Framework;

namespace PewPew2.Entity.SpriteTypes
{
    public class AnimatedSprite : Entity
    {
        public Animator Animator { get; set; }

        public AnimatedSprite(PewPew2Game game, Animator animator) 
            : base(game)
        {
            Animator = animator;
        }

        public AnimatedSprite(PewPew2Game game)
            : base(game)
        {

        }

        public override void Draw(GameTime gameTime)
        {
            Animator.Draw(gameTime);
            base.Draw(gameTime);
        }

    }
}
