using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
namespace helloGraphics;
class Renderer
{
    public int count;
    int vertexBufferObject;
    int elementBufferObject;

    public BoxRenderer box;
    public LampRenderer lamp;

    public Renderer()
    {

        vertexBufferObject = GL.GenBuffer();
        elementBufferObject = GL.GenBuffer();
        box = new BoxRenderer();
        lamp = new LampRenderer();
    }
    public void Load(float[] vertices, uint[] indices)
    {
        count = indices.Length;
        GL.ClearColor(0.1f, 0.1f, 0.1f, 1.0f);
        GL.Enable(EnableCap.DepthTest);

        GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
        GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
        box.Load();
        lamp.Load();


    }
    public void Draw(Camera cam)
    {

        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
        Matrix4 view = cam.GetView();
        Matrix4 projection = cam.GetProjection();
        lamp.Draw(this, view, projection);
        box.Draw(this, view, projection);
    }
    public void Unload()
    {
        box.Unload();
        lamp.Unload();
    }
}