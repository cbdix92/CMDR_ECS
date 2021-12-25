#version 330 core

in vec2 uv;
in vec3 normal;
in vec3 fragPos;

out vec4 colorOut;

uniform sampler2D image;
uniform vec4 color;
uniform vec3 lightPos;
uniform vec4 lightColor;

void main()
{    
    float ambient = 0.5;
    vec3 lightDir = normalize(lightPos - fragPos);
    float diff = max(dot(normalize(normal), lightDir), 0.0);
    vec4 diffuse = diff * lightColor;
    //colorOut = color * texture(image, TexCoords);
    vec4 result = (ambient + diffuse) * color;
    colorOut = result;
}  