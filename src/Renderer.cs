using System.Diagnostics;
using System.Security.Cryptography;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Common.Input;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
namespace helloGraphics;
class Renderer
{
    int count;
    int vertexBufferObject;
    int elementBufferObject;
    int vertexArrayObject;
    Shader shader;

    public Renderer()
    {
        vertexBufferObject = GL.GenBuffer();
        vertexArrayObject = GL.GenVertexArray();
        elementBufferObject = GL.GenBuffer();
        shader = new Shader("resources/basic.vert", "resources/basic.frag");
    }
    public void Load(float[] vertices, uint[] indices)
    {
        count = indices.Length;

        GL.BindVertexArray(vertexArrayObject);

        GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
        GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(float), indices, BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);

        GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
        GL.EnableVertexAttribArray(1);

    }
    public void Draw()
    {
        shader.Use();
        GL.DrawElements(PrimitiveType.Triangles, count, DrawElementsType.UnsignedInt, 0);
    }
    public void Unload()
    {
        shader.Dispose();
    }
}