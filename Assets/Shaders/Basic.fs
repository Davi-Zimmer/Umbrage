#version 330

out vec4 finalColor;

uniform float time;

void main() {

    float alpha = 0.5 + 0.5 * sin( time );

    finalColor = vec4(1.0, 0.0, 1.0, alpha);
    
}