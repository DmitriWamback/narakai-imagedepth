using OpenTK.Mathematics;

namespace NarakaiImageDepth {

    public class Camera {

        public static Matrix4 projection, lookAt;
        static float time;

        public static void Initialize() {
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(90f), 1f, 0.01f, 1000f);
            lookAt = Matrix4.LookAt(new Vector3(0, 0, -1), new Vector3(0, 0, 0), Vector3.UnitY);
        }

        public static void Update(Vector2i screenSizepxl) {
            
            time += 0.002f;

            float dist = 20;
            float rx = MathF.Sin(time) * dist;
            float ry = MathF.Cos(time) * dist;
            lookAt = Matrix4.LookAt(new Vector3(ry, 0, rx), new Vector3(0, 0, 0), Vector3.UnitY);

            float aspect = (float)screenSizepxl.X / (float)screenSizepxl.Y;
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(90f), 1f, 0.01f, 1000f);
        }
    }
}