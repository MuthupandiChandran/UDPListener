# UDPRepository
.NET Windows Form Application which operates UDP Listener
Step 1:
Run the .NET Application which act as UDP Listener

Powershell Administrator run below command:
$udpClient = New-Object System.Net.Sockets.UdpClient
$bytes = [System.Text.Encoding]::UTF8.GetBytes("Hello UDP!")
$udpClient.Send($bytes, $bytes.Length, "127.0.0.1", 9999)
