# BC Local Resources

Application to receive and send data from BC directly trough the oData services.

The client installed on the local machine periodically queries BC for printing, folder and shell commands. It also can upload files from folders to BC.

No additional subscriptions required.

<img width="1278" alt="image" src="https://user-images.githubusercontent.com/64136814/218323647-c0c6aa0a-1339-42c2-866a-6716539ff97b.png">

## Current issues
- Installer not registering the windows service - currently it needs to be done manually with the SC command
- Configuration upload errors if one already exists in BC for the instance
- Printer Trays are not supported

## c# Components
- **BCLRS:** common library
- **Tester:** GUI app to setup and test the connection and functions
- **BCLRSService:** Windows service
- **Installer:** component installer for windows

The app supports two printing modes: pdf and raw. The raw print will directly send the attached file to the printer. This can be useful for barcode printing like zpl files.
The pdf printing invokes Adobe Acrobat or the Foxit pdf readers and sends the pdf trough them to the selected printer.

Of course you can try to use some non free alteratives as well.

**Download the Installer "BC Local Resources Service Setup.zip" from the repo.**

## BC extension and Setup
Deploy the extension form the repository.
It will register a few web services for access - make sure that they are accessible for the Client machine.

<img width="482" alt="image" src="https://user-images.githubusercontent.com/64136814/218334049-0021ebd7-3f90-4e9d-a851-980605dc6f83.png">

Open the **Local Resources Setup**:

<img width="482" alt="image" src="https://user-images.githubusercontent.com/64136814/218334156-8ca83306-a58a-410f-93db-e0b5a32a6b63.png">

- Set the Heart Beat Keep Alive time 
- Create and set the Service Resource Nos. 


## Client Initial Setup and testing
Use the tester application to establish the connection with the BC instance.

![image](https://user-images.githubusercontent.com/64136814/218324735-cadfa7de-da03-4d83-acfd-012c1ee2c0f5.png)

Instance: will be the destination from BC

<img width="481" alt="image" src="https://user-images.githubusercontent.com/64136814/218324818-e41dce44-dec3-446c-af35-406e0102ab87.png">

### Connection details
oData URL: base url for the BC services including the company name._ Example for MS Cloud: https://api.businesscentral.dynamics.com/v2.0/6e9fbeef-85d9-4bcb-8ec4-xxxxxxxxxb9/Production/ODataV4/Company('CRONUS%20UK%20Ltd.')/_

**Authentication:**
+ **Basic:** simple user/password authentication<br>
+ **oAuth:** use of application scope tokens<br>
    
**oAuth**
- **Client ID**: client id form azure<br>
- **Client Secret**: client secret form azure
- **Authorization URL**: token url from azure  _Example: https://login.microsoftonline.com/6e9fbeef-xxxx-xxxx-8ec4-xxxxxxx12b9/oauth2/v2.0/token _
- **Redirect URL**: redirect web url set in azure 
- **Scope**: default scope for BC _Example: https://api.businesscentral.dynamics.com/.default_

![image](https://user-images.githubusercontent.com/64136814/218325198-9943ab76-8edd-49a9-910b-58c5693853df.png)

**Basic**
- **Username**: BC username
- **Password**: BC password or API key.

### Connection Functions

<img width="266" alt="image" src="https://user-images.githubusercontent.com/64136814/218333352-0c54511b-3e04-44b5-971c-f8789eabe652.png">

- **Register Instance**: register the service and local printers for the use of BC
- **Update Heartbeat**: updates the BC service record
- **Upload Configuration**: uploads the test configuration lines to BC - this can be later exported for the windows service
- **Clear Token**: clears the current authentication token (oAuth only)
- **Token Information**: display the current token and expiry time (oAuth only)

**Once you tested all the requeued components use the _Upload Configuration_ function to store it in BC and for furter use to configure the Windows Service.**

### Printing
Download the list unprinted documents from BC and print them or mark them as completed.

<img width="231" alt="image" src="https://user-images.githubusercontent.com/64136814/218335284-e8e4427e-3c2b-42cb-a6f4-58f86719d30e.png">

- **Get documents**: download the list of documents
- **Mark as Completed**: set the Completed flag to **true** in BC on the selected document
- **Save file**: save the selected pdf/ raw print file 
- **Print Document**: send the document to the printer - the printer selection will reflect the printer on the selected line

### Local functions
Test for the printing function with local files.

<img width="233" alt="image" src="https://user-images.githubusercontent.com/64136814/218335602-347e3b51-86b7-48e7-9095-0ce0d96b7a32.png">

### File Services
Display or add registered folder used by BC to synchronize.

<img width="235" alt="image" src="https://user-images.githubusercontent.com/64136814/218335635-a85b8b9d-0a5b-483d-bf2f-d6a29db6bdd0.png">

- **Get BC Folders**: get the list of registered folders from BC
- **Synchronize files**: upload and download files from **all** the folders

**Register Folder in BC**
- **Folder**: the local folder path
- **Type**: Download from BC, Upload from Client
- **Register**: create the entry in BC but it won't be enabled yet. 

### Commands
Shell commands sent from BC. 

<img width="236" alt="image" src="https://user-images.githubusercontent.com/64136814/218335887-ebcd582d-4d7e-4133-af60-cfd6c046463b.png">

- **Get Commands**: get the list of commands from BC
- **Execute**: run the selected command and set the BC line to completed 

### Timer
Test the selected functions on a timer and log the outcome to the list. Double click on the list item to see the details.

<img width="232" alt="image" src="https://user-images.githubusercontent.com/64136814/218336027-7d944ab2-2353-4cd1-9cf8-3e109b5e6791.png">

- **Time interval**: set the timer
- **Functions**: select which function or functions you want to run
- **Start**: starts the timer - will turn green while the timer is running
- **Clear Log**: deletes the log

## Windows Service 
The windows service runs in the background and synchronizes with BC in a set time interval.

### Installation
"BC Local Resources Service Setup.zip" you can find the installer in the repo.

Currently the installer is not creating the service so you need to add it manually from a admin command prompt:
Example:
```
sc create BCLRS binpath= "C:\Program Files (x86)\SG\BC Local Resources Service Setup\BC Print Service.exe" DisplayName= "BC Local Resources" 
```
The service should run with admin priviliges. This is due to the check of the running process of the PDF Viewers and their attempted close.

Once it installed create a new folder named **Configuration** to the service directory:
<img width="836" alt="image" src="https://user-images.githubusercontent.com/64136814/218337407-01f85586-649a-4921-be38-e2c873a8cf14.png">

Download the configuration from BC:
Find the instance created by the Tester app (or modify one with the use of the Setup button) and use the **Export Config XML** function to generate and download the _settings.config_ file.

<img width="485" alt="image" src="https://user-images.githubusercontent.com/64136814/218543802-4e030cf6-ecaf-4c5e-b081-f22f407d7248.png">


## BC Extension
The purpose of the extension is to collect the printouts, files and commands for the individual local services.

### Local Resource Services
The list of registerd services/clients. It shows the status of the service: alive and pingig back to BC and the number of connected printers and folders.
You can also view, edit or export the service configuration.

<img width="478" alt="image" src="https://user-images.githubusercontent.com/64136814/218547915-7c4b09ec-2e44-4c04-a79c-8025f20643ab.png">

**Additional functions:**
- Open the Registered Resources list
- **Request Resource Update**: this is useful to ask a running service to update its printer list

### Local Service Resources
The list of resources published by the local service.

<img width="1225" alt="image" src="https://user-images.githubusercontent.com/64136814/218550106-521ba8fa-06cd-440e-82bf-e6786f7591b1.png">

|Field |Description|
|------|-----------|
|No. | Primary key - pulled form the No. Series from the Setup|
|Local Service Code||
|Local Resource Code||
|**Enabled**|During syncronization new resources are not enabled therefore they can't be used.|
|Description||
|**Resource Type**|Sets the purpose of the resource: Printer, File Queue, Command Queue|
|**After Import File Action**|What should happen after the file is uploaded to BC or downloaded to the client computer|
|No. of Pending Entries|Numbrof the unprocessed entries.|
|**Landscape**|Printer: Paper setup|
|**Paper Size**|Printer: size of paper|
|Paper Tray|Printer: Destination tray - not in use|
|**Use Raw Print**|Bypass the PDF reader and send the file directly to the printer (ZPL)|

### Local Service Entries
List of entries processed or queud for a service to process.
The table is company independent. Might change in the future.

<img width="1225" alt="image" src="https://user-images.githubusercontent.com/64136814/218581889-b76b755a-e673-42c5-af18-45a3be2fe824.png">

|Field |Description|
|------|-----------|
|Entry GUID|PK|
|Company Name|Entry created in company|
|Entry Type|File Queue, Print, Command|
|Document Name|Print: File Name|
|Blob Value Exits|Check if the BLOB is populated or not|
|Local Service Code||
|Local Resource Code||
|Print Copies|Print: number of copies|
|Entry Completed||
|Entry Completed At||

**Blob export/import**
Functions added to the page to handle the blob field: Upload file and Download file

**Create a command**
1. Open the COMMAND Resource
2. Add a new line
3. Use the Command Editor to add the command

### Printing
The enabled local printers will show up on the Print Request Page under the Printer:

<img width="397" alt="image" src="https://user-images.githubusercontent.com/64136814/218583561-ceb85b02-2613-46ea-9555-ce185ae162df.png">

### File Inbox
Files will be uploaded from the registered folder from the client computer. 
Once the record is created the After File Import Action is preformed. **Important to have a Delete action selected otherwise the client will keep uploading the same file.**

If you selected _Delete Source and Run Subscriber_ the file will be deleted from the client and the following event is triggered:
```
    /// <summary>
    /// OnAfterFileUpload. Event Codeunit 90100 "BC LRS Web Service"
    /// </summary>
    /// <param name="BCLRSEntry">VAR Record "BC LRS Entry".</param>
    /// <param name="StopFurtherEventProcessing">VAR Boolean.</param>
    [IntegrationEvent(true, false)]
    procedure OnAfterFileUpload(var BCLRSEntry: Record "BC LRS Entry"; var StopFurtherEventProcessing: Boolean)
    begin
    end;
```

### File Outbox
By creating a Local Service Entry for a File Queue To Client Resource.

The client will only attempt to download the file if the Description is populated with a file name so you'll have time to insert the record, upload the blob and lastly the Description. With this you can avoid to find the records before the blob data is added. 

## Windows Event Log
The service creates log entries to the windows event log:

<img width="1053" alt="image" src="https://user-images.githubusercontent.com/64136814/218337622-352a0581-288f-4001-923f-5d3dc9315ca7.png">

If your run multiple services on the same machine the Source column will help to identify the instance.
