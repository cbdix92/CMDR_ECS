#version 330 core
layout (location = 0) in vec3 vertex;
layout (location = 1) in vec2 inUV;
layout (location = 2) in vec3 inNormal;

out vec2 uv;
out vec3 normal;
out vec3 fragPos;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    uv = inUV;
    normal = mat3(transpose(inverse(model))) * inNormal;
    fragPos = vec3(model * vec4(vertex.xyx, 1.0));
    gl_Position = projection * model * view * vec4(vertex.xyz, 1.0);
}