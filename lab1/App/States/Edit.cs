// using Silk.NET.Input;

// namespace Lab1.App.States
// {
//     public class Edit : AppState
//     {
//         public Edit(App app) : base(app) { }

//         public override void Enter()
//         {
//             _app.MakeAllLayersTransperent(0.2f);
//             _app.Layers[_app.LayerID].Transperent = 1.0f;

//             _app.Layers[_app.LayerID].ApplyLayerTransform = false;
//             _app.AddHoverVertex();
//         }

//         public override void Exit()
//         {
//             _app.Layers[_app.LayerID].ApplyLayerTransform = true;
//             _app.RemoveHoverVertex();
//         }

//         public override void OnKeyDown(IKeyboard keyboard, Key key, int arg3)
//         {
//             switch (key)
//             {
//                 case Key.Escape:
//                     _app.ChangeState("Workspace");
//                     break;
//             }
//         }

//         public override void OnMouseDown(IMouse mouse, MouseButton button)
//         {
//             switch (button)
//             {
//                 case MouseButton.Left:
//                     _app.AddVertexByMousePosition();
//                     break;

//                 case MouseButton.Right:
//                     _app.Layers[_app.LayerID].RemoveLastVertex();
//                     break;
//             }
//         }

//         public override void OnMouseMove(IMouse mouse, System.Numerics.Vector2 position)
//         {
//             _app.UpdateHoverVertexPosition();
//         }

//         public override void OnMouseScroll(IMouse _mouse, ScrollWheel scroll)
//         {
//             _app.UpdateHoverVertexPosition();
//         }
//     }
// }