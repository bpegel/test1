[System.Reflection.Assembly]::LoadWithPartialName("System.Security") > $null
[System.Reflection.Assembly]::LoadWithPartialName("System.Text.Encoding") > $null
[System.Reflection.Assembly]::LoadWithPartialName("System.Convert") > $null

$Password = Read-Host

$PasswordBytes = [System.Text.Encoding]::Unicode.GetBytes("Spymoms5%")
$SecurePassword = [Security.Cryptography.ProtectedData]::Protect($PasswordBytes, $null, [Security.Cryptography.DataProtectionScope]::LocalMachine)
$SecurePasswordStr = [System.Convert]::ToBase64String($SecurePassword)

$File = "C:\Test\Molina.txt"
#$File = "D:\Molina.DataFix.Service.SERVICEEXE\Molina.txt"
$SecurePasswordStr | Out-File $File

