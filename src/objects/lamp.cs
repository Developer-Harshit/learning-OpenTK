using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
namespace helloGraphics;
class LampRenderer
{
    int vao;
    Shader shader;
    Matrix4 model;
    public Vector3 pos;
    public LampRenderer()
    {
        vao = GL.GenVertexArray();
        shader = new Shader("resources/shaders/lamp.vert", "resources/shaders/lamp.frag");
        pos = new Vector3(1f, 3f, 2f);
        model = Matrix4.Identity;
        model *= Matrix4.CreateScale(0.6f);
        model *= Matrix4.CreateTranslation(pos);
    }
    public void Load()
    {
        GL.BindVertexArray(vao);
        // position
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);
    }
    public void Draw(Renderer rnd, Matrix4 view, Matrix4 projection)
    {
        GL.BindVertexArray(vao);
        shader.Use();
        shader.SetUniform("uView", view);
        shader.SetUniform("uProjection", projection);
        shader.SetUniform("uModel", model);
        GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
    }
    public void Unload()
    {
        shader.Dispose();
    }
}