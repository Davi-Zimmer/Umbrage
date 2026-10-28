Version.Load();

Window.Config();

Texture.Load();

Shaders.Load();

// Model3D.LoadModels();

Window.StartUpdate( Game.Update );

Texture.UnLoad();

Shaders.Unload();
