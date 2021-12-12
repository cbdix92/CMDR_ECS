﻿#version 330 core
in vec2 TexCoords;
out vec4 colorOut;

uniform sampler2D image;
uniform vec4 color;

void main()
{    
    colorOut = color * texture(image, TexCoords);
    //colorOut = color;
}  