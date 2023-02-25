#version 410 core

layout(location=0) in vec2 vertex;

out vec2 uv;

void main() {
    uv = (vertex + vec2(1)) / 2;
    gl_Position = vec4(vertex, 0.0, 1.0); 
}