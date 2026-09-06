#version 330

in vec3 direction;

uniform sampler2D texture0;

out vec4 finalColor;

const float PI = 3.14159265359;

void main()
{
    vec3 dir = normalize(direction);

    float longitude = atan(dir.z, dir.x);
    float latitude = asin(dir.y);

    float u = longitude / (2.0 * PI) + 0.5;
    float v = 0.5 - latitude / PI;

    finalColor = texture(texture0, vec2(u, v));
}