#version 330 core

in vec2 uv;
in vec3 normal;
in vec3 fragPos;

out vec4 colorOut;

uniform sampler2D image;
uniform vec4 color;
uniform vec3 lightPos;
uniform vec4 lightColor;
uniform vec3 viewPos;

void main()
{
    
    float ambientStrength = 0.1;
    vec4 ambient = ambientStrength * lightColor;

    vec3 norm = normalize(normal);
    vec3 lightDir = normalize(lightPos - fragPos);
    float diff = max(dot(norm, lightDir), 0.0);
    vec4 diffuse = diff * lightColor;
    
    float specularStrength = 0.5;
    vec3 viewDir = normalize(viewPos - fragPos);
    vec3 reflectDir = reflect(-lightDir, norm);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), 32);
    vec4 specular = specularStrength * spec * lightColor;

    //colorOut = color * texture(image, TexCoords);
    vec4 result = (ambient + diffuse + specular) * color;
    colorOut = result;
}  