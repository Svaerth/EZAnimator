using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using System;

namespace Svaerth.EZAnimator
{

    public partial class EZAnimation
    {

        /// <summary>
        /// The first frame that should be played in the animation. Must be greater than 0 and less than or equal to ending frame. 0 by default.
        /// </summary>
        public int StartingFrame => StartingFrame_Implementation;

        /// <summary>
        /// The last frame that will be played. Must be greater than or equal to starting frame and less than the number of total frames in the animation.
        /// By default it will be the last frame of the animation.
        /// </summary>
        public int EndingFrame => EndingFrame_Implementation;

        /// <summary>
        /// The number of frames in the animation excluding the frames before starting frame and after ending frame.
        /// </summary>
        public int FrameCount => EndingFrame - StartingFrame + 1;

        /// <summary>
        /// Whether or not the animation should loop
        /// </summary>
        public bool Looping { get; private set; }

        /// <summary>
        /// Whether or not the animation should play in reverse
        /// </summary>
        public bool Reversed { get; private set; }

        ///<summary>the total list of sprites in their original order,
        ///including ones that won't be shown due to starting frame and ending frame not including them.
        ///</summary>
        public List<Sprite> Sprites => Sprites_Implementation;

        ///<summary>
        ///the list of sprites that will actually be shown in the order they'll be shown. 
        ///This excludes sprites left out by starting and ending frame 
        ///</summary>
        public List<Sprite> SpritesToAnimate => SpritesToAnimate_Implementation;

        /// <summary>
        /// The duration of the entire animation measured in milliseconds
        /// </summary>
        public int DurationMilliseconds => DurationMilliseconds_Implementation;

        /// <summary>
        /// The speed of the animation measured in frames per second
        /// </summary>
        public float FramesPerSecond => FramesPerSecond_Implementation;

        /// <summary>
        /// The amount of time each frame is shown measured in milliseconds
        /// </summary>
        public float MillisecondsPerFrame => DurationMilliseconds / (float)FrameCount;

        /// <summary>
        /// The duration of the entire animation expressed as a TimeSpan
        /// </summary>
        public TimeSpan DurationTimeSpan => DurationTimeSpan_Implementation;

        /// <summary>
        /// Create an animation
        /// </summary>
        /// <param name="sprites">The sprites that make up the animation</param>
        /// <param name="durationMilliseconds">the duration of the entire animation measured in milliseconds</param>
        /// <param name="framesPerSecond">the speed of the animation measured in frames per second</param>
        /// <param name="timeSpan">the duration of the entire animation expressed as a TimeSpan</param>
        /// <param name="startingFrame">the frame that the animation should begin playing on</param>
        /// <param name="endingFrame">the last frame the animation should play</param>
        /// <param name="looping">whether or not the animation should loop</param>
        /// <param name="reversed">whether or not the animation should play in reverse</param>
        /// <remarks>either durationMilliseconds, framesPerSecond, or timeSpan must be passed in, but only one can be passed in, the rest must be null.</remarks>
        /// <returns>An animation with the specified properties</returns>
        public static EZAnimation Create(
            List<Sprite> sprites,
            int? durationMilliseconds = null,
            int? framesPerSecond = null,
            TimeSpan? timeSpan = null,
            int startingFrame = 0,
            int? endingFrame = null,
            bool looping = false,
            bool reversed = false
        )
        {
            return Create_Implementation(sprites, 
                                         durationMilliseconds, 
                                         framesPerSecond, 
                                         timeSpan, 
                                         startingFrame, 
                                         endingFrame, 
                                         looping, 
                                         reversed);
        }

    }

}