/// <summary>
/// Codeunit BC LRS Web Service (ID 90100). External functions for the BC LRS - Client
/// </summary>
codeunit 90100 "BC LRS Web Service"
{
    /// <summary>
    /// Ping.
    /// </summary>
    /// <returns>Return value of type Text.</returns>
    [ServiceEnabled]
    procedure Ping(): Text
    begin
        exit('pong');
    end;

    /// <summary>
    /// GetEntryFile. Returns the requested file Base64 encoded
    /// </summary>
    /// <param name="DocumentGUID">Guid.</param>
    /// <param name="Base64Result">VAR BigText.</param>
    [ServiceEnabled]
    procedure GetEntryFile(DocumentGUID: Guid; var Base64Result: BigText);
    begin
        Base64Result := GetEntryFile2(DocumentGUID);
    end;

    /// <summary>
    /// GetEntryFile2. Returns the requested file Base64 encoded version 2
    /// </summary>
    /// <param name="DocumentGUID">Guid.</param>
    /// <returns>Return variable Base64ResultText of type BigText.</returns>
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

    /// <summary>
    /// UploadEntry. Upload file from the client.
    /// </summary>
    /// <param name="LocalService">Text.</param>
    /// <param name="FileQueue">Text.</param>
    /// <param name="Description">Text.</param>
    /// <param name="Base64Result">BigText.</param>
    /// <returns>Return value of type Integer. After upload action: Delete or move to archive</returns>
    [ServiceEnabled]
    procedure UploadEntry(LocalService: Text; FileQueue: Text; Description: Text; Base64Result: BigText): Integer;
    var
        BCLRSEntry: Record "BC LRS Entry";
        BCLRSServRes: Record "BC LRS Service Resource";
        Base64Convert: Codeunit "Base64 Convert";
        OutStr: OutStream;
        ReadText: Text;
        StopFurtherEventProcessing: Boolean;
    begin
        BCLRSServRes.SetRange("Local Service Code", LocalService);
        BCLRSServRes.SetRange("Local Resource Code", FileQueue);
        BCLRSServRes.FindFirst();

        BCLRSEntry.Init();
        BCLRSEntry."Local Service Code" := CopyStr(LocalService, 1, MaxStrLen(BCLRSEntry."Local Service Code"));
        BCLRSEntry."Local Resource Code" := CopyStr(FileQueue, 1, MaxStrLen(BCLRSEntry."Local Resource Code"));
        BCLRSEntry."Entry Type" := BCLRSEntry."Entry Type"::"File From Client";
        BCLRSEntry."Document Name" := CopyStr(Description, 1, MaxStrLen(BCLRSEntry."Document Name"));


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

    /// <summary>
    /// OnAfterFileUpload. Event Codeunit 90100 "BC LRS Web Service"
    /// </summary>
    /// <param name="BCLRSEntry">VAR Record "BC LRS Entry".</param>
    /// <param name="StopFurtherEventProcessing">VAR Boolean.</param>
    [IntegrationEvent(true, false)]
    procedure OnAfterFileUpload(var BCLRSEntry: Record "BC LRS Entry"; var StopFurtherEventProcessing: Boolean)
    begin
    end;

    /// <summary>
    /// SetDocumentComplete. Set the handled document to Complete.
    /// </summary>
    /// <param name="DocumentGUID">Guid.</param>
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

    /// <summary>
    /// RegisterPrinterService. Register the cleint instance.
    /// </summary>
    /// <param name="LocalService">Text.</param>
    [ServiceEnabled]
    procedure RegisterPrinterService(LocalService: Text)
    var
        BCLRSService: Record "BC LRS Service";
    begin
        if not BCLRSService.get(LocalService) then begin
            BCLRSService.Init();
            BCLRSService.Code := CopyStr(LocalService, 1, MaxStrLen(BCLRSService.Code));
            BCLRSService.Insert();
        end;
        UpdateHeartBeat(LocalService);
    end;

    /// <summary>
    /// ClearPrinterList. Delete the registered printers.
    /// </summary>
    /// <param name="PrinterService">Text.</param>
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

    /// <summary>
    /// AddPrinterServicePrinter. Register a printer for the cleint servive - no paper setting
    /// </summary>
    /// <param name="LocalService">Text.</param>
    /// <param name="Printer">Text.</param>
    [ServiceEnabled]
    procedure AddPrinterServicePrinter(LocalService: Text; Printer: Text)
    begin
        AddPrinterServicePrinter(LocalService, Printer, false, 'A4', 'AutomaticFeed');
    end;

    /// <summary>
    /// AddPrinterServicePrinter. Register a printer for the cleint servive with paper settings.
    /// </summary>
    /// <param name="LocalService">Text.</param>
    /// <param name="Printer">Text.</param>
    /// <param name="landscape">Boolean.</param>
    /// <param name="PaperSize">Text.</param>
    /// <param name="PaperTray">Text.</param>
    [ServiceEnabled]
    procedure AddPrinterServicePrinter(LocalService: Text; Printer: Text; landscape: Boolean; PaperSize: Text; PaperTray: Text)
    var
        BCServiceResource: Record "BC LRS Service Resource";
        BCLRSService: Record "BC LRS Service";
        PaperSizeFound: Enum "Printer Paper Kind";
        HeightFound, WidthFound : Decimal;
        PaperTrayFound: Enum "Printer Paper Source Kind";
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

            if ParsePaperSize(PaperSize, PaperSizeFound, HeightFound, WidthFound) then begin
                BCServiceResource."Paper Size" := PaperSizeFound;
                BCServiceResource."Paper Width" := WidthFound;
                BCServiceResource."Paper Height" := HeightFound;
            end else begin
                BCServiceResource."Paper Size" := BCServiceResource."Paper Size"::A4;
                BCServiceResource."Paper Width" := 827;
                BCServiceResource."Paper Height" := 1169;
            end;

            if TryGetPaperTray(PaperTray, PaperTrayFound) then
                BCServiceResource."Paper Tray" := PaperTrayFound
            else
                BCServiceResource."Paper Tray" := BCServiceResource."Paper Tray"::AutomaticFeed;

            BCServiceResource."Resource Type" := BCServiceResource."Resource Type"::Printer;
            if BCServiceResource.Insert(true) then;
        end;
        UpdateHeartBeat(LocalService);
    end;

    /// <summary>
    /// AddPrinterPaperSetup. Aditinal paper and tray settings for the printer - NOT IN USE yet
    /// </summary>
    /// <param name="LocalService">Text.</param>
    /// <param name="Printer">Text.</param>
    /// <param name="landscape">Boolean.</param>
    /// <param name="PaperSize">Text.</param>
    /// <param name="PaperTray">Text.</param>
    [ServiceEnabled]
    procedure AddPrinterPaperSetup(LocalService: Text; Printer: Text; landscape: Boolean; PaperSize: Text; PaperTray: Text)
    begin

    end;

    /// <summary>
    /// AddFileService. Register folder for the service.
    /// </summary>
    /// <param name="LocalService">Text.</param>
    /// <param name="FileQueue">Text.</param>
    /// <param name="Direction">Integer.</param>
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
            if BCServiceResource."Resource Type" = BCServiceResource."Resource Type"::"File Queue From Client" then
                BCServiceResource."After Import File Action" := BCServiceResource."After Import File Action"::"Delete Source + Run Subscriber Action";
            BCServiceResource.Insert(true);
        end;
        UpdateHeartBeat(LocalService);
    end;

    /// <summary>
    /// GetFileServiceStatus. Returns the Enabled/Disabled status of a specifi file queue on a local service
    /// </summary>
    /// <param name="LocalService">Text.</param>
    /// <param name="FileQueue">Text.</param>
    /// <returns>Return value of type Integer. 0 - disabled, >1 is enabled + after import action</returns>
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

    /// <summary>
    /// UpdateHeartBeat. Updates BC with the client running status and returns true
    /// </summary>
    /// <param name="LocalService">Text.</param>
    /// <returns>Return value of type Boolean.</returns>
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

    /// <summary>
    /// ServiceUpdateRequested. Signals the client that a printer discovery is requested from the client.
    /// </summary>
    /// <param name="LocalService">Text.</param>
    /// <returns>Return value of type Boolean.</returns>
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


    [TryFunction]
    internal procedure ParsePaperSize(PaperSize: Text; var PaperSizeFound: Enum "Printer Paper Kind"; var HeightFound: Decimal; var WidthFound: Decimal)
    var
        kind, height, width : Text;
    begin
        //{[PaperSize A4 Kind=A4 Height=1169 Width=827]}
        kind := CopyStr(PaperSize, StrPos(PaperSize, 'Kind=') + StrLen('Kind='), 100);
        kind := CopyStr(kind, 1, StrPos(kind, ' Height=') - 1);
        PaperSizeFound := "Printer Paper Kind".FromInteger("Printer Paper Kind".Ordinals().Get(("Printer Paper Kind".Names().IndexOf(kind))));

        height := CopyStr(PaperSize, StrPos(PaperSize, 'Height=') + StrLen('Height='), 100);
        height := CopyStr(height, 1, StrPos(height, ' Width=') - 1);
        if Evaluate(HeightFound, height) then;

        width := CopyStr(PaperSize, StrPos(PaperSize, 'Width=') + StrLen('Width='), 100);
        width := CopyStr(width, 1, StrPos(width, ']}') - 1);
        if Evaluate(WidthFound, width) then;
    end;

    [TryFunction]
    local procedure TryGetPaperTray(PaperTray: Text; var PaperTrayFound: Enum "Printer Paper Source Kind")
    begin
        PaperTrayFound := "Printer Paper Source Kind".FromInteger("Printer Paper Source Kind".Ordinals().Get(("Printer Paper Source Kind".Names().IndexOf(PaperTray))));
    end;
}