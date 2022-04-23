using UnityEngine;

namespace Game
{
    public static class Utils
    {
        /// <summary>
        /// This is inverse because Direction for the Spawnner is not the same as Direction for the Obstacles, and I need to differentiate it.
        /// </summary>
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

        /// <summary>
        /// Get a Random Position within the game's limits, and also modifies the the previousPosition of the calling class.
        /// </summary>
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
            } while (PositionsTooClose(vec, previousPos));

            previousPos = vec;
            return vec;
        }

        /// <summary>
        /// Method used to check whether to positions are too close or not
        /// </summary>
        public static bool PositionsTooClose(Vector3 pos1, Vector3 pos2)
        {
            Vector2 p1 = pos1;
            Vector2 p2 = pos2;
            return Vector2.Distance(p1, p2) <= 1f;
        }

        /// <summary>
        /// Returns a Random Color
        /// </summary>
        public static Color GetRandomColor()
        {
            return new Color(Random.Range(0f, 1f), .5f, .5f);
        }
    }
}
