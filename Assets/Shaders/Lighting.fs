/// !!!IA CODED!!!

#version 330

in vec3 fragPosition;
in vec3 fragNormal;
in vec2 fragTexCoord;
in vec4 fragColor;

uniform sampler2D texture0;
uniform vec3 lightPos;
uniform vec3 lightColor;

uniform float ambientStrength;
uniform float lightIntensity;
uniform float lightRange;

out vec4 finalColor;

void main()
{
    vec4 albedo = texture(texture0, fragTexCoord) * fragColor;

    vec3 normal = normalize(fragNormal);
    vec3 toLight = lightPos - fragPosition;

    float distanceToLight = length(toLight);
    vec3 lightDir = normalize(toLight);

    float diffuse = max(dot(normal, lightDir), 0.0);

    // Atenuação controlada pela distância máxima.
    float attenuation = clamp(
        1.0 - distanceToLight / lightRange,
        0.0,
        1.0
    );

    // Suaviza a transição até o limite da luz.
    attenuation *= attenuation;

    vec3 ambient = albedo.rgb * ambientStrength;

    vec3 direct = albedo.rgb
        * lightColor
        * diffuse
        * attenuation
        * lightIntensity;

    vec3 result = ambient + direct;

    finalColor = vec4(result, albedo.a);
}
