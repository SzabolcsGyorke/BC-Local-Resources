#pragma implicitwith disable
page 90102 "BC LRS Service Resources"
{
    PageType = List;
    UsageCategory = None;
    ApplicationArea = All;
    SourceTable = "BC LRS Service Resource";
    Caption = 'Local Service Resources';

    layout
    {
        area(Content)
        {
            repeater(Group)
            {
                field("No."; Rec."No.")
                {
                    ApplicationArea = all;
                }
                field("Local Service Code"; Rec."Local Service Code")
                {
                    ApplicationArea = All;
                    Visible = true;
                }
                field("Local Resource Code"; Rec."Local Resource Code")
                {
                    ApplicationArea = All;
                }
                field(Enabled; Rec.Enabled)
                {
                    ToolTip = 'Specifies the value of the Enabled field.';
                }
                field(Description; Rec.Description)
                {
                    ApplicationArea = all;
                }
                field("Resource Type"; Rec."Resource Type")
                {
                    ApplicationArea = All;
                }


                field("After Import File Action"; Rec."After Import File Action")
                {
                    ApplicationArea = All;
                }
                field("No. of Pending Entries"; Rec."No. of Pending Entries")
                {
                    ApplicationArea = all;
                }
                field(Landscape; Rec.Landscape)
                {
                    ToolTip = 'Specifies the value of the Landscape field.';
                }
                field("Paper Size"; Rec."Paper Size")
                {
                    ToolTip = 'Specifies the value of the Paper Size field.';
                }
                field("Paper Tray"; Rec."Paper Tray")
                {
                    ToolTip = 'Specifies the value of the Paper Tray field.';
                }
                field("Use Raw Print"; Rec."Use Raw Print")
                {
                    ToolTip = 'Specifies the value of the Use Raw Print field.';
                }
            }
        }
        area(Factboxes)
        {

        }
    }

    actions
    {
        area(Processing)
        {

            action(DeleteAllprints)
            {
                Caption = 'Delete all jobs';
                ApplicationArea = All;
                Image = "Invoicing-MDL-Delete";
                trigger OnAction();
                begin
                    Rec.DeleteEntries(true);
                end;
            }

            action(PharsePaperSizeText)
            {
                ApplicationArea = All;
                Caption = 'Pharse Paper Size';
                Image = PrepaymentCreditMemo;
                trigger OnAction()
                var
                    BCLRSWebService: Codeunit "BC LRS Web Service";
                    BCLRSEditText: Page "BC LRS Edit Text";
                    TextToEdit: Text;
                    PaperSizeFound: Enum "Printer Paper Kind";
                    HeightFound, WidthFound : Decimal;
                begin
                    BCLRSEditText.SetText(TextToEdit);
                    BCLRSEditText.RunModal();
                    TextToEdit := BCLRSEditText.GetText();

                    BCLRSWebService.ParsePaperSize(TextToEdit, PaperSizeFound, HeightFound, WidthFound);
                    Rec."Paper Size" := PaperSizeFound;
                    Rec."Paper Height" := HeightFound;
                    Rec."Paper Width" := WidthFound;
                end;
            }
        }
    }
}
#pragma implicitwith restore
