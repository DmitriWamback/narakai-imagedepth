using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace NarakaiImageDepth {

    class Cube {

        float[] vertices = {
            -1.0f, -1.0f, -1.0f, /* POSITIONS */  0.0f,  0.0f, -1.0f, /* NORMALS */ 0.0f, 0.0f /* UV COORDS */,
             1.0f,  1.0f, -1.0f,                  0.0f,  0.0f, -1.0f,               1.0f, 1.0f,
             1.0f, -1.0f, -1.0f,                  0.0f,  0.0f, -1.0f,               1.0f, 0.0f,
             1.0f,  1.0f, -1.0f,                  0.0f,  0.0f, -1.0f,               1.0f, 1.0f,
            -1.0f, -1.0f, -1.0f,                  0.0f,  0.0f, -1.0f,               0.0f, 0.0f,
            -1.0f,  1.0f, -1.0f,                  0.0f,  0.0f, -1.0f,               0.0f, 1.0f,
                
            -1.0f, -1.0f,  1.0f,                  0.0f,  0.0f,  1.0f,               1.0f, 0.0f, 
             1.0f, -1.0f,  1.0f,                  0.0f,  0.0f,  1.0f,               0.0f, 0.0f, 
             1.0f,  1.0f,  1.0f,                  0.0f,  0.0f,  1.0f,               0.0f, 1.0f, 
             1.0f,  1.0f,  1.0f,                  0.0f,  0.0f,  1.0f,               0.0f, 1.0f, 
            -1.0f,  1.0f,  1.0f,                  0.0f,  0.0f,  1.0f,               1.0f, 1.0f, 
            -1.0f, -1.0f,  1.0f,                  0.0f,  0.0f,  1.0f,               1.0f, 0.0f, 
                
            -1.0f,  1.0f,  1.0f,                 -1.0f,  0.0f,  0.0f,               0.0f, 1.0f, 
            -1.0f,  1.0f, -1.0f,                 -1.0f,  0.0f,  0.0f,               1.0f, 1.0f,
            -1.0f, -1.0f, -1.0f,                 -1.0f,  0.0f,  0.0f,               1.0f, 0.0f, 
            -1.0f, -1.0f, -1.0f,                 -1.0f,  0.0f,  0.0f,               1.0f, 0.0f, 
            -1.0f, -1.0f,  1.0f,                 -1.0f,  0.0f,  0.0f,               0.0f, 0.0f,
            -1.0f,  1.0f,  1.0f,                 -1.0f,  0.0f,  0.0f,               0.0f, 1.0f, 
                
             1.0f,  1.0f,  1.0f,                  1.0f,  0.0f,  0.0f,               0.0f, 1.0f, 
             1.0f, -1.0f, -1.0f,                  1.0f,  0.0f,  0.0f,               1.0f, 0.0f,
             1.0f,  1.0f, -1.0f,                  1.0f,  0.0f,  0.0f,               1.0f, 1.0f,
             1.0f, -1.0f, -1.0f,                  1.0f,  0.0f,  0.0f,               1.0f, 0.0f,
             1.0f,  1.0f,  1.0f,                  1.0f,  0.0f,  0.0f,               0.0f, 1.0f,
             1.0f, -1.0f,  1.0f,                  1.0f,  0.0f,  0.0f,               0.0f, 0.0f,
                
            -1.0f, -1.0f, -1.0f,                  0.0f, -1.0f,  0.0f,               0.0f, 1.0f,
             1.0f, -1.0f, -1.0f,                  0.0f, -1.0f,  0.0f,               1.0f, 1.0f,
             1.0f, -1.0f,  1.0f,                  0.0f, -1.0f,  0.0f,               1.0f, 0.0f,
             1.0f, -1.0f,  1.0f,                  0.0f, -1.0f,  0.0f,               1.0f, 0.0f,
            -1.0f, -1.0f,  1.0f,                  0.0f, -1.0f,  0.0f,               0.0f, 0.0f,
            -1.0f, -1.0f, -1.0f,                  0.0f, -1.0f,  0.0f,               0.0f, 1.0f,
                
            -1.0f,  1.0f, -1.0f,                  0.0f,  1.0f,  0.0f,               0.0f, 1.0f,
             1.0f,  1.0f , 1.0f,                  0.0f,  1.0f,  0.0f,               1.0f, 0.0f,
             1.0f,  1.0f, -1.0f,                  0.0f,  1.0f,  0.0f,               1.0f, 1.0f,
             1.0f,  1.0f,  1.0f,                  0.0f,  1.0f,  0.0f,               1.0f, 0.0f,
            -1.0f,  1.0f, -1.0f,                  0.0f,  1.0f,  0.0f,               0.0f, 1.0f,
            -1.0f,  1.0f,  1.0f,                  0.0f,  1.0f,  0.0f,               0.0f, 0.0f
        };
        int vertexArrayObject;
        int vertexBufferObject;
        float time;

        public Vector3 position = new Vector3(0);
        public Vector3 rotation = new Vector3(0);
        public Vector3 scale = new Vector3(0.5f);

        public Cube() {

            vertexArrayObject = GL.GenVertexArray();
            vertexBufferObject = GL.GenBuffer();

            GL.BindVertexArray(vertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));
            GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));

            GL.EnableVertexAttribArray(0); GL.EnableVertexAttribArray(1); GL.EnableVertexAttribArray(2);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            position.Z = 10;
            position.Y = -10;
            GL.BindVertexArray(0);
        }

        public void Render(NarakaiOpenGLShader simple3DShader) {
            
            GL.BindVertexArray(vertexArrayObject);
            time = 10f;

            simple3DShader.SetMatrix4("model", Matrix4.CreateTranslation(position));
            GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
        }
    }
}