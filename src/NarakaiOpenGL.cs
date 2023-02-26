using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace NarakaiImageDepth {

    public class NarakaiOpenGL: GameWindow {

        Dictionary<string, Vector3i> textures = new Dictionary<string, Vector3i>();
        NarakaiOpenGLShader shader;

        float globeZoom = 100f;

        float[] frameVertices = {
            -1f, -1f,
             1f, -1f,
             1f,  1f,

            -1f, 1f,
             1f, 1f,
            -1f, -1f,
        };

        int frameVertexArray, frameVertexObject;
        public static Vector3 currentCountryPosition = new Vector3(100);
        private Vector2 rotation = new Vector2(0);

        public NarakaiOpenGL(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings) { Run(); }

        protected override void OnLoad() {
            base.OnLoad();

            if (!GLFW.Init()) throw new NotImplementedException();

            Camera.Initialize();

            shader = NarakaiOpenGLShader.Create("SimpleDepth");

            frameVertexArray = GL.GenVertexArray();
            frameVertexObject = GL.GenBuffer();

            GL.BindVertexArray(frameVertexArray);
            LoadDepthImage("src/output.png");
            LoadAlbedo("src/city_test.jpeg");

            frameVertices = Texture.ImageGrid(textures["DepthMap"]);

            GL.BindBuffer(BufferTarget.ArrayBuffer, frameVertexObject);
            GL.BufferData(BufferTarget.ArrayBuffer, frameVertices.Length * sizeof(float), frameVertices, BufferUsageHint.DynamicDraw);

            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.ProgramPointSize);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        protected override void OnMouseMove(MouseMoveEventArgs e) {
            base.OnMouseMove(e);

            if (this.IsMouseButtonDown(MouseButton.Right)) {
                rotation.X -= this.MouseState.Delta.X * 0.01f;
                rotation.Y += this.MouseState.Delta.Y * 0.01f;

                if (rotation.Y > 1.54362f) rotation.Y = 1.54362f;
                if (rotation.Y < -1.54362f) rotation.Y = -1.54362f;

                currentCountryPosition = new Vector3(MathF.Sin(rotation.X) * MathF.Cos(rotation.Y) * globeZoom, 
                                                     MathF.Sin(rotation.Y) * globeZoom, 
                                                     MathF.Cos(rotation.X) * MathF.Cos(rotation.Y) * globeZoom);
            }
        }
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
            globeZoom += e.OffsetY * 0.1f;

            if (globeZoom < 1.061f)     globeZoom = 1.061f;
            if (globeZoom > 100f) globeZoom = 100f;

            currentCountryPosition = new Vector3(MathF.Sin(rotation.X) * MathF.Cos(rotation.Y) * globeZoom, 
                                                 MathF.Sin(rotation.Y) * globeZoom, 
                                                 MathF.Cos(rotation.X) * MathF.Cos(rotation.Y) * globeZoom);
        }

        protected override void OnUpdateFrame(FrameEventArgs args) {
            base.OnUpdateFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            Camera.Update(ClientSize);

            shader.Use();
            GL.BindVertexArray(frameVertexArray);

            shader.SetInt("depthMap", 0);
            shader.SetInt("albedo", 1);

            shader.SetMatrix4("projection", Camera.projection);
            shader.SetMatrix4("lookAt", Camera.lookAt);

            try {
                Texture.BindTextureToTarget(TextureUnit.Texture0, textures["DepthMap"].X);
                Texture.BindTextureToTarget(TextureUnit.Texture1, textures["Albedo"].X);
            }
            catch(Exception e) { Console.WriteLine(e.Message); }
            GL.DrawArrays(PrimitiveType.Triangles, 0, frameVertices.Length/3);

            this.SwapBuffers();
            GLFW.PollEvents();
        }

        public void LoadAlbedo(string path) {
            textures["Albedo"] = Texture.LoadImage(path);
        }

        public void LoadDepthImage(string path) {
            textures["DepthMap"] = Texture.LoadImage(path);
        }
    }
}