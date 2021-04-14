using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System;

namespace Svaerth.EZAnimator
{

    public class EZAnimationTests
    {

        [Test]
        public void TestSpritesReversed()
        {
            List<Sprite> sprites = Helper.CreateListOfSprites(3);
            var animation = EZAnimation.Create(sprites, 100, reversed: true);
            Assert.AreEqual(animation.Sprites[0], sprites[0]);
            Assert.AreEqual(animation.Sprites[1], sprites[1]);
            Assert.AreEqual(animation.Sprites[2], sprites[2]);
        }

        [Test]
        public void TestSpritesNormal()
        {
            List<Sprite> sprites = Helper.CreateListOfSprites(3);
            var animation = EZAnimation.Create(sprites, 100);
            Assert.AreEqual(animation.Sprites[0], sprites[0]);
            Assert.AreEqual(animation.Sprites[1], sprites[1]);
            Assert.AreEqual(animation.Sprites[2], sprites[2]);
        }

        [Test]
        public void TestSpritesStartingEndingFrames()
        {
            List<Sprite> sprites = Helper.CreateListOfSprites(5);
            var animation = EZAnimation.Create(sprites, 100, startingFrame: 1, endingFrame: 3);
            Assert.AreEqual(animation.Sprites[0], sprites[0]);
            Assert.AreEqual(animation.Sprites[1], sprites[1]);
            Assert.AreEqual(animation.Sprites[2], sprites[2]);
            Assert.AreEqual(animation.Sprites[3], sprites[3]);
            Assert.AreEqual(animation.Sprites[4], sprites[4]);
        }

        [Test]
        public void TestSpritesToAnimateNormal()
        {
            List<Sprite> sprites = Helper.CreateListOfSprites(3);

            var animation = EZAnimation.Create(sprites, 100);
            Assert.AreEqual(animation.SpritesToAnimate[0], sprites[0]);
            Assert.AreEqual(animation.SpritesToAnimate[1], sprites[1]);
            Assert.AreEqual(animation.SpritesToAnimate[2], sprites[2]);
        }

        [Test]
        public void TestSpritesToAnimateReverse()
        {
            List<Sprite> sprites = Helper.CreateListOfSprites(3);
            var animation = EZAnimation.Create(sprites, 100, reversed: true);
            Assert.AreEqual(animation.SpritesToAnimate[0], sprites[2]);
            Assert.AreEqual(animation.SpritesToAnimate[1], sprites[1]);
            Assert.AreEqual(animation.SpritesToAnimate[2], sprites[0]);
        }

        [Test]
        public void TestSpritesToAnimateStartingEndingFrame()
        {
            List<Sprite> sprites = Helper.CreateListOfSprites(5);
            var animation = EZAnimation.Create(sprites, 100, startingFrame: 1, endingFrame: 3);
            Assert.AreEqual(animation.SpritesToAnimate[0], sprites[1]);
            Assert.AreEqual(animation.SpritesToAnimate[1], sprites[2]);
            Assert.AreEqual(animation.SpritesToAnimate[2], sprites[3]);
            Assert.IsTrue(animation.SpritesToAnimate.Count == 3);
        }

        [Test]
        public void TestFrameCount()
        {
            var animation = EZAnimation.Create(Helper.CreateListOfSprites(5), 100, startingFrame: 1, endingFrame: 3);
            Assert.AreEqual(animation.FrameCount, 3);
        }

        [Test]
        public void TestStartingEndingFrame()
        {
            var animation = EZAnimation.Create(Helper.CreateListOfSprites(5), 100);
            Assert.AreEqual(animation.StartingFrame, 0);
            Assert.AreEqual(animation.EndingFrame, 4);

            var animation2 = EZAnimation.Create(Helper.CreateListOfSprites(5), 100, startingFrame: 1, endingFrame: 3);
            Assert.AreEqual(animation2.StartingFrame, 1);
            Assert.AreEqual(animation2.EndingFrame, 3);
        }

        [Test]
        public void TestDurationsWithMSSpecified()
        {
            var animation = EZAnimation.Create(Helper.CreateListOfSprites(5), 100);
            Assert.AreEqual(animation.DurationMilliseconds, 100);
            Assert.AreEqual(animation.FramesPerSecond, 50);
            Assert.AreEqual(animation.DurationTimeSpan, new TimeSpan(0, 0, 0, 0, 100));
            Assert.AreEqual(animation.MillisecondsPerFrame, 20);
        }

        [Test]
        public void TestDurationsWithFPSSpecified()
        {
            var animation = EZAnimation.Create(Helper.CreateListOfSprites(5), framesPerSecond: 50);
            Assert.AreEqual(animation.DurationMilliseconds, 100);
            Assert.AreEqual(animation.FramesPerSecond, 50);
            Assert.AreEqual(animation.DurationTimeSpan, new TimeSpan(0, 0, 0, 0, 100));
            Assert.AreEqual(animation.MillisecondsPerFrame, 20);
        }

        [Test]
        public void TestDurationsWithTimeSpanSpecified()
        {
            var animation = EZAnimation.Create(Helper.CreateListOfSprites(5), timeSpan: new TimeSpan(0, 0, 0, 0, 100));
            Assert.AreEqual(animation.DurationMilliseconds, 100);
            Assert.AreEqual(animation.FramesPerSecond, 50);
            Assert.AreEqual(animation.DurationTimeSpan, new TimeSpan(0, 0, 0, 0, 100));
            Assert.AreEqual(animation.MillisecondsPerFrame, 20);
        }

        [Test]
        public void TestSingleFrameAnimationCreation()
        {
            _ = EZAnimation.Create(Helper.CreateListOfSprites(1), timeSpan: new TimeSpan(0, 0, 0, 0, 100));
        }

        [Test]
        public void TestLooping()
        {
            var animation = EZAnimation.Create(Helper.CreateListOfSprites(5), 100);
            Assert.AreEqual(animation.Looping, false);

            var animation2 = EZAnimation.Create(Helper.CreateListOfSprites(5), 100, looping: true);
            Assert.AreEqual(animation2.Looping, true);

            var animation3 = EZAnimation.Create(Helper.CreateListOfSprites(5), 100, looping: false);
            Assert.AreEqual(animation3.Looping, false);
        }

        [Test]
        public void TestReversed()
        {
            var animation = EZAnimation.Create(Helper.CreateListOfSprites(5), 100);
            Assert.AreEqual(animation.Reversed, false);

            var animation2 = EZAnimation.Create(Helper.CreateListOfSprites(5), 100, reversed: true);
            Assert.AreEqual(animation2.Reversed, true);

            var animation3 = EZAnimation.Create(Helper.CreateListOfSprites(5), 100, reversed: false);
            Assert.AreEqual(animation3.Reversed, false);
        }

    }

}
