#version 330 core

layout (location = 0) in vec3 vPos;

out vec4 fColor;

void main() {
    gl_Position = vec4(vPos, 1.0);
    fColor = vec4(0.7f, vPos.xy, 1.0f);
}