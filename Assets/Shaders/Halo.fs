#version 330

in vec2 fragTexCoord;
out vec4 finalColor;

uniform vec3 coreColor;
uniform vec3 fadeColor;
uniform float coreRadius;
uniform float intensity;

void main()
{
    vec2 centered = (fragTexCoord - vec2(0.5)) * 2.0;
    float dist = length(centered);

    float gradient = smoothstep(coreRadius, 1.0, dist);
    vec3 color = mix(coreColor, fadeColor, gradient);

    float alpha = 1.0 - smoothstep(0.0, 1.0, dist);
    alpha *= intensity;

    finalColor = vec4(color, alpha);
}