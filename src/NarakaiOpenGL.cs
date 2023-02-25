using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace NarakaiImageDepth {

    public class NarakaiOpenGL: GameWindow {

        Dictionary<string, Vector3i> textures = new Dictionary<string, Vector3i>();
        NarakaiOpenGLShader shader;

        float[] frameVertices = {
            -1f, -1f,
             1f, -1f,
             1f,  1f,

            -1f, 1f,
             1f, 1f,
            -1f, -1f,
        };

        int frameVertexArray, frameVertexObject;
        
        public NarakaiOpenGL(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings) { Run(); }

        protected override void OnLoad() {
            base.OnLoad();

            if (!GLFW.Init()) throw new NotImplementedException();

            Camera.Initialize();

            shader = NarakaiOpenGLShader.Create("SimpleDepth");

            frameVertexArray = GL.GenVertexArray();
            frameVertexObject = GL.GenBuffer();

            GL.BindVertexArray(frameVertexArray);
            LoadDepthImage("src/cube.jpeg");

            frameVertices = Texture.ImageGrid(textures["DepthMap"]);

            GL.BindBuffer(BufferTarget.ArrayBuffer, frameVertexObject);
            GL.BufferData(BufferTarget.ArrayBuffer, frameVertices.Length * sizeof(float), frameVertices, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.ProgramPointSize);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
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