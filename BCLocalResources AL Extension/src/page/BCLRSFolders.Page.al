page 90108 "BC LRS Folders"
{
    ApplicationArea = All;
    Caption = 'Local Service Folders';
    PageType = List;
    SourceTable = "BC LRS Service Resource";
    UsageCategory = None;

    layout
    {
        area(content)
        {
            repeater(General)
            {
                field("Local Resource Code"; Rec."Local Resource Code")
                {
                    ToolTip = 'Specifies the value of the Local Resource Code field.';
                }
                field(Description; Rec.Description)
                {
                    ToolTip = 'Specifies the value of the Description field.';
                }
                field("After Import File Action"; Rec."After Import File Action")
                {
                    ToolTip = 'Specifies the value of the After Import File Action field.';
                }
                field(Enabled; Rec.Enabled)
                {
                    ToolTip = 'Specifies the value of the Enabled field.';
                }
                field("No. of Pending Entries"; Rec."No. of Pending Entries")
                {
                    ToolTip = 'Specifies the value of the No. of Pending Entries field.';
                }
                field("Local Service Code"; Rec."Local Service Code")
                {
                    ToolTip = 'Specifies the value of the Local Service Code field.';
                    Visible = false;
                }
            }
        }
    }
}
