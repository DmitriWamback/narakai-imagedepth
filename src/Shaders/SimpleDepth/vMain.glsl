#version 410 core

layout(location=0) in vec2 vertex;
uniform sampler2D depthMap;

uniform mat4 projection;
uniform mat4 depthProjection;
uniform mat4 lookAt;

out vec2 uv_coord;
uniform int isOrtho = 1;

void main() {

    vec2 _vertex = vertex;
    //_vertex.x *= 10.1;
    vec2 uv = (_vertex + vec2(1.0)) / 2.0;
    uv.y = 1 - uv.y;
    float depthPoint = (texture(depthMap, vec2(uv.x, uv.y)).r) * 40;
    float x = _vertex.x * 20;
    float y = _vertex.y * 10;

    vec3 vert = vec3(x, y, depthPoint);
    uv_coord = uv;

    
    if (isOrtho == 0) { gl_Position = projection * lookAt * vec4(vert, 1.0); }
    else { gl_Position = depthProjection * lookAt * vec4(vert, 1.0); }
    gl_PointSize = 5.0;
}