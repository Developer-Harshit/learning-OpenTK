using System.Security.Cryptography;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Common.Input;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
namespace helloGraphics;
class Renderer
{
    int vertexBufferObject;
    int elementBufferObject;
    int vertexArrayObject;

    Shader? shader;
    int count;

    public void Load(float[] vertices, uint[] indices)
    {
        count = indices.Length;
        vertexBufferObject = GL.GenBuffer();
        vertexArrayObject = GL.GenVertexArray();
        elementBufferObject = GL.GenBuffer();
        shader = new Shader("resources/basic.vert", "resources/basic.frag");

        // 1. bind VAO
        GL.BindVertexArray(vertexArrayObject);
        // 2. copy vertices into buffer 
        GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
        // 3. set element buffer
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
        GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(float), indices, BufferUsageHint.StaticDraw);
        // 4. set attributes pointer
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);
        GL.BindVertexArray(vertexArrayObject);


    }
    public void Draw()
    {
        shader?.Use();
        GL.DrawElements(PrimitiveType.Triangles, count, DrawElementsType.UnsignedInt, 0);
    }
    public void Unload()
    {
        shader?.Dispose();
    }
}