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
    Texture texture;

    public Renderer()
    {
        vertexArrayObject = GL.GenVertexArray();
        vertexBufferObject = GL.GenBuffer();
        elementBufferObject = GL.GenBuffer();
        texture = new Texture();
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
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);
        // texture corrds
        GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
        GL.EnableVertexAttribArray(1);
        // loading texture
        texture.Load("tex1");


    }
    public void Draw(Matrix4 model, Matrix4 view, Matrix4 projection)
    {

        texture.Use(TextureUnit.Texture0);
        // texture1.Use(TextureUnit.Texture1);

        shader.Use();
        shader.SetUniform("uTexture0", 0);
        shader.SetUniform("uModel", model);
        shader.SetUniform("uView", view);
        shader.SetUniform("uProjection", projection);
        GL.BindVertexArray(vertexArrayObject);
        GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
        // GL.DrawElements(PrimitiveType.Triangles, count, DrawElementsType.UnsignedInt, 0);
    }
    public void Unload()
    {
        shader.Dispose();
    }
}