using System;
using System.Collections.Generic;
using UnityEngine;

namespace Svaerth.EZAnimator
{
    class Helper
    {

        public static List<Sprite> CreateListOfSprites(int count)
        {
            List<Sprite> sprites = new List<Sprite>();
            for (int i = 0; i < count; i++)
            {
                sprites.Add(Sprite.Create(new Texture2D(100, 100), new Rect(0, 0, 100, 100), new Vector2(0, 0)));
            }
            return sprites;
        }

    }
}
