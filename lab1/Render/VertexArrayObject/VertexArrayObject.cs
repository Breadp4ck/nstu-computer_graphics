using Silk.NET.OpenGL;

namespace Lab1.Render
{
    public class VertexArrayObject<TVertexType, TIndexType> : IDisposable
        where TVertexType : unmanaged
        where TIndexType : unmanaged
    {
        private uint _handle;
        private GL _gl;

        private BufferObject<TVertexType> _vbo;
        private BufferObject<TIndexType> _ebo;

        public VertexArrayObject(GL gl, BufferObject<TVertexType> vbo, BufferObject<TIndexType> ebo)
        {
            _gl = gl;
            _vbo = vbo;
            _ebo = ebo;

            _handle = _gl.GenVertexArray();
            Bind();

            vbo.Bind();
            ebo.Bind();
        }

        public unsafe void VertexAttributePointer(uint index, int count, VertexAttribPointerType type, uint stride, int offset)
        {
            _gl.VertexAttribPointer(index, count, type, false, stride * (uint)sizeof(TVertexType), (void*)(offset * sizeof(TVertexType)));
            _gl.EnableVertexAttribArray(index);
        }

        public void Bind()
        {
            _gl.BindVertexArray(_handle);
        }

        public void Dispose()
        {
            _gl.DeleteVertexArray(_handle);
        }
    }
}