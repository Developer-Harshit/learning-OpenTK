using OpenTK.Graphics.OpenGL4;
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
    public Game(int width, int height, string title, WindowIcon icon) :
    base(GameWindowSettings.Default, new NativeWindowSettings() { ClientSize = (width, height), Title = title, Icon = icon })
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
        renderer = new Renderer();
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

        renderer.Load(vertices, indices);
    }
    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);
        GL.Clear(ClearBufferMask.ColorBufferBit);

        renderer.Draw();

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
    }

}
