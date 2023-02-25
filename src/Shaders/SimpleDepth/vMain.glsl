#version 410 core

layout(location=0) in vec2 vertex;
uniform sampler2D depthMap;

uniform mat4 projection;
uniform mat4 lookAt;

out vec2 uv_coord;

void main() {

    vec2 uv = (vertex + vec2(1.0)) / 2.0;
    float depthPoint = (texture(depthMap, vec2(uv.x, 1 - uv.y)).r) * 2;
    float x = vertex.x * 10;
    float y = vertex.y * 10;

    vec3 vert = vec3(-x, -y, depthPoint);
    uv_coord = uv;

    gl_Position = projection * lookAt * vec4(vert, 1.0);
    //gl_Position = vec4(vertex, 0.0, 1.0);
    gl_PointSize = 10.0;
}