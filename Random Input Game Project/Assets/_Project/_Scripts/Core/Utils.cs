using UnityEngine;

namespace Game
{
    public static class Utils
    {
        /// This is inverse because Direction for the Spawnner is not the same as Direction for the Obstacles, and I need to differentiate it.
        public static Vector3 GetInverseMoveDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Right:
                    return Vector3.left;
                case Direction.Left:
                    return Vector3.right;
                case Direction.Up:
                    return Vector3.down;
                case Direction.Down:
                    return Vector3.up;
                default:
                    Debug.LogError("Error with direction in Obstacle");
                    return Vector3.zero;
            }
        }

        /// Get a Random Position within the game's limits, and also modifies the the previousPosition of the calling class.
        /// <param name="previousPos">Position previously obtained to avoid creating an object in the same postion twice.</param>
        /// <returns>The random position calculated.</returns>
        public static Vector3 GetRandomPos(ref Vector3 previousPos)
        {
            float x;
            float y;
            Vector3 vec;

            do
            {
                x = Random.Range(-8f, 8f);
                y = Random.Range(-4f, 4f);
                vec = new Vector3(x, y, 0f);
            } while (PositionsTooClose2D(vec, previousPos, 1f));

            previousPos = vec;
            return vec;
        }

        /// Method used to check whether two positions are too close or not
        public static bool PositionsTooClose3D(Vector3 pos1, Vector3 pos2, float minimumDistance)
        {
            return Vector3.Distance(pos1, pos2) <= minimumDistance;
        }
        
        /// Method used to check whether two positions are too close or not
        public static bool PositionsTooClose2D(Vector2 pos1, Vector2 pos2, float minimumDistance)
        {
            return Vector2.Distance(pos1, pos2) <= minimumDistance;
        }

        /// Returns a Random Color.
        public static Color GetRandomColor()
        {
            return new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        }
        
        /// To darken by balue a given color.
        public static Color Darken(this Color color, float value)
        {
            color.r = color.r - (value * color.r);
            color.g = color.g - (value * color.g);
            color.b = color.b - (value * color.b);

            return color;
        }

        /// To lighten by value a given color.
        public static Color Lighten(this Color color, float value)
        {
            color.r = color.r + (value * color.r);
            color.g = color.g + (value * color.g);
            color.b = color.b + (value * color.b);

            return color;
        }

        /// To transform a color to grayScale.
        public static Color ToGrayScale(this Color color)
        {
            float value = (color.r + color.g + color.b) / 3f;
            
            color.r = value;
            color.g = value;
            color.b = value;

            return color;
        }
    }
}
