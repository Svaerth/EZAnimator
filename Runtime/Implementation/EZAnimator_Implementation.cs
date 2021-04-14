using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using System.Threading;
using System;

namespace Svaerth.EZAnimator
{

    public partial class EZAnimator
    {
        int currentFrameBackValue = 0;
        int CurrentFrame_implementation
        {
            get
            {
                return currentFrameBackValue;
            }
            set
            {
                Assert.IsTrue(value >= 0, "CurrentFrame must be equal to or greater than 0!. attempted value: " + value);
                currentFrameBackValue = value;
            }
        }


        private CancellationTokenSource cancellationTokenSource = null;

        /// <summary>
        /// Plays a sequence of animations back to back, then returns when done.
        /// </summary>
        /// <param name="animations">the array of animations to play. Must contain atleast one animation</param>
        private IEnumerator PlaySequence(EZAnimation[] animations, CancellationToken cancellationToken)
        {

            Assert.IsTrue(animations.Length > 0, "animations array must have atleast one animation!");

            foreach (EZAnimation animation in animations)
            {

                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }

                CurrentAnimation = animation;

                do
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        break;
                    }
                    yield return StartCoroutine(CycleThroughFrames(animation, cancellationToken));
                }
                while (animation.Looping);

            }

            if (cancellationToken.IsCancellationRequested == false)
            {
                StopCurrentAnimation();
            }

        }

        private IEnumerator CycleThroughFrames(EZAnimation animation, CancellationToken cancellationToken)
        {

            Assert.IsTrue(animation != null, "animation cannot be null!");

            BeginningOfAnimation = DateTime.Now;
            for (int i = 0; i < animation.FrameCount; i++)
            {

                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }

                #region update frame of animation to show
                CurrentSprite = animation.SpritesToAnimate[i];
                CurrentFrame_implementation = animation.Sprites.IndexOf(CurrentSprite);
                GetComponent<SpriteRenderer>().sprite = CurrentSprite;
                #endregion

                #region wait until it's time to switch to the next frame
                int nextFrame = i + 1;
                DateTime nextFrameBegins = BeginningOfAnimation.AddMilliseconds(animation.MillisecondsPerFrame * nextFrame);
                TimeSpan timeTilNextFrame = nextFrameBegins - DateTime.Now;

                if (timeTilNextFrame > new TimeSpan(0))
                {
                    yield return new WaitForSeconds((float)timeTilNextFrame.TotalSeconds);
                }

                #endregion
            }

        }

        private void StopCurrentAnimation_Implementation()
        {
            cancellationTokenSource?.Cancel();
            CurrentAnimation = null;
            CurrentSprite = null;
        }

        private int GetMSPerFrame(List<List<Sprite>> animations, int durationMilliseconds)
        {
            int totalFrames = 0;
            foreach (var animation in animations)
            {
                totalFrames += animation.Count;
            }
            return (int)(durationMilliseconds * 1.0f / totalFrames);
        }

        IEnumerator PlayCustomCoroutine_Implementation(EZAnimation[] animations)
        {
            StopCurrentAnimation();
            cancellationTokenSource = new CancellationTokenSource();
            yield return StartCoroutine(PlaySequence(animations, cancellationTokenSource.Token));
        }

        IEnumerator PlayCoroutine_Implementation(List<List<Sprite>> animations, int? durationMilliseconds = null,
                                         int? framesPerSecond = null,
                                         bool looping = false,
                                         bool reversed = false,
                                         int startingFrame = 0,
                                         int? endingFrame = null)
        {


            List<EZAnimation> simpleAnimations = new List<EZAnimation>();
            foreach (var animation in animations)
            {

                int? durationMSForAnimation = null;
                if (durationMilliseconds != null)
                {
                    durationMSForAnimation = GetMSPerFrame(animations, (int)durationMilliseconds) * animation.Count;
                }

                simpleAnimations.Add(EZAnimation.Create(animation,
                                       durationMSForAnimation,
                                       framesPerSecond,
                                       looping: looping,
                                       reversed: reversed,
                                       startingFrame: startingFrame,
                                       endingFrame: endingFrame
                                       ));
            }
            return PlayCoroutine(simpleAnimations);
        }

        EZAnimation GetCurrentAnimationInReverse_Implementation()
        {
            Assert.IsNotNull(CurrentAnimation, "CurrentAnimation must be populated to get current animation in reverse!");
            return EZAnimation.Create(CurrentAnimation.Sprites,
                                          (int)TimeSinceBeginning.TotalMilliseconds,
                                          startingFrame: CurrentAnimation.Reversed ? CurrentFrame : CurrentAnimation.StartingFrame,
                                          endingFrame: CurrentAnimation.Reversed ? CurrentAnimation.EndingFrame : CurrentFrame,
                                          reversed: !CurrentAnimation.Reversed,
                                          looping: CurrentAnimation.Looping
                                          );
        }

    }

}
