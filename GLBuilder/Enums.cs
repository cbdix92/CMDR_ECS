using System;

namespace OpenGL
{
    public enum BUFFER_BINDING_TARGET
    {
        ARRAY_BUFFER =               0x8892,
        ATOMIC_COUNTER_BUFFER =      0x92C0,
        COPY_READ_BUFFER =           0x8F36,
        COPY_WRITE_BUFFER =          0x8F37,
        DISPATCH_INDIRECT_BUFFER =   0x90EE,
        DRAW_INDIRECT_BUFFER =       0x8F3F,
        ELEMENT_ARRAY_BUFFER =       0x8893,
        PIXEL_PACK_BUFFER =          0x88EB,
        PIXEL_UNPACK_BUFFER =        0x88EC,
        QUERY_BUFFER =               0x9192,
        SHADER_STORAGE_BUFFER =      0x90D2,
        TEXTURE_BUFFER =             0x8C2A,
        TRANSFORM_FEEDBACK_BUFFER =  0x8C8E,
        UNIFORM_BUFFER =             0x8A11
    }

    public enum USAGE
    {
        STREAM_DRAW  = 0x88E0,
        STREAM_READ  = 0x88E1,
        STREAM_COPY  = 0x88E2,
        STATIC_DRAW  = 0x88E4,
        STATIC_READ  = 0x88E5,
        STATIC_COPY  = 0x88E6,
        DYNAMIC_DRAW = 0x88E8,
        DYNAMIC_READ = 0x88E9,
        DYNAMIC_COPY = 0x88EA
    }

    [Flags]
    public enum BUFFER_MASK
    {
        COLOR_BUFFER_BIT = 0X4000,
        DEPTH_BUFFER_BIT = 0X0100,
        STENCIL_BUFFER_BIT = 0X0400
    }
}
