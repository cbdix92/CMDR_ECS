#version 430 core
in vec2 TexCoords;
out vec4 colorOut;

uniform sampler2D image;
uniform vec4 Color;

void main()
{    
    colorOut = Color * texture(image, TexCoords);
}  