#version 330 core

in vec4 fColor;

out vec4 FragColor;

void main() {
    FragColor = vec4(0.7f, 0.3f + fColor.y/2.0, 0.7f, 1.0f);;
}