
C:\Windows\Microsoft.NET\Framework64\v2.0.50727\InstallUtil.exe

1. 서비스 등록
installutil WindowsService1.exe

2. 서비스 실행
net start myservice

3. 서비스 제거
installutil /u WindowsService1.exe

3.5. 윈도우 방화벽 해당 포트 개방

4. test on linux
echo "echo test...">/dev/udp/ip/port
echo "echo test...">/dev/udp/172.30.1.111/9999

5. reboot windows
echo 'rebootnow!!'>/dev/udp/172.30.1.111/9999
