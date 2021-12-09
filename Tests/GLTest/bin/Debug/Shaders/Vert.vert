#version 330 core
layout (location = 0) in vec3 aPos;

out vec2 TexCoords;

void main()
{
    TexCoords = vec2(aPos.x, aPos.y);
    gl_Position = vec4(aPos.x, aPos.y, aPos.z, 1.0);
}