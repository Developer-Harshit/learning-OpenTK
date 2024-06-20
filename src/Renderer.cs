using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
namespace helloGraphics;
class Renderer
{
    int count;
    int vertexBufferObject;
    int elementBufferObject;
    int vertexArrayObject;
    Shader shader;
    Texture texture0;
    Texture texture1;
    public Renderer()
    {
        vertexArrayObject = GL.GenVertexArray();
        vertexBufferObject = GL.GenBuffer();
        elementBufferObject = GL.GenBuffer();
        texture0 = new Texture();
        texture1 = new Texture();
        shader = new Shader("resources/basic.vert", "resources/basic.frag");
    }
    public void Load(float[] vertices, uint[] indices, Matrix4 transform)
    {
        count = indices.Length;

        GL.BindVertexArray(vertexArrayObject);

        GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
        GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(float), indices, BufferUsageHint.StaticDraw);

        // position
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);
        // color
        GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));
        GL.EnableVertexAttribArray(1);
        // texture corrds
        GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));
        GL.EnableVertexAttribArray(2);
        // loading texture
        texture0.Load("tex0");
        texture1.Load("tex1");
        // setting texture and uniforms


    }
    public void Draw(Matrix4 transform)
    {
        texture0.Use(TextureUnit.Texture0);
        texture1.Use(TextureUnit.Texture1);

        shader.Use();
        shader.SetUniform("uTexture0", 0);
        shader.SetUniform("uTexture1", 1);
        shader.SetUniform("uTransform", transform);
        GL.BindVertexArray(vertexArrayObject);
        GL.DrawElements(PrimitiveType.Triangles, count, DrawElementsType.UnsignedInt, 0);
    }
    public void Unload()
    {
        shader.Dispose();
    }
}