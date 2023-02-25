using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace NarakaiImageDepth {

    public class NarakaiOpenGLShader {

        public int program;

        public static NarakaiOpenGLShader Create(string shaderFolder) {

            NarakaiOpenGLShader openGLShader = new NarakaiOpenGLShader();

            string v = File.ReadAllText("src/Shaders/"+shaderFolder+"/vMain.glsl");
            string f = File.ReadAllText("src/Shaders/"+shaderFolder+"/fMain.glsl");

            int vertex   = GL.CreateShader(ShaderType.VertexShader);
            int fragment = GL.CreateShader(ShaderType.FragmentShader);

            GL.ShaderSource(vertex, v);
            GL.CompileShader(vertex);

            GL.ShaderSource(fragment, f);
            GL.CompileShader(fragment);

            Console.WriteLine(GL.GetShaderInfoLog(fragment));

            openGLShader.program = GL.CreateProgram();
            GL.AttachShader(openGLShader.program, vertex);
            GL.AttachShader(openGLShader.program, fragment);
            GL.LinkProgram(openGLShader.program);

            return openGLShader;
        }

        public void SetMatrix4(string attribName, Matrix4 value) {
            int location = GL.GetUniformLocation(program, attribName);
            GL.UniformMatrix4(location, false, ref value);
        }

        public void SetInt(string attribName, int val) {
            int location = GL.GetUniformLocation(program, attribName);
            GL.Uniform1(location, val);
        }

        public void Use() {
            GL.UseProgram(program);
        }
    }
}