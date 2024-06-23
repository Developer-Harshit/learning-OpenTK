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
    public float orthoX;
    public float orthoY;
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
            -0.5f, -0.5f, -0.5f,  0.0f, 0.0f,
            0.5f, -0.5f, -0.5f,  1.0f, 0.0f,
            0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
            0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
            -0.5f,  0.5f, -0.5f,  0.0f, 1.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, 0.0f,

            -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
            0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
            0.5f,  0.5f,  0.5f,  1.0f, 1.0f,
            0.5f,  0.5f,  0.5f,  1.0f, 1.0f,
            -0.5f,  0.5f,  0.5f,  0.0f, 1.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,

            -0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
            -0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
            -0.5f,  0.5f,  0.5f,  1.0f, 0.0f,

            0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
            0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
            0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
            0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
            0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
            0.5f,  0.5f,  0.5f,  1.0f, 0.0f,

            -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
            0.5f, -0.5f, -0.5f,  1.0f, 1.0f,
            0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
            0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,

            -0.5f,  0.5f, -0.5f,  0.0f, 1.0f,
            0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
            0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
            0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
            -0.5f,  0.5f,  0.5f,  0.0f, 0.0f,
            -0.5f,  0.5f, -0.5f,  0.0f, 1.0f
        ];
        // im not using indices for now
        indices = [
           0,1,2,
           0,2,3
        ];
        Angle = 0;
        orthoX = 2f;
        orthoY = 2f;
        Matrix4 rotation = Matrix4.CreateRotationX(MathHelper.DegreesToRadians((int)Angle));
        Matrix4 scale = Matrix4.CreateScale(0.5f);
        transform = rotation * scale;
        renderer = new Renderer();
        gui = new CreateGui(this);
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        base.OnUpdateFrame(args);
        if (KeyboardState.IsKeyDown(Keys.Escape)) Close();
        gui.OnUpdateFrame(args);
    }

    protected override void OnLoad()
    {
        base.OnLoad();
        GL.ClearColor(0.1f, 0.1f, 0.1f, 1.0f);
        GL.Enable(EnableCap.DepthTest);

        renderer.Load(vertices, indices, transform);
    }
    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        Matrix4 model = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(-angle));
        Matrix4 view = Matrix4.CreateTranslation(0.0f, 0.0f, -3.0f);
        // Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), (float)ClientSize.X / ClientSize.Y, 0.1f, 100.0f);
        Matrix4 projection = Matrix4.CreateOrthographicOffCenter(-orthoX, orthoX, -orthoY, orthoY, 0.01f, 100f);

        renderer.Draw(model, view, projection);
        gui.OnRenderFrame(args);
        SwapBuffers();
    }
    protected override void OnFramebufferResize(FramebufferResizeEventArgs args)
    {
        base.OnFramebufferResize(args);
        GL.Viewport(0, 0, args.Width, args.Height);
        gui.OnResize(args);
    }
    protected override void OnUnload()
    {
        base.OnUnload();
        renderer.Unload();
        gui.OnUnload();
    }

}
