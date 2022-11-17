using Lab1.Main.Scene3D;
using Lab1.Input;

using System.Numerics;

namespace Lab1.App
{
    public class FlyCamera3D : Camera3D
    {
        private Vector2 cameraRotation = new Vector2((float)(-System.Math.PI / 2.0f), 0.0f);
        private bool _stoped = false;

        public float Speed { get; private set; } = 20.0f;
        public FlyCamera3D(string name) : base(name) { }

        public override void Ready()
        {
            base.Ready();
        }

        public override void Process(float delta)
        {
            base.Process(delta);

            Vector3 direction = Vector3.Zero;

            if (InputServer!.IsActionPressed("movement_forward"))
            {
                direction += _cameraFront;
            }

            if (InputServer!.IsActionPressed("movement_backward"))
            {
                direction -= _cameraFront;
            }

            if (InputServer!.IsActionPressed("movement_left"))
            {
                direction -= Vector3.Cross(_cameraFront, _cameraUp);
            }

            if (InputServer!.IsActionPressed("movement_right"))
            {
                direction += Vector3.Cross(_cameraFront, _cameraUp);
            }

            if (InputServer!.IsActionPressed("movement_upward"))
            {
                direction.Y += 1.0f;
            }

            if (InputServer!.IsActionPressed("movement_down"))
            {
                direction.Y -= 1.0f;
            }

            _cameraFront = new Vector3(
                (float)(System.Math.Cos(cameraRotation.X) * System.Math.Cos(cameraRotation.Y)),
                (float)System.Math.Sin(cameraRotation.Y),
                (float)(System.Math.Sin(cameraRotation.X) * System.Math.Cos(cameraRotation.Y))
            );

            if (direction != Vector3.Zero)
            {
                direction = Vector3.Normalize(direction);
                Translate(direction * delta * Speed);
            }
        }

        public override void Input(InputEvent input)
        {
            base.Input(input);

            if (!_stoped && input is InputMouseMotion)
            {
                var offset = ((InputMouseMotion)input).Offset;

                cameraRotation.X += offset.X * 0.004f;
                cameraRotation.Y -= offset.Y * 0.004f;

                if (cameraRotation.Y > (System.Math.PI - 0.02) / 2.0)
                {
                    cameraRotation.Y = (float)(System.Math.PI - 0.02) / 2.0f;
                }
                else if (cameraRotation.Y < -(System.Math.PI - 0.02) / 2.0)
                {
                    cameraRotation.Y = -(float)(System.Math.PI - 0.02) / 2.0f;
                }
            }

            if (input is InputEventKey
                     && ((InputEventKey)input).Button == KeyboardButton.Tab
                     && input.IsInvoked)
            {
                if (_stoped)
                {
                    _stoped = false;
                    InputServer!.SetCursorMode(Silk.NET.Input.CursorMode.Disabled);
                }
                else
                {
                    _stoped = true;
                    InputServer!.SetCursorMode(Silk.NET.Input.CursorMode.Normal);
                }
            }
        }
    }
}