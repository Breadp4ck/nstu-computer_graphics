// using Silk.NET.OpenGL;
// using Silk.NET.Windowing;
// using Silk.NET.Input;
// using Silk.NET.Maths;
// using System.Numerics;

// using Lab1.Scene;

// namespace Lab1.App
// {
//     public class App
//     {
//         private IWindow _window;
//         private GL? _gl;
//         private IInputContext? _input;

//         private Scene.Scene? _scene;


//         public App()
//         {
//             var options = WindowOptions.Default;

//             options.Title = "Simple Drawer";
//             options.Size = new Vector2D<int>(1280, 720);

//             _window = Window.Create(options);

//             _window.Load += OnWindowLoad;
//             _window.Resize += OnWindowResize;
//             _window.Render += OnWindowRender;
//             _window.Closing += OnWindowClosing;

//             _window.Run();
//         }

//         public void Close()
//         {
//             _window.Close();
//         }

//         private void OnWindowLoad()
//         {
//             // _context.AttachWindow(_window);
//             _gl = _window.CreateOpenGL();
//             _input = _window.CreateInput();

//             _scene = new Scene.Scene(_window, _gl, _input);
//         }

//         private void OnWindowRender(double delta)
//         {
//             _scene!.Process((float)delta);
//         }

//         private void OnWindowResize(Vector2D<int> size)
//         {

//         }

//         private void OnWindowClosing()
//         {
//             _window.Load -= OnWindowLoad;
//             _window.Resize -= OnWindowResize;
//             _window.Render -= OnWindowRender;
//             _window.Closing -= OnWindowClosing;
//         }

//         internal void MakeAllLayersTransperent(float alpha)
//         {
//             foreach (var layer in Layers)
//             {
//                 layer.Transperent = alpha;
//             }
//         }

//         internal void ChangeState(string newStateName)
//         {
//             if (!_states.ContainsKey(newStateName))
//             {
//                 return;
//             }

//             _states[CurrentState].Exit();

//             DisposeStateEventHangling(CurrentState);
//             ListenStateEventHangling(newStateName);

//             _context.Gui!.ModeName = newStateName;
//             CurrentState = newStateName;

//             _states[CurrentState].Enter();
//         }

//         private void ListenStateEventHangling(string stateName)
//         {
//             if (_context.Keyboard != null)
//             {
//                 _context.Keyboard.KeyUp += _states[stateName].OnKeyUp;
//                 _context.Keyboard.KeyDown += _states[stateName].OnKeyDown;
//             }

//             if (_context.Mouse != null)
//             {
//                 _context.Mouse.MouseUp += _states[stateName].OnMouseUp;
//                 _context.Mouse.MouseDown += _states[stateName].OnMouseDown;
//                 _context.Mouse.MouseMove += _states[stateName].OnMouseMove;
//                 _context.Mouse.Scroll += _states[stateName].OnMouseScroll;
//             }
//         }

//         private void DisposeStateEventHangling(string stateName)
//         {
//             if (_context.Keyboard != null)
//             {
//                 _context.Keyboard.KeyUp -= _states[stateName].OnKeyUp;
//                 _context.Keyboard.KeyDown -= _states[stateName].OnKeyDown;
//             }

//             if (_context.Mouse != null)
//             {
//                 _context.Mouse.MouseUp -= _states[stateName].OnMouseUp;
//                 _context.Mouse.MouseDown -= _states[stateName].OnMouseDown;
//                 _context.Mouse.MouseMove -= _states[stateName].OnMouseMove;
//                 _context.Mouse.Scroll -= _states[stateName].OnMouseScroll;
//             }
//         }
//     }
// }