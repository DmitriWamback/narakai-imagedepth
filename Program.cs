using NarakaiImageDepth;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

NarakaiOpenGL narakaiOpenGL = new NarakaiOpenGL(new GameWindowSettings(),
                                                new NativeWindowSettings() {
                                                    APIVersion  = new Version(4, 1),
                                                    Profile     = ContextProfile.Core,
                                                    Flags       = ContextFlags.ForwardCompatible,
                                                    Title       = "Concepts",
                                                    Size        = new Vector2i(1200, 800),
                                                    NumberOfSamples = 14
                                                });

