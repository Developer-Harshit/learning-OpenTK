#version 330 core

in vec3 vColor;
out vec4 FragColor;

uniform vec3 uLightColor;
uniform vec3 uLightPos;

const float lightIntensity = 0.1;
void main()
{
     vec3 ambient = uLightColor * lightIntensity;

     vec3 color = vColor * ambient;
     FragColor = vec4(color,1.0);
}  