using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Common.Input;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace helloGraphics;

public class Game : GameWindow
{
    float[] vertices;
    uint[] indices;

    Renderer renderer;
    Matrix4 transform;
    public float angle;
    public float Angle
    {
        get { return angle; }
        set
        {
            angle = value;
            Matrix4 rotation = Matrix4.CreateRotationX(MathHelper.DegreesToRadians((int)Angle));
            Matrix4 scale = Matrix4.CreateScale(0.5f);
            transform = rotation * scale;
        }
    }
    CreateGui gui;
    public Game(int width, int height, string title, WindowIcon icon) :
    base(GameWindowSettings.Default, new NativeWindowSettings() { ClientSize = (width, height), Title = title, Icon = icon })
    {
        // Anti-CLock Wise
        vertices = [
        // positions          // colors           // texture coords
        -1.0f, -1.0f, 0.0f,   0.0f, 0.0f, 0.0f,   0.0f, 0.0f,
         1.0f, -1.0f, 0.0f,   1.0f, 1.0f, 0.0f,   1.0f, 0.0f,
         1.0f,  1.0f, 0.0f,   0.0f, 1.0f, 1.0f,   1.0f, 1.0f,
        -1.0f,  1.0f, 0.0f,   0.0f, 0.0f, 1.0f,   0.0f, 1.0f,
        ];
        indices = [
           0,1,2,
           0,2,3
        ];
        Angle = 0;
        Matrix4 rotation = Matrix4.CreateRotationX(MathHelper.DegreesToRadians((int)Angle));
        Matrix4 scale = Matrix4.CreateScale(0.5f);
        transform = rotation * scale;
        renderer = new Renderer();
        gui = new CreateGui(this, 500, 300, "Control");
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        base.OnUpdateFrame(args);
        if (KeyboardState.IsKeyDown(Keys.Escape)) Close();
        gui.Run();
    }

    protected override void OnLoad()
    {
        base.OnLoad();
        GL.ClearColor(0.1f, 0.1f, 0.1f, 1.0f);

        renderer.Load(vertices, indices, transform);
    }
    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);
        GL.Clear(ClearBufferMask.ColorBufferBit);

        renderer.Draw(transform);

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
        renderer.Unload();
        gui.visible = false;
    }

}
