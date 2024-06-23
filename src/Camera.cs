
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
namespace helloGraphics;
class Camera
{
	float speed = 5;
	public float fov = 45;
	public float pitch = 0;
	public float yaw = 0;
	public bool firstTime = true;
	public float sensitivity = 0.07f;
	public float aspect;
	public Vector3 pos;
	public Vector3 front;
	public Vector3 up;
	public Vector3 right;
	public Vector2 prevMousePos;
	public Camera(float _aspect)
	{
		pos = new Vector3(-7.0f, 0.0f, 0.0f);
		front = Vector3.UnitZ;
		up = Vector3.UnitY;
		right = Vector3.UnitX;
		aspect = _aspect;
	}
	public Matrix4 GetView()
	{
		CalculateFancyMathStuff();
		return Matrix4.LookAt(pos, pos + front, up);
	}
	public Matrix4 GetProjection()
	{
		return Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(fov), aspect, 0.1f, 100.0f);
	}
	public void Update(MouseState mouse)
	{
		if (firstTime)
		{
			prevMousePos = new Vector2(mouse.X, mouse.Y);
			firstTime = false;
			return;
		}

		var dx = mouse.X - prevMousePos.X;
		var dy = mouse.Y - prevMousePos.Y;
		yaw += dx * sensitivity;
		pitch -= dy * sensitivity;

		pitch = MathHelper.Clamp(pitch, -89f, 89f);
		prevMousePos = new Vector2(mouse.X, mouse.Y);

	}
	public void UpdateFov(float offset)
	{
		if (fov > 45.0f) fov = 45.0f;
		else if (fov < 1.0f) fov = 1.0f;
		else fov -= offset;


	}
	public void CalculateFancyMathStuff()
	{
		front.X = (float)(Math.Cos(MathHelper.DegreesToRadians(pitch)) * Math.Cos(MathHelper.DegreesToRadians(yaw)));
		front.Y = (float)Math.Sin(MathHelper.DegreesToRadians(pitch));
		front.Z = (float)(Math.Cos(MathHelper.DegreesToRadians(pitch)) * Math.Sin(MathHelper.DegreesToRadians(yaw)));

		front = Vector3.Normalize(front);
		right = Vector3.Normalize(Vector3.Cross(front, Vector3.UnitY));
		up = Vector3.Normalize(Vector3.Cross(right, front));

	}
	public void MoveZ(float scale)
	{
		pos += front * speed * scale;
	}
	public void MoveX(float scale)
	{
		pos += right * speed * scale;
	}
	public void MoveY(float scale)
	{
		pos += up * speed * scale;
	}
}