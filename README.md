# EZAnimator
An easier way to perform 2D sprite animations in Unity. By simply adding the EZAnimator component to the object you wish to animate, you can now easily play a list of sprites with a single method call.

## Installation Instructions
Add this package via Unity's Package Manager using the Git URL: https://github.com/Svaerth/EZAnimator.git 
You can find more detailed instructions [here](https://docs.unity3d.com/Manual/upm-ui-giturl.html)

## Usage

### Playing a list of sprites
1. Add the EZAnimator component to the object you wish to animate
2. Procure a list of Sprite objects that you wish to make up the animation
3. Use one of the overloads for the Play or PlayCoroutine methods.

Example:
`
List<Sprite> RunningAnimation = new List<Sprite>(){
  RunningSprite1, RunningSprite2, RunningSprite3
};
GetComponent<EZAnimator>().Play(RunningAnimation, 300);
`

### Playing an Animation during a Coroutine
Methods with the suffix 'Coroutine' are Coroutines, you can use these if you wish to pause a coroutine until an animation is done playing.

Example:
`
IEnumerator ChargeAndShoot()
{
  var animator = GetComponent<EZAnimator>()
  yield return StartCoroutine(animator.PlayCouroutine(ChargingAnimation, 300));
  CreateFireball();
  yield return StartCoroutine(animator.PlayCouroutine(RecoilAnimation, 300));
}
`

### Customizing Animation Properties
You can customize how the animation plays by populating the following optional parameters in the Play method:
1. durationMilliseconds - how long (in milliseconds) it will take to play the whole animation.
2. framesPerSecond - the speed of the animation measured in frames per seconds 
*NOTE: you must populate either durationMilliseconds or framesPerSecond but not both*
3. looping - whether or not the animation should loop
4. reversed - whether or not the animation should play in reverse
5. startingFrame - the frame of animation that the animation begin playing on
6. endingFrame - the frame that the animation should stop playing on

