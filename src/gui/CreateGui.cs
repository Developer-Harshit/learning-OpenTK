using ImGuiNET;
using OpenTK.Windowing.Common;

namespace helloGraphics
{
    public class CreateGui
    {
        bool showGUI = true;
        GuiController controller;
        Game game;

        public CreateGui(Game _game)
        {
            game = _game;
            controller = new GuiController(game.ClientSize.X, game.ClientSize.Y);
        }

        public void OnLoad()
        {

        }

        public void OnResize(FramebufferResizeEventArgs args)
        {
            controller.WindowResized(game.ClientSize.X, game.ClientSize.Y);

        }
        public void OnUpdateFrame(FrameEventArgs args)
        {
            controller.Update(game, (float)args.Time);
        }

        public void OnRenderFrame(FrameEventArgs args)
        {

            ImGui.DockSpaceOverViewport(null, ImGuiDockNodeFlags.PassthruCentralNode);
            if (showGUI)
            {

                ImGui.Begin("debug", ImGuiWindowFlags.AlwaysAutoResize);
                if (ImGui.Button("Hide Panel")) showGUI = false;
                float angle = game.Angle;
                if (ImGui.SliderFloat("Angle", ref angle, 0, 360)) game.Angle = angle;
                ImGui.SliderFloat("ortho x", ref game.orthoX, 0, 20);
                ImGui.SliderFloat("ortho y", ref game.orthoY, 0, 20);

                ImGui.End();
            }
            else
            {
                ImGui.Begin("debug", ImGuiWindowFlags.AlwaysAutoResize);
                if (ImGui.Button("Show Panel")) showGUI = true;
                ImGui.End();
            }

            controller.Render();
            GuiController.CheckGLError("End of frame");
        }

        public void OnTextInput(TextInputEventArgs e)
        {
            controller.PressChar((char)e.Unicode);
        }

        public void OnMouseWheel(MouseWheelEventArgs e)
        {
            controller.MouseScroll(e.Offset);

        }
        public void OnUnload()
        {
            controller.Dispose();
        }
    }
}
