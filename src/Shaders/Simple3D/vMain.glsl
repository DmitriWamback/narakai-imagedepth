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

void main() {

    o.fragp = (model * vec4(vertex, 1.0)).xyz;
    o.normal = normalize(normal);
    o.uv = uv;
    gl_Position = depthProjection * lookAt * model * vec4(vertex, 1.0);
}