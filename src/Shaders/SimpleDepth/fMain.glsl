#version 410 core

out vec4 fragc;
in vec2 uv_coord;

uniform sampler2D depthMap;

void main() {
    fragc = texture(depthMap, uv_coord);
    //fragc = vec4(1.0, 0.0, 0.0, 1.0);
    //fragc = vec4(uv_coord, 0.0, 1.0);
}