#version 330 core
//layout (location = 0) in vec4 aPos;
layout (location = 0) in vec3 aPos;

//out vec2 TexCoords;

uniform mat4 model;
uniform mat4 projection;
uniform vec4 pos;

void main()
{
    //gl_Position = vec4(x, y, 0.0, 1.0);
    //TexCoords = aPos.zw;
    gl_Position = projection * model * vec4(aPos.x, aPos.y, aPos.z, 1.0);
}