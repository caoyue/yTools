yTools
-------------------------
[![Build status](https://ci.appveyor.com/api/projects/status/i9gaq83t429rcfld?svg=true)](https://ci.appveyor.com/project/caoyue/ytools)  

my command-line utility

> use `ytools -h` for help

# Image
- mark pic location on maps  
    ```powershell
    ytools image -o -m google -f "D:\test.jpg"
    ```  
    - config  
        Key/secret is needed to use baidu/tencent map api, set it in `config.json`  

# PowerPan  
- list  
    ```powershell
    ytools power -l
    ```  
- active  
    ```powershell
    ytools power -a 381b4222-f694-41f0-9685-ff5bb260df2e
    ```

# TaskBar  
- switch taskbar visibility  
    ```powershell
    ytools taskbar -a toggle
    ```
