#version 330 core

in vec3 vPosition;
in vec3 vNormal;
in vec3 vFragPos;

out vec4 FragColor;

uniform vec3 uLightColor;
uniform vec3 uLightPos;
uniform vec3 uViewPos;

const float ambientStrength = 0.1;
const float specularStrength = 0.9;
const float diffuseStrength = 0.3;

void main()
{
     vec3 color =  vPosition  + 0.5;
     
     vec3 ambient = uLightColor * ambientStrength;

     vec3 normal = normalize(vNormal);
     vec3 lightDir = normalize( uLightPos - vFragPos );
     float diffuseFactor = max(dot(normal, lightDir), 0.0);
     vec3 diffuse = uLightColor * diffuseStrength * diffuseFactor;

     vec3 viewDir = normalize(uViewPos - vFragPos);
     vec3 reflectDir = reflect(-lightDir, normal);
     float specularFactor = pow(max(dot(viewDir, reflectDir), 0.0), 16);
     vec3 specular = uLightColor * specularStrength * specularFactor;  

     color *= ambient + diffuse + specular;
     FragColor = vec4(color,1.0);
}  