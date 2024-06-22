#version 330 core
layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec2 aTexCoord;
uniform mat4 uModel;
uniform mat4 uView;
uniform mat4 uProjection;
out vec2 vTexCoord;
void main()
{
    vTexCoord = aTexCoord;
    gl_Position = vec4(aPosition, 1.0) * uModel * uView * uProjection ;
}