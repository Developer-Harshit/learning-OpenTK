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
    Camera cam;
    Renderer renderer;
    CreateGui gui;
    public Game(int width, int height, string title, WindowIcon icon) :
    base(GameWindowSettings.Default, new NativeWindowSettings() { ClientSize = (width, height), Title = title, Icon = icon })
    {

        vertices = [
            // positions          // color
            -0.5f, -0.5f, -0.5f,  1.0f,0.0f,1.0f, // magenta
            +0.5f, -0.5f, -0.5f,  0.0f,0.0f,1.0f, // blue
            +0.5f,  0.5f, -0.5f,  0.0f,1.0f,1.0f, // cyan
            -0.5f,  0.5f, -0.5f,  1.0f,1.0f,1.0f, // white
            -0.5f, -0.5f,  0.5f,  1.0f,0.0f,0.0f, // red
            +0.5f, -0.5f,  0.5f,  0.0f,0.0f,0.0f, // black
            +0.5f,  0.5f,  0.5f,  0.0f,1.0f,0.0f, // green
            -0.5f,  0.5f,  0.5f,  1.0f,1.0f,0.0f, // yellow
        ];
        // Anti-CLock Wise
        indices = [
            0,1,2,2,3,0,
            4,5,6,6,7,4,
            7,3,0,0,4,7,
            6,2,1,1,5,6,
            0,1,5,5,4,0,
            3,2,6,6,7,3,
        ];


        cam = new Camera((float)ClientSize.X / ClientSize.Y);
        renderer = new Renderer();
        gui = new CreateGui(this);
        CursorState = CursorState.Grabbed;
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        if (!IsFocused) return;
        base.OnUpdateFrame(args);
        var kstate = KeyboardState;
        float dt = (float)args.Time;
        if (kstate.IsKeyDown(Keys.Escape)) Close();
        if (kstate.IsKeyDown(Keys.W)) cam.MoveZ(dt);
        if (kstate.IsKeyDown(Keys.S)) cam.MoveZ(-dt);
        if (kstate.IsKeyDown(Keys.A)) cam.MoveX(-dt);
        if (kstate.IsKeyDown(Keys.D)) cam.MoveX(dt);
        if (kstate.IsKeyDown(Keys.Space)) cam.MoveY(-dt);
        if (kstate.IsKeyDown(Keys.LeftShift)) cam.MoveY(dt);

        cam.Update(MouseState);
        gui.OnUpdateFrame(args);
    }

    protected override void OnLoad()
    {
        base.OnLoad();
        renderer.Load(vertices, indices);
    }
    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);
        renderer.Draw(cam);
        gui.OnRenderFrame(args);
        SwapBuffers();
    }
    protected override void OnFramebufferResize(FramebufferResizeEventArgs args)
    {
        base.OnFramebufferResize(args);
        GL.Viewport(0, 0, args.Width, args.Height);
        cam.aspect = (float)args.Width / args.Height;
        gui.OnResize(args);
    }
    protected override void OnMouseMove(MouseMoveEventArgs e)
    {
        base.OnMouseMove(e);
    }
    protected override void OnMouseWheel(MouseWheelEventArgs args)
    {
        base.OnMouseWheel(args);
        cam.Fov -= args.OffsetY * 4;
        gui.OnMouseWheel(args);
    }
    protected override void OnTextInput(TextInputEventArgs args)
    {
        base.OnTextInput(args);
        gui.OnTextInput(args);
    }
    protected override void OnUnload()
    {
        base.OnUnload();
        renderer.Unload();
        gui.OnUnload();
    }

}
