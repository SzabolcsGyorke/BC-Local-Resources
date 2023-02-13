#pragma implicitwith disable
page 90104 "BC LRS Resources Setup"
{
    PageType = Card;
    ApplicationArea = All;
    UsageCategory = Administration;
    SourceTable = "BC LRS Setup";
    Caption = 'Local Resources Setup';

    layout
    {
        area(Content)
        {
            group(General)
            {

                field("Heart Beat Keep Alive (min)"; Rec."Heart Beat Keep Alive (min)")
                {
                    ApplicationArea = All;
                    Editable = true;
                    ToolTip = 'Specifies the number of minutes while we consider the local service alive based on it`s latest heartbeat.';
                }

                field("Service Resource Nos."; Rec."Service Resource Nos.")
                {
                    ApplicationArea = All;
                    Editable = true;
                    ToolTip = 'Specifies the value of the Service Resource Nos. field.';
                }
            }
        }
    }
    actions
    {
        area(Promoted)
        {
            actionref(ref_addretepolicy; act_addretpolicy) { }
        }
        area(Processing)
        {
            action(act_addretpolicy)
            {
                ApplicationArea = All;
                Caption = 'Add Data Retention Policy';
                ToolTip = 'Create a data retenion policy for 1 week to maintain the BC LRS Entries.';
                Image = RemoveFilterLines;
                trigger OnAction()
                var
                    BCLRSManagement: Codeunit "BC LRS Management";
                begin
                    BCLRSManagement.AddDataRetentionPolicy();
                end;
            }
        }
    }


    trigger OnOpenPage()
    begin
        if NOT Rec.FindFirst() then begin
            Rec.Init();
            Rec.Insert();
        end;
    end;


    //unbound service functions
    var
        BCLRSWebService: Codeunit "BC LRS Web Service";

    [ServiceEnabled]
    procedure Ping(): Text
    var
        actioncontext: WebServiceActionContext;
    begin
        actioncontext.SetObjectType(ObjectType::Page);
        actioncontext.SetObjectId(Page::"BC LRS Resources Setup");
        actioncontext.AddEntityKey(Rec.FieldNo("Primary Key"), Rec."Primary Key");
        actioncontext.SetResultCode(WebServiceActionResultCode::None);

        exit('pong')
    end;

    [ServiceEnabled]
    procedure UpdateHeartBeat(LocalService: Text): Boolean
    begin
        exit(BCLRSWebService.UpdateHeartBeat(LocalService));
    end;

    [ServiceEnabled]
    procedure GetEntryFile(DocumentGUID: Guid): BigText
    begin
        exit(BCLRSWebService.GetEntryFile2(DocumentGUID));
    end;

    [ServiceEnabled]
    procedure UploadEntry(LocalService: Text; FileQueue: Text; Description: Text; Base64Result: BigText): Integer;
    begin
        exit(BCLRSWebService.UploadEntry(LocalService, FileQueue, Description, Base64Result))
    end;

    [ServiceEnabled]
    procedure SetDocumentComplete(DocumentGUID: Guid);
    begin
        BCLRSWebService.SetDocumentComplete(DocumentGUID);
    end;

    [ServiceEnabled]
    procedure RegisterPrinterService(LocalService: Text)
    begin
        BCLRSWebService.RegisterPrinterService(LocalService);
    end;

    [ServiceEnabled]
    procedure ClearPrinterList(PrinterService: Text)
    begin
        BCLRSWebService.ClearPrinterList(PrinterService);
    end;

    [ServiceEnabled]
    procedure AddPrinterServicePrinter(LocalService: Text; Printer: Text)
    begin
        BCLRSWebService.AddPrinterServicePrinter(LocalService, Printer);
    end;

    [ServiceEnabled]
    procedure AddPrinterServicePrinter(LocalService: Text; Printer: Text; landscape: Boolean; PaperSize: Text; PaperTray: Text)
    begin
        BCLRSWebService.AddPrinterServicePrinter(LocalService, Printer, landscape, PaperSize, PaperTray);
    end;

    [ServiceEnabled]
    procedure AddPrinterPaperSetup(LocalService: Text; Printer: Text; landscape: Boolean; PaperSize: Text; PaperTray: Text)
    begin
        BCLRSWebService.AddPrinterPaperSetup(LocalService, Printer, landscape, PaperSize, PaperTray);
    end;

    [ServiceEnabled]
    procedure AddFileService(LocalService: Text; FileQueue: Text; Direction: Integer)
    begin
        BCLRSWebService.AddFileService(LocalService, FileQueue, Direction);
    end;

    [ServiceEnabled]
    procedure GetFileServiceStatus(LocalService: Text; FileQueue: Text): Integer;
    begin
        exit(BCLRSWebService.GetFileServiceStatus(LocalService, FileQueue));
    end;

    [ServiceEnabled]
    procedure ServiceUpdateRequested(LocalService: Text): Boolean;
    begin
        exit(BCLRSWebService.ServiceUpdateRequested(LocalService));
    end;
}
#pragma implicitwith restore
