using Phoenix.CounterStrike.Structs;
using Phoenix.MemorySystem;
using Phoenix.Structs;
using System;

namespace Phoenix.CounterStrike
{
    internal static class BaseClient
    {
        private static ProcessMemory Memory => Phoenix.Memory;
        private static bool readYet;
        private static IntPtr w2sViewMatrixPtr;
        private static ViewMatrix vMatrix;

        public static ViewMatrix ViewMatrix
        {
            get
            {
                if(w2sViewMatrixPtr == IntPtr.Zero)
                {
                    w2sViewMatrixPtr = SignatureManager.GetWorldToViewMatrix();
                }
                if(!readYet)
                    vMatrix = Memory.Read<ViewMatrix>(w2sViewMatrixPtr);
                return vMatrix;
            }
        }

        public static Vector2D WorldToScreen(Vector3D world)
        {
            Vector2D vec2;
            Vector2D vector2D;
            ViewMatrix viewMatrix = ViewMatrix;
            float m41 = viewMatrix.M41 * world.X + viewMatrix.M42 * world.Y + viewMatrix.M43 * world.Z + viewMatrix.M44;
            if (m41 < 0.01)
            {
                vector2D = new Vector2D()
                {
                    X = 0f,
                    Y = 0f
                };
                vec2 = vector2D;
            }
            else
            {
                float single = 1f / m41;
                float m11 = (viewMatrix.M11 * world.X + viewMatrix.M12 * world.Y + viewMatrix.M13 * world.Z + viewMatrix.M14) * single;
                float m21 = (viewMatrix.M21 * world.X + viewMatrix.M22 * world.Y + viewMatrix.M23 * world.Z + viewMatrix.M24) * single;
                vector2D = new Vector2D()
                {
                    X = (m11 + 1f) * 0.5f * Phoenix.Overlay.Width,
                    Y = (m21 - 1f) * -0.5f * Phoenix.Overlay.Height
                };
                vec2 = vector2D;
            }
            return vec2;
        }

        public static void ClearCache()
        {
            readYet = false;
        }
    }
}
