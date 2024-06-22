using System.Numerics;
using ImGuiNET;
using Veldrid;
using Veldrid.Sdl2;
using Veldrid.StartupUtilities;
namespace helloGraphics;
class CreateGui
{
    readonly Game game;
    readonly GraphicsDevice gd;
    readonly CommandList cl;
    readonly Sdl2Window wnd;
    readonly ImGuiRenderer renderer;
    public bool visible = true;
    public CreateGui(Game _game, int width, int height, string title)
    {
        game = _game;
        var windowInfo = new WindowCreateInfo(50, 50, width, height, Veldrid.WindowState.Normal, title);
        VeldridStartup.CreateWindowAndGraphicsDevice(windowInfo, out wnd, out gd);

        renderer = new ImGuiRenderer(
            gd, gd.MainSwapchain.Framebuffer.OutputDescription,
            (int)gd.MainSwapchain.Framebuffer.Width,
            (int)gd.MainSwapchain.Framebuffer.Height
        );

        cl = gd.ResourceFactory.CreateCommandList();
        ImGui.SetNextWindowPos(new Vector2(0, 0));
        wnd.Resizable = false;
    }
    public CreateGui(Game _game, string title)
    {
        game = _game;
        var windowInfo = new WindowCreateInfo(50, 50, 500, 500, Veldrid.WindowState.Maximized, title);
        VeldridStartup.CreateWindowAndGraphicsDevice(windowInfo, out wnd, out gd);
        renderer = new ImGuiRenderer(
            gd, gd.MainSwapchain.Framebuffer.OutputDescription,
            (int)gd.MainSwapchain.Framebuffer.Width,
            (int)gd.MainSwapchain.Framebuffer.Height
        );
        cl = gd.ResourceFactory.CreateCommandList();
        wnd.Resizable = false;
    }

    public void Run()
    {
        var input = wnd.PumpEvents();
        renderer.Update(1f / 60f, input);

        // Draw stuff
        ImGui.Begin("Debug", ImGuiWindowFlags.AlwaysAutoResize | ImGuiWindowFlags.AlwaysUseWindowPadding);

        float angle = game.Angle;
        if (ImGui.SliderFloat("Angle", ref angle, 0, 360)) game.Angle = angle;
        ImGui.End();

        // do renderer stuff
        cl.Begin();
        cl.SetFramebuffer(gd.MainSwapchain.Framebuffer);
        cl.ClearColorTarget(0, RgbaFloat.Black);
        renderer.Render(gd, cl);
        cl.End();

        gd.SubmitCommands(cl);
        gd.SwapBuffers(gd.MainSwapchain);
    }
}