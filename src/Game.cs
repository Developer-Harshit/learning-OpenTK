using System.Security.Cryptography;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;


namespace helloGraphics;

public class Game : GameWindow
{
    float[] vertices;
    uint[] indices;
    int vertexBufferObject;
    int elementBufferObject;
    int vertexArrayObject;
    Shader? shader;
    public Game(int width, int height, string title) :
    base(GameWindowSettings.Default, new NativeWindowSettings() { ClientSize = (width, height), Title = title })
    {
        // Anti-CLock Wise
        vertices = [
            -0.5f,0.5f,0.0f,
            -0.5f,-0.5f,0.0f,
            0.5f,-0.5f,0.0f,
            0.5f,0.5f,0.0f,
        ];
        indices = [
            0,1,3,
            1,2,3
        ];


    }
    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        base.OnUpdateFrame(args);
        if (KeyboardState.IsKeyDown(Keys.Escape)) Close();
    }

    protected override void OnLoad()
    {
        base.OnLoad();
        GL.ClearColor(0.2f, 0.9f, 0.6f, 1.0f);

        // doing buffer stuff
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
    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);
        GL.Clear(ClearBufferMask.ColorBufferBit);

        shader?.Use();
        GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);

        SwapBuffers();
    }
    protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
    {
        base.OnFramebufferResize(e);
        GL.Viewport(0, 0, e.Width, e.Height);
    }
    protected override void OnUnload()
    {
        base.OnUnload();
        shader?.Dispose();



    }

}
