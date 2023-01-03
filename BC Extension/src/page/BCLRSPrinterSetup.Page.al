page 90106 "BC LRS Printer Setup"
{
    ApplicationArea = All;
    Caption = 'Printer Setup';
    PageType = StandardDialog;
    SourceTable = "BC LRS Service Resource";

    layout
    {
        area(content)
        {
            group(General)
            {
                field("No."; Rec."No.")
                {
                    ToolTip = 'Specifies the value of the No. field.';
                    Editable = false;
                }
                field(Description; Rec.Description)
                {
                    ToolTip = 'Specifies the value of the Description field.';
                }
                field(Enabled; Rec.Enabled)
                {
                    ToolTip = 'Specifies the value of the Enabled field.';
                }
                field("Local Resource Code"; Rec."Local Resource Code")
                {
                    ToolTip = 'Specifies the value of the Local Resource Code field.';
                    Editable = false;
                }
                field("Local Service Code"; Rec."Local Service Code")
                {
                    ToolTip = 'Specifies the value of the Local Service Code field.';
                    Editable = false;
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
                field("Paper Height"; Rec."Paper Height")
                {
                    ToolTip = 'Specifies the value of the Printer Paper Height field.';
                }
                field("Paper Width"; Rec."Paper Width")
                {
                    ToolTip = 'Specifies the value of the Printer Paper Width field.';
                }
                field("Use Raw Print"; Rec."Use Raw Print")
                {
                    ToolTip = 'Specifies the value of the Use Raw Print field.';
                }
            }
        }
    }
}
