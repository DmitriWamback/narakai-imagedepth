#version 410 core

out vec4 fragc;

in VERTEX {
    vec3 fragp;
    vec3 normal;
    vec2 uv;
} i;

void main() {

    vec3 lightPosition = vec3(10, 10, 10);

    vec3 lightDir = -normalize(i.fragp - lightPosition);

    float diff = max(dot(i.normal, lightDir), 0.0);
    vec3 diffuse = vec3(diff);

    fragc = vec4(diffuse + vec3(0.1), 1.0);
}