#version 330 core

layout (location = 0) in vec3 vPos;

out vec4 fColor;

void main() {
    gl_Position = vec4(vPos, 1.0);
    fColor = vec4(vPos.x, 0.7, vPos.y, 1.0f);
}