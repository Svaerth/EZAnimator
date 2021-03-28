using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System;

namespace Svaerth.EZAnimator
{

    public class EZAnimatorTests
    {

        [UnityTest]
        public IEnumerator TestNormalAnimation()
        {

            GameObject go = CreateGameObject();
            List<Sprite> sprites = Helper.CreateListOfSprites(3);

            var animator = go.GetComponent<EZAnimator>();

            TestPropertiesNotPlaying(animator);

            animator.StartCoroutine(animator.PlayCoroutine(sprites, 300));

            yield return new WaitForFixedUpdate();

            TestProperties(animator, null, sprites, sprites[0], 0, true);
            Assert.AreEqual(go.GetComponent<SpriteRenderer>().sprite, sprites[0]);

            yield return new WaitForSeconds(.1f);

            TestProperties(animator, null, sprites, sprites[1], 1, true);
            Assert.AreEqual(go.GetComponent<SpriteRenderer>().sprite, sprites[1]);

            yield return new WaitForSeconds(.1f);

            Assert.AreEqual(go.GetComponent<SpriteRenderer>().sprite, sprites[2]);

            yield return new WaitForSeconds(.1f);

            TestPropertiesNotPlaying(animator);
            Assert.AreEqual(go.GetComponent<SpriteRenderer>().sprite, sprites[2]);
        }

        [UnityTest]
        public IEnumerator TestOneFrameAnimation()
        {

            GameObject go = CreateGameObject();
            List<Sprite> sprites = Helper.CreateListOfSprites(1);

            var animator = go.GetComponent<EZAnimator>();

            TestPropertiesNotPlaying(animator);

            animator.StartCoroutine(animator.PlayCoroutine(sprites, 100));

            yield return new WaitForFixedUpdate();

            TestProperties(animator, null, sprites, sprites[0], 0, true);
            Assert.AreEqual(go.GetComponent<SpriteRenderer>().sprite, sprites[0]);

            yield return new WaitForSeconds(.1f);

            TestPropertiesNotPlaying(animator);
            Assert.AreEqual(go.GetComponent<SpriteRenderer>().sprite, sprites[0]);
        }

        [UnityTest]
        public IEnumerator TestCustomSingleAnimation()
        {

            GameObject go = CreateGameObject();
            List<Sprite> sprites = Helper.CreateListOfSprites(3);

            var animation = EZAnimation.Create(sprites, 300);

            var animator = go.GetComponent<EZAnimator>();

            animator.Play(animation);

            yield return new WaitForFixedUpdate();

            Assert.AreEqual(go.GetComponent<SpriteRenderer>().sprite, sprites[0]);

            yield return new WaitForSeconds(.1f);

            Assert.AreEqual(go.GetComponent<SpriteRenderer>().sprite, sprites[1]);

            yield return new WaitForSeconds(.1f);

            Assert.AreEqual(go.GetComponent<SpriteRenderer>().sprite, sprites[2]);

            yield return new WaitForSeconds(.1f);

            Assert.AreEqual(go.GetComponent<SpriteRenderer>().sprite, sprites[2]);
        }

        [UnityTest]
        public IEnumerator TestCustomAnimationArray()
        {

            GameObject go = CreateGameObject();

            List<Sprite> sprites = Helper.CreateListOfSprites(3);
            var animation = EZAnimation.Create(sprites, 300);

            List<Sprite> sprites2 = Helper.CreateListOfSprites(3);
            var animation2 = EZAnimation.Create(sprites2, 300);

            var animator = go.GetComponent<EZAnimator>();

            animator.Play(new EZAnimation[] { animation, animation2 });

            yield return new WaitForFixedUpdate();

            Assert.AreEqual(go.GetComponent<SpriteRenderer>().sprite, sprites[0]);

            yield return new WaitForSeconds(.1f);

            Assert.AreEqual(go.GetComponent<SpriteRenderer>().sprite, sprites[1]);

            yield return new WaitForSeconds(.1f);

            Assert.AreEqual(go.GetComponent<SpriteRenderer>().sprite, sprites[2]);

            yield return new WaitForSeconds(.1f);

            Assert.AreEqual(go.GetComponent<SpriteRenderer>().sprite, sprites2[0]);

            yield return new WaitForSeconds(.1f);

            Assert.AreEqual(go.GetComponent<SpriteRenderer>().sprite, sprites2[1]);
        }

        [UnityTest]
        public IEnumerator TestCustomAnimationList()
        {

            GameObject go = CreateGameObject();

            List<Sprite> sprites = Helper.CreateListOfSprites(3);
            var animation = EZAnimation.Create(sprites, 300);

            List<Sprite> sprites2 = Helper.CreateListOfSprites(3);
            var animation2 = EZAnimation.Create(sprites2, 300);

            var animator = go.GetComponent<EZAnimator>();

            animator.Play(new List<EZAnimation>() { animation, animation2 });

            yield return new WaitForFixedUpdate();

            Assert.AreEqual(go.GetComponent<SpriteRenderer>().sprite, sprites[0]);

            yield return new WaitForSeconds(.1f);

            Assert.AreEqual(go.GetComponent<SpriteRenderer>().sprite, sprites[1]);

            yield return new WaitForSeconds(.1f);

            Assert.AreEqual(go.GetComponent<SpriteRenderer>().sprite, sprites[2]);

            yield return new WaitForSeconds(.1f);

            Assert.AreEqual(go.GetComponent<SpriteRenderer>().sprite, sprites2[0]);

            yield return new WaitForSeconds(.1f);

            Assert.AreEqual(go.GetComponent<SpriteRenderer>().sprite, sprites2[1]);
        }

        [UnityTest]
        public IEnumerator TestSequenceOfAnimations()
        {

            GameObject go = CreateGameObject();
            List<Sprite> sprites = Helper.CreateListOfSprites(3);
            List<Sprite> sprites2 = Helper.CreateListOfSprites(3);

            var animator = go.GetComponent<EZAnimator>();
            animator.StartCoroutine(animator.PlayCoroutine(new List<List<Sprite>>() { sprites, sprites2 }, 600));

            yield return new WaitForFixedUpdate();

            TestProperties(animator, null, sprites, sprites[0], 0, true);
            Assert.AreEqual(go.GetComponent<SpriteRenderer>().sprite, sprites[0]);

            yield return new WaitForSeconds(.1f);

            Assert.AreEqual(go.GetComponent<SpriteRenderer>().sprite, sprites[1]);

            yield return new WaitForSeconds(.1f);

            Assert.AreEqual(go.GetComponent<SpriteRenderer>().sprite, sprites[2]);

            yield return new WaitForSeconds(.1f);

            TestProperties(animator, null, sprites2, sprites2[0], 0, true);
            Assert.AreEqual(go.GetComponent<SpriteRenderer>().sprite, sprites2[0]);

            yield return new WaitForSeconds(.1f);

            Assert.AreEqual(go.GetComponent<SpriteRenderer>().sprite, sprites2[1]);

            yield return new WaitForSeconds(.1f);

            Assert.AreEqual(go.GetComponent<SpriteRenderer>().sprite, sprites2[2]);
        }

        [UnityTest]
        public IEnumerator TestLoopingAnimation()
        {

            GameObject go = CreateGameObject();
            List<Sprite> sprites = Helper.CreateListOfSprites(3);

            var animator = go.GetComponent<EZAnimator>();
            animator.Play(sprites, 300, looping: true);

            yield return new WaitForFixedUpdate();

            Assert.AreEqual(go.GetComponent<SpriteRenderer>().sprite, sprites[0]);

            yield return new WaitForSeconds(.1f);

            Assert.AreEqual(go.GetComponent<SpriteRenderer>().sprite, sprites[1]);

            yield return new WaitForSeconds(.1f);

            Assert.AreEqual(go.GetComponent<SpriteRenderer>().sprite, sprites[2]);

            yield return new WaitForSeconds(.1f);

            Assert.AreEqual(go.GetComponent<SpriteRenderer>().sprite, sprites[0]);
        }

        [UnityTest]
        public IEnumerator TestReverseAnimation()
        {

            GameObject go = CreateGameObject();
            List<Sprite> sprites = Helper.CreateListOfSprites(3);

            var animator = go.GetComponent<EZAnimator>();
            animator.StartCoroutine(animator.PlayCoroutine(sprites, 300, reversed: true));

            yield return new WaitForFixedUpdate();

            Assert.AreEqual(go.GetComponent<SpriteRenderer>().sprite, sprites[2]);

            yield return new WaitForSeconds(.1f);

            Assert.AreEqual(go.GetComponent<SpriteRenderer>().sprite, sprites[1]);

            yield return new WaitForSeconds(.1f);

            Assert.AreEqual(go.GetComponent<SpriteRenderer>().sprite, sprites[0]);

            yield return new WaitForSeconds(.1f);

            Assert.AreEqual(go.GetComponent<SpriteRenderer>().sprite, sprites[0]);
        }

        [UnityTest]
        public IEnumerator TestReversingCurrentAnimation()
        {

            GameObject go = CreateGameObject();
            List<Sprite> sprites = Helper.CreateListOfSprites(3);

            var animator = go.GetComponent<EZAnimator>();
            animator.Play(sprites, 300);

            yield return new WaitForFixedUpdate();

            Assert.AreEqual(go.GetComponent<SpriteRenderer>().sprite, sprites[0]);

            yield return new WaitForSeconds(.1f);

            Assert.AreEqual(go.GetComponent<SpriteRenderer>().sprite, sprites[1]);

            EZAnimation a = animator.GetCurrentAnimationInReverse();

            animator.ReverseCurrentAnimation();

            yield return new WaitForSeconds(.1f);

            Assert.AreEqual(go.GetComponent<SpriteRenderer>().sprite, sprites[0]);

            yield return new WaitForSeconds(.1f);

            Assert.AreEqual(go.GetComponent<SpriteRenderer>().sprite, sprites[0]);
        }

        [UnityTest]
        public IEnumerator TestInterruptingAnimation()
        {

            GameObject go = CreateGameObject();
            List<Sprite> sprites = Helper.CreateListOfSprites(3);
            List<Sprite> sprites2 = Helper.CreateListOfSprites(3);

            var animator = go.GetComponent<EZAnimator>();
            animator.Play(sprites, 300);

            yield return new WaitForFixedUpdate();

            TestProperties(animator, null, sprites, sprites[0], 0, true);
            Assert.AreEqual(go.GetComponent<SpriteRenderer>().sprite, sprites[0]);

            yield return new WaitForSeconds(.1f);

            Assert.AreEqual(go.GetComponent<SpriteRenderer>().sprite, sprites[1]);

            animator.Play(sprites2, 300);

            yield return new WaitForFixedUpdate();

            TestProperties(animator, null, sprites2, sprites2[0], 0, true);
            Assert.AreEqual(go.GetComponent<SpriteRenderer>().sprite, sprites2[0]);

            yield return new WaitForSeconds(.1f);

            Assert.AreEqual(go.GetComponent<SpriteRenderer>().sprite, sprites2[1]);
        }

        [UnityTest]
        public IEnumerator TestStoppingAnimation()
        {

            GameObject go = CreateGameObject();
            List<Sprite> sprites = Helper.CreateListOfSprites(3);

            var animator = go.GetComponent<EZAnimator>();
            animator.StartCoroutine(animator.PlayCoroutine(sprites, 300));

            yield return new WaitForFixedUpdate();

            Assert.AreEqual(go.GetComponent<SpriteRenderer>().sprite, sprites[0]);

            yield return new WaitForSeconds(.1f);

            Assert.AreEqual(go.GetComponent<SpriteRenderer>().sprite, sprites[1]);
            animator.StopCurrentAnimation();

            yield return new WaitForSeconds(.1f);

            TestPropertiesNotPlaying(animator);
            Assert.AreEqual(go.GetComponent<SpriteRenderer>().sprite, sprites[1]);
        }

        [UnityTest]
        public IEnumerator TestWaitingOnAnimation()
        {

            GameObject go = CreateGameObject();
            List<Sprite> sprites = Helper.CreateListOfSprites(3);

            var animator = go.GetComponent<EZAnimator>();
            yield return animator.StartCoroutine(animator.PlayCoroutine(sprites, 100));

            TestPropertiesNotPlaying(animator);
            Assert.AreEqual(go.GetComponent<SpriteRenderer>().sprite, sprites[2]);
        }

        void TestPropertiesNotPlaying(EZAnimator animator)
        {
            Assert.IsNull(animator.CurrentAnimation);
            Assert.IsNull(animator.CurrentSprite);
            Assert.IsFalse(animator.AnimationInProgress);
        }

        void TestProperties(EZAnimator animator,
                            EZAnimation currentAnimation = null,
                            List<Sprite> currentSprites = null,
                            Sprite currentSprite = null,
                            int? currentFrame = null,
                            bool? animationInProgress = null)
        {
            if (currentAnimation != null)
                Assert.AreEqual(animator.CurrentAnimation, currentAnimation);
            if (currentSprites != null)
                Assert.AreEqual(animator.CurrentAnimation?.Sprites, currentSprites);
            if (currentSprite != null)
                Assert.AreEqual(animator.CurrentSprite, currentSprite);
            if (currentFrame != null)
                Assert.AreEqual(animator.CurrentFrame, currentFrame);
            if (animationInProgress != null)
                Assert.IsTrue(animationInProgress == animator.AnimationInProgress);
        }

        GameObject CreateGameObject()
        {
            GameObject go = new GameObject();
            go.AddComponent<SpriteRenderer>();
            go.AddComponent<EZAnimator>();
            return go;
        }

    }

}
