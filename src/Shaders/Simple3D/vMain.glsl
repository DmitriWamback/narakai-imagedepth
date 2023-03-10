#version 410 core

layout(location=0) in vec3 vertex;
layout(location=1) in vec3 normal;
layout(location=2) in vec2 uv;

uniform mat4 projection;
uniform mat4 depthProjection;
uniform mat4 lookAt;
uniform mat4 model;

out VERTEX {
    vec3 fragp;
    vec3 normal;
    vec2 uv;
} o;
uniform int isOrtho = 1;

void main() {

    o.fragp = (model * vec4(vertex, 1.0)).xyz;
    o.normal = mat3(transpose(inverse(model))) * normal;  
    o.uv = uv;

    if (isOrtho == 0) { gl_Position = projection * lookAt * model * vec4(vertex, 1.0); }
    else { gl_Position = depthProjection * lookAt * model * vec4(vertex, 1.0); }
}