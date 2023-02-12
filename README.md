# BC Local Resources

Application to recive and send data from BC directly trough the oData services.

The client installed on the local macihine periodicaly queries BC for printing, folder and shell commands. It also can upload files from folders to BC.

No additionald subscriptions required.

<img width="1278" alt="image" src="https://user-images.githubusercontent.com/64136814/218323647-c0c6aa0a-1339-42c2-866a-6716539ff97b.png">

#c# Components
BCLRS - common library 
Tester - GUI app to setup and test the connection and functions
BCLRSService - Windows service
Installer - component installer for windows

#BC extension

#Initial Setup
Use the tester application to establish the connection with the BC instance.

![image](https://user-images.githubusercontent.com/64136814/218324735-cadfa7de-da03-4d83-acfd-012c1ee2c0f5.png)

Instance: will be the destination from BC
<img width="481" alt="image" src="https://user-images.githubusercontent.com/64136814/218324818-e41dce44-dec3-446c-af35-406e0102ab87.png">

Connection details
oData URL: base url for the BC services including the compnay name. Example: https://api.businesscentral.dynamics.com/v2.0/6e9fbeef-85d9-4bcb-8ec4-xxxxxxxxxb9/Production/ODataV4/Company('CRONUS%20UK%20Ltd.')/

Authentication: 
  Basic - simple user/password authentication
  
  oAuth - use of application scope tokens
    
