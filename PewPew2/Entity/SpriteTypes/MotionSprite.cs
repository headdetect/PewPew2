using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics.Dynamics;

namespace PewPew2.Entity.SpriteTypes
{
    public abstract class MotionSprite : AnimatedSprite
    {
        public Body PhysicsBody { get; set; }

        protected MotionSprite(PewPew2Game game) 
            : base(game)
        {

        }
    }
}
