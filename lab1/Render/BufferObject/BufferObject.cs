using Silk.NET.OpenGL;

namespace Lab1.Render
{
    public class BufferObject<TDataType> : IDisposable
        where TDataType : unmanaged
    {
        private uint _handle;
        private BufferTargetARB _bufferType;
        private BufferUsageARB _usageType;
        private GL _gl;

        public unsafe BufferObject(GL gl, Span<TDataType> data, BufferTargetARB bufferType, BufferUsageARB usageType = BufferUsageARB.StaticDraw)
        {
            _gl = gl;
            _bufferType = bufferType;
            _usageType = usageType;

            _handle = _gl.GenBuffer();
            Bind();

            fixed (void* d = data)
            {
                _gl.BufferData(bufferType, (nuint)(data.Length * sizeof(TDataType)), d, usageType);
            }
        }

        public unsafe void Update(Span<TDataType> data)
        {
            fixed (void* d = data)
            {
                _gl.BufferData(_bufferType, (nuint)(data.Length * sizeof(TDataType)), d, _usageType);
            }
        }

        public void Bind()
        {
            _gl.BindBuffer(_bufferType, _handle);
        }

        public void Dispose()
        {
            _gl.DeleteBuffer(_handle);
        }
    }
}