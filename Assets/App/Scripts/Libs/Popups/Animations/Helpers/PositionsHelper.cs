using UnityEngine;

namespace Libs.Popups.Animations.Helpers
{
    public static class PositionsHelper
    {
        private const int Margin = 10;
        
        public static Vector2 Top(RectTransform main) => new Vector2(0, main.rect.height + Margin);
        
        public static Vector2 Bottom(RectTransform main) => new Vector3(0, -main.rect.height - Margin);
        
        public static Vector2 Right(RectTransform main) => new Vector3(main.rect.width + Margin, 0);
        
        public static Vector2 Left(RectTransform main) => new Vector3(-main.rect.width - Margin, 0);

        public static void ToRight(RectTransform transform, RectTransform parent)
        {
            var position = Right(parent);
            transform.localPosition = new Vector3(position.x, transform.localPosition.y);
        }
        
        public static void ToLeft(RectTransform transform, RectTransform parent)
        {
            var position = Left(parent);
            transform.localPosition = new Vector3(position.x, transform.localPosition.y);
        }
        
        public static void ToTop(RectTransform transform, RectTransform parent)
        {
            var position = Top(parent);
            transform.localPosition = new Vector3(transform.position.x, position.y);
        }
        
        public static void ToBottom(RectTransform transform, RectTransform parent)
        {
            var position = Bottom(parent);
            transform.localPosition = new Vector3(transform.position.x, position.y);
        }
    }
}