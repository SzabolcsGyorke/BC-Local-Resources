page 90105 "BC LRS Printers"
{
    ApplicationArea = All;
    Caption = 'Local Service Printers';
    PageType = List;
    SourceTable = "BC LRS Service Resource";
    UsageCategory = None;
    Editable = false;
    layout
    {
        area(content)
        {
            repeater(General)
            {
                field("Local Service Code"; Rec."Local Service Code")
                {
                    ToolTip = 'Specifies the value of the Local Service Code field.';
                }
                field("Local Resource Code"; Rec."Local Resource Code")
                {
                    ToolTip = 'Specifies the value of the Local Resource Code field.';
                }
                field(Enabled; Rec.Enabled)
                {
                    ToolTip = 'Specifies the value of the Enabled field.';
                }
                field(Description; Rec.Description)
                {
                    ToolTip = 'Specifies the value of the Description field.';
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
    }

    trigger OnOpenPage()
    begin
        CurrPage.Editable(not CurrPage.LookupMode);
    end;


    trigger OnQueryClosePage(CloseAction: Action): Boolean
    var
        BCLRSServiceResource: Record "BC LRS Service Resource";
    begin
        if not CurrPage.LookupMode then exit(true);

        CurrPage.SetSelectionFilter(BCLRSServiceResource);
        if (CloseAction in [ACTION::OK, ACTION::LookupOK]) then
            if Confirm('Do you want to add the selected Printers?', false) then
                if BCLRSServiceResource.FindSet() then
                    repeat
                        BCLRSServiceResource.Enabled := true;
                        BCLRSServiceResource.Modify();
                    until BCLRSServiceResource.Next() < 1;

        exit(true);
    end;
}
