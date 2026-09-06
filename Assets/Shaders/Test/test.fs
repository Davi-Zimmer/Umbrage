#version 330

in vec3 fragNormal;
in vec3 fragPosition;

out vec4 finalColor;

void main()
{
    vec3 normal = normalize(fragNormal);

    flaot x = 0;

    // Direção da luz
    vec3 lightDirection = normalize(vec3(-0.5, 1.0, x ));

    float light = max(dot(normal, lightDirection), 0.0);

    // Luz ambiente
    float ambient = 0.25;

    float brightness = ambient + light * 0.75;

    vec3 baseColor = vec3(0.65, 0.68, 0.72);

    finalColor = vec4(baseColor * brightness, 1.0);
    

}