#version 330 core
layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec3 aColor;
layout (location = 1) in vec2 aTexCoord;
out vec3 vColor;
out vec2 vTexCoord;
void main()
{
    vColor = aColor;
    vTexCoord = aTexCoord;
    gl_Position = vec4(aPosition, 1.0);   
}