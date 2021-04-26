cd C:\Users\roberto\Documents\Development\HCDv2010\HCDv2\Content\Certificate
rem makecert -n "CN=mdcomRoot" -r -sv mdcomRoot.pvk mdcomRoot.cer 
@echo Install root certificate
pause
rem makecert -crl -n "CN=mdcomRoot" -r -sv mdcomRoot.pvk mdcomRoot.crl
@echo Install root revocation list
pause
makecert -sk mdcomKey -iv mdcomRoot.pvk -n "CN=mdcom" -ic mdcomRoot.cer -sr LocalMachine -ss My -sky exchange -pe
pause
rem FindPrivateKey.exe My LocalMachine –n "CN=SI2TempCert" 
rem cacls.exe "C:\Documents and Settings\All Users\Application Data\Microsoft\Crypto\RSA\Machinekeys\4d657b73466481beba7b0e1b5781db81_c225a308-d2ad-4e58-91a8-6e87f354b030" /E /G "NT AUTHORITY\NETWORK SERVICE":R 