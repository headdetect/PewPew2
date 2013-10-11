using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PewPew2.Entity
{
    public class Animator
    {
        private readonly Dictionary<string, Animation> _animations;

        private readonly Entity _entity;


        private float _time;

        private int _frameIndex;

        /// <summary>
        /// Gets a texture origin at the bottom center of each frame.
        /// </summary>
        public Vector2 Origin
        {
            get { return new Vector2(_active.FrameSize.X / 2.0f, _active.FrameSize.Y); }
        }

        /// <summary>
        /// Gets or sets the active animation. Which ever animation is set, will be the animation currently animating.
        /// </summary>
        public string ActiveAnimation
        {
            get
            {
                var animation = _animations.FirstOrDefault(pair => pair.Value.Equals(_active));
                return animation.Equals(null) ? null : animation.Key;
            }
            set
            {
                _active = _animations[value];

                // Start the new animation.
                _frameIndex = 0;
                _time = 0.0f;
            }
        }
        private Animation _active;

        /// <summary>
        /// Creates a new Entity animator
        /// </summary>
        /// <param name="entity">Entity to attach to</param>
        /// <param name="numOfAnimations">Number of total animations that there can be at any given time. (The lower the better)</param>
        public Animator(Entity entity, int numOfAnimations)
        {
            _entity = entity;
            _animations = new Dictionary<string, Animation>(numOfAnimations);
        }

        /// <summary>
        /// Creates a new Entity animator
        /// </summary>
        /// <param name="numOfAnimations">Number of total animations that there can be at any given time. (The lower the better)</param>
        public Animator(int numOfAnimations)
        {
            _animations = new Dictionary<string, Animation>(numOfAnimations);
        }

        /// <summary>
        /// Creates a new Entity animator
        /// </summary>
        public Animator()
        {
            _animations = new Dictionary<string, Animation>(5);
        }

        public void Draw(GameTime gameTime)
        {
            // Process passing time.
            _time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            while (_time > _active.FrameTime)
            {
                _time -= _active.FrameTime;

                // Advance the frame index; looping or clamping as appropriate.
                if (_active.IsLooping)
                {
                    _frameIndex = (_frameIndex + 1) % _active.FrameCount;
                }
                else
                {
                    _frameIndex = Math.Min(_frameIndex + 1, _active.FrameCount - 1);
                }
            }

            // Calculate the source rectangle of the current frame.
            Rectangle source = new Rectangle(_frameIndex * (int)_active.FrameSize.X, 0, (int)_active.FrameSize.Y, _active.Texture.Height);

            // Draw the current frame.
            if (!_active.Texture.IsDisposed)
                _entity.PewPew2Game.SpriteBatch.Draw(_active.Texture, _entity.Position, source, _entity.Color, _entity.Rotation, Vector2.Zero, _entity.Scale, _entity.Effects, 1f);
        }

        /// <summary>
        /// Sets the current animation. It does the same thing as `animator.ActiveAnimation = "myAnimation";`
        /// </summary>
        /// <param name="animation">The animation to set</param>
        public void PlayAnimation(string animation)
        {
            ActiveAnimation = animation;
        }


        /// <summary>
        /// Creates and store the specified animation
        /// </summary>
        /// <param name="animationName">Name of the animation.</param>
        /// <param name="texture">Texture sheet of the whole animation</param>
        /// <param name="timePerFrame">time each frame will be shown. (In milliseconds)</param>
        /// <param name="animationSize">width and height of the animation. width = x, height = y</param>
        /// <param name="loop">True if the animation will loop</param>
        public void CreateAnimation(
            string animationName,
            Texture2D texture,
            float timePerFrame,
            Vector2 animationSize,
            bool loop)
        {
            if (_animations.ContainsKey(animationName))
                throw new ArgumentException("Animation already exists");

            Animation animation = new Animation(texture, timePerFrame, loop, animationSize);

            _animations.Add(animationName, animation);
            ActiveAnimation = animationName;
        }


        /// <summary>
        /// Removes the specified animation
        /// </summary>
        /// <param name="animationName">Name of the animation to remove</param>
        /// <returns>True if the value was successfully removed; false otherwise.</returns>
        public bool RemoveAnimation(string animationName)
        {
            if (_animations.ContainsKey(animationName))
                throw new ArgumentException("Animation already exists");

            return _animations.Remove(animationName);
        }

        /// <summary>
        /// Returns true if animation exists; false otherwise.
        /// </summary>
        /// <param name="animationName">The name of the animation to look for</param>
        /// <returns>Returns true if animation exists; false otherwise.</returns>
        public bool AnimationExists(string animationName)
        {
            return _animations.ContainsKey(animationName);
        }
    }

    /// <summary>
    /// Animation instances.
    /// </summary>
    class Animation
    {
        /// <summary>
        /// All frames in the animation arranged horizontally.
        /// </summary>
        public Texture2D Texture
        {
            get { return _texture; }
        }

        readonly Texture2D _texture;

        /// <summary>
        /// Duration of time to show each frame.
        /// </summary>
        public float FrameTime
        {
            get { return _frameTime; }
        }

        readonly float _frameTime;

        /// <summary>
        /// When the end of the animation is reached, should it
        /// continue playing from the beginning?
        /// </summary>
        public bool IsLooping
        {
            get { return _isLooping; }
        }

        readonly bool _isLooping;

        /// <summary>
        /// Gets the number of frames in the animation.
        /// </summary>
        public int FrameCount
        {
            get { return (int)(Texture.Width / FrameSize.X) * (int)(Texture.Height / FrameSize.Y); }
        }

        /// <summary>
        /// Gets the width of a frame in the animation.
        /// </summary>
        public Vector2 FrameSize
        {
            get { return _frameSize; }
        }
        readonly Vector2 _frameSize;


        public Animation(Texture2D texture, float frameTime, bool isLooping, Vector2 frameSize)
        {
            _texture = texture;
            _frameTime = frameTime;
            _isLooping = isLooping;
            _frameSize = frameSize;
        }
    }
}
