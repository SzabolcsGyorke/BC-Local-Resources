#pragma implicitwith disable
page 90100 "BC LRS Entry List"
{
    PageType = List;
    ApplicationArea = All;
    UsageCategory = Administration;
    SourceTable = "BC LRS Entry";
    Caption = 'Local Service Entries';
    DelayedInsert = true;
    layout
    {
        area(Content)
        {
            repeater(GroupName)
            {

                field("Entry GUID"; Rec."Entry GUID")
                {
                    ApplicationArea = All;
                }
                field("Company Name"; Rec."Company Name")
                {
                    ApplicationArea = all;
                }
                field("Entry Type"; Rec."Entry Type")
                {
                    ApplicationArea = all;
                }

                field("Document Name"; Rec."Document Name")
                {
                    ApplicationArea = all;
                }

                field("Entry Blob"; Rec."Entry Blob".HasValue())
                {
                    Editable = false;
                    ApplicationArea = all;
                    Caption = 'Blob Value Exits';
                }

                field("Local Service Code"; Rec."Local Service Code")
                {
                    ApplicationArea = all;
                }

                field("Local Resource Code"; Rec."Local Resource Code")
                {
                    ApplicationArea = all;
                }

                field("Print Copies"; Rec."Print Copies")
                {
                    ApplicationArea = all;
                }

                field("Use Raw Print"; Rec."Use Raw Print")
                {
                    ToolTip = 'Specifies the value of the Use Raw Print field.';
                }
                field("Entry Completed"; Rec."Entry Completed")
                {
                    ToolTip = 'Specifies the value of the Entry Completed field.';
                }
                field("Entry Completed At"; Rec."Entry Completed At")
                {
                    ToolTip = 'Specifies the value of the Entry Completed At field.';
                }
            }
        }
    }

    actions
    {
        area(Processing)
        {
            action("Upload File")
            {
                ApplicationArea = All;
                Image = Import;
                Promoted = true;
                PromotedCategory = Process;
                PromotedOnly = true;
                trigger OnAction()
                var
                    filemgt: Codeunit "File Management";
                    tempblob: Codeunit "Temp Blob";
                    intsr: InStream;
                    outstr: OutStream;
                    filename: Text;
                    inFileName: Text;
                begin
                    inFileName := filemgt.BLOBImport(tempblob, filename);
                    tempblob.CreateInStream(intsr);
                    Rec."Entry Blob".CreateOutStream(outstr);
                    CopyStream(outstr, intsr);
                    Rec."Document Name" := copystr(inFileName, 1, MaxStrLen(Rec."Document Name"));
                    if not Rec.Modify(true) then
                        Rec.Insert(true);
                end;
            }
            action("Download File")
            {
                ApplicationArea = All;
                Image = Export;
                Caption = 'Download File';
                Promoted = true;
                PromotedCategory = Process;
                PromotedOnly = true;

                trigger OnAction()
                var
                    filemgt: Codeunit "File Management";
                    tempblob: Codeunit "Temp Blob";
                    filename: Text;
                begin
                    Rec.CalcFields("Entry Blob");
                    if Rec."Entry Blob".HasValue then begin
                        If Rec."Entry Type" in [Rec."Entry Type"::"File From Client", Rec."Entry Type"::"File To Client"] then
                            filename := Rec."Document Name"
                        else
                            filename := 'export.pdf';
                        tempblob.FromRecord(rec, Rec.FieldNo("Entry Blob"));
                        filemgt.BLOBExport(tempblob, filename, true);
                    end;
                end;
            }
            action("Command Editor")
            {
                ApplicationArea = All;
                Image = DebugNext;
                Caption = 'Command Editor';
                Promoted = true;
                PromotedCategory = Process;
                PromotedOnly = true;
                trigger OnAction()
                var
                    BCLRSEditText: Page "BC LRS Edit Text";
                    lblCommandTxt: Label 'Command';
                    lblTooltipTxt: Label 'For a shell command separate the executable with ^ from the parameters. Example: cmd.exe^ dir c:\ >>ls.txt';
                begin
                    if Rec."Entry Type" <> Rec."Entry Type"::"Command to Client" then exit;
                    BCLRSEditText.Caption(lblCommandTxt);
                    BCLRSEditText.SetTip(lblTooltipTxt);
                    BCLRSEditText.SetText(Rec.GetCommand());
                    BCLRSEditText.RunModal();
                    Rec.SetCommand(BCLRSEditText.GetText());
                end;
            }
        }
    }

    trigger OnNewRecord(BelowxRec: Boolean)
    begin
        if Rec.GetFilter("Local Resource Code") <> '' then
            Rec.Validate("Local Resource Code", Rec.GetFilter("Local Resource Code"));

        if Rec.GetFilter("Local Service Code") <> '' then
            Rec.Validate("Local Service Code", Rec.GetFilter("Local Service Code"));
    end;
}
#pragma implicitwith restore
