using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Assertions;
using UnityEngine;

namespace Svaerth.EZAnimator
{
    public partial class EZAnimation
    {
        int StartingFrame_Implementation
        {
            get
            {
                return _startingFrame ?? 0;
            }
            set
            {
                Assert.IsTrue(value >= 0, "Starting frame must be equal to or greater than 0! Attempted Value: " + value);
                Assert.IsTrue(value <= EndingFrame, "Starting frame must be less than or equal to ending frame! Attempted Value: " + value + ", EndingFrame: " + EndingFrame);
                _startingFrame = value;
            }
        }
        int? _startingFrame = null;

        int EndingFrame_Implementation
        {
            get
            {
                if (_endingFrame == null)
                {
                    Assert.IsNotNull(Sprites, "Sprites must be populated before accessing ending frame!");
                    return Sprites.Count;
                }
                else
                {
                    return (int)_endingFrame;
                }
            }
            set
            {
                Assert.IsNotNull(Sprites, "Sprites must be populated before Ending Frame!");
                Assert.IsTrue(value < Sprites.Count, "Ending frame must be less than number of frames! Attempted Value: " + value);
                Assert.IsTrue(value >= StartingFrame, "Ending frame must be greater than or equal to starting frame! Attempted Value: " + value + ", StartingFrame: " + StartingFrame);
                _endingFrame = value;
            }
        }
        int? _endingFrame = null;

        List<Sprite> Sprites_Implementation
        {
            get
            {
                Assert.IsNotNull(_sprites, "Sprites must be populated before being accessed!");
                return _sprites;
            }
            set
            {
                Assert.IsTrue(value != null, "sprites cannot be null!");
                Assert.IsTrue(value.Count > 0, "sprites must have atleast one sprite!");
                _sprites = value;
            }
        }
        List<Sprite> _sprites = null;

        List<Sprite> SpritesToAnimate_Implementation
        {
            get
            {
                if (_spritesToAnimate == null)
                {
                    _spritesToAnimate = GetSpritesToAnimate();
                }
                return _spritesToAnimate;
            }
        }
        List<Sprite> _spritesToAnimate = null;
        List<Sprite> GetSpritesToAnimate()
        {
            List<Sprite> spritesToAnimate = new List<Sprite>();
            for (int i = StartingFrame; i <= EndingFrame; i++)
            {
                spritesToAnimate.Add(Sprites[i]);
            }
            if (Reversed)
            {
                spritesToAnimate.Reverse();
            }
            return spritesToAnimate;
        }

        int DurationMilliseconds_Implementation
        {
            get
            {
                Assert.IsTrue(_durationMilliseconds != null, "DurationMilliseconds must be populated before being accessed!");
                return (int)_durationMilliseconds;
            }
            set
            {
                Assert.IsTrue(value > 0, "DurationMilliseconds must be greater than 0!");
                _durationMilliseconds = value;
            }
        }
        int? _durationMilliseconds = null;

        float FramesPerSecond_Implementation
        {
            get
            {
                float DurationSeconds = DurationMilliseconds / 1000f;
                return FrameCount / DurationSeconds;
            }
            set
            {
                Assert.IsTrue(value > 0, "FramesPerSecond must be greater than 0!");
                DurationMilliseconds_Implementation = (int)Math.Round(1000f / value * FrameCount);
            }
        }

        TimeSpan DurationTimeSpan_Implementation
        {
            get
            {
                return new TimeSpan(0, 0, 0, 0, DurationMilliseconds);
            }
            set
            {
                Assert.IsTrue(value.TotalMilliseconds > 0, "duration must be greater than 0 milliseconds!");
                DurationMilliseconds_Implementation = (int)value.TotalMilliseconds;
            }
        }

        static EZAnimation Create_Implementation(
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
            Assert.IsTrue(durationMilliseconds != null && framesPerSecond == null && timeSpan == null ||
                          durationMilliseconds == null && framesPerSecond != null && timeSpan == null ||
                          durationMilliseconds == null && framesPerSecond == null && timeSpan != null,
                          "One and only one of the following parameters must be populated: durationMilliseconds, framesPerSecond, timeSpan");

            var animation = new EZAnimation();
            animation.Sprites_Implementation = sprites;
            animation.StartingFrame_Implementation = startingFrame;
            animation.EndingFrame_Implementation = endingFrame ?? sprites.Count - 1;
            animation.Looping = looping;
            animation.Reversed = reversed;

            if (durationMilliseconds != null)
            {
                animation.DurationMilliseconds_Implementation = (int)durationMilliseconds;
            }
            if (framesPerSecond != null)
            {
                animation.FramesPerSecond_Implementation = (int)framesPerSecond;
            }
            if (timeSpan != null)
            {
                animation.DurationTimeSpan_Implementation = (TimeSpan)timeSpan;
            }

            return animation;
        }
    }
}
