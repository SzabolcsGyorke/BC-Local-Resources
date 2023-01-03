#pragma implicitwith disable
page 90100 "BC LRS Entry List"
{
    PageType = List;
    ApplicationArea = All;
    UsageCategory = Administration;
    SourceTable = "BC LRS Entry";

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

                field("Entry Blob"; Rec."Entry Blob")
                {
                    Editable = false;
                    ApplicationArea = all;
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
                    Rec."Document Name" := inFileName;
                    Rec.Modify();

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
        }
    }
}
#pragma implicitwith restore
