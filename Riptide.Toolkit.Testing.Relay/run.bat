echo off
start "" "..\Riptide.Toolkit.Testing.Relay.Server\bin\Debug\net8.0\Riptide.Toolkit.Testing.Relay.Server.exe"
timeout 2
start "" "..\Riptide.Toolkit.Testing.Relay.Client\bin\Debug\net8.0\Riptide.Toolkit.Testing.Relay.Client.exe"
pause