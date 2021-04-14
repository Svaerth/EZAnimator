using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Svaerth.EZAnimator
{

    public partial class EZAnimator : MonoBehaviour
    {

        /// <summary>
        /// Whether or not an animation is currently playing
        /// </summary>
        public bool AnimationInProgress => CurrentAnimation != null;

        /// <summary>
        /// The animation that is currently playing. Null if no animation is in progress.
        /// </summary>
        public EZAnimation CurrentAnimation { get; private set; } = null;

        /// <summary>
        /// The sprite that is currently being shown in the current animation. Null if no animation is in progress
        /// </summary>
        public Sprite CurrentSprite { get; private set; } = null;

        /// <summary>
        /// The index of the currently shown frame of animation
        /// </summary>
        public int CurrentFrame => CurrentFrame_implementation;

        /// <summary>
        /// When the current animation began
        /// </summary>
        public DateTime BeginningOfAnimation { get; private set; }

        /// <summary>
        /// The amount of time that has elapsed since the current animation began
        /// </summary>
        public TimeSpan TimeSinceBeginning => DateTime.Now - BeginningOfAnimation;


        /// <summary>
        /// Play an array of animations one after the other
        /// </summary>
        /// <param name="animations">the animations to play</param>
        public void Play(EZAnimation[] animations)
        {
            StartCoroutine(PlayCoroutine(animations));
        }

        /// <summary>
        /// Play an array of animations one after the other
        /// </summary>
        /// <param name="animations">the animations to play</param>
        /// <returns>returns the IEnumerator of the animation coroutine</returns>
        public IEnumerator PlayCoroutine(EZAnimation[] animations)
        {
            return PlayCustomCoroutine_Implementation(animations);
        }

        /// <summary>
        /// Play a list of animations one after another
        /// </summary>
        /// <param name="animations">the animations to play</param>
        public void Play(List<EZAnimation> animations)
        {
            StartCoroutine(PlayCoroutine(animations));
        }

        /// <summary>
        /// Play a list of animations one after another
        /// </summary>
        /// <param name="animations">the animations to play</param>
        /// <returns>returns the IEnumerator of the animation coroutine</returns>
        public IEnumerator PlayCoroutine(List<EZAnimation> animations)
        {
            return PlayCoroutine(animations.ToArray());
        }

        /// <summary>
        /// Plays an animation
        /// </summary>
        /// <param name="animation">the animation to play</param>
        public void Play(EZAnimation animation)
        {
            StartCoroutine(PlayCoroutine(animation));
        }

        /// <summary>
        /// Plays an animation
        /// </summary>
        /// <param name="animation">the animation to play</param>
        /// <returns>returns the IEnumerator of the animation coroutine</returns>
        public IEnumerator PlayCoroutine(EZAnimation animation)
        {
            return PlayCoroutine(new EZAnimation[] { animation });
        }

        /// <summary>
        /// Play an animation. 
        /// </summary>
        /// <param name="sprites">The sprites that make up the animation</param>
        /// <param name="durationMilliseconds">The duration of the entire animation in Milliseconds</param>
        /// <param name="framesPerSecond">The speed of the animation measured in frames per second</param>
        /// <param name="looping">Whether or not the animation should loop</param>
        /// <param name="reversed">Whether or not to play the animation in reverse</param>
        /// <param name="startingFrame">the frame that the animation should start playing on</param>
        /// <param name="endingFrame">the frame that the animation should stop playing on</param>
        /// <remarks>either durationMilliseconds or framesPerSecond must be populated but not both</remarks>
        public void Play(List<Sprite> sprites,
                        int? durationMilliseconds = null,
                        int? framesPerSecond = null,
                        bool looping = false,
                        bool reversed = false,
                        int startingFrame = 0,
                        int? endingFrame = null)
        {
            StartCoroutine(PlayCoroutine(sprites, durationMilliseconds, framesPerSecond, looping, reversed, startingFrame, endingFrame));
        }

        /// <summary>
        /// Play an animation. 
        /// </summary>
        /// <param name="sprites">The sprites that make up the animation</param>
        /// <param name="durationMilliseconds">The duration of the entire animation in Milliseconds</param>
        /// <param name="framesPerSecond">The speed of the animation measured in frames per second</param>
        /// <param name="looping">Whether or not the animation should loop</param>
        /// <param name="reversed">Whether or not to play the animation in reverse</param>
        /// <param name="startingFrame">the frame that the animation should start playing on</param>
        /// <param name="endingFrame">the frame that the animation should stop playing on</param>
        /// <remarks>either durationMilliseconds or framesPerSecond must be populated but not both</remarks>
        /// <returns>returns the IEnumerator of the animation coroutine</returns>
        public IEnumerator PlayCoroutine(List<Sprite> sprites,
                                         int? durationMilliseconds = null,
                                         int? framesPerSecond = null,
                                         bool looping = false,
                                         bool reversed = false,
                                         int startingFrame = 0,
                                         int? endingFrame = null)
        {
            return PlayCoroutine(EZAnimation.Create(sprites,
                                                    durationMilliseconds,
                                                    framesPerSecond,
                                                    looping: looping,
                                                    reversed: reversed,
                                                    startingFrame: startingFrame,
                                                    endingFrame: endingFrame
                                                    ));
        }

        /// <summary>
        /// Play a list of animations one after another
        /// </summary>
        /// <param name="animations">The sprites that make up the animations to play</param>
        /// <param name="durationMilliseconds">The duration of the entire animation in Milliseconds</param>
        /// <param name="framesPerSecond">The speed of the animation measured in frames per second</param>
        /// <param name="looping">Whether or not the animation should loop</param>
        /// <param name="reversed">Whether or not to play the animation in reverse</param>
        /// <param name="startingFrame">the frame that the animation should start playing on</param>
        /// <param name="endingFrame">the frame that the animation should stop playing on</param>
        /// <remarks>either durationMilliseconds or framesPerSecond must be populated but not both</remarks>
        public void Play(List<List<Sprite>> animations, int? durationMilliseconds = null,
                                         int? framesPerSecond = null,
                                         bool looping = false,
                                         bool reversed = false,
                                         int startingFrame = 0,
                                         int? endingFrame = null)
        {
            StartCoroutine(PlayCoroutine(animations, 
                                         durationMilliseconds, 
                                         framesPerSecond, 
                                         looping, 
                                         reversed, 
                                         startingFrame, 
                                         endingFrame));
        }

        /// <summary>
        /// Play a list of animations one after another
        /// </summary>
        /// <param name="animations">The sprites that make up the animations to play</param>
        /// <param name="durationMilliseconds">The duration of the entire animation in Milliseconds</param>
        /// <param name="framesPerSecond">The speed of the animation measured in frames per second</param>
        /// <param name="looping">Whether or not the animation should loop</param>
        /// <param name="reversed">Whether or not to play the animation in reverse</param>
        /// <param name="startingFrame">the frame that the animation should start playing on</param>
        /// <param name="endingFrame">the frame that the animation should stop playing on</param>
        /// <remarks>either durationMilliseconds or framesPerSecond must be populated but not both</remarks>
        /// <returns>returns the IEnumerator of the animation coroutine</returns>
        public IEnumerator PlayCoroutine(List<List<Sprite>> animations, 
                                         int? durationMilliseconds = null,
                                         int? framesPerSecond = null,
                                         bool looping = false,
                                         bool reversed = false,
                                         int startingFrame = 0,
                                         int? endingFrame = null)
        {
            return PlayCoroutine_Implementation(animations, 
                                                durationMilliseconds, 
                                                framesPerSecond, 
                                                looping, 
                                                reversed, 
                                                startingFrame, 
                                                endingFrame);
        }

        /// <summary>
        /// Makes the current animation play backwards starting from the frame it's currently on.
        /// </summary>
        public void ReverseCurrentAnimation()
        {
            StartCoroutine(ReverseCurrentAnimationCoroutine());
        }

        /// <summary>
        /// Makes the current animation play backwards starting from the frame it's currently on.
        /// </summary>
        /// <returns>returns the IEnumerator of the animation coroutine</returns>
        public IEnumerator ReverseCurrentAnimationCoroutine()
        {
            return PlayCoroutine(GetCurrentAnimationInReverse());
        }

        /// <returns>An animation identical to the currently playing one, but in reverse and starting at the current frame</returns>
        public EZAnimation GetCurrentAnimationInReverse()
        {
            return GetCurrentAnimationInReverse_Implementation();
        }
        
        /// <summary>
        /// Stops the currently playing animation
        /// </summary>
        public void StopCurrentAnimation()
        {
            StopCurrentAnimation_Implementation();
        }

    }
}
