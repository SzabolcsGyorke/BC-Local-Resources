codeunit 90101 "BC LRS Print Event Subscriber"
{

    [EventSubscriber(ObjectType::Codeunit, Codeunit::"Reporting Triggers", 'SetupPrinters', '', true, true)]
    local procedure SetUniversalPrinter(var Printers: Dictionary of [Text[250], JsonObject])
    var
        BCLRSServiceResource: Record "BC LRS Service Resource";
        PaperTrays: JsonArray;
        PaperTray: JsonObject;
        Payload: JsonObject;
    begin
        BCLRSServiceResource.SetRange("Resource Type", BCLRSServiceResource."Resource Type"::Printer);
        BCLRSServiceResource.Setrange(Enabled, true);
        if not BCLRSServiceResource.FindSet() then
            exit;

        repeat
            if (BCLRSServiceResource."Local Resource Code" <> '') and (not Printers.ContainsKey(BCLRSServiceResource."No.")) then begin
                Clear(PaperTray);
                Clear(PaperTrays);
                Clear(Payload);

                PaperTray.Add('papersourcekind', BCLRSServiceResource."Paper Tray".AsInteger());
                PaperTray.Add('paperkind', BCLRSServiceResource."Paper Size".AsInteger());
                PaperTray.Add('landscape', BCLRSServiceResource.Landscape);
                PaperTrays.Add(PaperTray);
                Payload.Add('version', 1);
                Payload.Add('description', BCLRSServiceResource."Local Service Code" + ' - ' + BCLRSServiceResource."Local Resource Code");
                Payload.Add('papertrays', PaperTrays);
                Printers.Add(BCLRSServiceResource."No.", Payload);
            end;
        until BCLRSServiceResource.Next() = 0;
    end;

    [EventSubscriber(ObjectType::Codeunit, Codeunit::"Printer Setup", 'OnOpenPrinterSettings', '', false, false)]
    local procedure OnOpenUniversalPrinter(PrinterID: Text; var IsHandled: Boolean)
    var
        BCLRSServiceResource: Record "BC LRS Service Resource";
    begin
        if IsHandled then
            exit;

        if BCLRSServiceResource.Get(CopyStr(PrinterID, 1, MaxStrLen(BCLRSServiceResource."No."))) then begin
            Page.Run(Page::"BC LRS Printer Setup", BCLRSServiceResource);
            IsHandled := true;
        end;
    end;

    [EventSubscriber(ObjectType::Codeunit, Codeunit::"Reporting Triggers", 'OnDocumentPrintReady', '', true, true)]
    local procedure CapturPrintPdf(ObjectType: Option "Report","Page"; ObjectId: Integer; ObjectPayload: JsonObject; DocumentStream: InStream; var Success: Boolean);
    var
        BCLRSServiceResource: Record "BC LRS Service Resource";
        BCLRSEntry: Record "BC LRS Entry";

        PrinterNameToken: JsonToken;
        PropertyBag: JsonToken;
        PrinterName: Text[250];
        FileName: Text;
        DocumentType: Text;

        DocumentOutStream: OutStream;
    begin
        if Success then
            exit;

        if ObjectType <> ObjectType::Report then
            exit;

        if ObjectPayload.Get('printername', PrinterNameToken) then
            PrinterName := CopyStr(PrinterNameToken.AsValue().AsText(), 1, MaxStrLen(PrinterName));
        if not BCLRSServiceResource.Get(PrinterName) then
            exit;

        if ObjectPayload.Get('objectname', PropertyBag) then
            FileName := PropertyBag.AsValue().AsText();

        if ObjectPayload.Get('documenttype', PropertyBag) then
            DocumentType := PropertyBag.AsValue().AsText();

        BCLRSEntry.Init();
        BCLRSEntry."Company Name" := CompanyName();
        BCLRSEntry."Entry Type" := BCLRSEntry."Entry Type"::Print;
        BCLRSEntry."Local Service Code" := BCLRSServiceResource."Local Service Code";
        BCLRSEntry."Local Resource Code" := BCLRSServiceResource."Local Resource Code";
        BCLRSEntry."Document Name" := FileName;
        BCLRSEntry."Print Copies" := 1;
        BCLRSEntry."Entry Blob".CreateOutStream(DocumentOutStream);
        CopyStream(DocumentOutStream, DocumentStream);
        BCLRSEntry."Use Raw Print" := BCLRSServiceResource."Use Raw Print";
        BCLRSEntry.Insert(true);
        Success := true;
    end;
}