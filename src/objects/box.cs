using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
namespace helloGraphics;
class BoxRenderer
{
    int vao;
    Shader shader;
    Matrix4[] models;
    public BoxRenderer()
    {
        vao = GL.GenVertexArray();
        shader = new Shader("resources/shaders/box.vert", "resources/shaders/box.frag");

        // generating models
        Vector3[] boxPositions = [
            new Vector3( 0.0f,  0.0f,  0.0f),
            new Vector3( 2.0f,  5.0f, -15.0f),
            new Vector3(-1.5f, -2.2f, -2.5f),
            new Vector3(-3.8f, -2.0f, -12.3f),
            new Vector3( 2.4f, -0.4f, -3.5f),
            new Vector3(-1.7f,  3.0f, -7.5f),
            new Vector3( 1.3f, -2.0f, -2.5f),
            new Vector3( 1.5f,  2.0f, -2.5f),
            new Vector3( 1.5f,  0.2f, -1.5f),
            new Vector3(-1.3f,  1.0f, -1.5f)
        ];
        models = new Matrix4[boxPositions.Length];
        for (uint i = 0; i < boxPositions.Length; i++)
        {
            float angle = 20.0f * i;
            models[i] = Matrix4.CreateTranslation(boxPositions[i] + new Vector3(1.0f, 0.5f, 1.0f));
            models[i] *= Matrix4.CreateFromAxisAngle(new Vector3(1f, 0.3f, 0.5f), MathHelper.DegreesToRadians(angle));
        }
    }
    public void Load()
    {
        GL.BindVertexArray(vao);
        // position
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);
        // color
        GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
        GL.EnableVertexAttribArray(1);
    }
    public void Draw(Renderer rnd, Matrix4 view, Matrix4 projection)
    {

        GL.BindVertexArray(vao);
        shader.Use();
        shader.SetUniform("uView", view);
        shader.SetUniform("uProjection", projection);
        shader.SetUniform("uLightColor", new Vector3(1f));
        shader.SetUniform("uLightPos", rnd.lamp.pos);
        foreach (var model in models)
        {
            shader.SetUniform("uModel", model);
            GL.DrawElements(PrimitiveType.Triangles, rnd.count, DrawElementsType.UnsignedInt, 0);
        }
    }
    public void Unload()
    {
        shader.Dispose();
    }
}