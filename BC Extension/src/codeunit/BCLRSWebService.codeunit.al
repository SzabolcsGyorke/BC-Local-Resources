codeunit 90100 "BC LRS Web Service"
{
    [ServiceEnabled]
    procedure Ping(): Text
    begin
        exit('pong');
    end;

    [ServiceEnabled]
    procedure GetEntryFile(DocumentGUID: Guid; var Base64Result: BigText);
    begin
        Base64Result := GetEntryFile2(DocumentGUID);
    end;

    [ServiceEnabled]
    procedure GetEntryFile2(DocumentGUID: Guid) Base64ResultText: BigText;
    var
        BCLRSEntry: Record "BC LRS Entry";
        Base64Convert: Codeunit "Base64 Convert";
        InStr: InStream;
    begin
        if BCLRSEntry.get(DocumentGUID) then begin
            BCLRSEntry.CalcFields("Entry Blob");

            BCLRSEntry."Entry Blob".CreateInStream(InStr);

            Base64ResultText.AddText(Base64Convert.ToBase64(InStr));
        end;

    end;

    [ServiceEnabled]
    procedure UploadEntry(LocalService: Text; FileQueue: Text; Description: Text; Base64Result: BigText): Integer;
    var
        BCLRSEntry: Record "BC LRS Entry";
        BCLRSServRes: Record "BC LRS Service Resource";
        TempBlob: Codeunit "Temp Blob";
        Base64Convert: Codeunit "Base64 Convert";
        OutStr: OutStream;
        ReadText: Text;
        StopFurtherEventProcessing: Boolean;
    begin
        BCLRSServRes.SetRange("Local Service Code", LocalService);
        BCLRSServRes.SetRange("Local Resource Code", FileQueue);
        BCLRSServRes.FindFirst();

        BCLRSEntry.init;
        BCLRSEntry."Local Service Code" := LocalService;
        BCLRSEntry."Local Resource Code" := FileQueue;
        BCLRSEntry."Entry Type" := BCLRSEntry."Entry Type"::"File From Client";
        BCLRSEntry."Document Name" := Description;


        //Store the file
        BCLRSEntry."Entry Blob".CreateOutStream(OutStr);
        Base64Result.GetSubText(ReadText, 1);
        Base64Convert.FromBase64(ReadText, OutStr);
        BCLRSEntry.Insert(true);

        if (BCLRSServRes."After Import File Action" = BCLRSServRes."After Import File Action"::"Delete Source + Run Subscriber Action") or
           (BCLRSServRes."After Import File Action" = BCLRSServRes."After Import File Action"::"Move to Archive + Run Subscriber Action") then
            OnAfterFileUpload(BCLRSEntry, StopFurtherEventProcessing);

        exit(BCLRSServRes."After Import File Action");
    end;

    [IntegrationEvent(true, false)]
    procedure OnAfterFileUpload(var BCLRSEntry: Record "BC LRS Entry"; var StopFurtherEventProcessing: Boolean)
    begin
    end;

    [ServiceEnabled]
    procedure SetDocumentComplete(DocumentGUID: Guid);
    var
        BCRSEntry: Record "BC LRS Entry";
    begin
        if BCRSEntry.get(DocumentGUID) then begin
            BCRSEntry.Validate("Entry Completed", true);
            BCRSEntry.Modify(true);
        end;
    end;

    [ServiceEnabled]
    procedure RegisterPrinterService(LocalService: Text)
    var
        BCLRSService: Record "BC LRS Service";
    begin
        if not BCLRSService.get(LocalService) then begin
            BCLRSService.Init();
            BCLRSService.Code := LocalService;
            BCLRSService.Insert();
        end;
        UpdateHeartBeat(LocalService);
    end;

    [ServiceEnabled]
    procedure ClearPrinterList(PrinterService: Text)
    var
        BCLRSServiceResource: Record "BC LRS Service Resource";
    begin
        BCLRSServiceResource.SetRange("Local Service Code", PrinterService);
        if BCLRSServiceResource.FindSet() then
            repeat
                BCLRSServiceResource.Delete(true);
            until BCLRSServiceResource.Next() < 1;
    end;

    [ServiceEnabled]
    procedure AddPrinterServicePrinter(LocalService: Text; Printer: Text)
    begin
        AddPrinterServicePrinter(LocalService, Printer, false, 'A4', 'AutomaticFeed');
    end;

    [ServiceEnabled]
    procedure AddPrinterServicePrinter(LocalService: Text; Printer: Text; landscape: Boolean; PaperSize: Text; PaperTray: Text)
    var
        BCServiceResource: Record "BC LRS Service Resource";
        BCLRSService: Record "BC LRS Service";
    begin
        if BCLRSService.get(LocalService) then begin
            BCLRSService."Last Resource Update" := CurrentDateTime;
            BCLRSService."Request Resource Update" := false;
            BCLRSService.Modify();
        end;
        BCServiceResource.SetRange("Local Service Code", LocalService);
        BCServiceResource.SetRange("Local Resource Code", Printer);
        if BCServiceResource.IsEmpty() then begin
            BCServiceResource.Init();
            BCServiceResource."Local Resource Code" := CopyStr(Printer, 1, 250);
            BCServiceResource."Local Service Code" := CopyStr(LocalService, 1, 250);
            BCServiceResource.Landscape := landscape;
            BCServiceResource."Paper Size" := "Printer Paper Kind".FromInteger("Printer Paper Kind".Names().IndexOf(PaperSize));
            BCServiceResource."Paper Tray" := "Printer Paper Source Kind".FromInteger("Printer Paper Source Kind".Names().IndexOf(PaperTray));
            BCServiceResource."Resource Type" := BCServiceResource."Resource Type"::Printer;
            if BCServiceResource.Insert(true) then;
        end;
        UpdateHeartBeat(LocalService);
    end;

    [ServiceEnabled]
    procedure AddFileService(LocalService: Text; FileQueue: Text; Direction: Integer)
    var
        BCServiceResource: Record "BC LRS Service Resource";
        BCLRSService: Record "BC LRS Service";
    begin
        if BCLRSService.get(LocalService) then begin
            BCLRSService."Last Resource Update" := CurrentDateTime;
            BCLRSService."Request Resource Update" := false;
            BCLRSService.Modify();
        end;
        BCServiceResource.SetRange("Local Service Code", LocalService);
        BCServiceResource.SetRange("Local Resource Code", FileQueue);
        if BCServiceResource.IsEmpty() then begin
            BCServiceResource.Init();
            BCServiceResource."Local Resource Code" := Copystr(FileQueue, 1, MaxStrLen(BCServiceResource."Local Resource Code"));
            BCServiceResource."Local Service Code" := Copystr(LocalService, 1, MaxStrLen(BCServiceResource."Local Service Code"));
            BCServiceResource."Resource Type" := Direction;
            BCServiceResource.Insert(true);
        end;
        UpdateHeartBeat(LocalService);
    end;

    [ServiceEnabled]
    procedure GetFileServiceStatus(LocalService: Text; FileQueue: Text): Integer;
    var
        BCServiceResource: Record "BC LRS Service Resource";
    begin
        if BCServiceResource.get(LocalService, FileQueue) then begin
            if BCServiceResource.Enabled then
                exit(1 + BCServiceResource."After Import File Action");
        end else
            exit(0);
    end;

    [ServiceEnabled]
    procedure UpdateHeartBeat(LocalService: Text): Boolean;
    var
        BCLRSService: Record "BC LRS Service";
    begin
        if BCLRSService.get(LocalService) then begin
            BCLRSService."Last Heartbeat" := CurrentDateTime;
            BCLRSService.Modify();
            exit(true);
        end;
        exit(false);
    end;

    [ServiceEnabled]
    procedure ServiceUpdateRequested(LocalService: Text): Boolean;
    var
        BCLRSService: Record "BC LRS Service";
    begin
        if BCLRSService.get(LocalService) then
            exit(BCLRSService."Request Resource Update")
        else
            exit(false);
    end;
}