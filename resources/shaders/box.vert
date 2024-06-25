#version 330 core

layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec3 aNormal;

out vec3 vNormal;
out vec3 vPosition;
out vec3 vFragPos;

uniform mat4 uModel;
uniform mat4 uView;
uniform mat4 uProjection;

void main()
{
    vPosition = aPosition;
    vNormal = mat3(transpose(inverse(uModel))) * aNormal; // inverse is costly calculation
    vFragPos =vec3(uModel * vec4(aPosition, 1.0));
    gl_Position = vec4(aPosition, 1.0) * uModel * uView * uProjection;
}