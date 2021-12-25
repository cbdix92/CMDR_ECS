#version 330 core
layout (location = 0) in vec3 vertex;
layout (location = 1) in vec2 inTexCoord;
layout (location = 2) in vec3 inNormals;

out vec2 texCoords;
out vec3 normals;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    texCoords = inTexCoord;
    normals = inNormals;
    gl_Position = projection * model * view * vec4(vertex.xyz, 1.0);
}