using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
namespace helloGraphics;
class Renderer
{
    int vbo;
    public Game game;

    public BoxRenderer box;
    public LampRenderer lamp;

    public Renderer(Game _game)
    {
        game = _game;
        vbo = GL.GenBuffer();
        box = new BoxRenderer();
        lamp = new LampRenderer();
    }
    public void Load(float[] vertices)
    {
        GL.ClearColor(0.1f, 0.1f, 0.1f, 1.0f);
        GL.Enable(EnableCap.DepthTest);

        GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);


        box.Load();
        lamp.Load();


    }
    public void Draw()
    {

        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
        Matrix4 view = game.cam.GetView();
        Matrix4 projection = game.cam.GetProjection();
        lamp.Draw(this, view, projection);
        box.Draw(this, view, projection);
    }
    public void Unload()
    {
        box.Unload();
        lamp.Unload();
    }
}