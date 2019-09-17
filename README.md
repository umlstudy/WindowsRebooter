
0. installutil 파일 위치<br/>
C:\Windows\Microsoft.NET\Framework64\v2.0.50727\InstallUtil.exe<br/>

1. 서비스 등록<br/>
installutil WindowsService1.exe<br/>

2. 서비스 실행<br/>
net start myservice<br/>

3. 서비스 제거<br/>
installutil /u WindowsService1.exe<br/>

4. 윈도우 방화벽 해당 포트 개방<br/>

5. test on linux<br/>
echo "echo test...">/dev/udp/ip/port<br/>
echo "echo test...">/dev/udp/172.30.1.111/9999<br/>

6. reboot windows<br/>
echo 'rebootnow!!'>/dev/udp/172.30.1.111/9999<br/>
