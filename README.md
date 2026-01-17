# Purpose
It's just a console application, built on .NET8, to test if Riptide.Toolkit works.
As a tester, you can ccopy this repository and test everything in this environment.

To keep DLL references valid, we recommend storing repositories like so:
`..\<SharedFolder>\RiptideToolkit\Riptide.Toolkit\...`
`..\<SharedFolder>\RiptideToolkit\Riptide.Toolkit.Examples\...`
`..\<SharedFolder>\RiptideToolkit.Testing\Riptide.Toolkit.Testing\...`

## Compatibility
You are **not** supposed to use `Riptide.Toolkit.Testing` anywhere.

`Riptide.Toolkit` is built on .NET Standard 2.0, while `Riptide.Toolkit.Testing` is built on .NET 8.
This means that apps which support `Riptide.Toolkit` might not support `Riptide.Toolkit.Testing` - Unity won't support it for sure.
